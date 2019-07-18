using Evoflare.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using System.Globalization;


namespace Evoflare.API.Data
{
    public static partial class DbInitializer
    {
    
		public static bool SeedProjectCareerPath(EvoflareDbContext context)
        {
            if (context.ProjectCareerPath.Any()) return false;
            var trans = context.Database.BeginTransaction();
			try
            {
                if(context.Database.IsSqlServer())
                    context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [ProjectCareerPath] ON");
			}
            catch { trans.Rollback(); } // TODO find better solution 

            var items = new[]
            {
				new ProjectCareerPath {Id = 1, ProjectId = 1, Name = "Mainenance developer", RoleId = 2, TeamId = null, OrganizationId = 1 },
				new ProjectCareerPath {Id = 2, ProjectId = 1, Name = "QA", RoleId = 3, TeamId = null, OrganizationId = 1 },

            };
            context.ProjectCareerPath.AddRange(items);

            context.SaveChanges();

			try
            {
				if(context.Database.IsSqlServer())
					context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [ProjectCareerPath] OFF");
				trans.Commit();
			}
            catch { } // TODO find better solution 
            
            return true;
        }
    }
}
