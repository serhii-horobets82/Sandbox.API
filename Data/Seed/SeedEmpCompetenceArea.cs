using System.Linq;
using Evoflare.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Evoflare.API.Data
{
    public static partial class DbInitializer
    {
        public static bool SeedEmpCompetenceArea(EvoflareDbContext context)
        {
            if (context.EmpCompetenceArea.Any())
            {
                return false;
            }

            IDbContextTransaction trans = null;

            if (true && context.Database.IsSqlServer())
            {
                trans = context.Database.BeginTransaction();
              //  context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [EmpCompetenceArea] ON");
            }

            var items = new[] {
                new EmpCompetenceArea {Id = 1, Name = "A", Description = "PLAN"},
                new EmpCompetenceArea {Id = 2, Name = "B", Description = "BUILD"},
                new EmpCompetenceArea {Id = 3, Name = "C", Description = "RUN"},
                new EmpCompetenceArea {Id = 4, Name = "D", Description = "ENABLE"},
                new EmpCompetenceArea {Id = 5, Name = "E", Description = "MANAGE"}
            };

            context.EmpCompetenceArea.AddRange(items);

            context.SaveChanges();

            if (true && context.Database.IsSqlServer())
            {
              //  context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [EmpCompetenceArea] OFF");
                trans.Commit();
            }

            return true;
        }
    }


}
