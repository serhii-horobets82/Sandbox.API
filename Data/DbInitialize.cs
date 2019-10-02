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
using System.Globalization;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using Evoflare.API.Auth.Identity;
using Npgsql;

namespace Evoflare.API.Data
{
    public static partial class DbInitializer
    {
        public const string DefaultPassword = "qwerty";
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
                new ApplicationRole { Name = Constants.Roles.SysAdmin, DefaultPermission = AccessFlag.GodMode,  PolicyName=nameof(PolicyTypes.SysAdminPolicy)},
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
                            //await roleManager.AddClaimAsync(foundRole, new Claim(CustomClaims.Permission, AppPermissions.SalaryPermission.Add));
                            //await roleManager.AddClaimAsync(foundRole, new Claim(CustomClaims.Permission, AppPermissions.SalaryPermission.Edit));
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
                            await roleManager.AddClaimAsync(foundRole, new Claim(CustomClaims.Permission, AppPermissions.SalaryPermission.View));

                            await roleManager.AddClaimAsync(foundRole, new Claim(CustomClaims.Permission, AppPermissions.EmployeePermission.Add));
                            await roleManager.AddClaimAsync(foundRole, new Claim(CustomClaims.Permission, AppPermissions.EmployeePermission.Edit));
                            await roleManager.AddClaimAsync(foundRole, new Claim(CustomClaims.Permission, AppPermissions.EmployeePermission.View));

