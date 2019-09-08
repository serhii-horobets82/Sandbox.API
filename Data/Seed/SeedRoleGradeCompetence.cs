using Evoflare.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Globalization;
using System.Linq;


namespace Evoflare.API.Data
{
    public static partial class DbInitializer
    {
    
		public static bool SeedRoleGradeCompetence(EvoflareDbContext context)
        {
            if (context.RoleGradeCompetence.Any()) return false;
            IDbContextTransaction trans = null;
            
			if(true && context.Database.IsSqlServer())
			{
			    trans = context.Database.BeginTransaction();
                context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [RoleGradeCompetence] ON");
			}
            var items = new[]
            {
				new RoleGradeCompetence {Id = 10, CompetenceId = 2, CompetenceLevelId = 3, RoleGradeId = 14 },
				new RoleGradeCompetence {Id = 11, CompetenceId = 4, CompetenceLevelId = 8, RoleGradeId = 14 },
				new RoleGradeCompetence {Id = 12, CompetenceId = 6, CompetenceLevelId = 14, RoleGradeId = 14 },
				new RoleGradeCompetence {Id = 13, CompetenceId = 2, CompetenceLevelId = 3, RoleGradeId = 15 },
				new RoleGradeCompetence {Id = 14, CompetenceId = 4, CompetenceLevelId = 9, RoleGradeId = 15 },
				new RoleGradeCompetence {Id = 15, CompetenceId = 6, CompetenceLevelId = 15, RoleGradeId = 15 },
				new RoleGradeCompetence {Id = 22, CompetenceId = 6, CompetenceLevelId = 14, RoleGradeId = 2 },
				new RoleGradeCompetence {Id = 23, CompetenceId = 10, CompetenceLevelId = 23, RoleGradeId = 2 },
				new RoleGradeCompetence {Id = 24, CompetenceId = 11, CompetenceLevelId = 26, RoleGradeId = 2 },
				new RoleGradeCompetence {Id = 25, CompetenceId = 12, CompetenceLevelId = 29, RoleGradeId = 2 },
				new RoleGradeCompetence {Id = 26, CompetenceId = 13, CompetenceLevelId = 33, RoleGradeId = 2 },
				new RoleGradeCompetence {Id = 27, CompetenceId = 14, CompetenceLevelId = 36, RoleGradeId = 2 },
				new RoleGradeCompetence {Id = 28, CompetenceId = 16, CompetenceLevelId = 41, RoleGradeId = 2 },
				new RoleGradeCompetence {Id = 37, CompetenceId = 5, CompetenceLevelId = 11, RoleGradeId = 3 },
				new RoleGradeCompetence {Id = 38, CompetenceId = 6, CompetenceLevelId = 15, RoleGradeId = 3 },
				new RoleGradeCompetence {Id = 39, CompetenceId = 10, CompetenceLevelId = 24, RoleGradeId = 3 },
				new RoleGradeCompetence {Id = 40, CompetenceId = 11, CompetenceLevelId = 27, RoleGradeId = 3 },
				new RoleGradeCompetence {Id = 41, CompetenceId = 12, CompetenceLevelId = 30, RoleGradeId = 3 },
				new RoleGradeCompetence {Id = 42, CompetenceId = 13, CompetenceLevelId = 34, RoleGradeId = 3 },
				new RoleGradeCompetence {Id = 43, CompetenceId = 14, CompetenceLevelId = 37, RoleGradeId = 3 },
				new RoleGradeCompetence {Id = 44, CompetenceId = 16, CompetenceLevelId = 41, RoleGradeId = 3 },
				new RoleGradeCompetence {Id = 45, CompetenceId = 19, CompetenceLevelId = 49, RoleGradeId = 3 },
				new RoleGradeCompetence {Id = 46, CompetenceId = 4, CompetenceLevelId = 8, RoleGradeId = 4 },
				new RoleGradeCompetence {Id = 47, CompetenceId = 5, CompetenceLevelId = 12, RoleGradeId = 4 },
				new RoleGradeCompetence {Id = 48, CompetenceId = 6, CompetenceLevelId = 16, RoleGradeId = 4 },
				new RoleGradeCompetence {Id = 49, CompetenceId = 7, CompetenceLevelId = 17, RoleGradeId = 4 },
				new RoleGradeCompetence {Id = 50, CompetenceId = 10, CompetenceLevelId = 25, RoleGradeId = 4 },
				new RoleGradeCompetence {Id = 51, CompetenceId = 11, CompetenceLevelId = 28, RoleGradeId = 4 },
				new RoleGradeCompetence {Id = 52, CompetenceId = 12, CompetenceLevelId = 30, RoleGradeId = 4 },
				new RoleGradeCompetence {Id = 53, CompetenceId = 13, CompetenceLevelId = 34, RoleGradeId = 4 },
				new RoleGradeCompetence {Id = 54, CompetenceId = 14, CompetenceLevelId = 38, RoleGradeId = 4 },
				new RoleGradeCompetence {Id = 55, CompetenceId = 16, CompetenceLevelId = 42, RoleGradeId = 4 },
				new RoleGradeCompetence {Id = 56, CompetenceId = 18, CompetenceLevelId = 46, RoleGradeId = 4 },
				new RoleGradeCompetence {Id = 57, CompetenceId = 19, CompetenceLevelId = 49, RoleGradeId = 4 },
				new RoleGradeCompetence {Id = 58, CompetenceId = 4, CompetenceLevelId = 9, RoleGradeId = 5 },
				new RoleGradeCompetence {Id = 59, CompetenceId = 35, CompetenceLevelId = 92, RoleGradeId = 5 },
				new RoleGradeCompetence {Id = 60, CompetenceId = 34, CompetenceLevelId = 89, RoleGradeId = 5 },
				new RoleGradeCompetence {Id = 61, CompetenceId = 31, CompetenceLevelId = 71, RoleGradeId = 5 },
				new RoleGradeCompetence {Id = 62, CompetenceId = 27, CompetenceLevelId = 61, RoleGradeId = 5 },
				new RoleGradeCompetence {Id = 63, CompetenceId = 22, CompetenceLevelId = 78, RoleGradeId = 5 },
				new RoleGradeCompetence {Id = 64, CompetenceId = 21, CompetenceLevelId = 74, RoleGradeId = 5 },
				new RoleGradeCompetence {Id = 65, CompetenceId = 19, CompetenceLevelId = 50, RoleGradeId = 5 },
				new RoleGradeCompetence {Id = 66, CompetenceId = 18, CompetenceLevelId = 47, RoleGradeId = 5 },
				new RoleGradeCompetence {Id = 67, CompetenceId = 16, CompetenceLevelId = 42, RoleGradeId = 5 },
				new RoleGradeCompetence {Id = 68, CompetenceId = 15, CompetenceLevelId = 39, RoleGradeId = 5 },
				new RoleGradeCompetence {Id = 69, CompetenceId = 14, CompetenceLevelId = 38, RoleGradeId = 5 },
				new RoleGradeCompetence {Id = 70, CompetenceId = 13, CompetenceLevelId = 35, RoleGradeId = 5 },
				new RoleGradeCompetence {Id = 71, CompetenceId = 12, CompetenceLevelId = 30, RoleGradeId = 5 },
				new RoleGradeCompetence {Id = 72, CompetenceId = 11, CompetenceLevelId = 28, RoleGradeId = 5 },
				new RoleGradeCompetence {Id = 73, CompetenceId = 10, CompetenceLevelId = 25, RoleGradeId = 5 },
				new RoleGradeCompetence {Id = 74, CompetenceId = 9, CompetenceLevelId = 21, RoleGradeId = 5 },
				new RoleGradeCompetence {Id = 75, CompetenceId = 7, CompetenceLevelId = 18, RoleGradeId = 5 },
				new RoleGradeCompetence {Id = 76, CompetenceId = 6, CompetenceLevelId = 16, RoleGradeId = 5 },
				new RoleGradeCompetence {Id = 77, CompetenceId = 5, CompetenceLevelId = 12, RoleGradeId = 5 },
				new RoleGradeCompetence {Id = 78, CompetenceId = 36, CompetenceLevelId = 94, RoleGradeId = 5 },
				new RoleGradeCompetence {Id = 79, CompetenceId = 39, CompetenceLevelId = 102, RoleGradeId = 5 },
				new RoleGradeCompetence {Id = 83, CompetenceId = 16, CompetenceLevelId = 41, RoleGradeId = 1 },
				new RoleGradeCompetence {Id = 84, CompetenceId = 11, CompetenceLevelId = 26, RoleGradeId = 1 },
				new RoleGradeCompetence {Id = 85, CompetenceId = 12, CompetenceLevelId = 29, RoleGradeId = 1 },
				new RoleGradeCompetence {Id = 86, CompetenceId = 14, CompetenceLevelId = 36, RoleGradeId = 1 },

            };
            context.RoleGradeCompetence.AddRange(items);

            context.SaveChanges();

			if(true && context.Database.IsSqlServer())
            {
				context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [RoleGradeCompetence] OFF");
				trans.Commit();
			}
            
            return true;
        }
    }
}
