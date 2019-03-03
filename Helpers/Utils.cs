using Evoflare.API.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Evoflare.API.Auth.Models;
using Newtonsoft.Json;

namespace Evoflare.API.Helpers
{
    public static class Errors
    {
        public static ModelStateDictionary AddErrorsToModelState(IdentityResult identityResult,
            ModelStateDictionary modelState)
        {
            foreach (var e in identityResult.Errors) modelState.TryAddModelError(e.Code, e.Description);

            return modelState;
        }

        public static ModelStateDictionary AddErrorToModelState(string code, string description,
            ModelStateDictionary modelState)
        {
            modelState.TryAddModelError(code, description);
            return modelState;
        }
    }

    public class Tokens
    {
        public static async Task<Token> GenerateJwt(ClaimsIdentity identity, IJwtFactory jwtFactory, string userName, JwtIssuerOptions jwtOptions, JsonSerializerSettings serializerSettings)
        {
            var response = new Token()
            {
                Id = identity.Claims.Single(c => c.Type == "id").Value,
                AuthToken = await jwtFactory.GenerateEncodedToken(userName, identity),
                ExpiresIn = (int)jwtOptions.ValidFor.TotalSeconds
            };

            return response;
        }
    }
}