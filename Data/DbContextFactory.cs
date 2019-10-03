using Evoflare.API.Configuration;
using Evoflare.API.Constants;
using Evoflare.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System;
using Npgsql;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace Evoflare.API.Data
{
    public interface IDbContextFactory
    {
        EvoflareDbContext Create();

        EvoflareDbContext CreateDefault();

        EvoflareDbContext CreateFromHeaders();
    }

    public class DbContextFactory : IDbContextFactory
    {
        private readonly IConfiguration configuration;
        private readonly IConnectionStringBuilder connectionStringBuilder;
        private readonly IHttpContextAccessor contextAccessor;
        private readonly IServiceProvider serviceProvider;


        private List<DatabaseInstance> dbInstances;

        public DbContextFactory(
            IConfiguration configuration,
            IConnectionStringBuilder connectionStringBuilder,
            IHttpContextAccessor contextAccessor,
            IServiceProvider serviceProvider
            )
        {
            this.configuration = configuration;
            this.connectionStringBuilder = connectionStringBuilder;
            this.contextAccessor = contextAccessor;
            this.serviceProvider = serviceProvider;
            // read configuration 
            this.dbInstances = configuration.GetSection("DatabaseInstances").Get<List<DatabaseInstance>>();
        }

        public EvoflareDbContext Create()
        {
            var user = contextAccessor.HttpContext.User;
            var idValue = user.Claims.First(x => x.Type == Constants.JwtClaimIdentifiers.EmployeeId);
            var connectionString = connectionStringBuilder.Build();
            var optionsBuilder = new DbContextOptionsBuilder<EvoflareDbContext>();
            optionsBuilder.UseSqlServer(connectionString);
            return new EvoflareDbContext(optionsBuilder.Options);
        }

        public EvoflareDbContext CreateDefault()
        {
            var appSettings = configuration.GetSection("AppSettings").Get<AppSettings>();
            var instance = new DatabaseInstance
            {
                Id = "Default",
                Type = appSettings.DatabaseType
            };
            if (appSettings.DatabaseType == DatabaseType.POSTGRES)
                instance.ConnectionStringEnvironmentName = "DATABASE_URL";
            var builder = BuildContext<EvoflareDbContext>(instance);
            return new EvoflareDbContext(builder.Options);
        }

        private DbContextOptionsBuilder<TContext> BuildContext<TContext>(DatabaseInstance dbInstance) where TContext : DbContext
        {
            var builder = new DbContextOptionsBuilder<TContext>();

            // MS SQL 
            if (dbInstance.Type == DatabaseType.MSSQL)
            {
                builder.UseSqlServer(
                    configuration.GetConnectionString(dbInstance.ConnectionStringName),
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

        public EvoflareDbContext CreateFromHeaders()
        {
            if (dbInstances != null && contextAccessor.HttpContext.Request.Headers.TryGetValue(CustomHeaders.ServerId, out var headerValues))
            {
                // get ID from request header
                var serverId = headerValues.First();
                var instance = dbInstances?.FirstOrDefault(i => i.Id == serverId);
                if (instance != null)
                {
                    var builder = BuildContext<EvoflareDbContext>(instance);
                    return new EvoflareDbContext(builder.Options);
                }
            }
            return CreateDefault();
        }
    }

    public interface IConnectionStringBuilder
    {
        string Build();
    }

    public class ConnectionStringBuilder : IConnectionStringBuilder
    {
        private readonly IConfiguration configuration;

        public ConnectionStringBuilder(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string Build()
        {
            return configuration.GetConnectionString("DefaultConnection");
        }
    }
}
