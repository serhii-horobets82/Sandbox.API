using Evoflare.API.Configuration;
using Evoflare.API.Constants;
using Evoflare.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq;
using Boxed.AspNetCore;
using System;
using Microsoft.Extensions.Options;
using Npgsql;
using System.Collections.Generic;

namespace Evoflare.API.Data
{
    public interface IDbContextFactory
    {
        EvoflareDbContext Create();

        EvoflareDbContext CreateFromHeaders();
    }

    public class DbContextFactory : IDbContextFactory
    {

        private readonly IConfiguration configuration;
        private readonly IConnectionStringBuilder connectionStringBuilder;
        private readonly IHttpContextAccessor contextAccessor;

        public DbContextFactory(IConfiguration configuration, IConnectionStringBuilder connectionStringBuilder, IHttpContextAccessor contextAccessor)
        {
            this.configuration = configuration;
            this.connectionStringBuilder = connectionStringBuilder;
            this.contextAccessor = contextAccessor;
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

        public EvoflareDbContext CreateFromHeaders()
        {
            var dbContextBuilder = new DbContextOptionsBuilder<EvoflareDbContext>();

            if (contextAccessor.HttpContext.Request.Headers.TryGetValue(CustomHeaders.ServerId, out var headerValues))
            {
                var serverId = headerValues.First();
                var dbInstances = configuration.GetSection("DatabaseSettings").Get<List<DBInstance>>();
                var instance = dbInstances?.FirstOrDefault(i => i.Id == serverId);
                if (instance != null)
                {
                    if (instance.Type == DataBaseType.MSSQL)
                    {
                        dbContextBuilder.UseSqlServer(
                            configuration.GetConnectionString(instance.ConnStrSettingsName),
                            sqlServerOptions =>
                            {
                                sqlServerOptions.CommandTimeout(instance.CommandTimeout);
                                sqlServerOptions.MigrationsHistoryTable("Migrations", "core");
                            });
                    }
                    else if (instance.Type == DataBaseType.POSTGRES)
                    {
                        var connectionString = configuration.GetConnectionString(instance.ConnStrSettingsName);
                        // Search env variable first 
                        if (!string.IsNullOrEmpty(instance.ConnStrEnvVarName))
                            connectionString = Environment.GetEnvironmentVariable(instance.ConnStrEnvVarName) ?? connectionString;

                        // Parse as database URL
                        if (connectionString.StartsWith("postgres://"))
                        {
                            var databaseUri = new Uri(connectionString);
                            var userInfo = databaseUri.UserInfo.Split(':');
                            var isLocal = databaseUri.Host == "localhost";
                            var builder = new NpgsqlConnectionStringBuilder
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
                            };
                            connectionString = builder.ToString();
                        }

                        dbContextBuilder.UseNpgsql(
                            connectionString,
                            pgOptions =>
                            {
                                pgOptions.CommandTimeout(instance.CommandTimeout);
                                pgOptions.MigrationsHistoryTable("Migrations", "core");
                            });
                    }
                }
            }

            return new EvoflareDbContext(dbContextBuilder.Options);
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
