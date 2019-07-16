using Evoflare.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using System.Globalization;


namespace Evoflare.API.Data
{
    public static partial class DbInitializer
    {
    
		public static bool SeedEmployee(EvoflareDbContext context)
        {
            if (context.Employee.Any()) return false;
            var trans = context.Database.BeginTransaction();
            context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [Employee] ON");

            var items = new[]
            {
				new Employee {Id = 1, UserId = null, IsManager = true, EmployeeTypeId = 1, OrganizationId = 1, NameTemp = "John Manager", HiringDate = DateTime.ParseExact("2010-02-01T00:00:00.0000000", "O", CultureInfo.InvariantCulture), Name = "John", Surname = "Manager" },
				new Employee {Id = 2, UserId = null, IsManager = true, EmployeeTypeId = 1, OrganizationId = 1, NameTemp = "Bob Manager", HiringDate = DateTime.ParseExact("2011-12-01T00:00:00.0000000", "O", CultureInfo.InvariantCulture), Name = "Bob", Surname = "Manager" },
				new Employee {Id = 3, UserId = null, IsManager = false, EmployeeTypeId = 3, OrganizationId = 1, NameTemp = "Karl QA", HiringDate = DateTime.ParseExact("2017-11-21T00:00:00.0000000", "O", CultureInfo.InvariantCulture), Name = "Karl", Surname = "QA" },
				new Employee {Id = 4, UserId = null, IsManager = false, EmployeeTypeId = 4, OrganizationId = 1, NameTemp = "Marta AutoQA", HiringDate = DateTime.ParseExact("2010-02-02T00:00:00.0000000", "O", CultureInfo.InvariantCulture), Name = "Marta", Surname = "AutoQA" },
				new Employee {Id = 6, UserId = null, IsManager = false, EmployeeTypeId = 2, OrganizationId = 1, NameTemp = "Linus Developer", HiringDate = DateTime.ParseExact("2010-02-03T00:00:00.0000000", "O", CultureInfo.InvariantCulture), Name = "Linus", Surname = "Developer" },
				new Employee {Id = 7, UserId = null, IsManager = false, EmployeeTypeId = 2, OrganizationId = 1, NameTemp = "Mark Developer", HiringDate = DateTime.ParseExact("2010-04-04T00:00:00.0000000", "O", CultureInfo.InvariantCulture), Name = "Mark", Surname = "Developer" },
				new Employee {Id = 10, UserId = null, IsManager = true, EmployeeTypeId = 1, OrganizationId = 1, NameTemp = "Petra Manager", HiringDate = DateTime.ParseExact("2010-05-05T00:00:00.0000000", "O", CultureInfo.InvariantCulture), Name = "Petra", Surname = "Manager" },
				new Employee {Id = 11, UserId = null, IsManager = true, EmployeeTypeId = 1, OrganizationId = 1, NameTemp = "Barak MEGA Manager", HiringDate = DateTime.ParseExact("2010-02-11T00:00:00.0000000", "O", CultureInfo.InvariantCulture), Name = "Barak", Surname = "MEGA Manager" },
				new Employee {Id = 12, UserId = null, IsManager = false, EmployeeTypeId = 3, OrganizationId = 1, NameTemp = "Tapak QA", HiringDate = DateTime.ParseExact("2010-01-01T00:00:00.0000000", "O", CultureInfo.InvariantCulture), Name = "Tapak", Surname = "QA" },
				new Employee {Id = 13, UserId = null, IsManager = false, EmployeeTypeId = 3, OrganizationId = 1, NameTemp = "Mikki QA", HiringDate = DateTime.ParseExact("2010-04-15T00:00:00.0000000", "O", CultureInfo.InvariantCulture), Name = "Mikki", Surname = "QA" },
				new Employee {Id = 15, UserId = null, IsManager = false, EmployeeTypeId = 4, OrganizationId = 1, NameTemp = "Billy AutoQA", HiringDate = DateTime.ParseExact("2010-05-01T00:00:00.0000000", "O", CultureInfo.InvariantCulture), Name = "Billy", Surname = "AutoQA" },
				new Employee {Id = 16, UserId = null, IsManager = false, EmployeeTypeId = 2, OrganizationId = 1, NameTemp = "Todd Developer", HiringDate = DateTime.ParseExact("2010-06-03T00:00:00.0000000", "O", CultureInfo.InvariantCulture), Name = "Todd", Surname = "Developer" },
				new Employee {Id = 17, UserId = null, IsManager = false, EmployeeTypeId = 2, OrganizationId = 1, NameTemp = "Riana Developer", HiringDate = DateTime.ParseExact("2010-07-04T00:00:00.0000000", "O", CultureInfo.InvariantCulture), Name = "Riana", Surname = "Developer" },
				new Employee {Id = 18, UserId = null, IsManager = false, EmployeeTypeId = 2, OrganizationId = 1, NameTemp = "Mila Developer", HiringDate = DateTime.ParseExact("2010-12-05T00:00:00.0000000", "O", CultureInfo.InvariantCulture), Name = "Mila", Surname = "Developer" },
				new Employee {Id = 21, UserId = null, IsManager = false, EmployeeTypeId = 2, OrganizationId = 1, NameTemp = "Alex Took", HiringDate = DateTime.ParseExact("2010-05-17T00:00:00.0000000", "O", CultureInfo.InvariantCulture), Name = "Alex", Surname = "Took" },

            };
            context.Employee.AddRange(items);

            context.SaveChanges();
			context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [Employee] OFF");
            trans.Commit();
            return true;
        }
    }
}
