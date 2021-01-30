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
    [Route("api/user/recipes/favourites")]
    public class FavouritesRecipesController : Controller
    {
        private readonly ILogger<FavouritesRecipesController> _logger;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;

        public FavouritesRecipesController(ILogger<FavouritesRecipesController> logger, ApplicationDbContext context,
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
                _context.UserFavouriteRecipes.Where(x => x.UserId == userId).ToList()
                    .ForEach(x => list.Add(_mapper.Map<DTOShortDbRecipeModel>(x)));

                return new OkObjectResult(list);
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
                var obj = _mapper.Map<UserFavouriteRecipes>(body);
                obj.UserId = userId;
                obj.AddedDate = DateTime.Now;
                var item = _context.UserFavouriteRecipes.Add(obj).Entity;
                _context.SaveChanges();

                return new CreatedResult("", _mapper.Map<DTOShortDbRecipeModel>(item));
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
                    _context.UserFavouriteRecipes.FirstOrDefault(x => x.UserId == userId && x.RecipeId == recipeId);
                if (itemToRemove == null) throw new Exception("This recipe is not in favourites' list of that user");
                _context.UserFavouriteRecipes.Remove(itemToRemove);
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