using System.Linq;
using Evoflare.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Evoflare.API.Data
{
    public static partial class DbInitializer
    {
        public static bool SeedCompetenceArea(EvoflareDbContext context)
        {
            if (context.CompetenceArea.Any())
            {
                return false;
            }

            IDbContextTransaction trans = null;

            if (true && context.Database.IsSqlServer())
            {
                trans = context.Database.BeginTransaction();
                context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [CompetenceArea] ON");
            }

            var items = new[] {
                new CompetenceArea {Id = 1, Name = "A", Description = "PLAN"},
                new CompetenceArea {Id = 2, Name = "B", Description = "BUILD"},
                new CompetenceArea {Id = 3, Name = "C", Description = "RUN"},
                new CompetenceArea {Id = 4, Name = "D", Description = "ENABLE"},
                new CompetenceArea {Id = 5, Name = "E", Description = "MANAGE"}
            };

            context.CompetenceArea.AddRange(items);

            context.SaveChanges();

            if (true && context.Database.IsSqlServer())
            {
                context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [CompetenceArea] OFF");
                trans.Commit();
            }

            return true;
        }
    }


}
