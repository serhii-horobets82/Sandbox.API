using Evoflare.API.Auth.Models;
using Evoflare.API.Core.Models;
using Evoflare.API.Constants;
using Evoflare.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading;
using Boxed.AspNetCore;
using Evoflare.API.Configuration;
using System.Collections.Generic;
using Evoflare.API.Core.Permissions;
using System.Threading.Tasks;
using System.Security.Claims;
using Evoflare.API.Auth;

namespace Evoflare.API.Data
{
    public static partial class DbInitializer
    {
        private const string DefaultPassword = "qwerty";
        private const string DefaultLocation = "Ukraine";
        private const string DefaultLocale = "en";
        private const string DefaultPictureUrl = "https://picsum.photos/300/300/?random";


        /// <summary>
        ///     Create a role if not exists.
        /// </summary>
        /// <param name="serviceProvider">Service Provider</param>
        /// <param name="roleName">Role Name</param>
        private static void CreateRole(IServiceProvider serviceProvider, string roleName)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

            var roleExists = roleManager.RoleExistsAsync(roleName);
            roleExists.Wait();

            if (roleExists.Result) return;

            var roleResult = roleManager.CreateAsync(new ApplicationRole(roleName));
            roleResult.Wait();
        }

        private static async Task CreateRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            var roles = new List<ApplicationRole>
            {
                new ApplicationRole { Name = Constants.Roles.SysAdmin, DefaultPermission = AccessFlag.GodMode,  PolicyName="SysPolicy" },
                new ApplicationRole { Name = Constants.Roles.Admin, DefaultPermission = AccessFlag.Manage,  PolicyName="AdminPolicy" },
                new ApplicationRole { Name = Constants.Roles.ChiefManager, DefaultPermission = AccessFlag.Manage, PolicyName="ManagerPolicy" },
                new ApplicationRole { Name = Constants.Roles.Manager, DefaultPermission = AccessFlag.Read | AccessFlag.Create  | AccessFlag.Edit | AccessFlag.Details, PolicyName="ManagerPolicy" },
                new ApplicationRole { Name = Constants.Roles.ChiefHR, DefaultPermission = AccessFlag.Manage, PolicyName="HrPolicy" },
                new ApplicationRole { Name = Constants.Roles.HR, DefaultPermission = AccessFlag.Read | AccessFlag.Create | AccessFlag.Edit, PolicyName="HrPolicy" },
                new ApplicationRole { Name = Constants.Roles.User },
            };

