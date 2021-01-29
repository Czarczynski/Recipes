using System.Security.Claims;

namespace Recipes.TokenGenerator
{
    public interface IAuthContainerModel
    {
        #region Members
        string SecretKey { get; set; }
        string SecurityAlgorithm { get; set; }
        int ExpireMinutes { get; set; }
        
        Claim[] Claims { get; set; }
        #endregion
    }
}