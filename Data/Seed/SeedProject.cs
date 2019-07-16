using Evoflare.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using System.Globalization;


namespace Evoflare.API.Data
{
    public static partial class DbInitializer
    {
    
		public static bool SeedProject(EvoflareDbContext context)
        {
            if (context.Project.Any()) return false;
            var trans = context.Database.BeginTransaction();
            context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [Project] ON");

            var items = new[]
            {
				new Project {Id = 1, Name = "Smart product 1", OrganizationId = 1 },
				new Project {Id = 2, Name = "Usual product 1", OrganizationId = 1 },
				new Project {Id = 3, Name = "Karandash", OrganizationId = 1 },

            };
            context.Project.AddRange(items);

            context.SaveChanges();
			context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [Project] OFF");
            trans.Commit();
            return true;
        }
    }
}
