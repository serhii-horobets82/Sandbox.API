using Evoflare.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using System.Globalization;


namespace Evoflare.API.Data
{
    public static partial class DbInitializer
    {
    
		public static bool SeedRoleGradeCompetence(EvoflareDbContext context)
        {
            if (context.RoleGradeCompetence.Any()) return false;
            var trans = context.Database.BeginTransaction();
            context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [RoleGradeCompetence] ON");

            var items = new[]
            {
				new RoleGradeCompetence {Id = 10, CompetenceId = "A2 ", CompetenceLevelId = 3, RoleGradeId = 14 },
				new RoleGradeCompetence {Id = 11, CompetenceId = "A4 ", CompetenceLevelId = 8, RoleGradeId = 14 },
				new RoleGradeCompetence {Id = 12, CompetenceId = "A6 ", CompetenceLevelId = 14, RoleGradeId = 14 },
				new RoleGradeCompetence {Id = 13, CompetenceId = "A2 ", CompetenceLevelId = 3, RoleGradeId = 15 },
				new RoleGradeCompetence {Id = 14, CompetenceId = "A4 ", CompetenceLevelId = 9, RoleGradeId = 15 },
				new RoleGradeCompetence {Id = 15, CompetenceId = "A6 ", CompetenceLevelId = 15, RoleGradeId = 15 },
				new RoleGradeCompetence {Id = 22, CompetenceId = "A6 ", CompetenceLevelId = 14, RoleGradeId = 2 },
				new RoleGradeCompetence {Id = 23, CompetenceId = "B1 ", CompetenceLevelId = 23, RoleGradeId = 2 },
				new RoleGradeCompetence {Id = 24, CompetenceId = "B2 ", CompetenceLevelId = 26, RoleGradeId = 2 },
				new RoleGradeCompetence {Id = 25, CompetenceId = "B3 ", CompetenceLevelId = 29, RoleGradeId = 2 },
				new RoleGradeCompetence {Id = 26, CompetenceId = "B4 ", CompetenceLevelId = 33, RoleGradeId = 2 },
				new RoleGradeCompetence {Id = 27, CompetenceId = "B5 ", CompetenceLevelId = 36, RoleGradeId = 2 },
				new RoleGradeCompetence {Id = 28, CompetenceId = "C1 ", CompetenceLevelId = 41, RoleGradeId = 2 },
				new RoleGradeCompetence {Id = 37, CompetenceId = "A5 ", CompetenceLevelId = 11, RoleGradeId = 3 },
				new RoleGradeCompetence {Id = 38, CompetenceId = "A6 ", CompetenceLevelId = 15, RoleGradeId = 3 },
				new RoleGradeCompetence {Id = 39, CompetenceId = "B1 ", CompetenceLevelId = 24, RoleGradeId = 3 },
				new RoleGradeCompetence {Id = 40, CompetenceId = "B2 ", CompetenceLevelId = 27, RoleGradeId = 3 },
				new RoleGradeCompetence {Id = 41, CompetenceId = "B3 ", CompetenceLevelId = 30, RoleGradeId = 3 },
				new RoleGradeCompetence {Id = 42, CompetenceId = "B4 ", CompetenceLevelId = 34, RoleGradeId = 3 },
				new RoleGradeCompetence {Id = 43, CompetenceId = "B5 ", CompetenceLevelId = 37, RoleGradeId = 3 },
				new RoleGradeCompetence {Id = 44, CompetenceId = "C1 ", CompetenceLevelId = 41, RoleGradeId = 3 },
				new RoleGradeCompetence {Id = 45, CompetenceId = "C4 ", CompetenceLevelId = 49, RoleGradeId = 3 },
				new RoleGradeCompetence {Id = 46, CompetenceId = "A4 ", CompetenceLevelId = 8, RoleGradeId = 4 },
				new RoleGradeCompetence {Id = 47, CompetenceId = "A5 ", CompetenceLevelId = 12, RoleGradeId = 4 },
				new RoleGradeCompetence {Id = 48, CompetenceId = "A6 ", CompetenceLevelId = 16, RoleGradeId = 4 },
				new RoleGradeCompetence {Id = 49, CompetenceId = "A7 ", CompetenceLevelId = 17, RoleGradeId = 4 },
				new RoleGradeCompetence {Id = 50, CompetenceId = "B1 ", CompetenceLevelId = 25, RoleGradeId = 4 },
				new RoleGradeCompetence {Id = 51, CompetenceId = "B2 ", CompetenceLevelId = 28, RoleGradeId = 4 },
				new RoleGradeCompetence {Id = 52, CompetenceId = "B3 ", CompetenceLevelId = 30, RoleGradeId = 4 },
				new RoleGradeCompetence {Id = 53, CompetenceId = "B4 ", CompetenceLevelId = 34, RoleGradeId = 4 },
				new RoleGradeCompetence {Id = 54, CompetenceId = "B5 ", CompetenceLevelId = 38, RoleGradeId = 4 },
				new RoleGradeCompetence {Id = 55, CompetenceId = "C1 ", CompetenceLevelId = 42, RoleGradeId = 4 },
				new RoleGradeCompetence {Id = 56, CompetenceId = "C3 ", CompetenceLevelId = 46, RoleGradeId = 4 },
				new RoleGradeCompetence {Id = 57, CompetenceId = "C4 ", CompetenceLevelId = 49, RoleGradeId = 4 },
				new RoleGradeCompetence {Id = 58, CompetenceId = "A4 ", CompetenceLevelId = 9, RoleGradeId = 5 },
				new RoleGradeCompetence {Id = 59, CompetenceId = "E4 ", CompetenceLevelId = 92, RoleGradeId = 5 },
				new RoleGradeCompetence {Id = 60, CompetenceId = "E3 ", CompetenceLevelId = 89, RoleGradeId = 5 },
				new RoleGradeCompetence {Id = 61, CompetenceId = "D9 ", CompetenceLevelId = 71, RoleGradeId = 5 },
				new RoleGradeCompetence {Id = 62, CompetenceId = "D5 ", CompetenceLevelId = 61, RoleGradeId = 5 },
				new RoleGradeCompetence {Id = 63, CompetenceId = "D11", CompetenceLevelId = 78, RoleGradeId = 5 },
				new RoleGradeCompetence {Id = 64, CompetenceId = "D10", CompetenceLevelId = 74, RoleGradeId = 5 },
				new RoleGradeCompetence {Id = 65, CompetenceId = "C4 ", CompetenceLevelId = 50, RoleGradeId = 5 },
				new RoleGradeCompetence {Id = 66, CompetenceId = "C3 ", CompetenceLevelId = 47, RoleGradeId = 5 },
				new RoleGradeCompetence {Id = 67, CompetenceId = "C1 ", CompetenceLevelId = 42, RoleGradeId = 5 },
				new RoleGradeCompetence {Id = 68, CompetenceId = "B6 ", CompetenceLevelId = 39, RoleGradeId = 5 },
				new RoleGradeCompetence {Id = 69, CompetenceId = "B5 ", CompetenceLevelId = 38, RoleGradeId = 5 },
				new RoleGradeCompetence {Id = 70, CompetenceId = "B4 ", CompetenceLevelId = 35, RoleGradeId = 5 },
				new RoleGradeCompetence {Id = 71, CompetenceId = "B3 ", CompetenceLevelId = 30, RoleGradeId = 5 },
				new RoleGradeCompetence {Id = 72, CompetenceId = "B2 ", CompetenceLevelId = 28, RoleGradeId = 5 },
				new RoleGradeCompetence {Id = 73, CompetenceId = "B1 ", CompetenceLevelId = 25, RoleGradeId = 5 },
				new RoleGradeCompetence {Id = 74, CompetenceId = "A9 ", CompetenceLevelId = 21, RoleGradeId = 5 },
				new RoleGradeCompetence {Id = 75, CompetenceId = "A7 ", CompetenceLevelId = 18, RoleGradeId = 5 },
				new RoleGradeCompetence {Id = 76, CompetenceId = "A6 ", CompetenceLevelId = 16, RoleGradeId = 5 },
				new RoleGradeCompetence {Id = 77, CompetenceId = "A5 ", CompetenceLevelId = 12, RoleGradeId = 5 },
				new RoleGradeCompetence {Id = 78, CompetenceId = "E5 ", CompetenceLevelId = 94, RoleGradeId = 5 },
				new RoleGradeCompetence {Id = 79, CompetenceId = "E8 ", CompetenceLevelId = 102, RoleGradeId = 5 },
				new RoleGradeCompetence {Id = 83, CompetenceId = "C1 ", CompetenceLevelId = 41, RoleGradeId = 1 },
				new RoleGradeCompetence {Id = 84, CompetenceId = "B2 ", CompetenceLevelId = 26, RoleGradeId = 1 },
				new RoleGradeCompetence {Id = 85, CompetenceId = "B3 ", CompetenceLevelId = 29, RoleGradeId = 1 },
				new RoleGradeCompetence {Id = 86, CompetenceId = "B5 ", CompetenceLevelId = 36, RoleGradeId = 1 },

            };
            context.RoleGradeCompetence.AddRange(items);

            context.SaveChanges();
			context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [RoleGradeCompetence] OFF");
            trans.Commit();
            return true;
        }
    }
}
