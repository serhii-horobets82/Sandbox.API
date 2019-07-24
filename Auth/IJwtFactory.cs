using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Evoflare.API.Auth.Models;

namespace Evoflare.API.Auth
{
    public interface IJwtFactory
    {
        Task<Token> GenerateAuthToken(ApplicationUser user, IList<string> userRoles, IList<Claim> userClaims);
    }
}