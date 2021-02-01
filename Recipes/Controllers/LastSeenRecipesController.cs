using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Recipes.Common;
using Recipes.Models;
using Recipes.Services;
using Recipes.TokenGenerator.Managers;
using static System.Int32;

namespace Recipes.Controllers
{
    [ApiController]
    [Route("api/lastseen")]
    public class LastSeenRecipesController : Controller
    {
        private readonly ILogger<LastSeenRecipesController> _logger;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;

        public LastSeenRecipesController(ILogger<LastSeenRecipesController> logger, ApplicationDbContext context,
            IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetFavourites()
        {
            try
            {
                if (!Request.Headers.TryGetValue("Authorization", out var auth))
                    return new UnauthorizedResult();
                var token = auth.First().Remove(0, "Bearer ".Length);
                IAuthService authService = new JwtService(Consts.SECRET);
                if (!authService.IsTokenValid(token)) return new UnauthorizedResult();

                var claims = authService.GetTokenClaims(token).ToList();
                var userId = Parse(claims.FirstOrDefault(e => e.Type.Equals(ClaimTypes.Name))?.Value ?? "0");
                var list = new List<DTOShortDbRecipeModel>();
                _context.UserLastSeenRecipes.Where(x => x.UserId == userId).ToList()
                    .ForEach(x => list.Add(_mapper.Map<DTOShortDbRecipeModel>(x)));

                return new OkObjectResult(list.OrderByDescending(x=> x.AddedDate));
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new {error = ex});
            }
        }

        [HttpPost]
        public IActionResult AddNewFavourite([Required] [FromBody] DTORecipeAddToDbModel body)
        {
            try
            {
                if (!Request.Headers.TryGetValue("Authorization", out var auth))
                    return new UnauthorizedResult();
                var token = auth.First().Remove(0, "Bearer ".Length);
                IAuthService authService = new JwtService(Consts.SECRET);
                if (!authService.IsTokenValid(token)) return new UnauthorizedResult();

                var claims = authService.GetTokenClaims(token).ToList();
                var userId = Parse(claims.FirstOrDefault(e => e.Type.Equals(ClaimTypes.Name))?.Value ?? "0");
                var obj = _mapper.Map<UserLastSeenRecipes>(body);
                obj.UserId = userId;
                obj.AddedDate = DateTime.Now;

                var item = _context.UserLastSeenRecipes.FirstOrDefault(x =>
                    x.UserId == obj.UserId && x.RecipeId == obj.RecipeId);
                if (item == null)
                {
                    
                item = _context.UserLastSeenRecipes.Add(obj).Entity;
                }
                else
                {
                    item.AddedDate = DateTime.Now;
                }

                _context.SaveChanges();
                
                var list = new List<DTOShortDbRecipeModel>();
                _context.UserLastSeenRecipes.Where(x => x.UserId == userId).ToList()
                    .ForEach(x => list.Add(_mapper.Map<DTOShortDbRecipeModel>(x)));

                return new OkObjectResult(list.OrderByDescending(x=> x.AddedDate));
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new {error = ex});
            }
        }

        [HttpDelete("{Recipeid}")]
        public IActionResult RemoveFavourite(int recipeId)
        {
            try
            {
                if (!Request.Headers.TryGetValue("Authorization", out var auth))
                    return new UnauthorizedResult();
                var token = auth.First().Remove(0, "Bearer ".Length);
                IAuthService authService = new JwtService(Consts.SECRET);
                if (!authService.IsTokenValid(token)) return new UnauthorizedResult();

                var claims = authService.GetTokenClaims(token).ToList();
                var userId = Parse(claims.FirstOrDefault(e => e.Type.Equals(ClaimTypes.Name))?.Value ?? "0");
                var itemToRemove =
                    _context.UserLastSeenRecipes.FirstOrDefault(x => x.UserId == userId && x.RecipeId == recipeId);
                if (itemToRemove == null) return new NotFoundObjectResult(new{error="This recipe could not be found on history list of that user"});
                _context.UserLastSeenRecipes.Remove(itemToRemove);
                _context.SaveChanges();
                return new OkObjectResult("Deleted");
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new {error = ex});
            }
        }
    }
}