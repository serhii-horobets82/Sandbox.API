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
    
		public static bool SeedProject(EvoflareDbContext context)
        {
            if (context.Project.Any()) return false;
            IDbContextTransaction trans = null;
            
			if(true && context.Database.IsSqlServer())
			{
			    trans = context.Database.BeginTransaction();
                context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [Project] ON");
			}
            var items = new[]
            {
				new Project {Id = 1, Name = @"Smart product 1", CreatedDate = DateTime.ParseExact("2019-02-01T00:00:00.0000000", "O", CultureInfo.InvariantCulture), OrganizationId = 1 },
				new Project {Id = 2, Name = @"Usual product 1", CreatedDate = DateTime.ParseExact("2019-02-01T00:00:00.0000000", "O", CultureInfo.InvariantCulture), OrganizationId = 1 },
				new Project {Id = 3, Name = @"Karandash", CreatedDate = DateTime.ParseExact("2019-02-01T00:00:00.0000000", "O", CultureInfo.InvariantCulture), OrganizationId = 1 },
				new Project {Id = 4, Name = @"Rapid carrot", CreatedDate = DateTime.ParseExact("2019-08-05T20:41:58.7330000", "O", CultureInfo.InvariantCulture), OrganizationId = 1 },
				new Project {Id = 5, Name = @"Yellow submarine", CreatedDate = DateTime.ParseExact("2019-08-05T20:44:12.0200000", "O", CultureInfo.InvariantCulture), OrganizationId = 1 },
				new Project {Id = 6, Name = @"Pumpkin insanity", CreatedDate = DateTime.ParseExact("2019-08-05T21:22:17.6470000", "O", CultureInfo.InvariantCulture), OrganizationId = 1 },
				new Project {Id = 7, Name = @"Vicious potato", CreatedDate = DateTime.ParseExact("2019-08-05T21:26:55.9770000", "O", CultureInfo.InvariantCulture), OrganizationId = 1 },
				new Project {Id = 8, Name = @"Crucified banana", CreatedDate = DateTime.ParseExact("2019-08-05T21:28:25.7030000", "O", CultureInfo.InvariantCulture), OrganizationId = 1 },
				new Project {Id = 9, Name = @"Immortal lemongrass", CreatedDate = DateTime.ParseExact("2019-08-05T21:29:00.8970000", "O", CultureInfo.InvariantCulture), OrganizationId = 1 },

            };
            context.Project.AddRange(items);

            context.SaveChanges();

			if(true && context.Database.IsSqlServer())
            {
				context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [Project] OFF");
				trans.Commit();
			}
            
            return true;
        }
    }
}
