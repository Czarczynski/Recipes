using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Recipes.Common;

namespace Recipes.TokenGenerator
{
    public class JWTContainerModel :  IAuthContainerModel
    {
        #region Public Methods

        public int ExpireMinutes { get; set; } = 10080;
        public string SecretKey { get; set; } = Consts.SECRET;
        public string SecurityAlgorithm { get; set; } = SecurityAlgorithms.HmacSha256Signature;
        public Claim[] Claims { get; set; }
        #endregion
    }
}