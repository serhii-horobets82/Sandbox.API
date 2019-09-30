using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Evoflare.API.Constants;

namespace Evoflare.API
{
    public static class ClaimsExtensions
    {
        public static string GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.Claims.Where(c => c.Type == JwtClaimIdentifiers.Id).Select(c => c.Value).FirstOrDefault();
        }

        public static int GetEmployeeId(this ClaimsPrincipal claimsPrincipal)
        {
            var employeeId = claimsPrincipal.Claims.First(x => x.Type == Constants.JwtClaimIdentifiers.EmployeeId);
            return Convert.ToInt32(employeeId.Value);
        }

        public static int GetOrganizationId(this ClaimsPrincipal claimsPrincipal)
        {
            var organizationId = claimsPrincipal.Claims.First(x => x.Type == Constants.JwtClaimIdentifiers.OrganizationId);
            return Convert.ToInt32(organizationId.Value);
        }

        public static string GetOrganizationName(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.Claims.Where(c => c.Type == JwtClaimIdentifiers.OrganizationName).Select(c => c.Value).FirstOrDefault();
        }

        public static string GetUserEmail(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.Claims.Where(c => c.Type == ClaimTypes.Email).Select(c => c.Value).FirstOrDefault();
        }

        public static bool IsSysAdmin(this ClaimsPrincipal claimsPrincipal)
        {
            var roles = claimsPrincipal.Claims.Where(c => c.Type == ClaimsIdentity.DefaultRoleClaimType);
            return roles.All(rm => rm.Value == Roles.SysAdmin);
        }

        public static bool IsAdmin(this ClaimsPrincipal claimsPrincipal)
        {
            var roles = claimsPrincipal.Claims.Where(c => c.Type == ClaimsIdentity.DefaultRoleClaimType);
            return roles.All(rm => rm.Value == Roles.Admin);
        }

        public static bool IsManager(this ClaimsPrincipal claimsPrincipal)
        {
            var roles = claimsPrincipal.Claims.Where(c => c.Type == ClaimsIdentity.DefaultRoleClaimType);
            return roles.All(rm => rm.Value == Roles.Manager);
        }

        public static bool IsHr(this ClaimsPrincipal claimsPrincipal)
        {
            var roles = claimsPrincipal.Claims.Where(c => c.Type == ClaimsIdentity.DefaultRoleClaimType);
            return roles.All(rm => rm.Value == Roles.HR);
        }

        public static string GetRole(this ClaimsPrincipal claimsPrincipal)
        {
            var roles = claimsPrincipal.Claims.Where(c => c.Type == ClaimsIdentity.DefaultRoleClaimType);
            var singleRole = roles.FirstOrDefault();
            return singleRole != null ? singleRole.Value : Roles.User;
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