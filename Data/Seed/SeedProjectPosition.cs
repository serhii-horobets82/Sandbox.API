using Evoflare.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using System.Globalization;


namespace Evoflare.API.Data
{
    public static partial class DbInitializer
    {
    
		public static bool SeedProjectPosition(EvoflareDbContext context)
        {
            if (context.ProjectPosition.Any()) return false;
            var trans = context.Database.BeginTransaction();
            context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [ProjectPosition] ON");

            var items = new[]
            {
				new ProjectPosition {Id = 1, ProjectId = 1, CareerPathId = 1, RoleGradeId = 2, OrganizationId = 1 },
				new ProjectPosition {Id = 2, ProjectId = 1, CareerPathId = 1, RoleGradeId = 3, OrganizationId = 1 },
				new ProjectPosition {Id = 3, ProjectId = 1, CareerPathId = 1, RoleGradeId = 4, OrganizationId = 1 },

            };
            context.ProjectPosition.AddRange(items);

            context.SaveChanges();
			context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [ProjectPosition] OFF");
            trans.Commit();
            return true;
        }
    }
}
