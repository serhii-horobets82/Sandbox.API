using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using Evoflare.API.Auth.Models;
using Evoflare.API.Constants;
using Evoflare.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Evoflare.API.Data
{
    public static class DbInitializer
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
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            var roleExists = roleManager.RoleExistsAsync(roleName);
            roleExists.Wait();

            if (roleExists.Result) return;

            var roleResult = roleManager.CreateAsync(new IdentityRole(roleName));
            roleResult.Wait();
        }

        /// <summary>
        ///     Add user to a role if the user exists, otherwise, create the user and adds him to the role.
        /// </summary>
        /// <param name="serviceProvider">Service Provider</param>
        /// <param name="userEmail">User Email</param>
        /// <param name="userPwd">User Password. Used to create the user if not exists.</param>
        /// <param name="roleName">Role Name</param>
        /// <param name="firstName">First Name</param>
        /// <param name="lastName">Last Name</param>
        /// <param name="gender"></param>
        /// <param name="age"></param>
        private static void AddUserToRole(IServiceProvider serviceProvider, string userEmail, string userPwd,
            string roleName, string firstName, string lastName, Gender gender, int age)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var appDbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();

            var checkAppUser = userManager.FindByEmailAsync(userEmail);
            checkAppUser.Wait();

            var appUser = checkAppUser.Result;

            if (checkAppUser.Result == null)
            {
                var newAppUser = new ApplicationUser
                {
                    UserName = userEmail,
                    NormalizedUserName = userEmail,
                    Email = userEmail,
                    NormalizedEmail = userEmail,
                    FirstName = firstName,
                    LastName = lastName,
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                    Gender = gender,
                    Age = age,
                    SecurityStamp = Guid.NewGuid().ToString()
                };

                var task = userManager.CreateAsync(newAppUser, userPwd);
                task.Wait();
                if (task.Result.Succeeded) appUser = newAppUser;

                // Add random profile
                appDbContext.Profile.Add(new UserProfile
                    {
                        IdentityId = newAppUser.Id,
                        Location = DefaultLocation,
                        Locale = DefaultLocale,
                        PictureUrl = DefaultPictureUrl
                    }
                );
            }

            if (!string.IsNullOrEmpty(roleName))
            {
                userManager.AddToRoleAsync(appUser, roleName).Wait();
            }
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

            // flag from config - recreate DB on application starts (if true) 
            var recreateDatabase = configuration.GetValue("AppSettings:RecreateDbOnStart", false);
            var retryTimeout = configuration.GetValue("AppSettings:RetryTimeout", 60) * 1000;

            // main context, user\roles\auth
            var applicationContext = serviceProvider.GetRequiredService<ApplicationDbContext>();

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
                CreateRole(serviceProvider, Constants.Roles.Admin);
                CreateRole(serviceProvider, Constants.Roles.Manager);
                CreateRole(serviceProvider, Constants.Roles.HR);
            }

            // check for users
            if (!applicationContext.Users.Any())
            {
                var userEmail = "admin@evoflare.com";
                var userFirstName = "Super";
                var userLastName = "Admin";

                AddUserToRole(serviceProvider, userEmail, DefaultPassword, Constants.Roles.Admin, userFirstName, userLastName, Gender.Male, 30);

                userEmail = "manager@evoflare.com";
                userFirstName = "Local";
                userLastName = "Manager";

                AddUserToRole(serviceProvider, userEmail, DefaultPassword, Constants.Roles.Manager, userFirstName,
                    userLastName, Gender.Female, 25);

                userEmail = "hr@evoflare.com";
                userFirstName = "Human";
                userLastName = "Resources";

                AddUserToRole(serviceProvider, userEmail, DefaultPassword, Constants.Roles.HR, userFirstName,
                    userLastName, Gender.Female, 30);

                userEmail = "user@evoflare.com";
                userFirstName = "Typical";
                userLastName = "User";

                AddUserToRole(serviceProvider, userEmail, DefaultPassword, "", userFirstName, userLastName, Gender.Male, 20);
            }

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
                        Database = $"{connection.DataSource}, v.{connection.ServerVersion}"
                    };

                    // initial insert
                    if (recreateDatabase || string.IsNullOrEmpty(previousVersion))
                        applicationContext.AppVersion.Add(appAppVersion);
                    else
                        applicationContext.AppVersion.Update(appAppVersion);

                    applicationContext.SaveChanges();

                    #region  Process with sql files

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
                            command.ExecuteNonQuery();
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

                    #endregion
                }
        }
    }
}