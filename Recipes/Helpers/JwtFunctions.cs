using System.Security.Claims;
using Recipes.TokenGenerator;

namespace Recipes.Helpers
{
    public class JwtFunctions
    {
        public static JWTContainerModel GetJwtContainerModel(int userId, string email)
        {
            return new()
            {
                Claims = new[]
                {
                    new Claim(ClaimTypes.Name, $"{userId}"),
                    new Claim(ClaimTypes.Email, email)
                }
            };
        }
    }
}