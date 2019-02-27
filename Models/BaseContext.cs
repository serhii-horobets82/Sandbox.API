using System;
using Microsoft.EntityFrameworkCore;

namespace Evoflare.API.Models
{
    public class BaseAppContext : DbContext
    {
        public BaseAppContext(DbContextOptions<BaseAppContext> options) : base(options)
        {
        }

        public DbSet<CoreAppVersion> AppVersion { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CoreAppVersion>()
                .HasKey(c => c.Name);
        }
    }


    public class CoreAppVersion
    {

        public string Name { get; set; }

        public string Version { get; set; }

        public string Database { get; set; }

        public DateTime CreationDate { get; set; }
    }
}