using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using Evoflare.API.Auth.Models;
using Microsoft.AspNetCore.Identity;
using Evoflare.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Evoflare.API.Auth
{
    public class JwtFactory : IJwtFactory
    {
        private readonly EvoflareDbContext dbContext;
        private readonly JwtIssuerOptions jwtOptions;
        private readonly RoleManager<ApplicationRole> roleManager;

        public JwtFactory(EvoflareDbContext dbContext, IOptions<JwtIssuerOptions> jwtOptions, RoleManager<ApplicationRole> roleManager)
        {
            this.dbContext = dbContext;
            this.jwtOptions = jwtOptions.Value;
            ThrowIfInvalidOptions(this.jwtOptions);
            this.roleManager = roleManager;
        }

        public async Task<Token> GenerateAuthToken(ApplicationUser user, IList<string> userRoles, IList<Claim> userClaims)
        {
            var employee = await dbContext.Employee.Include(c => c.Users).SingleAsync(e => e.UserId == user.Id);
            var claims = new List<Claim>(userClaims);
            // common claims 
            claims.AddRange(new[] {
                new Claim(Constants.JwtClaimIdentifiers.Id, user.Id),
                new Claim(Constants.JwtClaimIdentifiers.EmployeeId, employee.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(Constants.JwtClaimIdentifiers.Rol, Constants.JwtClaims.ApiAccess),
                new Claim(JwtRegisteredClaimNames.Jti, await jwtOptions.JtiGenerator()),
                new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(jwtOptions.IssuedAt).ToString(),ClaimValueTypes.Integer64)
            });

            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim(Constants.JwtClaimIdentifiers.Roles, userRole));
                var role = await roleManager.FindByNameAsync(userRole);
                if (role != null)
                {
                    var roleClaims = await roleManager.GetClaimsAsync(role);
                    claims.AddRange(roleClaims);
                }
            }
            var token = new JwtSecurityToken(
               jwtOptions.Issuer,
               jwtOptions.Audience,
               claims,
               jwtOptions.NotBefore,
               jwtOptions.Expiration,
               jwtOptions.SigningCredentials);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return new Token()
            {
                Id = user.Id.ToString(),
                AuthToken = jwt,
                ExpiresIn = (int)jwtOptions.ValidFor.TotalSeconds
            };
        }

        /// <returns>Date converted to seconds since Unix epoch (Jan 1, 1970, midnight UTC).</returns>
        private static long ToUnixEpochDate(DateTime date)
        {
            return (long)Math.Round((date.ToUniversalTime() -
                                      new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
                .TotalSeconds);
        }

        private static void ThrowIfInvalidOptions(JwtIssuerOptions options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            if (options.ValidFor <= TimeSpan.Zero)
                throw new ArgumentException("Must be a non-zero TimeSpan.", nameof(JwtIssuerOptions.ValidFor));

            if (options.SigningCredentials == null)
                throw new ArgumentNullException(nameof(JwtIssuerOptions.SigningCredentials));

            if (options.JtiGenerator == null) throw new ArgumentNullException(nameof(JwtIssuerOptions.JtiGenerator));
        }
    }
}