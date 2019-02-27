using Evoflare.API.Auth.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Evoflare.API.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // user groups
        public DbSet<Group> Groups { get; set; }

        // user activity log
        public DbSet<ActivityLog> ActivityLogs { get; set; }

        public DbSet<AppVersion> AppVersion { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AppVersion>()
                .HasKey(c => c.Name);
        }
    }
}