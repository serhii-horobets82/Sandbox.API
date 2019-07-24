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
    
		public static bool SeedEmployeeType(EvoflareDbContext context)
        {
            if (context.EmployeeType.Any()) return false;
            IDbContextTransaction trans = null;
            
			if(true && context.Database.IsSqlServer())
			{
			    trans = context.Database.BeginTransaction();
                context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [EmployeeType] ON");
			}
            var items = new[]
            {
				new EmployeeType {Id = 1, Type = @"MANAGER", OrganizationId = 1 },
				new EmployeeType {Id = 2, Type = @"DEV", OrganizationId = 1 },
				new EmployeeType {Id = 3, Type = @"QA", OrganizationId = 1 },
				new EmployeeType {Id = 4, Type = @"AUTOMATION", OrganizationId = 1 },
				new EmployeeType {Id = 5, Type = @"DEVOPS", OrganizationId = 1 },
				new EmployeeType {Id = 6, Type = @"OPERATIONS", OrganizationId = 1 },
				new EmployeeType {Id = 8, Type = @"Performance Engineer", OrganizationId = 1 },
				new EmployeeType {Id = 9, Type = @"Another role", OrganizationId = 1 },

            };
            context.EmployeeType.AddRange(items);

            context.SaveChanges();

			if(true && context.Database.IsSqlServer())
            {
				context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [EmployeeType] OFF");
				trans.Commit();
			}
            
            return true;
        }
    }
}
