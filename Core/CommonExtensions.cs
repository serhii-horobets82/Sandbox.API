using System;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using Evoflare.API.Configuration;
using Evoflare.API.Constants;
using Evoflare.API.Data;
using Evoflare.API.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Serilog;

namespace Evoflare.API
{
    public static class CommonExtensions
    {
        public static Uri GetBaseUri(this HttpRequest request)
        {
            var uriBuilder = new UriBuilder
            {
                Scheme = request.Scheme,
                Host = request.Host.Host,
                Port = request.Host.Port.GetValueOrDefault(80)
            };
            return uriBuilder.Uri;
        }

        // Construct DbContext 
        public static DbContextOptionsBuilder<TContext> BuildDbContext<TContext>(this IConfiguration configuration, DatabaseInstance dbInstance, GlobalSettings globalSettings) where TContext : DbContext
        {
            var builder = new DbContextOptionsBuilder<TContext>();
            Log.Debug($"Start BuildDbContext [id={dbInstance.Id}, name={dbInstance.Name}, type={dbInstance.Type}, dbName={dbInstance.DbName}, connectionStringName={dbInstance.ConnectionStringName}]");
            // MS SQL 
            if (dbInstance.Type == DatabaseType.MSSQL)
            {
                var connectionString = configuration.GetConnectionString(dbInstance.ConnectionStringName);
                // then trying find by env variable name  
                if (!string.IsNullOrEmpty(dbInstance.ConnectionStringEnvironmentName))
                    connectionString = Environment.GetEnvironmentVariable(dbInstance.ConnectionStringEnvironmentName) ?? connectionString;

                // getting default connection     
                if (string.IsNullOrEmpty(connectionString))
                {
                    connectionString = configuration.GetConnectionString(DatabaseOptions.DefaultConnectionName);
                }

                connectionString = connectionString.Trim('"');

                if (string.IsNullOrEmpty(connectionString))
                    throw new ArgumentException($"Connection string for SQL Server not found, instanceId={dbInstance.Id}");
                
                var sqlBuilder = new SqlConnectionStringBuilder(connectionString);
                if (!string.IsNullOrEmpty(dbInstance.DbName))
                    sqlBuilder.InitialCatalog = dbInstance.DbName;

                connectionString = sqlBuilder.ConnectionString;
                Log.Debug($"MSSQL ConnectionString [{connectionString}]");

                builder.UseSqlServer(
                    connectionString,
                    sqlServerOptions =>
                    {
                        sqlServerOptions.CommandTimeout(dbInstance.CommandTimeout);
                        sqlServerOptions.MigrationsHistoryTable(DatabaseOptions.MigrationTableName, DatabaseOptions.MigrationTableScheme);
                    });
            }
            else if (dbInstance.Type == DatabaseType.POSTGRES)
            {
                // trying to find env variable with ID name 
                var connectionString = Environment.GetEnvironmentVariable(dbInstance.Id);
                if (string.IsNullOrEmpty(connectionString))
                {
                    connectionString = configuration.GetConnectionString(dbInstance.ConnectionStringEnvironmentName);
                    // then trying find by env variable name  
                    if (!string.IsNullOrEmpty(dbInstance.ConnectionStringEnvironmentName))
                        connectionString = Environment.GetEnvironmentVariable(dbInstance.ConnectionStringEnvironmentName) ?? connectionString;
                }
                if (string.IsNullOrEmpty(connectionString))
                    throw new ArgumentException($"Connection string for POSTGRES not found, instanceId={dbInstance.Id}");

                // Parse as database URL
                if (connectionString.StartsWith("postgres://"))
                {
                    var databaseUri = new Uri(connectionString);
                    var userInfo = databaseUri.UserInfo.Split(':');
                    var isLocal = databaseUri.Host == "localhost";
                    connectionString = new NpgsqlConnectionStringBuilder
                    {
                        Host = databaseUri.Host,
                            Port = databaseUri.Port,
                            Username = userInfo[0],
                            Password = userInfo[1],
                            Database = databaseUri.LocalPath.TrimStart('/'),
                            Pooling = true,
                            UseSslStream = !isLocal,
                            SslMode = isLocal ? SslMode.Disable : SslMode.Require,
                            TrustServerCertificate = !isLocal
                    }.ToString();
                }
                Log.Debug($"POSTGRES ConnectionString [{connectionString}]");
                builder.UseNpgsql(
                    connectionString,
                    pgOptions =>
                    {
                        pgOptions.CommandTimeout(dbInstance.CommandTimeout);
                        pgOptions.MigrationsHistoryTable(DatabaseOptions.MigrationTableName, DatabaseOptions.MigrationTableScheme);
                    });
            }

            return builder;
        }

        // Get ServerId from user request (header, subdomain, etc.)
        public static string GetServerId(this IHttpContextAccessor httpContextAccessor)
        {
            if (httpContextAccessor.HttpContext.Request.Headers.TryGetValue(CustomHeaders.ServerId, out var headerValues))
            {
                // get ID from request header
                return headerValues.First();
            }
            return null;
        }

        public static DatabaseInstance GetCurrentDbInstance(this IHttpContextAccessor httpContextAccessor)
        {
            var dbInstances = ConfigurationManager.DatabaseInstances;
            var serverId = httpContextAccessor.GetServerId();
            // if empty - get primary DB
            if (string.IsNullOrEmpty(serverId))
                return dbInstances.First();
            else
            {
                return dbInstances?.FirstOrDefault(i => i.Id == serverId);
            }
        }

        public static IQueryable Set(this DbContext context, Type T)
        {
            // Get the generic type definition
            MethodInfo method = typeof(DbContext).GetMethod(nameof(DbContext.Set), BindingFlags.Public | BindingFlags.Instance);

            // Build a method with the specific type argument you're interested in
            method = method.MakeGenericMethod(T);

            return method.Invoke(context, null) as IQueryable;
        }

        public static void Times(this int count, Action action)
        {
            for (int i = 0; i < count; i++)
            {
                action();
            }
        }
    }
}