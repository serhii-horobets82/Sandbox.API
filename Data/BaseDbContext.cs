using System.Linq;
using Boxed.AspNetCore;
using Evoflare.API.Auth.Models;
using Evoflare.API.Configuration;
using Evoflare.API.Constants;
using Evoflare.API.Core.Models;
using Evoflare.API.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Evoflare.API.Models
{
    public class BaseDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string, IdentityUserClaim<string>, ApplicationUserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public BaseDbContext() { }
        public BaseDbContext(DbContextOptions<EvoflareDbContext> options) : base(options) { }

        public virtual DbSet<ActivityLog> ActivityLogs { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Installation> Installations { get; set; }
        public virtual DbSet<AppVersion> AppVersion { get; set; }
        public virtual DbSet<UserProfile> Profile { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            if (this.Database.IsNpgsql())
            {
                // postgres workaround
                foreach (var pb in modelBuilder.Model
                        .GetEntityTypes()
                        .SelectMany(t => t.GetProperties())
                        .Where(p => p.GetAnnotations().Any(b => b.Name == "Relational:ColumnType" && b.Value.ToString() == "datetime"))
                        .Select(p =>
                            modelBuilder.Entity(p.DeclaringEntityType.ClrType).Property(p.Name)))
                {
                    pb.HasColumnType("timestamp"); // MSSQL datetime maped to PG timestamp
                    if (pb.Metadata.DeclaringEntityType.Name == "Evoflare.API.Models.Position" || pb.Metadata.DeclaringEntityType.Name == "Evoflare.API.Models.PositionRole")
                    {
                        var defaultValueSql = pb.Metadata.Relational().DefaultValueSql;
                        if (defaultValueSql != null)
                            pb.HasDefaultValueSql(defaultValueSql.Replace("getutcdate", "now")); // MSSQL getutcdate() maped to PG now()
                    }
                }
            }

            // Workaroound 
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.Metadata.RemoveIndex(new [] { entity.Property(r => r.UserId).Metadata });
            });
            //

            // == core =======
            modelBuilder.Entity<ActivityLog>(entity =>
            {
                entity.ToTable("ActivityLogs", DatabaseOptions.CoreSchemaName);
            });

            modelBuilder.Entity<Installation>(entity =>
            {
                entity.ToTable("Installations", DatabaseOptions.CoreSchemaName);
            });

            modelBuilder.Entity<AppVersion>(entity =>
            {
                entity.ToTable("AppVersion", DatabaseOptions.CoreSchemaName);
                entity.HasKey(e => e.Name);
                entity.Property(e => e.Name).ValueGeneratedNever();
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.ToTable("Groups", DatabaseOptions.CoreSchemaName);
                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<Auth.Models.UserProfile>(entity =>
            {
                entity.ToTable("Profile", DatabaseOptions.CoreSchemaName);

                entity.HasIndex(e => e.IdentityId);
            });

            //----------------

            // == security =======
            modelBuilder.Entity<ApplicationUser>(entity =>
            {
                entity.ToTable(name: "Users", schema : DatabaseOptions.SecuritySchemaName);
            });
            modelBuilder.Entity<ApplicationRole>(entity =>
            {
                entity.ToTable(name: "Roles", schema : DatabaseOptions.SecuritySchemaName);
            });
            modelBuilder.Entity<ApplicationUserRole>(entity =>
            {
                entity.ToTable("UserRoles", DatabaseOptions.SecuritySchemaName);
                entity.HasKey(ur => new { ur.UserId, ur.RoleId });

                entity.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                entity.HasOne(ur => ur.User)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });
            // modelBuilder.Entity<IdentityUserRole<string>>(entity =>
            // {
            //     entity.ToTable("UserRoles", DatabaseOptions.SecuritySchemaName);
            // });
            modelBuilder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.ToTable("UserClaims", DatabaseOptions.SecuritySchemaName);
            });
            modelBuilder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable("UserLogins", DatabaseOptions.SecuritySchemaName);
            });
            modelBuilder.Entity<IdentityRoleClaim<string>>(entity =>
            {
                entity.ToTable("RoleClaims", DatabaseOptions.SecuritySchemaName);
            });

            modelBuilder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.ToTable("UserTokens", DatabaseOptions.SecuritySchemaName);
            });

        }
    }
}