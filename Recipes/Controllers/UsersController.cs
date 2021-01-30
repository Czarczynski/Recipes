using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Recipes.Common;
using Recipes.Helpers;
using Recipes.Models;
using Recipes.Services;
using Recipes.TokenGenerator.Managers;
using static System.Int32;
namespace Recipes.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : Controller
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;

        public UsersController(ILogger<UsersController> logger, ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> PostUser([Required] [FromBody] Users body)
        {
            try
            {
                if (body.Password.Length < 8)
                {
                    return new BadRequestObjectResult("Password is too short");
                }
                if (_context.Users.FirstOrDefault(x => x.Email == body.Email) != null)
                {
                    return new BadRequestObjectResult("Given email is already used");
                } 
                body.Password = SecurePasswordHasher.Hash(body.Password);
                _context.Users.Add(body);

                _context.SaveChanges();

                return new OkObjectResult(new {user = body});
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new {error = ex, body});
            }
        }

        [HttpGet]
        public IActionResult GetUser()
        {
            try
            {
                Request.Headers.TryGetValue("Authorization", out var auth);
                var token = auth.First().Remove(0, "Bearer ".Length);
                IAuthService authService = new JwtService(Consts.SECRET);
                if (!authService.IsTokenValid(token)) return new UnauthorizedResult();
                
                var claims = authService.GetTokenClaims(token).ToList();
                var userId = Parse(claims.FirstOrDefault(e => e.Type.Equals(ClaimTypes.Name))?.Value ?? "0");
                var user = _context.Users.First(x => x.Id == userId);

                return new OkObjectResult(new
                {user = _mapper.Map<DTOUserModel>(user)});
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new {error = ex});
            }
        }
    }
}