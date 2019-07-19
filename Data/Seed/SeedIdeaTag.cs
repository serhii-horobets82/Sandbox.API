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
    
		public static bool SeedIdeaTag(EvoflareDbContext context)
        {
            if (context.IdeaTag.Any()) return false;
            IDbContextTransaction trans = null;
            
			if(true && context.Database.IsSqlServer())
			{
			    trans = context.Database.BeginTransaction();
                context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [IdeaTag] ON");
			}
            var items = new[]
            {
				new IdeaTag {Id = 1, Name = @"office" },
				new IdeaTag {Id = 2, Name = @"lunch" },
				new IdeaTag {Id = 3, Name = @"business" },
				new IdeaTag {Id = 4, Name = @"tea" },
				new IdeaTag {Id = 5, Name = @"ice" },
				new IdeaTag {Id = 6, Name = @"office" },
				new IdeaTag {Id = 7, Name = @"office" },
				new IdeaTag {Id = 8, Name = @"table" },
				new IdeaTag {Id = 9, Name = @"trees" },

            };
            context.IdeaTag.AddRange(items);

            context.SaveChanges();

			if(true && context.Database.IsSqlServer())
            {
				context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [IdeaTag] OFF");
				trans.Commit();
			}
            
            return true;
        }
    }
}
