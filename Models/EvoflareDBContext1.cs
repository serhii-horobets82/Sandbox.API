using Evoflare.API.Auth.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Evoflare.API.Models
{
    // public partial class TechnicalEvaluationContext : DbContext
    // {
    //     public DbSet<UserProfile> Profile { get; set; }
    // }

    // public partial class EvoflareDBContext : TechnicalEvaluationContext
    // {
    //     public EvoflareDBContext()
    //     {
    //     }

    //     public EvoflareDBContext(DbContextOptions<TechnicalEvaluationContext> options)
    //         : base(options)
    //     {
    //     }

    //     public DbSet<AppVersion> AppVersion { get; set; }

    //     public DbSet<ActivityLog> ActivityLogs { get; set; }

    //     protected override void OnModelCreating(ModelBuilder modelBuilder)
    //     {
    //         //Configure default schema
    //         modelBuilder.HasDefaultSchema("core");

    //         base.OnModelCreating(modelBuilder);

    //         modelBuilder.Entity<AppVersion>()
    //             .HasKey(c => c.Name);
    //     }

    //     // public EvoflareDBContext(DbContextOptions<EvoflareDbContext> options, [FromServices]IConfiguration configuration) : base(options, configuration)
    //     // {
    //     // }
    // }
}