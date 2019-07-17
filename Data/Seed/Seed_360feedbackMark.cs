using Evoflare.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using System.Globalization;


namespace Evoflare.API.Data
{
    public static partial class DbInitializer
    {
    
		public static bool Seed_360feedbackMark(EvoflareDbContext context)
        {
            if (context._360feedbackMark.Any()) return false;
            var trans = context.Database.BeginTransaction();
			try
            {
				context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [360feedbackMark] ON");
			}
            catch { trans.Rollback(); } // TODO find better solution 

            var items = new[]
            {
				new _360feedbackMark {Id = 1, Mark = 1, Title = "Far below expectations" },
				new _360feedbackMark {Id = 2, Mark = 2, Title = "Below expectations" },
				new _360feedbackMark {Id = 3, Mark = 3, Title = "Meets expectations" },
				new _360feedbackMark {Id = 4, Mark = 4, Title = "Above expectations" },
				new _360feedbackMark {Id = 5, Mark = 5, Title = "Far above expectations" },

            };
            context._360feedbackMark.AddRange(items);

            context.SaveChanges();

			try
            {
				context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [360feedbackMark] OFF");
				trans.Commit();
			}
            catch { } // TODO find better solution 
            
            return true;
        }
    }
}
