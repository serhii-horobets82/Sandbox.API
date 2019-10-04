using System;
using System.Linq;
using System.Reflection;
using Evoflare.API.Configuration;
using Evoflare.API.Constants;
using Evoflare.API.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;

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
        public static DbContextOptionsBuilder<TContext> BuildDbContext<TContext>(this IConfiguration configuration, DatabaseInstance dbInstance) where TContext : DbContext
        {
            var builder = new DbContextOptionsBuilder<TContext>();

            // MS SQL 
            if (dbInstance.Type == DatabaseType.MSSQL)
            {
                var connectionString = configuration.GetConnectionString(dbInstance.ConnectionStringName);
                // then trying find by env variable name  
                if (!string.IsNullOrEmpty(dbInstance.ConnectionStringEnvironmentName))
                    connectionString = Environment.GetEnvironmentVariable(dbInstance.ConnectionStringEnvironmentName) ?? connectionString;
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
                    connectionString = configuration.GetConnectionString(dbInstance.ConnectionStringName);
                    // then trying find by env variable name  
                    if (!string.IsNullOrEmpty(dbInstance.ConnectionStringEnvironmentName))
                        connectionString = Environment.GetEnvironmentVariable(dbInstance.ConnectionStringEnvironmentName) ?? connectionString;
                }
                if (string.IsNullOrEmpty(connectionString))
                    throw new ArgumentException("Can't build connection string");

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

        // 
        public static DatabaseInstance GetCurrentDbInstance(this IHttpContextAccessor httpContextAccessor)
        {
            var serverId = httpContextAccessor.GetServerId();

            var dbInstances = ConfigurationManager.DatabaseInstances;
            if (dbInstances != null && serverId != null)
            {
                return dbInstances?.FirstOrDefault(i => i.Id == serverId);
            }
            return null;
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