            foreach (var role in roles)
            {
                var result = await roleManager.RoleExistsAsync(role.Name);
                if (!result)
                {
                    await roleManager.CreateAsync(role);

                    var foundRole = await roleManager.FindByNameAsync(role.Name);

                    switch (foundRole.Name)
                    {
                        case Constants.Roles.SysAdmin:
                            await roleManager.AddClaimAsync(foundRole, new Claim(CustomClaims.Permission, AppPermissions.SystemPermission.Manage));

                            await roleManager.AddClaimAsync(foundRole, new Claim(CustomClaims.Permission, AppPermissions.AdminPermission.Add));
                            await roleManager.AddClaimAsync(foundRole, new Claim(CustomClaims.Permission, AppPermissions.AdminPermission.Delete));
                            await roleManager.AddClaimAsync(foundRole, new Claim(CustomClaims.Permission, AppPermissions.AdminPermission.Edit));
                            await roleManager.AddClaimAsync(foundRole, new Claim(CustomClaims.Permission, AppPermissions.AdminPermission.View));

                            await roleManager.AddClaimAsync(foundRole, new Claim(CustomClaims.Permission, AppPermissions.UsersPermission.Add));
                            await roleManager.AddClaimAsync(foundRole, new Claim(CustomClaims.Permission, AppPermissions.UsersPermission.Delete));
                            await roleManager.AddClaimAsync(foundRole, new Claim(CustomClaims.Permission, AppPermissions.UsersPermission.Edit));
                            await roleManager.AddClaimAsync(foundRole, new Claim(CustomClaims.Permission, AppPermissions.UsersPermission.View));

                            await roleManager.AddClaimAsync(foundRole, new Claim(CustomClaims.Permission, AppPermissions.OrganizationsPermission.Details));
                            await roleManager.AddClaimAsync(foundRole, new Claim(CustomClaims.Permission, AppPermissions.OrganizationsPermission.Manage));
                            break;

                        case Constants.Roles.Admin:
                            await roleManager.AddClaimAsync(foundRole, new Claim(CustomClaims.Permission, AppPermissions.AdminPermission.Add));
                            await roleManager.AddClaimAsync(foundRole, new Claim(CustomClaims.Permission, AppPermissions.AdminPermission.Delete));
                            await roleManager.AddClaimAsync(foundRole, new Claim(CustomClaims.Permission, AppPermissions.AdminPermission.Edit));
                            await roleManager.AddClaimAsync(foundRole, new Claim(CustomClaims.Permission, AppPermissions.AdminPermission.View));

                            await roleManager.AddClaimAsync(foundRole, new Claim(CustomClaims.Permission, AppPermissions.UsersPermission.Add));
                            await roleManager.AddClaimAsync(foundRole, new Claim(CustomClaims.Permission, AppPermissions.UsersPermission.Delete));
                            await roleManager.AddClaimAsync(foundRole, new Claim(CustomClaims.Permission, AppPermissions.UsersPermission.Edit));
                            await roleManager.AddClaimAsync(foundRole, new Claim(CustomClaims.Permission, AppPermissions.UsersPermission.View));

                            await roleManager.AddClaimAsync(foundRole, new Claim(CustomClaims.Permission, AppPermissions.OrganizationsPermission.Details));

                            break;
                        case Constants.Roles.ChiefManager:
                            await roleManager.AddClaimAsync(foundRole, new Claim(CustomClaims.Permission, AppPermissions.OrganizationsPermission.Details));

                            await roleManager.AddClaimAsync(foundRole, new Claim(CustomClaims.Permission, AppPermissions.SalaryPermission.Add));
                            await roleManager.AddClaimAsync(foundRole, new Claim(CustomClaims.Permission, AppPermissions.SalaryPermission.Delete));
                            await roleManager.AddClaimAsync(foundRole, new Claim(CustomClaims.Permission, AppPermissions.SalaryPermission.Edit));
                            await roleManager.AddClaimAsync(foundRole, new Claim(CustomClaims.Permission, AppPermissions.SalaryPermission.View));

                            break;
                        case Constants.Roles.Manager:
                            await roleManager.AddClaimAsync(foundRole, new Claim(CustomClaims.Permission, AppPermissions.SalaryPermission.Add));
                            await roleManager.AddClaimAsync(foundRole, new Claim(CustomClaims.Permission, AppPermissions.SalaryPermission.Edit));
                            await roleManager.AddClaimAsync(foundRole, new Claim(CustomClaims.Permission, AppPermissions.SalaryPermission.View));

                            break;
                        case Constants.Roles.ChiefHR:
                            await roleManager.AddClaimAsync(foundRole, new Claim(CustomClaims.Permission, AppPermissions.OrganizationsPermission.Details));

                            await roleManager.AddClaimAsync(foundRole, new Claim(CustomClaims.Permission, AppPermissions.SalaryPermission.Add));
                            await roleManager.AddClaimAsync(foundRole, new Claim(CustomClaims.Permission, AppPermissions.SalaryPermission.Delete));
                            await roleManager.AddClaimAsync(foundRole, new Claim(CustomClaims.Permission, AppPermissions.SalaryPermission.Edit));
                            await roleManager.AddClaimAsync(foundRole, new Claim(CustomClaims.Permission, AppPermissions.SalaryPermission.View));

                            await roleManager.AddClaimAsync(foundRole, new Claim(CustomClaims.Permission, AppPermissions.EmployeePermission.Add));
                            await roleManager.AddClaimAsync(foundRole, new Claim(CustomClaims.Permission, AppPermissions.EmployeePermission.Edit));
                            await roleManager.AddClaimAsync(foundRole, new Claim(CustomClaims.Permission, AppPermissions.EmployeePermission.View));
                            await roleManager.AddClaimAsync(foundRole, new Claim(CustomClaims.Permission, AppPermissions.EmployeePermission.Delete));

                            break;
                        case Constants.Roles.HR:
                            await roleManager.AddClaimAsync(foundRole, new Claim(CustomClaims.Permission, AppPermissions.SalaryPermission.Add));
                            await roleManager.AddClaimAsync(foundRole, new Claim(CustomClaims.Permission, AppPermissions.SalaryPermission.Edit));
                            await roleManager.AddClaimAsync(foundRole, new Claim(CustomClaims.Permission, AppPermissions.SalaryPermission.View));

                            await roleManager.AddClaimAsync(foundRole, new Claim(CustomClaims.Permission, AppPermissions.EmployeePermission.Add));
                            await roleManager.AddClaimAsync(foundRole, new Claim(CustomClaims.Permission, AppPermissions.EmployeePermission.Edit));
                            await roleManager.AddClaimAsync(foundRole, new Claim(CustomClaims.Permission, AppPermissions.EmployeePermission.View));

                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Add user to a role if the user exists, otherwise, create the user and adds him to the role.
        /// </summary>
        /// <param name="serviceProvider">Service Provider</param>
        /// <param name="userEmail">User Email</param>
        /// <param name="userPwd">User Password. Used to create the user if not exists.</param>
        /// <param name="roleName">Role Name</param>
        /// <param name="firstName">First Name</param>
        /// <param name="lastName">Last Name</param>
        /// <param name="gender"></param>
        /// <param name="age"></param>
        private static async Task AddUserToRole(IServiceProvider serviceProvider, string userEmail, string userPwd,
            string roleName, string firstName, string lastName, Gender gender, int age)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var dbContext = serviceProvider.GetRequiredService<EvoflareDbContext>();
            var user = await userManager.FindByEmailAsync(userEmail);

            if (user == null)
            {
                user = new ApplicationUser
                {
                    UserName = userEmail,
                    Email = userEmail,
                    FirstName = firstName,
                    LastName = lastName,
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                    Gender = gender,
                    Age = age
                };

                var result = await userManager.CreateAsync(user, userPwd);

                await userManager.AddClaimsAsync(user, new Claim[]{
                        new Claim(JwtClaimTypes.Name, $"{firstName} {lastName}"),
                        new Claim(JwtClaimTypes.Email, userEmail)
                });

                // Add random profile
                dbContext.Profile.Add(new UserProfile
                {
                    IdentityId = user.Id,
                    Location = DefaultLocation,
                    Locale = DefaultLocale,
                    PictureUrl = DefaultPictureUrl
                });
            }
            await userManager.AddToRoleAsync(user, roleName);
        }

        private static void RecreateDatabase(DbContext context, int timeout)
        {
            // drop database
            Log.Information("Deleting database - start");
            if (context.Database.EnsureDeleted()) Log.Logger.Information("Deleting database - finish");
            try
            {
                // and create again
                Log.Information("Creating database - start");
                context.Database.EnsureCreated();
                Log.Information("Creating database - finish");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error creating database");
                Log.Information($"Start sleep for {timeout} sec");

                // Azure issue - need more time to restore :(
                Thread.Sleep(timeout);
                Log.Information("Creating database - start retry");
                context.Database.EnsureCreated();
                Log.Information("Creating database - finish retry");
            }
        }

        public static void Initialize(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            var assemblyInfo = Assembly.GetExecutingAssembly().GetName();
            // version of assembly, format x.y.z.w  
            var currentVersion = assemblyInfo.Version.ToString();
            // version in database table AppVersion
            var previousVersion = string.Empty;

            // main context, user\roles\auth
            var applicationContext = serviceProvider.GetRequiredService<EvoflareDbContext>();

            var appSettings = configuration.GetSection<AppSettings>();
            var dbType = appSettings.DataBaseType;

            var exportData = configuration.GetValue("AppSettings:ExportData", false) || configuration.GetValue("export-data", false);
            // flag from config - recreate DB on application starts (if true) 
            var recreateDatabase = configuration.GetValue("AppSettings:RecreateDbOnStart", false);
            var retryTimeout = configuration.GetValue("AppSettings:RetryTimeout", 60) * 1000;

            if (exportData)
            {
                Log.Information("Start generate seed classes");
                // generate seed-clasess in Data\Seed folder
                ExportDataFromDB.Run(applicationContext);
                Log.Information("Finish generate seed classes");

                Program.Shutdown();
                return;
            }

            if (recreateDatabase)
                RecreateDatabase(applicationContext, retryTimeout);
            else
                applicationContext.Database.EnsureCreated();

            try
            {
                // check core table AppVersion for records
                if (applicationContext.AppVersion.Any())
                    previousVersion = applicationContext.AppVersion.AsNoTracking().FirstOrDefault()?.Version;

                // workaround to update db without migration 
                applicationContext.Users.Where(e => e.Age > 0).ToList();
                applicationContext.Roles.Where(e => e.DefaultPermission > 0).ToList();
            }
            catch
            {
                // if something wrong - init database from scratch
                RecreateDatabase(applicationContext, retryTimeout);
                recreateDatabase = true;
            }

            // check for roles
            if (!applicationContext.Roles.Any())
            {
                CreateRoles(serviceProvider).Wait();
            }

            // check for users
            if (!applicationContext.Users.Any())
            {
                AddUserToRole(serviceProvider, "sysadmin@evoflare.com", DefaultPassword, Constants.Roles.SysAdmin, "System", "Admin", Gender.Male, 50).Wait();
                AddUserToRole(serviceProvider, "sysadmin@evoflare.com", DefaultPassword, Constants.Roles.Admin, "System", "Admin", Gender.Male, 50).Wait();
                
                AddUserToRole(serviceProvider, "admin@evoflare.com", DefaultPassword, Constants.Roles.Admin, "Regular", "Admin", Gender.Male, 30).Wait();

                AddUserToRole(serviceProvider, "chief.manager@evoflare.com", DefaultPassword, Constants.Roles.ChiefManager, "Chief", "Manager", Gender.Female, 25).Wait();
                AddUserToRole(serviceProvider, "manager@evoflare.com", DefaultPassword, Constants.Roles.Manager, "Regular", "Manager", Gender.Female, 25).Wait();

                AddUserToRole(serviceProvider, "chief.hr@evoflare.com", DefaultPassword, Constants.Roles.ChiefHR, "Chief", "HR", Gender.Female, 25).Wait();
                AddUserToRole(serviceProvider, "hr@evoflare.com", DefaultPassword, Constants.Roles.HR, "Regular", "HR", Gender.Female, 25).Wait();

                AddUserToRole(serviceProvider, "user@evoflare.com", DefaultPassword, Constants.Roles.User, "Typical", "User", Gender.Male, 20).Wait();
            }

            SeedOrganization(applicationContext);
            SeedEmployeeType(applicationContext);
            SeedEmployee(applicationContext);
            SeedEmployeeEvaluation(applicationContext);

            SeedCertificationExam(applicationContext);

            SeedEcfCompetence(applicationContext);
            SeedEcfCompetenceLevel(applicationContext);
            SeedEcfEmployeeEvaluation(applicationContext);
            SeedEcfEvaluationResult(applicationContext);
            SeedEcfRole(applicationContext);
            SeedEcfRoleCompetence(applicationContext);

            SeedProject(applicationContext);
            SeedPosition(applicationContext);
            SeedPositionRole(applicationContext);
            SeedProjectCareerPath(applicationContext);

            SeedRoleGrade(applicationContext);
            SeedRoleGradeCompetence(applicationContext);
            SeedTeam(applicationContext);
            SeedProjectPosition(applicationContext);
            SeedProjectPositionCompetence(applicationContext);

            Seed_360feedbackGroup(applicationContext);
            Seed_360feedbackMark(applicationContext);
            Seed_360questionarie(applicationContext);
            Seed_360questionToMark(applicationContext);
            Seed_360question(applicationContext);

            Seed_360employeeEvaluation(applicationContext);
            Seed_360evaluation(applicationContext);

            SeedCustomerContact(applicationContext);
            SeedCertificate(applicationContext);
            SeedCompetenceCertificate(applicationContext);

            SeedEmployeeRelations(applicationContext);

            SeedIdea(applicationContext);
            SeedIdeaComment(applicationContext);
            SeedIdeaTag(applicationContext);
            SeedIdeaTagRef(applicationContext);
            SeedIdeaLike(applicationContext);
            SeedIdeaView(applicationContext);

            SeedOrganizationStructureType(applicationContext);

            // if version different - recreate business data with sql 
            if (previousVersion != currentVersion)
                using (var connection = applicationContext.Database.GetDbConnection())
                {
                    if (connection.State != ConnectionState.Open)
                        connection.Open();
                    var command = connection.CreateCommand();

                    var appAppVersion = new AppVersion
                    {
                        Name = assemblyInfo.Name,
                        Version = currentVersion,
                        CreationDate = DateTime.Now,
                        Organization = applicationContext.Organization.FirstOrDefault().Name,
                        Database = $"{connection.DataSource}, v.{connection.ServerVersion}",
                        DatabaseType = dbType.ToString()
                    };

                    // initial insert
                    if (recreateDatabase || string.IsNullOrEmpty(previousVersion))
                        applicationContext.AppVersion.Add(appAppVersion);
                    else
                        applicationContext.AppVersion.Update(appAppVersion);

                    applicationContext.SaveChanges();

                    #region  Process with sql files
                    /*
                    // directory with sql files (copied to release folder)
                    var baseDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Database");

                    foreach (var file in Directory.GetFiles(baseDir, "*.sql"))
                    {
                        var sql = File.ReadAllText(file, Encoding.UTF8);
                        sql = sql.Replace("CREATE DATABASE", "--"); // comment creation statement (already exists)
                        sql = sql.Replace("GO\r\n", "\r\n"); // remove lines with GO commands 
                        sql = sql.Replace("\r\nGO", "\r\n"); // remove last lines with GO commands 
                        sql = sql.Replace("USE [", "--"); // comment USE statement (Azure DB issue)
                        sql = sql.Replace("[TechnicalEvaluation]",
                            $"[{connection.Database}]"); // replace database name 

                        command.CommandText = sql;
                        try
                        {
                            //command.ExecuteNonQuery();
                        }
                        catch (SqlException ex)
                        {
                            if (ex.Number == 3726
                            ) // "Could not drop object 'xxx' because it is referenced by a FOREIGN KEY constraint.
                            {
                                // TODO: temporary solution for deleting all table retry 3 times
                                var retryExecution = 2;
                                do
                                {
                                    try
                                    {
                                        command.ExecuteNonQuery();
                                    }
                                    catch
                                    {
                                        // ignored
                                    }
                                } while (retryExecution-- > 0);
                            }
                            else if (ex.Number != 2714) // "There is already an object named ..
                            {
                                throw;
                            }
                        }
                    }
                    */
                    #endregion
                }
        }
    }
}