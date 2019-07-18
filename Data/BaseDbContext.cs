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
        private readonly IConfiguration configuration;

        public BaseDbContext() { }
        public BaseDbContext(DbContextOptions<EvoflareDbContext> options) : base(options) { }

        public BaseDbContext(DbContextOptions<EvoflareDbContext> options, [FromServices]IConfiguration configuration) : base(options)
        {
            this.configuration = configuration;
        }


        public virtual DbSet<ActivityLog> ActivityLogs { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<AppVersion> AppVersion { get; set; }
        public virtual DbSet<UserProfile> Profile { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

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
                    pb.HasColumnType("date");
                    if(pb.Metadata.Name == "Position" || pb.Metadata.Name == "PositionRole") // TODO FInd better way
                    pb.HasDefaultValueSql("now()");
                }
            }

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