using Evoflare.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using System.Globalization;


namespace Evoflare.API.Data
{
    public static partial class DbInitializer
    {
    
		public static bool SeedCustomerContact(EvoflareDbContext context)
        {
            if (context.CustomerContact.Any()) return false;
            var trans = context.Database.BeginTransaction();
            context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [CustomerContact] ON");

            var items = new[]
            {
				new CustomerContact {Id = 2, Name = "Name namin", Email = "mail@mail.com", Phone = "+242342343", ProjectId = 3, OrganizationId = 1 },

            };
            context.CustomerContact.AddRange(items);

            context.SaveChanges();
			context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [CustomerContact] OFF");
            trans.Commit();
            return true;
        }
    }
}