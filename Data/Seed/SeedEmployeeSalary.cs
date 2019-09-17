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
    
		public static bool SeedEmployeeSalary(EvoflareDbContext context)
        {
            if (context.EmployeeSalary.Any()) return false;
            IDbContextTransaction trans = null;
            
			if(true && context.Database.IsSqlServer())
			{
			    trans = context.Database.BeginTransaction();
                context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [EmployeeSalary] ON");
			}
            var items = new[]
            {
				new EmployeeSalary {Id = 1, EmployeeId = 1, Period = DateTime.ParseExact("2010-01-01T00:00:00.0000000", "O", CultureInfo.InvariantCulture), Basic = 1000, Bonus = 0, Archived = false },
				new EmployeeSalary {Id = 2, EmployeeId = 2, Period = DateTime.ParseExact("2012-02-01T00:00:00.0000000", "O", CultureInfo.InvariantCulture), Basic = 2500, Bonus = 0, Archived = false },
                new EmployeeSalary {Id = 3, EmployeeId = 3, Period = DateTime.ParseExact("2012-02-01T00:00:00.0000000", "O", CultureInfo.InvariantCulture), Basic = 1000, Bonus = 0, Archived = false },
                new EmployeeSalary {Id = 4, EmployeeId = 4, Period = DateTime.ParseExact("2014-02-01T00:00:00.0000000", "O", CultureInfo.InvariantCulture), Basic = 3000, Bonus = 10, Archived = false },
                new EmployeeSalary {Id = 5, EmployeeId = 6, Period = DateTime.ParseExact("2013-02-01T00:00:00.0000000", "O", CultureInfo.InvariantCulture), Basic = 3000, Bonus = 200, Archived = false },
                new EmployeeSalary {Id = 6, EmployeeId = 7, Period = DateTime.ParseExact("2010-02-01T00:00:00.0000000", "O", CultureInfo.InvariantCulture), Basic = 1000, Bonus = 0, Archived = false },
                new EmployeeSalary {Id = 7, EmployeeId = 10, Period = DateTime.ParseExact("2010-02-01T00:00:00.0000000", "O", CultureInfo.InvariantCulture), Basic = 1000, Bonus = 100, Archived = false },
                new EmployeeSalary {Id = 8, EmployeeId = 11, Period = DateTime.ParseExact("2010-02-01T00:00:00.0000000", "O", CultureInfo.InvariantCulture), Basic = 1000, Bonus = 100, Archived = false },
                new EmployeeSalary {Id = 9, EmployeeId = 12, Period = DateTime.ParseExact("2010-02-01T00:00:00.0000000", "O", CultureInfo.InvariantCulture), Basic = 1000, Bonus = 5, Archived = false },
                new EmployeeSalary {Id = 10, EmployeeId = 13, Period = DateTime.ParseExact("2010-02-01T00:00:00.0000000", "O", CultureInfo.InvariantCulture), Basic = 3000, Bonus = 0, Archived = false },
                new EmployeeSalary {Id = 11, EmployeeId = 15, Period = DateTime.ParseExact("2010-02-01T00:00:00.0000000", "O", CultureInfo.InvariantCulture), Basic = 4000, Bonus = 30, Archived = false },
                new EmployeeSalary {Id = 12, EmployeeId = 16, Period = DateTime.ParseExact("2010-02-01T00:00:00.0000000", "O", CultureInfo.InvariantCulture), Basic = 1000, Bonus = 40, Archived = false },
                new EmployeeSalary {Id = 13, EmployeeId = 17, Period = DateTime.ParseExact("2010-02-01T00:00:00.0000000", "O", CultureInfo.InvariantCulture), Basic = 5000, Bonus = 10, Archived = false },
                new EmployeeSalary {Id = 14, EmployeeId = 18, Period = DateTime.ParseExact("2010-02-01T00:00:00.0000000", "O", CultureInfo.InvariantCulture), Basic = 1000, Bonus = 0, Archived = false },
                new EmployeeSalary {Id = 15, EmployeeId = 21, Period = DateTime.ParseExact("2010-02-01T00:00:00.0000000", "O", CultureInfo.InvariantCulture), Basic = 1000, Bonus = 300, Archived = false },
                new EmployeeSalary {Id = 16, EmployeeId = 22, Period = DateTime.ParseExact("2010-02-01T00:00:00.0000000", "O", CultureInfo.InvariantCulture), Basic = 1000, Bonus = 400, Archived = false },
                new EmployeeSalary {Id = 17, EmployeeId = 23, Period = DateTime.ParseExact("2010-02-01T00:00:00.0000000", "O", CultureInfo.InvariantCulture), Basic = 1000, Bonus = 0, Archived = false },
                new EmployeeSalary {Id = 18, EmployeeId = 24, Period = DateTime.ParseExact("2010-02-01T00:00:00.0000000", "O", CultureInfo.InvariantCulture), Basic = 2000, Bonus = 0, Archived = false },
                new EmployeeSalary {Id = 19, EmployeeId = 25, Period = DateTime.ParseExact("2010-02-01T00:00:00.0000000", "O", CultureInfo.InvariantCulture), Basic = 3000, Bonus = 0, Archived = false },
                new EmployeeSalary {Id = 20, EmployeeId = 26, Period = DateTime.ParseExact("2010-02-01T00:00:00.0000000", "O", CultureInfo.InvariantCulture), Basic = 4000, Bonus = 0, Archived = false },
                new EmployeeSalary {Id = 21, EmployeeId = 27, Period = DateTime.ParseExact("2010-02-01T00:00:00.0000000", "O", CultureInfo.InvariantCulture), Basic = 1500, Bonus = 0, Archived = false },

            };
            context.EmployeeSalary.AddRange(items);

            context.SaveChanges();

			if(true && context.Database.IsSqlServer())
            {
				context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [EmployeeSalary] OFF");
				trans.Commit();
			}
            
            return true;
        }
    }
}
