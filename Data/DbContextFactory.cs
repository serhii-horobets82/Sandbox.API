﻿using System;
using System.Linq;
using Evoflare.API.Configuration;
using Evoflare.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Evoflare.API.Data
{
    public interface IDbContextFactory
    {
        EvoflareDbContext Create();

        EvoflareDbContext CreateCustom();
    }

    public class DbContextFactory : IDbContextFactory
    {
        private readonly IConfiguration configuration;
        private readonly IConnectionStringBuilder connectionStringBuilder;
        private readonly IHttpContextAccessor contextAccessor;
        private readonly IServiceProvider serviceProvider;

        private readonly GlobalSettings globalSettings;

        public DbContextFactory(
            IConfiguration configuration,
            IConnectionStringBuilder connectionStringBuilder,
            IHttpContextAccessor contextAccessor,
            IServiceProvider serviceProvider,
            GlobalSettings globalSettings
        )
        {
            this.configuration = configuration;
            this.connectionStringBuilder = connectionStringBuilder;
            this.contextAccessor = contextAccessor;
            this.serviceProvider = serviceProvider;
            this.globalSettings = globalSettings;
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

        public EvoflareDbContext CreateCustom()
        {
            var instance = contextAccessor.GetCurrentDbInstance();
            var builder = configuration.BuildDbContext<EvoflareDbContext>(instance, globalSettings);
            return new EvoflareDbContext(builder.Options);
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