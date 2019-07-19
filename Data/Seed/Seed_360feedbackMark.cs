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
    
		public static bool Seed_360feedbackMark(EvoflareDbContext context)
        {
            if (context._360feedbackMark.Any()) return false;
            IDbContextTransaction trans = null;
            
			if(true && context.Database.IsSqlServer())
			{
			    trans = context.Database.BeginTransaction();
                context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [360feedbackMark] ON");
			}
            var items = new[]
            {
				new _360feedbackMark {Id = 1, Mark = 1, Title = @"Far below expectations" },
				new _360feedbackMark {Id = 2, Mark = 2, Title = @"Below expectations" },
				new _360feedbackMark {Id = 3, Mark = 3, Title = @"Meets expectations" },
				new _360feedbackMark {Id = 4, Mark = 4, Title = @"Above expectations" },
				new _360feedbackMark {Id = 5, Mark = 5, Title = @"Far above expectations" },

            };
            context._360feedbackMark.AddRange(items);

            context.SaveChanges();

			if(true && context.Database.IsSqlServer())
            {
				context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [360feedbackMark] OFF");
				trans.Commit();
			}
            
            return true;
        }
    }
}
