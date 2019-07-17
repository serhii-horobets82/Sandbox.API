using Evoflare.API.Auth.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Evoflare.API.Core.Models;

namespace Evoflare.API.Models
{
    public class BaseDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public BaseDbContext() { }
        public BaseDbContext(DbContextOptions<EvoflareDbContext> options) : base(options) { }

        public virtual DbSet<ActivityLog> ActivityLogs { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<AppVersion> AppVersion { get; set; }
        public virtual DbSet<UserProfile> Profile { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            const string CoreSchemaName = "core";
            const string SecuritySchemaName = "security";

            // == core =======
            modelBuilder.Entity<ActivityLog>(entity =>
            {
                entity.ToTable("ActivityLogs", CoreSchemaName);
            });

            modelBuilder.Entity<AppVersion>(entity =>
            {
                entity.ToTable("AppVersion", CoreSchemaName);
                entity.HasKey(e => e.Name);
                entity.Property(e => e.Name).ValueGeneratedNever();
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.ToTable("Groups", CoreSchemaName);
                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<Auth.Models.UserProfile>(entity =>
            {
                entity.ToTable("Profile", "core");

                entity.HasIndex(e => e.IdentityId);
            });

            //----------------

            // == security =======
            modelBuilder.Entity<ApplicationUser>(entity =>
            {
                entity.ToTable(name: "AspNetUser", schema: SecuritySchemaName);
            });
            modelBuilder.Entity<ApplicationRole>(entity =>
            {
                entity.ToTable(name: "AspNetRole", schema: SecuritySchemaName);
            });
            modelBuilder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.ToTable("AspNetUserClaim", SecuritySchemaName);
            });
            modelBuilder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable("AspNetUserLogin", SecuritySchemaName);
            });
            modelBuilder.Entity<IdentityRoleClaim<string>>(entity =>
            {
                entity.ToTable("AspNetRoleClaim", SecuritySchemaName);
            });
            modelBuilder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.ToTable("AspNetUserRole", SecuritySchemaName);
            });
            modelBuilder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.ToTable("AspNetUserToken", SecuritySchemaName);
            });
        }
    }
}