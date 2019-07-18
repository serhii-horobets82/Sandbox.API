using Evoflare.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using System.Globalization;


namespace Evoflare.API.Data
{
    public static partial class DbInitializer
    {
    
		public static bool SeedEmployeeType(EvoflareDbContext context)
        {
            if (context.EmployeeType.Any()) return false;
            var trans = context.Database.BeginTransaction();
			try
            {
                if(context.Database.IsSqlServer())
                    context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [EmployeeType] ON");
			}
            catch { trans.Rollback(); } // TODO find better solution 

            var items = new[]
            {
				new EmployeeType {Id = 1, Type = "MANAGER", OrganizationId = 1 },
				new EmployeeType {Id = 2, Type = "DEV", OrganizationId = 1 },
				new EmployeeType {Id = 3, Type = "QA", OrganizationId = 1 },
				new EmployeeType {Id = 4, Type = "AUTOMATION", OrganizationId = 1 },
				new EmployeeType {Id = 5, Type = "DEVOPS", OrganizationId = 1 },
				new EmployeeType {Id = 6, Type = "OPERATIONS", OrganizationId = 1 },
				new EmployeeType {Id = 8, Type = "Performance Engineer", OrganizationId = 1 },
				new EmployeeType {Id = 9, Type = "Another role", OrganizationId = 1 },

            };
            context.EmployeeType.AddRange(items);

            context.SaveChanges();

			try
            {
				if(context.Database.IsSqlServer())
					context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [EmployeeType] OFF");
				trans.Commit();
			}
            catch { } // TODO find better solution 
            
            return true;
        }
    }
}
