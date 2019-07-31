using System.Security.Claims;
using Evoflare.API.Auth.Identity;
using Microsoft.AspNetCore.Http;

namespace Evoflare.API.Services
{
    public class HttpUserAccessor : IUserAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpUserAccessor(
            IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public ClaimsPrincipal User => _httpContextAccessor.HttpContext.User;
    }
}