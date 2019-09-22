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
    
		public static bool SeedProjectPositionCompetence(EvoflareDbContext context)
        {
            if (context.ProjectPositionCompetence.Any()) return false;
            IDbContextTransaction trans = null;
            
			if(true && context.Database.IsSqlServer())
			{
			    trans = context.Database.BeginTransaction();
                context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [ProjectPositionCompetence] ON");
			}
            var items = new[]
            {
				new ProjectPositionCompetence {Id = 1, RoleGradeId = 2, ProjectPositionId = 1, CompetenceId = 6, CompetenceLevelId = 14 },
				new ProjectPositionCompetence {Id = 2, RoleGradeId = 2, ProjectPositionId = 1, CompetenceId = 10, CompetenceLevelId = 23 },
				new ProjectPositionCompetence {Id = 3, RoleGradeId = 2, ProjectPositionId = 1, CompetenceId = 11, CompetenceLevelId = 26 },
				new ProjectPositionCompetence {Id = 4, RoleGradeId = 2, ProjectPositionId = 1, CompetenceId = 12, CompetenceLevelId = 29 },
				new ProjectPositionCompetence {Id = 5, RoleGradeId = 2, ProjectPositionId = 1, CompetenceId = 13, CompetenceLevelId = 33 },
				new ProjectPositionCompetence {Id = 6, RoleGradeId = 2, ProjectPositionId = 1, CompetenceId = 14, CompetenceLevelId = 36 },
				new ProjectPositionCompetence {Id = 7, RoleGradeId = 2, ProjectPositionId = 1, CompetenceId = 16, CompetenceLevelId = 41 },
				new ProjectPositionCompetence {Id = 8, RoleGradeId = 3, ProjectPositionId = 2, CompetenceId = 5, CompetenceLevelId = 11 },
				new ProjectPositionCompetence {Id = 9, RoleGradeId = 3, ProjectPositionId = 2, CompetenceId = 6, CompetenceLevelId = 15 },
				new ProjectPositionCompetence {Id = 10, RoleGradeId = 3, ProjectPositionId = 2, CompetenceId = 10, CompetenceLevelId = 24 },
				new ProjectPositionCompetence {Id = 11, RoleGradeId = 3, ProjectPositionId = 2, CompetenceId = 11, CompetenceLevelId = 27 },
				new ProjectPositionCompetence {Id = 12, RoleGradeId = 3, ProjectPositionId = 2, CompetenceId = 12, CompetenceLevelId = 30 },
				new ProjectPositionCompetence {Id = 13, RoleGradeId = 3, ProjectPositionId = 2, CompetenceId = 13, CompetenceLevelId = 34 },
				new ProjectPositionCompetence {Id = 14, RoleGradeId = 3, ProjectPositionId = 2, CompetenceId = 14, CompetenceLevelId = 37 },
				new ProjectPositionCompetence {Id = 15, RoleGradeId = 3, ProjectPositionId = 2, CompetenceId = 16, CompetenceLevelId = 41 },
				new ProjectPositionCompetence {Id = 16, RoleGradeId = 3, ProjectPositionId = 2, CompetenceId = 19, CompetenceLevelId = 49 },
				new ProjectPositionCompetence {Id = 17, RoleGradeId = 4, ProjectPositionId = 3, CompetenceId = 4, CompetenceLevelId = 8 },
				new ProjectPositionCompetence {Id = 18, RoleGradeId = 4, ProjectPositionId = 3, CompetenceId = 5, CompetenceLevelId = 12 },
				new ProjectPositionCompetence {Id = 19, RoleGradeId = 4, ProjectPositionId = 3, CompetenceId = 6, CompetenceLevelId = 16 },
				new ProjectPositionCompetence {Id = 20, RoleGradeId = 4, ProjectPositionId = 3, CompetenceId = 7, CompetenceLevelId = 17 },
				new ProjectPositionCompetence {Id = 21, RoleGradeId = 4, ProjectPositionId = 3, CompetenceId = 10, CompetenceLevelId = 25 },
				new ProjectPositionCompetence {Id = 22, RoleGradeId = 4, ProjectPositionId = 3, CompetenceId = 11, CompetenceLevelId = 28 },
				new ProjectPositionCompetence {Id = 23, RoleGradeId = 4, ProjectPositionId = 3, CompetenceId = 12, CompetenceLevelId = 30 },
				new ProjectPositionCompetence {Id = 24, RoleGradeId = 4, ProjectPositionId = 3, CompetenceId = 13, CompetenceLevelId = 34 },
				new ProjectPositionCompetence {Id = 25, RoleGradeId = 4, ProjectPositionId = 3, CompetenceId = 14, CompetenceLevelId = 38 },
				new ProjectPositionCompetence {Id = 26, RoleGradeId = 4, ProjectPositionId = 3, CompetenceId = 16, CompetenceLevelId = 42 },
				new ProjectPositionCompetence {Id = 27, RoleGradeId = 4, ProjectPositionId = 3, CompetenceId = 18, CompetenceLevelId = 46 },
				new ProjectPositionCompetence {Id = 28, RoleGradeId = 4, ProjectPositionId = 3, CompetenceId = 19, CompetenceLevelId = 49 },

            };
            context.ProjectPositionCompetence.AddRange(items);

            context.SaveChanges();

			if(true && context.Database.IsSqlServer())
            {
				context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [ProjectPositionCompetence] OFF");
				trans.Commit();
			}
            
            return true;
        }
    }
}
