using Evoflare.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace Evoflare.API.Data
{
    public interface IDbContextFactory
    {
        EvoflareDbContext Create();
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
