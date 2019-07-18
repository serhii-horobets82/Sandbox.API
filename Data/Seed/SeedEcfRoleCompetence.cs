using Evoflare.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using System.Globalization;


namespace Evoflare.API.Data
{
    public static partial class DbInitializer
    {
    
		public static bool SeedEcfRoleCompetence(EvoflareDbContext context)
        {
            if (context.EcfRoleCompetence.Any()) return false;
            var trans = context.Database.BeginTransaction();
			try
            {
                if(context.Database.IsSqlServer())
                    context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [EcfRoleCompetence] ON");
			}
            catch { trans.Rollback(); } // TODO find better solution 

            var items = new[]
            {
				new EcfRoleCompetence {Id = 1, RoleId = 1, CompetenceId = "D5 ", CompetenceLevel = 3 },
				new EcfRoleCompetence {Id = 2, RoleId = 1, CompetenceId = "D6 ", CompetenceLevel = 4 },
				new EcfRoleCompetence {Id = 3, RoleId = 1, CompetenceId = "D7 ", CompetenceLevel = 4 },
				new EcfRoleCompetence {Id = 4, RoleId = 1, CompetenceId = "E1 ", CompetenceLevel = 3 },
				new EcfRoleCompetence {Id = 5, RoleId = 1, CompetenceId = "E4 ", CompetenceLevel = 4 },
				new EcfRoleCompetence {Id = 6, RoleId = 2, CompetenceId = "A1 ", CompetenceLevel = 4 },
				new EcfRoleCompetence {Id = 7, RoleId = 2, CompetenceId = "A3 ", CompetenceLevel = 4 },
				new EcfRoleCompetence {Id = 8, RoleId = 2, CompetenceId = "D10", CompetenceLevel = 4 },
				new EcfRoleCompetence {Id = 9, RoleId = 2, CompetenceId = "D11", CompetenceLevel = 4 },
				new EcfRoleCompetence {Id = 10, RoleId = 2, CompetenceId = "E5 ", CompetenceLevel = 1 },
				new EcfRoleCompetence {Id = 11, RoleId = 3, CompetenceId = "A1 ", CompetenceLevel = 4 },
				new EcfRoleCompetence {Id = 12, RoleId = 3, CompetenceId = "A3 ", CompetenceLevel = 4 },
				new EcfRoleCompetence {Id = 13, RoleId = 3, CompetenceId = "E4 ", CompetenceLevel = 4 },
				new EcfRoleCompetence {Id = 14, RoleId = 3, CompetenceId = "E7 ", CompetenceLevel = 4 },
				new EcfRoleCompetence {Id = 15, RoleId = 3, CompetenceId = "E9 ", CompetenceLevel = 5 },
				new EcfRoleCompetence {Id = 16, RoleId = 4, CompetenceId = "A1 ", CompetenceLevel = 5 },
				new EcfRoleCompetence {Id = 17, RoleId = 4, CompetenceId = "A3 ", CompetenceLevel = 5 },
				new EcfRoleCompetence {Id = 18, RoleId = 4, CompetenceId = "E2 ", CompetenceLevel = 5 },
				new EcfRoleCompetence {Id = 19, RoleId = 4, CompetenceId = "E4 ", CompetenceLevel = 4 },
				new EcfRoleCompetence {Id = 20, RoleId = 4, CompetenceId = "E9 ", CompetenceLevel = 5 },
				new EcfRoleCompetence {Id = 21, RoleId = 5, CompetenceId = "B1 ", CompetenceLevel = 3 },
				new EcfRoleCompetence {Id = 22, RoleId = 5, CompetenceId = "B2 ", CompetenceLevel = 3 },
				new EcfRoleCompetence {Id = 23, RoleId = 5, CompetenceId = "C2 ", CompetenceLevel = 3 },
				new EcfRoleCompetence {Id = 24, RoleId = 5, CompetenceId = "D10", CompetenceLevel = 3 },
				new EcfRoleCompetence {Id = 25, RoleId = 5, CompetenceId = "E8 ", CompetenceLevel = 3 },
				new EcfRoleCompetence {Id = 26, RoleId = 6, CompetenceId = "B1 ", CompetenceLevel = 3 },
				new EcfRoleCompetence {Id = 27, RoleId = 6, CompetenceId = "B2 ", CompetenceLevel = 2 },
				new EcfRoleCompetence {Id = 28, RoleId = 6, CompetenceId = "B3 ", CompetenceLevel = 2 },
				new EcfRoleCompetence {Id = 29, RoleId = 6, CompetenceId = "B5 ", CompetenceLevel = 3 },
				new EcfRoleCompetence {Id = 30, RoleId = 6, CompetenceId = "C4 ", CompetenceLevel = 3 },
				new EcfRoleCompetence {Id = 31, RoleId = 7, CompetenceId = "A6 ", CompetenceLevel = 2 },
				new EcfRoleCompetence {Id = 32, RoleId = 7, CompetenceId = "B1 ", CompetenceLevel = 3 },
				new EcfRoleCompetence {Id = 33, RoleId = 7, CompetenceId = "B3 ", CompetenceLevel = 2 },
				new EcfRoleCompetence {Id = 34, RoleId = 7, CompetenceId = "B4 ", CompetenceLevel = 3 },
				new EcfRoleCompetence {Id = 35, RoleId = 7, CompetenceId = "D12", CompetenceLevel = 2 },
				new EcfRoleCompetence {Id = 36, RoleId = 8, CompetenceId = "A1 ", CompetenceLevel = 5 },
				new EcfRoleCompetence {Id = 37, RoleId = 8, CompetenceId = "A3 ", CompetenceLevel = 4 },
				new EcfRoleCompetence {Id = 38, RoleId = 8, CompetenceId = "A5 ", CompetenceLevel = 4 },
				new EcfRoleCompetence {Id = 39, RoleId = 8, CompetenceId = "A7 ", CompetenceLevel = 5 },
				new EcfRoleCompetence {Id = 40, RoleId = 8, CompetenceId = "E8 ", CompetenceLevel = 3 },
				new EcfRoleCompetence {Id = 41, RoleId = 9, CompetenceId = "A7 ", CompetenceLevel = 4 },
				new EcfRoleCompetence {Id = 42, RoleId = 9, CompetenceId = "A9 ", CompetenceLevel = 4 },
				new EcfRoleCompetence {Id = 43, RoleId = 9, CompetenceId = "D11", CompetenceLevel = 4 },
				new EcfRoleCompetence {Id = 44, RoleId = 9, CompetenceId = "E3 ", CompetenceLevel = 4 },
				new EcfRoleCompetence {Id = 45, RoleId = 9, CompetenceId = "E7 ", CompetenceLevel = 4 },
				new EcfRoleCompetence {Id = 46, RoleId = 10, CompetenceId = "D9 ", CompetenceLevel = 4 },
				new EcfRoleCompetence {Id = 47, RoleId = 10, CompetenceId = "E2 ", CompetenceLevel = 4 },
				new EcfRoleCompetence {Id = 48, RoleId = 10, CompetenceId = "E3 ", CompetenceLevel = 3 },
				new EcfRoleCompetence {Id = 49, RoleId = 10, CompetenceId = "E6 ", CompetenceLevel = 3 },
				new EcfRoleCompetence {Id = 50, RoleId = 10, CompetenceId = "E8 ", CompetenceLevel = 3 },
				new EcfRoleCompetence {Id = 51, RoleId = 11, CompetenceId = "A7 ", CompetenceLevel = 4 },
				new EcfRoleCompetence {Id = 52, RoleId = 11, CompetenceId = "D1 ", CompetenceLevel = 5 },
				new EcfRoleCompetence {Id = 53, RoleId = 11, CompetenceId = "E3 ", CompetenceLevel = 4 },
				new EcfRoleCompetence {Id = 54, RoleId = 11, CompetenceId = "E8 ", CompetenceLevel = 4 },
				new EcfRoleCompetence {Id = 55, RoleId = 11, CompetenceId = "E9 ", CompetenceLevel = 4 },
				new EcfRoleCompetence {Id = 56, RoleId = 12, CompetenceId = "A7 ", CompetenceLevel = 4 },
				new EcfRoleCompetence {Id = 57, RoleId = 12, CompetenceId = "A9 ", CompetenceLevel = 4 },
				new EcfRoleCompetence {Id = 58, RoleId = 12, CompetenceId = "D1 ", CompetenceLevel = 4 },
				new EcfRoleCompetence {Id = 59, RoleId = 12, CompetenceId = "D3 ", CompetenceLevel = 3 },
				new EcfRoleCompetence {Id = 60, RoleId = 12, CompetenceId = "E3 ", CompetenceLevel = 3 },
				new EcfRoleCompetence {Id = 61, RoleId = 13, CompetenceId = "B5 ", CompetenceLevel = 3 },
				new EcfRoleCompetence {Id = 62, RoleId = 13, CompetenceId = "D3 ", CompetenceLevel = 3 },
				new EcfRoleCompetence {Id = 63, RoleId = 13, CompetenceId = "D9 ", CompetenceLevel = 3 },
				new EcfRoleCompetence {Id = 64, RoleId = 13, CompetenceId = "E2 ", CompetenceLevel = 2 },
				new EcfRoleCompetence {Id = 65, RoleId = 14, CompetenceId = "A6 ", CompetenceLevel = 3 },
				new EcfRoleCompetence {Id = 66, RoleId = 14, CompetenceId = "B2 ", CompetenceLevel = 3 },
				new EcfRoleCompetence {Id = 67, RoleId = 14, CompetenceId = "B4 ", CompetenceLevel = 3 },
				new EcfRoleCompetence {Id = 68, RoleId = 14, CompetenceId = "C4 ", CompetenceLevel = 3 },
				new EcfRoleCompetence {Id = 69, RoleId = 14, CompetenceId = "E8 ", CompetenceLevel = 3 },
				new EcfRoleCompetence {Id = 70, RoleId = 15, CompetenceId = "A4 ", CompetenceLevel = 4 },
				new EcfRoleCompetence {Id = 71, RoleId = 15, CompetenceId = "E2 ", CompetenceLevel = 4 },
				new EcfRoleCompetence {Id = 72, RoleId = 15, CompetenceId = "E3 ", CompetenceLevel = 3 },
				new EcfRoleCompetence {Id = 73, RoleId = 15, CompetenceId = "E4 ", CompetenceLevel = 3 },
				new EcfRoleCompetence {Id = 74, RoleId = 15, CompetenceId = "E7 ", CompetenceLevel = 3 },
				new EcfRoleCompetence {Id = 75, RoleId = 16, CompetenceId = "D2 ", CompetenceLevel = 4 },
				new EcfRoleCompetence {Id = 76, RoleId = 16, CompetenceId = "E3 ", CompetenceLevel = 3 },
				new EcfRoleCompetence {Id = 77, RoleId = 16, CompetenceId = "E5 ", CompetenceLevel = 4 },
				new EcfRoleCompetence {Id = 78, RoleId = 16, CompetenceId = "E6 ", CompetenceLevel = 4 },
				new EcfRoleCompetence {Id = 79, RoleId = 17, CompetenceId = "C1 ", CompetenceLevel = 2 },
				new EcfRoleCompetence {Id = 80, RoleId = 17, CompetenceId = "C2 ", CompetenceLevel = 2 },
				new EcfRoleCompetence {Id = 81, RoleId = 17, CompetenceId = "C3 ", CompetenceLevel = 1 },
				new EcfRoleCompetence {Id = 82, RoleId = 17, CompetenceId = "C4 ", CompetenceLevel = 2 },
				new EcfRoleCompetence {Id = 83, RoleId = 18, CompetenceId = "A2 ", CompetenceLevel = 4 },
				new EcfRoleCompetence {Id = 84, RoleId = 18, CompetenceId = "C3 ", CompetenceLevel = 3 },
				new EcfRoleCompetence {Id = 85, RoleId = 18, CompetenceId = "C4 ", CompetenceLevel = 4 },
				new EcfRoleCompetence {Id = 86, RoleId = 18, CompetenceId = "D8 ", CompetenceLevel = 3 },
				new EcfRoleCompetence {Id = 87, RoleId = 18, CompetenceId = "D9 ", CompetenceLevel = 3 },
				new EcfRoleCompetence {Id = 88, RoleId = 19, CompetenceId = "B2 ", CompetenceLevel = 2 },
				new EcfRoleCompetence {Id = 89, RoleId = 19, CompetenceId = "B3 ", CompetenceLevel = 2 },
				new EcfRoleCompetence {Id = 90, RoleId = 19, CompetenceId = "C2 ", CompetenceLevel = 3 },
				new EcfRoleCompetence {Id = 91, RoleId = 19, CompetenceId = "C4 ", CompetenceLevel = 2 },
				new EcfRoleCompetence {Id = 92, RoleId = 19, CompetenceId = "E8 ", CompetenceLevel = 2 },
				new EcfRoleCompetence {Id = 93, RoleId = 20, CompetenceId = "A5 ", CompetenceLevel = 3 },
				new EcfRoleCompetence {Id = 94, RoleId = 20, CompetenceId = "B5 ", CompetenceLevel = 3 },
				new EcfRoleCompetence {Id = 95, RoleId = 20, CompetenceId = "B6 ", CompetenceLevel = 4 },
				new EcfRoleCompetence {Id = 96, RoleId = 20, CompetenceId = "E5 ", CompetenceLevel = 4 },
				new EcfRoleCompetence {Id = 97, RoleId = 21, CompetenceId = "A5 ", CompetenceLevel = 4 },
				new EcfRoleCompetence {Id = 98, RoleId = 21, CompetenceId = "A7 ", CompetenceLevel = 4 },
				new EcfRoleCompetence {Id = 99, RoleId = 21, CompetenceId = "A9 ", CompetenceLevel = 4 },
				new EcfRoleCompetence {Id = 100, RoleId = 21, CompetenceId = "B2 ", CompetenceLevel = 4 },
				new EcfRoleCompetence {Id = 101, RoleId = 21, CompetenceId = "B6 ", CompetenceLevel = 4 },
				new EcfRoleCompetence {Id = 102, RoleId = 22, CompetenceId = "C2 ", CompetenceLevel = 3 },
				new EcfRoleCompetence {Id = 103, RoleId = 22, CompetenceId = "C3 ", CompetenceLevel = 2 },
				new EcfRoleCompetence {Id = 104, RoleId = 22, CompetenceId = "C4 ", CompetenceLevel = 3 },
				new EcfRoleCompetence {Id = 105, RoleId = 22, CompetenceId = "E3 ", CompetenceLevel = 2 },
				new EcfRoleCompetence {Id = 106, RoleId = 22, CompetenceId = "E6 ", CompetenceLevel = 2 },
				new EcfRoleCompetence {Id = 107, RoleId = 23, CompetenceId = "B2 ", CompetenceLevel = 3 },
				new EcfRoleCompetence {Id = 109, RoleId = 23, CompetenceId = "B3 ", CompetenceLevel = 3 },
				new EcfRoleCompetence {Id = 110, RoleId = 23, CompetenceId = "B4 ", CompetenceLevel = 2 },
				new EcfRoleCompetence {Id = 111, RoleId = 23, CompetenceId = "B5 ", CompetenceLevel = 3 },
				new EcfRoleCompetence {Id = 112, RoleId = 23, CompetenceId = "E3 ", CompetenceLevel = 2 },
				new EcfRoleCompetence {Id = 113, RoleId = 24, CompetenceId = "A6 ", CompetenceLevel = 3 },
				new EcfRoleCompetence {Id = 114, RoleId = 24, CompetenceId = "A9 ", CompetenceLevel = 4 },
				new EcfRoleCompetence {Id = 115, RoleId = 24, CompetenceId = "D10", CompetenceLevel = 3 },
				new EcfRoleCompetence {Id = 116, RoleId = 24, CompetenceId = "D11", CompetenceLevel = 4 },
				new EcfRoleCompetence {Id = 117, RoleId = 25, CompetenceId = "A3 ", CompetenceLevel = 5 },
				new EcfRoleCompetence {Id = 118, RoleId = 25, CompetenceId = "A5 ", CompetenceLevel = 5 },
				new EcfRoleCompetence {Id = 119, RoleId = 25, CompetenceId = "A9 ", CompetenceLevel = 5 },
				new EcfRoleCompetence {Id = 120, RoleId = 25, CompetenceId = "E7 ", CompetenceLevel = 5 },
				new EcfRoleCompetence {Id = 121, RoleId = 25, CompetenceId = "E9 ", CompetenceLevel = 5 },
				new EcfRoleCompetence {Id = 122, RoleId = 26, CompetenceId = "B1 ", CompetenceLevel = 3 },
				new EcfRoleCompetence {Id = 123, RoleId = 26, CompetenceId = "B2 ", CompetenceLevel = 4 },
				new EcfRoleCompetence {Id = 124, RoleId = 26, CompetenceId = "B3 ", CompetenceLevel = 4 },
				new EcfRoleCompetence {Id = 125, RoleId = 26, CompetenceId = "B4 ", CompetenceLevel = 3 },
				new EcfRoleCompetence {Id = 126, RoleId = 26, CompetenceId = "C2 ", CompetenceLevel = 3 },
				new EcfRoleCompetence {Id = 127, RoleId = 27, CompetenceId = "A7 ", CompetenceLevel = 5 },
				new EcfRoleCompetence {Id = 128, RoleId = 27, CompetenceId = "A9 ", CompetenceLevel = 4 },
				new EcfRoleCompetence {Id = 129, RoleId = 27, CompetenceId = "D10", CompetenceLevel = 5 },
				new EcfRoleCompetence {Id = 130, RoleId = 27, CompetenceId = "D11", CompetenceLevel = 4 },
				new EcfRoleCompetence {Id = 131, RoleId = 27, CompetenceId = "E1 ", CompetenceLevel = 4 },
				new EcfRoleCompetence {Id = 132, RoleId = 28, CompetenceId = "A6 ", CompetenceLevel = 3 },
				new EcfRoleCompetence {Id = 133, RoleId = 28, CompetenceId = "D10", CompetenceLevel = 4 },
				new EcfRoleCompetence {Id = 134, RoleId = 28, CompetenceId = "E6 ", CompetenceLevel = 4 },
				new EcfRoleCompetence {Id = 135, RoleId = 28, CompetenceId = "E8 ", CompetenceLevel = 4 },
				new EcfRoleCompetence {Id = 136, RoleId = 29, CompetenceId = "B3 ", CompetenceLevel = 3 },
				new EcfRoleCompetence {Id = 137, RoleId = 29, CompetenceId = "B6 ", CompetenceLevel = 4 },
				new EcfRoleCompetence {Id = 138, RoleId = 29, CompetenceId = "D9 ", CompetenceLevel = 3 },
				new EcfRoleCompetence {Id = 139, RoleId = 29, CompetenceId = "E4 ", CompetenceLevel = 3 },
				new EcfRoleCompetence {Id = 140, RoleId = 30, CompetenceId = "A4 ", CompetenceLevel = 4 },
				new EcfRoleCompetence {Id = 141, RoleId = 30, CompetenceId = "A9 ", CompetenceLevel = 4 },
				new EcfRoleCompetence {Id = 142, RoleId = 30, CompetenceId = "D11", CompetenceLevel = 4 },
				new EcfRoleCompetence {Id = 143, RoleId = 30, CompetenceId = "E4 ", CompetenceLevel = 4 },

            };
            context.EcfRoleCompetence.AddRange(items);

            context.SaveChanges();

			try
            {
				if(context.Database.IsSqlServer())
					context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [EcfRoleCompetence] OFF");
				trans.Commit();
			}
            catch { } // TODO find better solution 
            
            return true;
        }
    }
}
