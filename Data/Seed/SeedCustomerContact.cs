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
    
		public static bool SeedCustomerContact(EvoflareDbContext context)
        {
            if (context.CustomerContact.Any()) return false;
            IDbContextTransaction trans = null;
            
			if(true && context.Database.IsSqlServer())
			{
			    trans = context.Database.BeginTransaction();
                context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [CustomerContact] ON");
			}
            var items = new[]
            {
				new CustomerContact {Id = 2, Name = @"Name namin", Email = @"mail@mail.com", Phone = @"+242342343", ProjectId = 3, OrganizationId = 1 },

            };
            context.CustomerContact.AddRange(items);

            context.SaveChanges();

			if(true && context.Database.IsSqlServer())
            {
				context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [CustomerContact] OFF");
				trans.Commit();
			}
            
            return true;
        }
    }
}
