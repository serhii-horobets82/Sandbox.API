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
    
		public static bool SeedProjectCareerPath(EvoflareDbContext context)
        {
            if (context.ProjectCareerPath.Any()) return false;
            IDbContextTransaction trans = null;
            
			if(true && context.Database.IsSqlServer())
			{
			    trans = context.Database.BeginTransaction();
                context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [ProjectCareerPath] ON");
			}
            var items = new[]
            {
				new ProjectCareerPath {Id = 1, ProjectId = 1, Name = @"Mainenance developer", RoleId = 2, TeamId = null, OrganizationId = 1 },
				new ProjectCareerPath {Id = 2, ProjectId = 1, Name = @"QA", RoleId = 3, TeamId = null, OrganizationId = 1 },

            };
            context.ProjectCareerPath.AddRange(items);

            context.SaveChanges();

			if(true && context.Database.IsSqlServer())
            {
				context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [ProjectCareerPath] OFF");
				trans.Commit();
			}
            
            return true;
        }
    }
}
