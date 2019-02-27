using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using Evoflare.API.Models;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;

namespace Evoflare.API.Data
{
    public static class DbInitializer
    {

        public static bool Initialize(BaseAppContext context)
        {
            var assemblyInfo = Assembly.GetExecutingAssembly().GetName();
            var currentVersion = assemblyInfo.Version.ToString();

            // always try to create empty db 
            context.Database.EnsureCreated();

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

                using (var connection = context.Database.GetDbConnection())
                {
                    if (connection.State != ConnectionState.Open)
                        connection.Open();
                    // initial insert
                    context.AppVersion.Add(new CoreAppVersion()
                    {
                        Name = assemblyInfo.Name,
                        Version = currentVersion,
                        CreationDate = DateTime.Now,
                        Database = $"Source: {connection.DataSource}, v.{connection.ServerVersion}"
                    });
                    context.SaveChanges();
                }
                return true;
            }

            // check table existence and compare ve
            try
            {
                var databaseVersion = context.AppVersion.AsNoTracking().FirstOrDefault()?.Version;
                if (databaseVersion != currentVersion)
                {
                    return PurgeDb();
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 208 || ex.Number == 207) // "Invalid object name 'AppVersion'." or "Invalid column name 'Database'."
                {
                    return PurgeDb();
                }
            }
            return false;
        }

        public static void Initialize(TechnicalEvaluationContext context)
        {
            var baseDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Database");
            if (!Directory.Exists(baseDir)) return;

            foreach (var file in Directory.GetFiles(baseDir, "*.sql"))
            {
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
}