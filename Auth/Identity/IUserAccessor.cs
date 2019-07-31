using System.Security.Claims;

namespace Evoflare.API.Auth.Identity
{
    public interface IUserAccessor
    {
        ClaimsPrincipal User { get; }
    }
}