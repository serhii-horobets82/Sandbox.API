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


        private static async Task CreateOrUpdateEmployee(IServiceProvider serviceProvider, string userEmail, string roleName, Employee empl)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var dbContext = serviceProvider.GetRequiredService<EvoflareDbContext>();
            var user = await userManager.FindByEmailAsync(userEmail);

            if (user == null)
            {
                var trans = dbContext.Database.BeginTransaction();
                if (empl.Id != 0 &&  dbContext.Database.IsSqlServer())
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
                        LockoutEnabled = false,
                        Gender = Gender.Unknown
                    };
                    if (empl.UserId != null)
                        user.Id = empl.UserId;

                    empl.UserId = user.Id;
                    empl.NameTemp = $"{user.LastName} {user.FirstName}";
                    await dbContext.Employee.AddAsync(empl);

                    await userManager.CreateAsync(user, DefaultPassword);

                    await userManager.AddClaimsAsync(user, new Claim[]{
                        new Claim(JwtClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
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

        public static void RecreateDatabase(DbContext context, int timeout)
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

        public static void Initialize(IServiceProvider serviceProvider, IConfiguration configuration, bool forceRecreate = false)
        {
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

            if (recreateDatabase || forceRecreate)
                RecreateDatabase(applicationContext, retryTimeout);
            else
                applicationContext.Database.EnsureCreated();
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

            // check for roles
            if (!applicationContext.Roles.Any())
            {
                CreateRoles(serviceProvider).Wait();
            }

            if(previousVersion != null) return; // DB alredy has data

            try
            {

                SeedOrganization(applicationContext);
                SeedEmployeeType(applicationContext);
                //SeedEmployee(applicationContext);
                if (!applicationContext.Users.Any())
                {
                    var employees = new[]
                    {
                    new Employee {Id = 1, UserId = @"d91a5edf-31d4-471c-91e0-a1426e33fe73", IsManager = true, EmployeeTypeId = 1, OrganizationId = 1, NameTemp = @"Manager John", HiringDate = DateTime.ParseExact("2010-02-01T00:00:00.0000000", "O", CultureInfo.InvariantCulture), Name = @"John", Surname = @"Manager" },
                    new Employee {Id = 2, UserId = @"a156a0b8-62f1-443e-a341-be1ff040d7ed", IsManager = true, EmployeeTypeId = 1, OrganizationId = 1, NameTemp = @"Manager Bob", HiringDate = DateTime.ParseExact("2011-12-01T00:00:00.0000000", "O", CultureInfo.InvariantCulture), Name = @"Bob", Surname = @"Manager" },
                    new Employee {Id = 3, UserId = @"a6c25630-171b-4c43-891c-29ec301ebcf9", IsManager = false, EmployeeTypeId = 3, OrganizationId = 1, NameTemp = @"QA Karl", HiringDate = DateTime.ParseExact("2017-11-21T00:00:00.0000000", "O", CultureInfo.InvariantCulture), Name = @"Karl", Surname = @"QA" },
                    new Employee {Id = 4, UserId = @"df372ad6-cb7a-4261-a9ac-75465ae51f37", IsManager = false, EmployeeTypeId = 4, OrganizationId = 1, NameTemp = @"AutoQA Marta", HiringDate = DateTime.ParseExact("2010-02-02T00:00:00.0000000", "O", CultureInfo.InvariantCulture), Name = @"Marta", Surname = @"AutoQA" },
                    new Employee {Id = 6, UserId = @"3a719e65-a777-413b-b45e-e40d2e80dbf9", IsManager = false, EmployeeTypeId = 2, OrganizationId = 1, NameTemp = @"Developer Linus", HiringDate = DateTime.ParseExact("2010-02-03T00:00:00.0000000", "O", CultureInfo.InvariantCulture), Name = @"Linus", Surname = @"Developer" },
                    new Employee {Id = 7, UserId = @"2677b447-5ca2-4778-a220-1491b2246700", IsManager = false, EmployeeTypeId = 2, OrganizationId = 1, NameTemp = @"Developer Mark", HiringDate = DateTime.ParseExact("2010-04-04T00:00:00.0000000", "O", CultureInfo.InvariantCulture), Name = @"Mark", Surname = @"Developer" },
                    new Employee {Id = 10, UserId = @"e3ef731c-9ec9-4a32-8ea4-7da464ae8840", IsManager = true, EmployeeTypeId = 1, OrganizationId = 1, NameTemp = @"Manager Petra", HiringDate = DateTime.ParseExact("2010-05-05T00:00:00.0000000", "O", CultureInfo.InvariantCulture), Name = @"Petra", Surname = @"Manager" },
                    new Employee {Id = 11, UserId = @"9f98f523-1ca3-43ae-b52c-92ffe1bb4685", IsManager = true, EmployeeTypeId = 1, OrganizationId = 1, NameTemp = @"MEGA Manager Barak", HiringDate = DateTime.ParseExact("2010-02-11T00:00:00.0000000", "O", CultureInfo.InvariantCulture), Name = @"Barak", Surname = @"MEGA Manager" },
                    new Employee {Id = 12, UserId = @"b78b8000-01b1-47bc-a470-2e054c512249", IsManager = false, EmployeeTypeId = 3, OrganizationId = 1, NameTemp = @"QA Tapak", HiringDate = DateTime.ParseExact("2010-01-01T00:00:00.0000000", "O", CultureInfo.InvariantCulture), Name = @"Tapak", Surname = @"QA" },
                    new Employee {Id = 13, UserId = @"1f9d9168-70f4-4e40-b89c-2bd6f9a6d126", IsManager = false, EmployeeTypeId = 3, OrganizationId = 1, NameTemp = @"QA Mikki", HiringDate = DateTime.ParseExact("2010-04-15T00:00:00.0000000", "O", CultureInfo.InvariantCulture), Name = @"Mikki", Surname = @"QA" },
                    new Employee {Id = 15, UserId = @"4f444878-c404-4797-94f7-1d5ec8b7ee14", IsManager = false, EmployeeTypeId = 4, OrganizationId = 1, NameTemp = @"AutoQA Billy", HiringDate = DateTime.ParseExact("2010-05-01T00:00:00.0000000", "O", CultureInfo.InvariantCulture), Name = @"Billy", Surname = @"AutoQA" },
                    new Employee {Id = 16, UserId = @"238b8729-ccb3-4e90-911e-ccc377fc5e51", IsManager = false, EmployeeTypeId = 2, OrganizationId = 1, NameTemp = @"Developer Todd", HiringDate = DateTime.ParseExact("2010-06-03T00:00:00.0000000", "O", CultureInfo.InvariantCulture), Name = @"Todd", Surname = @"Developer" },
                    new Employee {Id = 17, UserId = @"c47ebe5b-993c-41cf-bc6f-e71f37a1df03", IsManager = false, EmployeeTypeId = 2, OrganizationId = 1, NameTemp = @"Developer Riana", HiringDate = DateTime.ParseExact("2010-07-04T00:00:00.0000000", "O", CultureInfo.InvariantCulture), Name = @"Riana", Surname = @"Developer" },
                    new Employee {Id = 18, UserId = @"2361580b-2a8d-470d-b90e-c65a0a5bfc13", IsManager = false, EmployeeTypeId = 2, OrganizationId = 1, NameTemp = @"Developer Mila", HiringDate = DateTime.ParseExact("2010-12-05T00:00:00.0000000", "O", CultureInfo.InvariantCulture), Name = @"Mila", Surname = @"Developer" },
                    new Employee {Id = 21, UserId = @"7bad83e3-30f7-4729-9fec-8b702cc706f1", IsManager = false, EmployeeTypeId = 2, OrganizationId = 1, NameTemp = @"Took Alex", HiringDate = DateTime.ParseExact("2010-05-17T00:00:00.0000000", "O", CultureInfo.InvariantCulture), Name = @"Alex", Surname = @"Took" },
                    new Employee {Id = 22, UserId = @"b1ae0dc1-1323-4af0-8d9e-8254eecfeff6", IsManager = false, EmployeeTypeId = 10, OrganizationId = 1, NameTemp = @"Admin System", HiringDate = DateTime.ParseExact("2010-01-01T00:00:00.0000000", "O", CultureInfo.InvariantCulture), Name = @"System", Surname = @"Admin" },
                    new Employee {Id = 23, UserId = @"6fac6ac2-c226-47c7-a6b8-753ed67f7fae", IsManager = false, EmployeeTypeId = 11, OrganizationId = 1, NameTemp = @"Admin Regular", HiringDate = DateTime.ParseExact("2010-01-01T00:00:00.0000000", "O", CultureInfo.InvariantCulture), Name = @"Regular", Surname = @"Admin" },
                    new Employee {Id = 24, UserId = @"f89109d4-0216-444f-8c6e-c56e8c2fcb21", IsManager = false, EmployeeTypeId = 1, OrganizationId = 1, NameTemp = @"Manager Chief", HiringDate = DateTime.ParseExact("2010-01-01T00:00:00.0000000", "O", CultureInfo.InvariantCulture), Name = @"Chief", Surname = @"Manager" },
                    new Employee {Id = 25, UserId = @"8b493138-5e91-4893-9c25-f0c006ad8f53", IsManager = false, EmployeeTypeId = 1, OrganizationId = 1, NameTemp = @"Manager Regular", HiringDate = DateTime.ParseExact("2010-01-01T00:00:00.0000000", "O", CultureInfo.InvariantCulture), Name = @"Regular", Surname = @"Manager" },
                    new Employee {Id = 26, UserId = @"1161f18b-ce54-44fb-a042-119fc75dbc50", IsManager = false, EmployeeTypeId = 12, OrganizationId = 1, NameTemp = @"HR Chief", HiringDate = DateTime.ParseExact("2010-01-01T00:00:00.0000000", "O", CultureInfo.InvariantCulture), Name = @"Chief", Surname = @"HR" },
                    new Employee {Id = 27, UserId = @"afdd760d-d2aa-412f-a368-57ad67f88c47", IsManager = false, EmployeeTypeId = 12, OrganizationId = 1, NameTemp = @"HR Regular", HiringDate = DateTime.ParseExact("2010-01-01T00:00:00.0000000", "O", CultureInfo.InvariantCulture), Name = @"Regular", Surname = @"HR" },
                };

                    foreach (var employee in employees)
                    {
                        var defRole = Constants.Roles.User;
                        switch (employee.EmployeeTypeId)
                        {
                            case 10: defRole = Constants.Roles.SysAdmin; break;
                            case 11: defRole = Constants.Roles.Admin; break;
                            case 1: defRole = Constants.Roles.Manager; break;
                            case 12: defRole = Constants.Roles.HR; break;
                        }
                        CreateOrUpdateEmployee(serviceProvider, $"user{employee.Id}@evoflare.com", defRole, employee).Wait();
                    }

                    // sysdadmin
                    // var empl = new Employee { Name = "System", Surname = "Admin", EmployeeTypeId = 10, HiringDate = DateTime.Parse("2010-01-01"), OrganizationId = 1 };
                    // CreateOrUpdateEmployee(serviceProvider, "sysadmin@evoflare.com", Constants.Roles.SysAdmin, empl).Wait();
                    // // admin
                    // empl = new Employee { Name = "Regular", Surname = "Admin", EmployeeTypeId = 11, HiringDate = DateTime.Parse("2010-01-01"), OrganizationId = 1 };
                    // CreateOrUpdateEmployee(serviceProvider, "admin@evoflare.com", Constants.Roles.Admin, empl).Wait();
                    // // chief.manager
                    // empl = new Employee { Name = "Chief", Surname = "Manager", EmployeeTypeId = 1, HiringDate = DateTime.Parse("2010-01-01"), OrganizationId = 1 };
                    // CreateOrUpdateEmployee(serviceProvider, "chief.manager@evoflare.com", Constants.Roles.ChiefManager, empl).Wait();
                    // // manager
                    // empl = new Employee { Name = "Regular", Surname = "Manager", EmployeeTypeId = 1, HiringDate = DateTime.Parse("2010-01-01"), OrganizationId = 1 };
                    // CreateOrUpdateEmployee(serviceProvider, "manager@evoflare.com", Constants.Roles.Manager, empl).Wait();
                    // // chief.hr
                    // empl = new Employee { Name = "Chief", Surname = "HR", EmployeeTypeId = 12, HiringDate = DateTime.Parse("2010-01-01"), OrganizationId = 1 };
                    // CreateOrUpdateEmployee(serviceProvider, "chief.hr@evoflare.com", Constants.Roles.ChiefHR, empl).Wait();
                    // // hr
                    // empl = new Employee { Name = "Regular", Surname = "HR", EmployeeTypeId = 12, HiringDate = DateTime.Parse("2010-01-01"), OrganizationId = 1 };
                    // CreateOrUpdateEmployee(serviceProvider, "hr@evoflare.com", Constants.Roles.HR, empl).Wait();
                }
                SeedEmployeeEvaluation(applicationContext);
                SeedCertificationExam(applicationContext);

            SeedEcfCompetence(applicationContext);
            SeedEcfCompetenceLevel(applicationContext);
            // disable for now to make 360 work.
            // TODO: This should be unlinked from 360 evaluation process. The initial implementation is incorrect.
            //SeedEcfEmployeeEvaluation(applicationContext);
            //SeedEcfEvaluationResult(applicationContext);
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

                SeedNotificationType(applicationContext);
                SeedNotification(applicationContext);
            }
            catch (Exception ex)
            {
                // Continue startup 
                Log.Error(ex.Message);
            }

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
                        Database = $"{connection.DataSource}, v.{connection.ServerVersion}",
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