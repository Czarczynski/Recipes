using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Recipes.Helpers;
using Recipes.Models;
using Recipes.Services;
using Recipes.TokenGenerator;
using Recipes.TokenGenerator.Managers;

namespace Recipes.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class TokenController : Controller
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;

        public TokenController(ILogger<UsersController> logger, ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> GetTokenAsync([Required] [FromBody] LoginModel body)
        {
            try
            {
                var user = _context.Users.First(x => x.Email == body.Email);

                if (SecurePasswordHasher.Verify(body.Password, user.Password))
                {
                    IAuthContainerModel model = JwtFunctions.GetJwtContainerModel(user.Id, user.Email);
                    IAuthService authService = new JwtService(model.SecretKey);

                    var token = authService.GenerateToken(model);

                    return new OkObjectResult(new
                        {token, expiresIn = model.ExpireMinutes, user = _mapper.Map<DTOUserModel>(user)});
                }

                return new UnauthorizedResult();
            }
            catch (UnauthorizedAccessException)
            {
                return new UnauthorizedResult();
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new {error = ex});
            }
        }
    }
}