﻿using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using Evoflare.API.Auth.Models;
using Evoflare.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Evoflare.API.Data
{
    public static class DbInitializer
    {
        private const string DefaultPassword = "qwerty";
        private const string AdminRoleName = "Admin";
        private const string ManagerRoleName = "Manager";


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
        private static void AddUserToRole(IServiceProvider serviceProvider, string userEmail, string userPwd,
            string roleName, string firstName, string lastName)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

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
                    SecurityStamp = Guid.NewGuid().ToString()
                };

                var taskCreateAppUser = userManager.CreateAsync(newAppUser, userPwd);
                taskCreateAppUser.Wait();

                if (taskCreateAppUser.Result.Succeeded) appUser = newAppUser;
            }

            if (!string.IsNullOrEmpty(roleName))
            {
                var newUserRole = userManager.AddToRoleAsync(appUser, roleName);
                newUserRole.Wait();
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
            var applicationContext = serviceProvider.GetRequiredService<ApplicationDbContext>();

            // check database for existence and initial creation
            applicationContext.Database.EnsureCreated();

            var recreateDatabase = configuration.GetValue("Common:RecreatDbOnStart", false);
            AppVersion av = null;
            // check core table for records, if exist - get previous version  
            try
            {
                if (applicationContext.AppVersion.Any())
                {
                    av = applicationContext.AppVersion.AsNoTracking().FirstOrDefault();
                    previousVersion = av?.Version;
                }
            }
            catch
            {
                // if something wrong - init database from scatch
                recreateDatabase = true;
            }

            if (recreateDatabase)
            {
                // drop database
                applicationContext.Database.EnsureDeleted();
                try
                {
                    // and create again
                    applicationContext.Database.EnsureCreated();
                }
                catch
                {
                    // Azure issue - need more time to restore :(
                    Thread.Sleep(60000);
                    applicationContext.Database.EnsureCreated();
                }
            }

            // check for roles
            if (!applicationContext.Roles.Any())
            {
                CreateRole(serviceProvider, AdminRoleName);
                CreateRole(serviceProvider, ManagerRoleName);
            }

            // check for users
            if (!applicationContext.Users.Any())
            {
                var userEmail = "admin@evoflare.com";
                var userFirstName = "Super";
                var userLastName = "Admin";

                AddUserToRole(serviceProvider, userEmail, DefaultPassword, AdminRoleName, userFirstName, userLastName);

                userEmail = "manager@evoflare.com";
                userFirstName = "Local";
                userLastName = "Manager";

                AddUserToRole(serviceProvider, userEmail, DefaultPassword, ManagerRoleName, userFirstName,
                    userLastName);

                userEmail = "user@evoflare.com";
                userFirstName = "Typical";
                userLastName = "User";

                AddUserToRole(serviceProvider, userEmail, DefaultPassword, "", userFirstName, userLastName);
            }

            // if version different - recreate business data with sql 
            if (previousVersion != currentVersion || recreateDatabase)  
            {
                using (var connection = applicationContext.Database.GetDbConnection())
                {
                    if (connection.State != ConnectionState.Open)
                        connection.Open();
                    var command = connection.CreateCommand();

                    // drop tables in 
                    //command.CommandText = "drop table if exists dbo";
                    //command.ExecuteNonQuery();

                    //if (av != null)
                    //   applicationContext.AppVersion.Remove(av);
                    // initial insert
                    applicationContext.AppVersion.Add(new AppVersion
                    {
                        Name = assemblyInfo.Name,
                        Version = currentVersion,
                        CreationDate = DateTime.Now,
                        Database = $"Source: {connection.DataSource}, v.{connection.ServerVersion}"
                    });
                    applicationContext.SaveChanges();

                    #region  Process with sql files

                    // directory with sql files (copied to release folder)
                    var baseDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Database");

                    foreach (var file in Directory.GetFiles(baseDir, "*.sql"))
                    {
                        var sql = File.ReadAllText(file, Encoding.UTF8);
                        sql = sql.Replace("CREATE DATABASE", "--"); // comment creation statement (already exists)
                        sql = sql.Replace("GO\r\n", "\r\n"); // remove lines with GO commands 
                        sql = sql.Replace("USE [", "--"); // comment USE statement (Azure DB issue)
                        sql = sql.Replace("[TechnicalEvaluation]", $"[{connection.Database}]"); // replace database name 

                        command.CommandText = sql;
                        try
                        {
                            command.ExecuteNonQuery();
                        }
                        catch (SqlException ex)
                        {
                            if (ex.Number != 2714) // "There is already an object named ..
                                throw;
                        }
                    }
                    #endregion
                }
            }
        }

        public static bool Initialize(BaseAppContext context)
        {
            var assemblyInfo = Assembly.GetExecutingAssembly().GetName();
            var currentVersion = assemblyInfo.Version.ToString();

            // always try to create empty db 
            context.Database.Migrate();

            // recreate database - inline function
            bool PurgeDb()
            {
                context.Database.EnsureDeleted();
                try
                {
                    context.Database.EnsureCreated();
                }
                catch
                {
                    // Azure issue - need more time to restore :(
                    Thread.Sleep(60000);
                    context.Database.EnsureCreated();
                }

                //using (var connection = context.Database.GetDbConnection())
                //{
                //    if (connection.State != ConnectionState.Open)
                //        connection.Open();
                //    // initial insert
                //    context.AppVersion.Add(new CoreAppVersion
                //    {
                //        Name = assemblyInfo.Name,
                //        Version = currentVersion,
                //        CreationDate = DateTime.Now,
                //        Database = $"Source: {connection.DataSource}, v.{connection.ServerVersion}"
                //    });
                //    context.SaveChanges();
                //}

                return true;
            }

            // check table existence and compare ve
            try
            {
                var databaseVersion = ""; //context.AppVersion.AsNoTracking().FirstOrDefault()?.Version;
                if (databaseVersion != currentVersion) return PurgeDb();
            }
            catch (SqlException ex)
            {
                if (ex.Number == 208 || ex.Number == 207
                ) // "Invalid object name 'AppVersion'." or "Invalid column name 'Database'."
                    return PurgeDb();
            }

            return false;
        }

        public static void Initialize(TechnicalEvaluationContext context)
        {
            var baseDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Database");
            if (!Directory.Exists(baseDir)) return;

            foreach (var file in Directory.GetFiles(baseDir, "*.sql"))
                using (var connection = context.Database.GetDbConnection())
                {
                    if (connection.State != ConnectionState.Open)
                        connection.Open();

                    var command = connection.CreateCommand();
                    var sql = File.ReadAllText(file, Encoding.UTF8);
                    sql = sql.Replace("CREATE DATABASE", "--"); // comment creation statement (already exists)
                    sql = sql.Replace("GO\r\n", "\r\n"); // remove lines with GO commands 
                    sql = sql.Replace("USE [", "--"); // comment USE statement (Azure DB issue)
                    sql = sql.Replace("[TechnicalEvaluation]", $"[{connection.Database}]"); // replace database name 

                    command.CommandText = sql;
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        var number = ex.Number;
                        if (ex.Number != 2714) // "There is already an object named ..
                            throw;
                    }
                }

            //var sqlContentLines = File.ReadAllLines(file, Encoding.UTF8);
            //context.Database.BeginTransaction();
            ////sqlContent.Replace("[TechnicalEvaluation]", $"[{currentDB}]");

            //var insertStatements  = sqlContentLines.Where(e => 
            //    e.StartsWith("INSERT") ||
            //    e.Contains("IDENTITY_INSERT"));

            //insertStatements.ToList().ForEach(line => context.Database.ExecuteSqlCommand(line));

            //context.Database.CommitTransaction();
        }
    }
}