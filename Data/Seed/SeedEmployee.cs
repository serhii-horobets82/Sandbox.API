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
    
		public static bool SeedEmployee(EvoflareDbContext context)
        {
            if (context.Employee.Any()) return false;
            IDbContextTransaction trans = null;
            
			if(true && context.Database.IsSqlServer())
			{
			    trans = context.Database.BeginTransaction();
                context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [Employee] ON");
			}
            var items = new[]
            {
				new Employee {Id = 1, UserId = @"d91a5edf-31d4-471c-91e0-a1426e33fe73", IsManager = true, EmployeeTypeId = 1, OrganizationId = 1, NameTemp = @"Manager John", HiringDate = DateTime.ParseExact("2010-02-01T00:00:00.0000000", "O", CultureInfo.InvariantCulture), Name = @"John", Surname = @"Manager" },
				new Employee {Id = 2, UserId = @"a156a0b8-62f1-443e-a341-be1ff040d7ed", IsManager = true, EmployeeTypeId = 1, OrganizationId = 1, NameTemp = @"Manager Bob", HiringDate = DateTime.ParseExact("2011-12-01T00:00:00.0000000", "O", CultureInfo.InvariantCulture), Name = @"Bob", Surname = @"Manager" },
				new Employee {Id = 3, UserId = @"a6c25630-171b-4c43-891c-29ec301ebcf9", IsManager = false, EmployeeTypeId = 3, OrganizationId = 1, NameTemp = @"QA Karl", HiringDate = DateTime.ParseExact("2017-11-21T00:00:00.0000000", "O", CultureInfo.InvariantCulture), Name = @"Karl", Surname = @"QA" },
				new Employee {Id = 4, UserId = @"df372ad6-cb7a-4261-a9ac-75465ae51f37", IsManager = false, EmployeeTypeId = 4, OrganizationId = 1, NameTemp = @"AutoQA Marta", HiringDate = DateTime.ParseExact("2010-02-02T00:00:00.0000000", "O", CultureInfo.InvariantCulture), Name = @"Marta", Surname = @"AutoQA" },
				new Employee {Id = 6, UserId = @"3a719e65-a777-413b-b45e-e40d2e80dbf9", IsManager = false, EmployeeTypeId = 2, OrganizationId = 1, NameTemp = @"Developer Linus", HiringDate = DateTime.ParseExact("2010-02-03T00:00:00.0000000", "O", CultureInfo.InvariantCulture), Name = @"Linus", Surname = @"Developer" },
				new Employee {Id = 7, UserId = @"2677b447-5ca2-4778-a220-1491b2246700", IsManager = false, EmployeeTypeId = 2, OrganizationId = 1, NameTemp = @"Developer Mark", HiringDate = DateTime.ParseExact("2010-04-04T00:00:00.0000000", "O", CultureInfo.InvariantCulture), Name = @"Mark", Surname = @"Developer" },
				new Employee {Id = 10, UserId = @"e3ef731c-9ec9-4a32-8ea4-7da464ae8840", IsManager = true, EmployeeTypeId = 1, OrganizationId = 1, NameTemp = @"Manager Petra", HiringDate = DateTime.ParseExact("2010-05-05T00:00:00.0000000", "O", CultureInfo.InvariantCulture), Name = @"Petra", Surname = @"Manager" },
				new Employee {Id = 11, UserId = @"9f98f523-1ca3-43ae-b52c-92ffe1bb4685", IsManager = true, EmployeeTypeId = 1, OrganizationId = 1, NameTemp = @"MEGA Manager Barak", HiringDate = DateTime.ParseExact("2010-02-11T00:00:00.0000000", "O", CultureInfo.InvariantCulture), Name = @"Barak", Surname = @"MEGA Manager" },
				new Employee {Id = 12, UserId = @"b78b8000-01b1-47bc-a470-2e054c512249", IsManager = false, EmployeeTypeId = 3, OrganizationId = 1, NameTemp = @"QA Tapak", HiringDate = DateTime.ParseExact("2010-01-01T00:00:00.0000000", "O", CultureInfo.InvariantCulture), Name = @"Tapak", Surname = @"QA" },
				new Employee {Id = 13, UserId = @"1f9d9168-70f4-4e40-b89c-2bd6f9a6d126", IsManager = false, EmployeeTypeId = 3, OrganizationId = 1, NameTemp = @"QA Mikki", HiringDate = DateTime.ParseExact("2010-04-15T00:00:00.0000000", "O", CultureInfo.InvariantCulture), Name = @"Mikki", Surname = @"QA" },
				new Employee {Id = 15, UserId = @"4f444878-c404-4797-94f7-1d5ec8b7ee14", IsManager = false, EmployeeTypeId = 4, OrganizationId = 1, NameTemp = @"AutoQA Billy", HiringDate = DateTime.ParseExact("2010-05-01T00:00:00.0000000", "O", CultureInfo.InvariantCulture), Name = @"Billy", Surname = @"AutoQA" },
				new Employee {Id = 16, UserId = @"238b8729-ccb3-4e90-911e-ccc377fc5e51", IsManager = false, EmployeeTypeId = 2, OrganizationId = 1, NameTemp = @"Developer Todd", HiringDate = DateTime.ParseExact("2010-06-03T00:00:00.0000000", "O", CultureInfo.InvariantCulture), Name = @"Todd", Surname = @"Developer" },
				new Employee {Id = 17, UserId = @"c47ebe5b-993c-41cf-bc6f-e71f37a1df03", IsManager = false, EmployeeTypeId = 2, OrganizationId = 1, NameTemp = @"Developer Riana", HiringDate = DateTime.ParseExact("2010-07-04T00:00:00.0000000", "O", CultureInfo.InvariantCulture), Name = @"Riana", Surname = @"Developer" },
				new Employee {Id = 18, UserId = @"2361580b-2a8d-470d-b90e-c65a0a5bfc13", IsManager = false, EmployeeTypeId = 2, OrganizationId = 1, NameTemp = @"Developer Mila", HiringDate = DateTime.ParseExact("2010-12-05T00:00:00.0000000", "O", CultureInfo.InvariantCulture), Name = @"Mila", Surname = @"Developer" },
				new Employee {Id = 21, UserId = @"7bad83e3-30f7-4729-9fec-8b702cc706f1", IsManager = false, EmployeeTypeId = 2, OrganizationId = 1, NameTemp = @"Took Alex", HiringDate = DateTime.ParseExact("2010-05-17T00:00:00.0000000", "O", CultureInfo.InvariantCulture), Name = @"Alex", Surname = @"Took" },
				new Employee {Id = 22, UserId = @"b1ae0dc1-1323-4af0-8d9e-8254eecfeff6", IsManager = false, EmployeeTypeId = 10, OrganizationId = 1, NameTemp = @"Admin System", HiringDate = DateTime.ParseExact("2010-01-01T00:00:00.0000000", "O", CultureInfo.InvariantCulture), Name = @"System", Surname = @"Admin" },
				new Employee {Id = 23, UserId = @"6fac6ac2-c226-47c7-a6b8-753ed67f7fae", IsManager = false, EmployeeTypeId = 11, OrganizationId = 1, NameTemp = @"Admin Regular", HiringDate = DateTime.ParseExact("2010-01-01T00:00:00.0000000", "O", CultureInfo.InvariantCulture), Name = @"Regular", Surname = @"Admin" },
				new Employee {Id = 24, UserId = @"f89109d4-0216-444f-8c6e-c56e8c2fcb21", IsManager = false, EmployeeTypeId = 1, OrganizationId = 1, NameTemp = @"Manager Chief", HiringDate = DateTime.ParseExact("2010-01-01T00:00:00.0000000", "O", CultureInfo.InvariantCulture), Name = @"Chief", Surname = @"Manager" },
				new Employee {Id = 25, UserId = @"8b493138-5e91-4893-9c25-f0c006ad8f53", IsManager = false, EmployeeTypeId = 1, OrganizationId = 1, NameTemp = @"Manager Regular", HiringDate = DateTime.ParseExact("2010-01-01T00:00:00.0000000", "O", CultureInfo.InvariantCulture), Name = @"Regular", Surname = @"Manager" },
				new Employee {Id = 26, UserId = @"1161f18b-ce54-44fb-a042-119fc75dbc50", IsManager = false, EmployeeTypeId = 12, OrganizationId = 1, NameTemp = @"HR Chief", HiringDate = DateTime.ParseExact("2010-01-01T00:00:00.0000000", "O", CultureInfo.InvariantCulture), Name = @"Chief", Surname = @"HR" },
				new Employee {Id = 27, UserId = @"afdd760d-d2aa-412f-a368-57ad67f88c47", IsManager = false, EmployeeTypeId = 12, OrganizationId = 1, NameTemp = @"HR Regular", HiringDate = DateTime.ParseExact("2010-01-01T00:00:00.0000000", "O", CultureInfo.InvariantCulture), Name = @"Regular", Surname = @"HR" },

            };
            context.Employee.AddRange(items);

            context.SaveChanges();

			if(true && context.Database.IsSqlServer())
            {
				context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [Employee] OFF");
				trans.Commit();
			}
            
            return true;
        }
    }
}
