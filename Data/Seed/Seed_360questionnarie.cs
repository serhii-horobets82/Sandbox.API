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
    
		public static bool Seed_360questionnarie(EvoflareDbContext context)
        {
            if (context._360questionnarie.Any()) return false;
            IDbContextTransaction trans = null;
            
			if(true && context.Database.IsSqlServer())
			{
			    trans = context.Database.BeginTransaction();
                context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [360questionnarie] ON");
			}
            var items = new[]
            {
				new _360questionnarie {Id = 2, Name = @"Question 1", IsForManager = false },
				new _360questionnarie {Id = 3, Name = @"Question 2", IsForManager = false },
				new _360questionnarie {Id = 4, Name = @"Question 3", IsForManager = false },

            };
            context._360questionnarie.AddRange(items);

            context.SaveChanges();

			if(true && context.Database.IsSqlServer())
            {
				context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [360questionnarie] OFF");
				trans.Commit();
			}
            
            return true;
        }
    }
}
