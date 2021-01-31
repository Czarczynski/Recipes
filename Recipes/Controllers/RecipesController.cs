using System;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Recipes.Common;
using Recipes.Models;
using Recipes.Services;
using Recipes.TokenGenerator.Managers;
using RestSharp;
using static System.Int32;

namespace Recipes.Controllers
{
    [ApiController]
    [Route("api/recipes")]
    public class RecipesController : Controller
    {
        private readonly ILogger<RecipesController> _logger;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;

        public RecipesController(ILogger<RecipesController> logger, ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> GetRecipesList([FromBody] ComplexSearch body)
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

                var client = new RestClient(Consts.EXT_API_URL);
                var request = new RestRequest("complexSearch", Method.GET);
                request.AddQueryParameter("apiKey", $"{Consts.SPOONACULAR_API_KEY}");
                if (!string.IsNullOrEmpty(body.Query)) request.AddQueryParameter("query", body.Query);
                if (!string.IsNullOrEmpty(body.Cuisine)) request.AddQueryParameter("cuisine", body.Cuisine);
                request.AddQueryParameter("number", "100");
                var response = await client.ExecuteAsync(request);
                if (response.StatusCode == HttpStatusCode.OK)
                    return new OkObjectResult(JsonConvert.DeserializeObject<RecipesListModel>(response.Content).Results);
                return new BadRequestObjectResult(new {response.Content});
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new {error = ex});
            }
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetRecipesList(int id)
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

                var client = new RestClient(Consts.EXT_API_URL);
                var request = new RestRequest("{recipeId}/information", Method.GET);
                request.AddUrlSegment("recipeId", id);
                request.AddQueryParameter("apiKey", $"{Consts.SPOONACULAR_API_KEY}");
                request.AddQueryParameter("number", "100");
                var response = await client.ExecuteAsync(request);
                if (response.StatusCode == HttpStatusCode.OK)
                    return new OkObjectResult(JsonConvert.DeserializeObject<ComplexRecipeModel>(response.Content));
                return new BadRequestObjectResult(new {response.Content});
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new {error = ex});
            }
        }
    }

    public class ComplexSearch
    {
        public string Query { get; set; } = null;
        public string Cuisine { get; set; } = null;
    }
}