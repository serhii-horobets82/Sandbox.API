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
    
		public static bool SeedIdeaLike(EvoflareDbContext context)
        {
            if (context.IdeaLike.Any()) return false;
            IDbContextTransaction trans = null;
            
			if(true && context.Database.IsSqlServer())
			{
			    trans = context.Database.BeginTransaction();
                context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [IdeaLike] ON");
			}
            var items = new[]
            {
				new IdeaLike {Id = 3, IdeaId = 1, EmployeeId = 1 },

            };
            context.IdeaLike.AddRange(items);

            context.SaveChanges();

			if(true && context.Database.IsSqlServer())
            {
				context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [IdeaLike] OFF");
				trans.Commit();
			}
            
            return true;
        }
    }
}
