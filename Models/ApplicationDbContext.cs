using Evoflare.API.Auth.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Evoflare.API.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        private readonly IConfiguration configuration;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, [FromServices]IConfiguration configuration) : base(options)
        {
            this.configuration = configuration;
        }

        // user groups
        public DbSet<Group> Groups { get; set; }

        // user activity log
        public DbSet<ActivityLog> ActivityLogs { get; set; }

        public DbSet<AppVersion> AppVersion { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Configure default schema
            var coreSchema = configuration.GetValue("Common:DbCoreSchema", "core");
            modelBuilder.HasDefaultSchema(coreSchema);

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AppVersion>()
                .HasKey(c => c.Name);
        }
    }
}