using Evoflare.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using System.Globalization;


namespace Evoflare.API.Data
{
    public static partial class DbInitializer
    {
    
		public static bool SeedOrganization(EvoflareDbContext context)
        {
            if (context.Organization.Any()) return false;
            var trans = context.Database.BeginTransaction();
			try
            {
				context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [Organization] ON");
			}
            catch { trans.Rollback(); } // TODO find better solution 

            var items = new[]
            {
				new Organization {Id = 1, Name = "Smart CORP" },

            };
            context.Organization.AddRange(items);

            context.SaveChanges();

			try
            {
				context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [Organization] OFF");
				trans.Commit();
			}
            catch { } // TODO find better solution 
            
            return true;
        }
    }
}
