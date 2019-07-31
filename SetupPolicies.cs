using System.Threading.Tasks;
using Evoflare.API.Constants;
using Evoflare.API.Core.Permissions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Evoflare.API
{
    public static class PoliciesExtensions
    {

        public class AdminRequirement : AuthorizationHandler<AdminRequirement>, IAuthorizationRequirement
        {
            protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AdminRequirement requirement)
            {
                if (!context.User.IsAdminOrSysAdmin())
                {
                    return Task.CompletedTask;
                }

                context.Succeed(requirement);
                return Task.CompletedTask;
            }
        }

        public static IServiceCollection AddCustomPolicies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthorization(options =>
                {
                    options.AddPolicy(nameof(AdminRequirement), policy => policy.AddRequirements(new AdminRequirement()));

                    options.AddPolicy("ApiUser",
                        policy => policy.RequireClaim(Constants.JwtClaimIdentifiers.Rol, Constants.JwtClaims.ApiAccess));

                    #region Admin policy    

                    options.AddPolicy(PolicyTypes.AdminPolicy.Crud, policy =>
                    {
                        policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
                        policy.RequireClaim(
                            CustomClaims.Permission,
                            AppPermissions.AdminPermission.Add,
                            AppPermissions.AdminPermission.Delete,
                            AppPermissions.AdminPermission.Edit,
                            AppPermissions.AdminPermission.View);
                    });

                    options.AddPolicy(PolicyTypes.AdminPolicy.Cru, policy =>
                    {
                        policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
                        policy.RequireClaim(
                            CustomClaims.Permission,
                            AppPermissions.AdminPermission.Add,
                            AppPermissions.AdminPermission.Edit,
                            AppPermissions.AdminPermission.View);
                    });

                    options.AddPolicy(PolicyTypes.AdminPolicy.View, policy =>
                    {
                        policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
                        policy.RequireClaim(
                            CustomClaims.Permission,
                            AppPermissions.AdminPermission.View);
                    });

                    #endregion    

                    #region Salary
                    options.AddPolicy(PolicyTypes.SalaryPolicy.Cru, policy =>
                    {
                        policy.RequireClaim(
                            CustomClaims.Permission,
                            AppPermissions.SalaryPermission.Add,
                            AppPermissions.SalaryPermission.Edit,
                            AppPermissions.SalaryPermission.View);
                    });

                    options.AddPolicy(PolicyTypes.SalaryPolicy.View, policy =>
                    {
                        policy.RequireClaim(
                            CustomClaims.Permission,
                            AppPermissions.SalaryPermission.View);
                    });

                    options.AddPolicy(PolicyTypes.SalaryPolicy.Delete, policy =>
                    {
                        policy.RequireClaim(
                            CustomClaims.Permission,
                            AppPermissions.SalaryPermission.Delete);
                    });

                    #endregion
                });

            return services;
        }
    }
}