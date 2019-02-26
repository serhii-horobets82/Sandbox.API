using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.IO;
using System.Reflection;
using System.Text;
using Evoflare.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Evoflare.API.Data
{
    public static class DbInitializer
    {

        public static bool Initialize(BaseAppContext context)
        {
            var assemblyInfo = Assembly.GetExecutingAssembly().GetName();
            var currentVersion = assemblyInfo.Version.ToString();

            // always try creatr empty db
            context.Database.EnsureCreated();

            // recreate database - inline function
            void purgeDB()
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                // initial insert
                context.AppVersion.Add(new CoreAppVersion()
                {
                    Name = assemblyInfo.Name,
                    Version = currentVersion,
                    CreationDate = DateTime.Now
                });
                context.SaveChanges();
            }

            var recordsCount = 0;
            // check table existance 
            try
            {
                recordsCount = context.AppVersion.Count();
            }
            catch (SqlException ex)
            {
                if (ex.Number == 208) // "Invalid object name 'AppVersion'."
                {
                    purgeDB();
                    return true;
                }
            }

            // check version in database, if old - recreate database 
            if (recordsCount == 0 || context.AppVersion.AsNoTracking().FirstOrDefault().Version != currentVersion)
            {
                purgeDB();
                return true;
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