                            break;
                    }
                }
            }
        }

        private static async Task CreateOrUpdateEmployee(IServiceProvider serviceProvider, EvoflareDbContext dbContext, string userEmail, string roleName, Employee empl, Claim extraClaim = null)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var user = await userManager.FindByEmailAsync(userEmail);

            if (user == null)
            {
                var trans = dbContext.Database.BeginTransaction();
                if (empl.Id != 0 && dbContext.Database.IsSqlServer())
                    dbContext.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [Employee] ON");
                try
                {
                    user = new ApplicationUser
                    {
                        UserName = userEmail,
                        Email = userEmail,
                        FirstName = empl.Name,
                        LastName = empl.Surname,
                        EmailConfirmed = true,
                        Gender = Gender.Unknown
                    };
                    if (empl.UserId != null)
                        user.Id = empl.UserId;

                    empl.UserId = user.Id;
                    empl.NameTemp = $"{user.LastName} {user.FirstName}";
                    await dbContext.Employee.AddAsync(empl);

                    await userManager.CreateAsync(user, DefaultPassword);

                    var claims = new Claim[]{
                        new Claim(JwtClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                        new Claim(JwtClaimTypes.Email, userEmail)};
                    if (extraClaim != null)
                        claims = claims.Append(extraClaim).ToArray();
                    await userManager.AddClaimsAsync(user, claims);

                    // Add random profile
                    dbContext.Profile.Add(new UserProfile
                    {
                        IdentityId = user.Id,
                        Location = DefaultLocation,
                        Locale = DefaultLocale,
                        PictureUrl = DefaultPictureUrl
                    });

                    await userManager.AddToRoleAsync(user, roleName);

                    dbContext.Entry(empl).Reload();
                    dbContext.Entry(user).Reload();

                    trans.Commit();
                }
                catch
                {
                    trans.Rollback();
                    throw;
                }
            }
        }

        /// Additional changes in DB after re-creation
        public static void PatchDatabase(DbContext context)
        {
            if (context.Database.IsNpgsql())
            {
                context.Database.ExecuteSqlCommand(ReadEmbeddedResource("Evoflare.API.Database.patchPg.sql"));
            }
        }

        public static string ReadEmbeddedResource(string key)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var content = "";
            using (var stream = assembly.GetManifestResourceStream(key))
            {
                using (var reader = new StreamReader(stream, Encoding.UTF8))
                {
                    content = reader.ReadToEnd();
                }
            }
            return content;
        }

        public static void RecreateDatabase(DbContext context, int timeout)
        {
            // drop database
            Log.Information("Truncate database - start");
            try
            {
                var resourceKey = "Evoflare.API.Database.dropTables.sql";
                if (context.Database.IsNpgsql())
                    resourceKey = "Evoflare.API.Database.dropTablesPg.sql";
                context.Database.ExecuteSqlCommand(ReadEmbeddedResource(resourceKey));

                Log.Logger.Information("Truncate database - finish");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error truncated database");
            }

            try
            {
                // and create again
                Log.Information("Creating database - start");
                if (context.Database.IsSqlServer())
                    context.Database.Migrate();
                else
                {
                    context.Database.EnsureCreated();
                    try { context.Database.Migrate(); } catch (Exception) { }
                    context.Database.GetPendingMigrations().ToList().ForEach(e => InsertMigration(e, context));
                }

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

        private static void InsertMigration(string migrationId, DbContext context)
        {
            // insert "Initial" migration manually
            var productVersion = "2.2.4-servicing-10062";
            context.Database.ExecuteSqlCommand($"INSERT INTO core.\"Migrations\"(\"MigrationId\", \"ProductVersion\") VALUES ('{migrationId}', '{productVersion}')", migrationId, productVersion);
        }

        public static void Initialize(IServiceProvider serviceProvider, IConfiguration configuration, bool forceRecreate = false)
        {
            // main context, user\roles\auth
            var userManager = serviceProvider.GetRequiredService<IUserManager>();
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

            if (recreateDatabase || forceRecreate)
                RecreateDatabase(applicationContext, retryTimeout);
            else
            {
                try
                {
                    applicationContext.Database.Migrate();
                }
                catch (Exception ex)
                {
                    // There is already an object named 'xxx' in the database.
                    if ((ex is PostgresException pgEx && pgEx.SqlState == "42P07") ||
                       (ex is SqlException sqlEx && sqlEx.Number == 2714))
                    {
                        var initMigration = applicationContext.Database.GetPendingMigrations().FirstOrDefault();
                        InsertMigration(initMigration, applicationContext);
                    }
                }
            }
            // only create empty db
            if (configuration.GetValue("only-migrate", false))
            {
                Program.Shutdown();
                return;
            }

            var assemblyInfo = Assembly.GetExecutingAssembly().GetName();
            // version of assembly, format x.y.z.w  
            var currentVersion = assemblyInfo.Version.ToString();
            // version in database table AppVersion
            string previousVersion = null;

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

            if (previousVersion != null)
            {
                // For new assembly version - recreate database    
                if (previousVersion != currentVersion)
                {
                    RecreateDatabase(applicationContext, retryTimeout);
                    recreateDatabase = true;
                }
                else
                {   // DB already has data but new tables seeding required 
                    SeedEmployeeSalary(applicationContext);
                    SeedInstallation(applicationContext);
                    return;
                }
            }

            var setupParams = new SetupParams
            {
                Id = PredefinedConfig.DefaultConfig,
                AdminEmail = "admin@evoflare.com",
                OrganizationName = "Unknown company",
                DefaultPassword = "qwerty",
            };
            DbInitializer.Seed(setupParams, applicationContext, serviceProvider, configuration);

            // check for roles
            if (!applicationContext.Roles.Any())
            {
                CreateRoles(serviceProvider).Wait();
            }
            else return;

            try
            {
                SeedOrganization(applicationContext);
                SeedEmployeeType(applicationContext);
                //SeedEmployee(applicationContext);
                if (!applicationContext.Users.Any())
                {
                    foreach (var employee in defaultEmployeeList)
                    {
                        var userRole = userManager.MapTypeToRole(employee.EmployeeTypeId);
                        // TODO Remove later 
                        Claim claim = null;
                        if (employee.Id == 24)
                        {
                            userRole = Roles.ChiefManager;
                        }
                        else if (employee.Id == 2)
                        {
                            claim = new Claim(CustomClaims.Permission, AppPermissions.SalaryPermission.Edit);
                        }
                        CreateOrUpdateEmployee(serviceProvider, applicationContext, $"user{employee.Id}@evoflare.com", userRole, employee, claim).Wait();
                    }
                }
                SeedEmployeeEvaluation(applicationContext);
                SeedCertificationExam(applicationContext);

                SeedCompetenceArea(applicationContext);
                SeedCompetence(applicationContext);
                SeedCompetenceLevel(applicationContext);

                // disable for now to make 360 work.
                // TODO: This should be unlinked from 360 evaluation process. The initial implementation is incorrect.
                //SeedEcfEmployeeEvaluation(applicationContext);
                //SeedEcfEvaluationResult(applicationContext);
                SeedEcfRole(applicationContext);

                SeedRoleCompetence(applicationContext);
                //SeedEcfRoleCompetence(applicationContext);

                SeedProject(applicationContext);
                SeedPosition(applicationContext);
                SeedPositionRole(applicationContext);
                SeedProjectCareerPath(applicationContext);

                SeedRoleGrade(applicationContext);
                SeedRoleGradeCompetence(applicationContext);
                SeedTeam(applicationContext);
                SeedProjectPosition(applicationContext);
                SeedProjectPositionCompetence(applicationContext);

                //Seed_360feedbackGroup(applicationContext);
                //Seed_360feedbackMark(applicationContext);
                //Seed_360questionarie(applicationContext);
                //Seed_360questionToMark(applicationContext);
                //Seed_360question(applicationContext);

                //Seed_360employeeEvaluation(applicationContext);
                //Seed_360evaluation(applicationContext);

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

                SeedEmployeeSalary(applicationContext);
                //SeedNotificationType(applicationContext);
                //SeedNotification(applicationContext);
            }
            catch (Exception ex)
            {
                // Continue startup 
                Log.Error(ex.Message);
            }

            PatchDatabase(applicationContext);

            // if version different - recreate business data with sql 
            if (previousVersion != currentVersion)
            {
                var trans = applicationContext.Database.BeginTransaction();
                var connection = applicationContext.Database.GetDbConnection();
                try
                {
                    var appAppVersion = new AppVersion
                    {
                        Name = assemblyInfo.Name,
                        Version = currentVersion,
                        CreationDate = DateTime.Now,
                        Organization = applicationContext.Organization.FirstOrDefault().Name,
                        Database = $"{connection.DataSource} ({connection.Database}) v.{connection.ServerVersion}",
                        DatabaseType = dbType.ToString()
                    };
                    if (recreateDatabase || string.IsNullOrEmpty(previousVersion))
                        applicationContext.AppVersion.Add(appAppVersion);
                    else
                        applicationContext.AppVersion.Update(appAppVersion);

                    applicationContext.SaveChanges();
                    trans.Commit();
                }
                catch
                {
                    trans.Rollback();
                    throw;
                }

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