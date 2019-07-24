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
    
		public static bool SeedProjectPosition(EvoflareDbContext context)
        {
            if (context.ProjectPosition.Any()) return false;
            IDbContextTransaction trans = null;
            
			if(true && context.Database.IsSqlServer())
			{
			    trans = context.Database.BeginTransaction();
                context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [ProjectPosition] ON");
			}
            var items = new[]
            {
				new ProjectPosition {Id = 1, ProjectId = 1, CareerPathId = 1, RoleGradeId = 2, OrganizationId = 1 },
				new ProjectPosition {Id = 2, ProjectId = 1, CareerPathId = 1, RoleGradeId = 3, OrganizationId = 1 },
				new ProjectPosition {Id = 3, ProjectId = 1, CareerPathId = 1, RoleGradeId = 4, OrganizationId = 1 },

            };
            context.ProjectPosition.AddRange(items);

            context.SaveChanges();

			if(true && context.Database.IsSqlServer())
            {
				context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [ProjectPosition] OFF");
				trans.Commit();
			}
            
            return true;
        }
    }
}
