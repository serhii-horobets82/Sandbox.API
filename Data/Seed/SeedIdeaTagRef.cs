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
    
		public static bool SeedIdeaTagRef(EvoflareDbContext context)
        {
            if (context.IdeaTagRef.Any()) return false;
            IDbContextTransaction trans = null;
            
			if(true && context.Database.IsSqlServer())
			{
			    trans = context.Database.BeginTransaction();
                context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [IdeaTagRef] ON");
			}
            var items = new[]
            {
				new IdeaTagRef {Id = 2, IdeaId = 4, TagId = 7 },
				new IdeaTagRef {Id = 3, IdeaId = 5, TagId = 8 },
				new IdeaTagRef {Id = 4, IdeaId = 5, TagId = 9 },

            };
            context.IdeaTagRef.AddRange(items);

            context.SaveChanges();

			if(true && context.Database.IsSqlServer())
            {
				context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [IdeaTagRef] OFF");
				trans.Commit();
			}
            
            return true;
        }
    }
}
