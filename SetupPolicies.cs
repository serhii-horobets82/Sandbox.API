using Evoflare.API.Constants;
using Evoflare.API.Core.Permissions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Evoflare.API
{
    public static class PoliciesExtensions
    {

        public static IServiceCollection AddCustomPolicies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthorization(options =>
                {
                    options.AddPolicy("ApiUser",
                        policy => policy.RequireClaim(Constants.JwtClaimIdentifiers.Rol, Constants.JwtClaims.ApiAccess));

                    #region Admin policy    
                    options.AddPolicy(PolicyTypes.AdminPolicy.Crud, policy =>
                    {
                        policy.RequireClaim(
                            CustomClaims.Permission,
                            AppPermissions.AdminPermission.Add,
                            AppPermissions.AdminPermission.Delete,
                            AppPermissions.AdminPermission.Edit,
                            AppPermissions.AdminPermission.View);
                    });

                    options.AddPolicy(PolicyTypes.AdminPolicy.Cru, policy =>
                    {
                        policy.RequireClaim(
                            CustomClaims.Permission,
                            AppPermissions.AdminPermission.Add,
                            AppPermissions.AdminPermission.Edit,
                            AppPermissions.AdminPermission.View);
                    });

                    options.AddPolicy(PolicyTypes.AdminPolicy.View, policy =>
                    {
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