using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Evoflare.API.Constants;

namespace Evoflare.API
{
    public static class ClaimsExtensions
    {
        public static string GetUserId(
            this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.Claims.Where(c => c.Type == JwtClaimIdentifiers.Id).Select(c => c.Value).FirstOrDefault();
        }

        public static string GetEmployeeId(
            this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.Claims.Where(c => c.Type == JwtClaimIdentifiers.EmployeeId).Select(c => c.Value).FirstOrDefault();
        }

        public static string GetUserEmail(
            this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.Claims.Where(c => c.Type == ClaimTypes.Email).Select(c => c.Value).FirstOrDefault();
        }

        public static void AddPermission(this ClaimsIdentity claimsIdentity, PermissionClaimValue claimValue) =>
                claimsIdentity.AddClaim(claimValue.ToClaim());

        public static IEnumerable<PermissionClaimValue> GetUserPermissions(this ClaimsPrincipal principal) =>
            principal.Claims
                .Where(c => c.Type == CustomClaims.Permission)
                .Select(c => c.Value.Split('.'))
                .Select(c => new PermissionClaimValue()
                {
                    ModuleId = c[0],
                    Action = c[1]
                });

        public static IEnumerable<UserClaim> GetUserClaims(this ClaimsPrincipal principal) =>
            principal.Claims.Select(c => new UserClaim
            {
                Id = c.Type,
                Value = c.Value
            });

        public static bool IsAdminOrSysAdmin(this ClaimsPrincipal claimsPrincipal)
        {
            var roles = claimsPrincipal.Claims.Where(c => c.Type == ClaimsIdentity.DefaultRoleClaimType);
            return roles.Any(rm =>
                rm.Value == Roles.SysAdmin ||
                rm.Value == Roles.Admin);
        }


        public class UserClaim
        {
            public string Id { get; set; }
            public string Value { get; set; }

        }

        public class PermissionClaimValue
        {
            public string ModuleId { get; set; }

            public string Action { get; set; }

            public Claim ToClaim() => new Claim(CustomClaims.Permission, $"{ModuleId}.{Action}");
        }
    }
}