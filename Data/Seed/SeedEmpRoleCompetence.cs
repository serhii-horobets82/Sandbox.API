using System.Linq;
using Evoflare.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Evoflare.API.Data
{
    public static partial class DbInitializer
    {
        public static bool SeedEmpRoleCompetence(EvoflareDbContext context)
        {
            if (context.EmpRoleCompetence.Any())
            {
                return false;
            }

            IDbContextTransaction trans = null;

            if (true && context.Database.IsSqlServer())
            {
                trans = context.Database.BeginTransaction();
                //context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [EmpRoleCompetence] ON");
            }

            var items = new[]
           {
                new EmpRoleCompetence {Id = 1, RoleId = 1, CompetenceId = 27, CompetenceLevel = 3 },
                new EmpRoleCompetence {Id = 2, RoleId = 1, CompetenceId = 28, CompetenceLevel = 4 },
                new EmpRoleCompetence {Id = 3, RoleId = 1, CompetenceId = 29, CompetenceLevel = 4 },
                new EmpRoleCompetence {Id = 4, RoleId = 1, CompetenceId = 32, CompetenceLevel = 3 },
                new EmpRoleCompetence {Id = 5, RoleId = 1, CompetenceId = 35, CompetenceLevel = 4 },
                new EmpRoleCompetence {Id = 6, RoleId = 2, CompetenceId = 1, CompetenceLevel = 4 },
                new EmpRoleCompetence {Id = 7, RoleId = 2, CompetenceId = 3, CompetenceLevel = 4 },
                new EmpRoleCompetence {Id = 8, RoleId = 2, CompetenceId = 21, CompetenceLevel = 4 },
                new EmpRoleCompetence {Id = 9, RoleId = 2, CompetenceId = 22, CompetenceLevel = 4 },
                new EmpRoleCompetence {Id = 10, RoleId = 2, CompetenceId = 36, CompetenceLevel = 1 },
                new EmpRoleCompetence {Id = 11, RoleId = 3, CompetenceId = 1, CompetenceLevel = 4 },
                new EmpRoleCompetence {Id = 12, RoleId = 3, CompetenceId = 3, CompetenceLevel = 4 },
                new EmpRoleCompetence {Id = 13, RoleId = 3, CompetenceId = 35, CompetenceLevel = 4 },
                new EmpRoleCompetence {Id = 14, RoleId = 3, CompetenceId = 38, CompetenceLevel = 4 },
                new EmpRoleCompetence {Id = 15, RoleId = 3, CompetenceId = 40, CompetenceLevel = 5 },
                new EmpRoleCompetence {Id = 16, RoleId = 4, CompetenceId = 1, CompetenceLevel = 5 },
                new EmpRoleCompetence {Id = 17, RoleId = 4, CompetenceId = 3, CompetenceLevel = 5 },
                new EmpRoleCompetence {Id = 18, RoleId = 4, CompetenceId = 33, CompetenceLevel = 5 },
                new EmpRoleCompetence {Id = 19, RoleId = 4, CompetenceId = 35, CompetenceLevel = 4 },
                new EmpRoleCompetence {Id = 20, RoleId = 4, CompetenceId = 40, CompetenceLevel = 5 },
                new EmpRoleCompetence {Id = 21, RoleId = 5, CompetenceId = 10, CompetenceLevel = 3 },
                new EmpRoleCompetence {Id = 22, RoleId = 5, CompetenceId = 11, CompetenceLevel = 3 },
                new EmpRoleCompetence {Id = 23, RoleId = 5, CompetenceId = 17, CompetenceLevel = 3 },
                new EmpRoleCompetence {Id = 24, RoleId = 5, CompetenceId = 21, CompetenceLevel = 3 },
                new EmpRoleCompetence {Id = 25, RoleId = 5, CompetenceId = 39, CompetenceLevel = 3 },
                new EmpRoleCompetence {Id = 26, RoleId = 6, CompetenceId = 10, CompetenceLevel = 3 },
                new EmpRoleCompetence {Id = 27, RoleId = 6, CompetenceId = 11, CompetenceLevel = 2 },
                new EmpRoleCompetence {Id = 28, RoleId = 6, CompetenceId = 12, CompetenceLevel = 2 },
                new EmpRoleCompetence {Id = 29, RoleId = 6, CompetenceId = 14, CompetenceLevel = 3 },
                new EmpRoleCompetence {Id = 30, RoleId = 6, CompetenceId = 19, CompetenceLevel = 3 },
                new EmpRoleCompetence {Id = 31, RoleId = 7, CompetenceId = 6, CompetenceLevel = 2 },
                new EmpRoleCompetence {Id = 32, RoleId = 7, CompetenceId = 10, CompetenceLevel = 3 },
                new EmpRoleCompetence {Id = 33, RoleId = 7, CompetenceId = 12, CompetenceLevel = 2 },
                new EmpRoleCompetence {Id = 34, RoleId = 7, CompetenceId = 13, CompetenceLevel = 3 },
                new EmpRoleCompetence {Id = 35, RoleId = 7, CompetenceId = 23, CompetenceLevel = 2 },
                new EmpRoleCompetence {Id = 36, RoleId = 8, CompetenceId = 1, CompetenceLevel = 5 },
                new EmpRoleCompetence {Id = 37, RoleId = 8, CompetenceId = 3, CompetenceLevel = 4 },
                new EmpRoleCompetence {Id = 38, RoleId = 8, CompetenceId = 5, CompetenceLevel = 4 },
                new EmpRoleCompetence {Id = 39, RoleId = 8, CompetenceId = 7, CompetenceLevel = 5 },
                new EmpRoleCompetence {Id = 40, RoleId = 8, CompetenceId = 39, CompetenceLevel = 3 },
                new EmpRoleCompetence {Id = 41, RoleId = 9, CompetenceId = 7, CompetenceLevel = 4 },
                new EmpRoleCompetence {Id = 42, RoleId = 9, CompetenceId = 9, CompetenceLevel = 4 },
                new EmpRoleCompetence {Id = 43, RoleId = 9, CompetenceId = 22, CompetenceLevel = 4 },
                new EmpRoleCompetence {Id = 44, RoleId = 9, CompetenceId = 34, CompetenceLevel = 4 },
                new EmpRoleCompetence {Id = 45, RoleId = 9, CompetenceId = 38, CompetenceLevel = 4 },
                new EmpRoleCompetence {Id = 46, RoleId = 10, CompetenceId = 31, CompetenceLevel = 4 },
                new EmpRoleCompetence {Id = 47, RoleId = 10, CompetenceId = 33, CompetenceLevel = 4 },
                new EmpRoleCompetence {Id = 48, RoleId = 10, CompetenceId = 34, CompetenceLevel = 3 },
                new EmpRoleCompetence {Id = 49, RoleId = 10, CompetenceId = 37, CompetenceLevel = 3 },
                new EmpRoleCompetence {Id = 50, RoleId = 10, CompetenceId = 39, CompetenceLevel = 3 },
                new EmpRoleCompetence {Id = 51, RoleId = 11, CompetenceId = 7, CompetenceLevel = 4 },
                new EmpRoleCompetence {Id = 52, RoleId = 11, CompetenceId = 20, CompetenceLevel = 5 },
                new EmpRoleCompetence {Id = 53, RoleId = 11, CompetenceId = 34, CompetenceLevel = 4 },
                new EmpRoleCompetence {Id = 54, RoleId = 11, CompetenceId = 39, CompetenceLevel = 4 },
                new EmpRoleCompetence {Id = 55, RoleId = 11, CompetenceId = 40, CompetenceLevel = 4 },
                new EmpRoleCompetence {Id = 56, RoleId = 12, CompetenceId = 7, CompetenceLevel = 4 },
                new EmpRoleCompetence {Id = 57, RoleId = 12, CompetenceId = 9, CompetenceLevel = 4 },
                new EmpRoleCompetence {Id = 58, RoleId = 12, CompetenceId = 20, CompetenceLevel = 4 },
                new EmpRoleCompetence {Id = 59, RoleId = 12, CompetenceId = 25, CompetenceLevel = 3 },
                new EmpRoleCompetence {Id = 60, RoleId = 12, CompetenceId = 34, CompetenceLevel = 3 },
                new EmpRoleCompetence {Id = 61, RoleId = 13, CompetenceId = 14, CompetenceLevel = 3 },
                new EmpRoleCompetence {Id = 62, RoleId = 13, CompetenceId = 25, CompetenceLevel = 3 },
                new EmpRoleCompetence {Id = 63, RoleId = 13, CompetenceId = 31, CompetenceLevel = 3 },
                new EmpRoleCompetence {Id = 64, RoleId = 13, CompetenceId = 33, CompetenceLevel = 2 },
                new EmpRoleCompetence {Id = 65, RoleId = 14, CompetenceId = 6, CompetenceLevel = 3 },
                new EmpRoleCompetence {Id = 66, RoleId = 14, CompetenceId = 11, CompetenceLevel = 3 },
                new EmpRoleCompetence {Id = 67, RoleId = 14, CompetenceId = 13, CompetenceLevel = 3 },
                new EmpRoleCompetence {Id = 68, RoleId = 14, CompetenceId = 19, CompetenceLevel = 3 },
                new EmpRoleCompetence {Id = 69, RoleId = 14, CompetenceId = 39, CompetenceLevel = 3 },
                new EmpRoleCompetence {Id = 70, RoleId = 15, CompetenceId = 4, CompetenceLevel = 4 },
                new EmpRoleCompetence {Id = 71, RoleId = 15, CompetenceId = 33, CompetenceLevel = 4 },
                new EmpRoleCompetence {Id = 72, RoleId = 15, CompetenceId = 34, CompetenceLevel = 3 },
                new EmpRoleCompetence {Id = 73, RoleId = 15, CompetenceId = 35, CompetenceLevel = 3 },
                new EmpRoleCompetence {Id = 74, RoleId = 15, CompetenceId = 38, CompetenceLevel = 3 },
                new EmpRoleCompetence {Id = 75, RoleId = 16, CompetenceId = 24, CompetenceLevel = 4 },
                new EmpRoleCompetence {Id = 76, RoleId = 16, CompetenceId = 34, CompetenceLevel = 3 },
                new EmpRoleCompetence {Id = 77, RoleId = 16, CompetenceId = 36, CompetenceLevel = 4 },
                new EmpRoleCompetence {Id = 78, RoleId = 16, CompetenceId = 37, CompetenceLevel = 4 },
                new EmpRoleCompetence {Id = 79, RoleId = 17, CompetenceId = 16, CompetenceLevel = 2 },
                new EmpRoleCompetence {Id = 80, RoleId = 17, CompetenceId = 17, CompetenceLevel = 2 },
                new EmpRoleCompetence {Id = 81, RoleId = 17, CompetenceId = 18, CompetenceLevel = 1 },
                new EmpRoleCompetence {Id = 82, RoleId = 17, CompetenceId = 19, CompetenceLevel = 2 },
                new EmpRoleCompetence {Id = 83, RoleId = 18, CompetenceId = 2, CompetenceLevel = 4 },
                new EmpRoleCompetence {Id = 84, RoleId = 18, CompetenceId = 18, CompetenceLevel = 3 },
                new EmpRoleCompetence {Id = 85, RoleId = 18, CompetenceId = 19, CompetenceLevel = 4 },
                new EmpRoleCompetence {Id = 86, RoleId = 18, CompetenceId = 30, CompetenceLevel = 3 },
                new EmpRoleCompetence {Id = 87, RoleId = 18, CompetenceId = 31, CompetenceLevel = 3 },
                //new EmpRoleCompetence {Id = 88, RoleId = 19, CompetenceId = 11, CompetenceLevel = 2 },
                //new EmpRoleCompetence {Id = 89, RoleId = 19, CompetenceId = 12, CompetenceLevel = 2 },
                //new EmpRoleCompetence {Id = 90, RoleId = 19, CompetenceId = 17, CompetenceLevel = 3 },
                //new EmpRoleCompetence {Id = 91, RoleId = 19, CompetenceId = 19, CompetenceLevel = 2 },
                //new EmpRoleCompetence {Id = 92, RoleId = 19, CompetenceId = 39, CompetenceLevel = 2 },
                new EmpRoleCompetence {Id = 93, RoleId = 20, CompetenceId = 5, CompetenceLevel = 3 },
                new EmpRoleCompetence {Id = 94, RoleId = 20, CompetenceId = 14, CompetenceLevel = 3 },
                new EmpRoleCompetence {Id = 95, RoleId = 20, CompetenceId = 15, CompetenceLevel = 4 },
                new EmpRoleCompetence {Id = 96, RoleId = 20, CompetenceId = 36, CompetenceLevel = 4 },
                new EmpRoleCompetence {Id = 97, RoleId = 21, CompetenceId = 5, CompetenceLevel = 4 },
                new EmpRoleCompetence {Id = 98, RoleId = 21, CompetenceId = 7, CompetenceLevel = 4 },
                new EmpRoleCompetence {Id = 99, RoleId = 21, CompetenceId = 9, CompetenceLevel = 4 },
                new EmpRoleCompetence {Id = 100, RoleId = 21, CompetenceId = 11, CompetenceLevel = 4 },
                new EmpRoleCompetence {Id = 101, RoleId = 21, CompetenceId = 15, CompetenceLevel = 4 },
                new EmpRoleCompetence {Id = 102, RoleId = 22, CompetenceId = 17, CompetenceLevel = 3 },
                new EmpRoleCompetence {Id = 103, RoleId = 22, CompetenceId = 18, CompetenceLevel = 2 },
                new EmpRoleCompetence {Id = 104, RoleId = 22, CompetenceId = 19, CompetenceLevel = 3 },
                new EmpRoleCompetence {Id = 105, RoleId = 22, CompetenceId = 34, CompetenceLevel = 2 },
                new EmpRoleCompetence {Id = 106, RoleId = 22, CompetenceId = 37, CompetenceLevel = 2 },
                new EmpRoleCompetence {Id = 107, RoleId = 23, CompetenceId = 11, CompetenceLevel = 3 },
                new EmpRoleCompetence {Id = 109, RoleId = 23, CompetenceId = 12, CompetenceLevel = 3 },
                new EmpRoleCompetence {Id = 110, RoleId = 23, CompetenceId = 13, CompetenceLevel = 2 },
                new EmpRoleCompetence {Id = 111, RoleId = 23, CompetenceId = 14, CompetenceLevel = 3 },
                new EmpRoleCompetence {Id = 112, RoleId = 23, CompetenceId = 34, CompetenceLevel = 2 },
                new EmpRoleCompetence {Id = 113, RoleId = 24, CompetenceId = 6, CompetenceLevel = 3 },
                new EmpRoleCompetence {Id = 114, RoleId = 24, CompetenceId = 9, CompetenceLevel = 4 },
                new EmpRoleCompetence {Id = 115, RoleId = 24, CompetenceId = 21, CompetenceLevel = 3 },
                new EmpRoleCompetence {Id = 116, RoleId = 24, CompetenceId = 22, CompetenceLevel = 4 },
                new EmpRoleCompetence {Id = 117, RoleId = 25, CompetenceId = 3, CompetenceLevel = 5 },
                new EmpRoleCompetence {Id = 118, RoleId = 25, CompetenceId = 5, CompetenceLevel = 5 },
                new EmpRoleCompetence {Id = 119, RoleId = 25, CompetenceId = 9, CompetenceLevel = 5 },
                new EmpRoleCompetence {Id = 120, RoleId = 25, CompetenceId = 38, CompetenceLevel = 5 },
                new EmpRoleCompetence {Id = 121, RoleId = 25, CompetenceId = 40, CompetenceLevel = 5 },
                new EmpRoleCompetence {Id = 122, RoleId = 26, CompetenceId = 10, CompetenceLevel = 3 },
                new EmpRoleCompetence {Id = 123, RoleId = 26, CompetenceId = 11, CompetenceLevel = 4 },
                new EmpRoleCompetence {Id = 124, RoleId = 26, CompetenceId = 12, CompetenceLevel = 4 },
                new EmpRoleCompetence {Id = 125, RoleId = 26, CompetenceId = 13, CompetenceLevel = 3 },
                new EmpRoleCompetence {Id = 126, RoleId = 26, CompetenceId = 17, CompetenceLevel = 3 },
                new EmpRoleCompetence {Id = 127, RoleId = 27, CompetenceId = 7, CompetenceLevel = 5 },
                new EmpRoleCompetence {Id = 128, RoleId = 27, CompetenceId = 9, CompetenceLevel = 4 },
                new EmpRoleCompetence {Id = 129, RoleId = 27, CompetenceId = 21, CompetenceLevel = 5 },
                new EmpRoleCompetence {Id = 130, RoleId = 27, CompetenceId = 22, CompetenceLevel = 4 },
                new EmpRoleCompetence {Id = 131, RoleId = 27, CompetenceId = 32, CompetenceLevel = 4 },
                new EmpRoleCompetence {Id = 132, RoleId = 28, CompetenceId = 6, CompetenceLevel = 3 },
                new EmpRoleCompetence {Id = 133, RoleId = 28, CompetenceId = 21, CompetenceLevel = 4 },
                new EmpRoleCompetence {Id = 134, RoleId = 28, CompetenceId = 37, CompetenceLevel = 4 },
                new EmpRoleCompetence {Id = 135, RoleId = 28, CompetenceId = 39, CompetenceLevel = 4 },
                new EmpRoleCompetence {Id = 136, RoleId = 29, CompetenceId = 12, CompetenceLevel = 3 },
                new EmpRoleCompetence {Id = 137, RoleId = 29, CompetenceId = 15, CompetenceLevel = 4 },
                new EmpRoleCompetence {Id = 138, RoleId = 29, CompetenceId = 31, CompetenceLevel = 3 },
                new EmpRoleCompetence {Id = 139, RoleId = 29, CompetenceId = 35, CompetenceLevel = 3 },
                new EmpRoleCompetence {Id = 140, RoleId = 30, CompetenceId = 4, CompetenceLevel = 4 },
                new EmpRoleCompetence {Id = 141, RoleId = 30, CompetenceId = 9, CompetenceLevel = 4 },
                new EmpRoleCompetence {Id = 142, RoleId = 30, CompetenceId = 22, CompetenceLevel = 4 },
                new EmpRoleCompetence {Id = 143, RoleId = 30, CompetenceId = 35, CompetenceLevel = 4 },
            };

            context.EmpRoleCompetence.AddRange(items);

            context.SaveChanges();

            if (true && context.Database.IsSqlServer())
            {
                //context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [EmpRoleCompetence] OFF");
                trans.Commit();
            }

            return true;
        }
    }
}
