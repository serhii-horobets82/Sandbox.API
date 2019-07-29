using Boxed.AspNetCore;
using Evoflare.API.Auth.Models;
using Evoflare.API.Configuration;
using Evoflare.API.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq;

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

            var configuration = Evoflare.API.Data.Extensions.Configuration;

            var appSettings = configuration.GetSection<AppSettings>();
            var dbType = appSettings.DataBaseType;

            if (dbType == DataBaseType.POSTGRES)
            {
                // postgress workaround
                foreach (var pb in modelBuilder.Model
                    .GetEntityTypes()
                    .SelectMany(t => t.GetProperties())
                    .Where(p => p.GetAnnotations().Any(b => b.Name == "Relational:ColumnType" && b.Value.ToString() == "datetime"))
                    .Select(p =>
                        modelBuilder.Entity(p.DeclaringEntityType.ClrType).Property(p.Name))
                    )
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
                entity.Metadata.RemoveIndex(new[] { entity.Property(r => r.UserId).Metadata });
            });
            //

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
                entity.ToTable("Profile", CoreSchemaName);

                entity.HasIndex(e => e.IdentityId);
            });

            //----------------

            // == security =======
            modelBuilder.Entity<ApplicationUser>(entity =>
            {
                entity.ToTable(name: "Users", schema: SecuritySchemaName);

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.Users)
                    .HasPrincipalKey<Employee>(p => p.UserId)
                    .HasForeignKey<ApplicationUser>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Users_Employee");
            });
            modelBuilder.Entity<ApplicationRole>(entity =>
            {
                entity.ToTable(name: "Roles", schema: SecuritySchemaName);
            });
            modelBuilder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.ToTable("UserClaims", SecuritySchemaName);
            });
            modelBuilder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable("UserLogins", SecuritySchemaName);
            });
            modelBuilder.Entity<IdentityRoleClaim<string>>(entity =>
            {
                entity.ToTable("RoleClaims", SecuritySchemaName);
            });
            modelBuilder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.ToTable("UserRoles", SecuritySchemaName);
            });
            modelBuilder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.ToTable("UserTokens", SecuritySchemaName);
            });
        }
    }
}