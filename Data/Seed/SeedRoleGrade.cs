using Evoflare.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using System.Globalization;


namespace Evoflare.API.Data
{
    public static partial class DbInitializer
    {
    
		public static bool SeedRoleGrade(EvoflareDbContext context)
        {
            if (context.RoleGrade.Any()) return false;
            var trans = context.Database.BeginTransaction();
            context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [RoleGrade] ON");

            var items = new[]
            {
				new RoleGrade {Id = 1, EmployeeTypeId = 2, Name = "Intern Developer", Order = 1, OrganizationId = 1 },
				new RoleGrade {Id = 2, EmployeeTypeId = 2, Name = "Junior Developer", Order = 2, OrganizationId = 1 },
				new RoleGrade {Id = 3, EmployeeTypeId = 2, Name = "Middle Developer", Order = 3, OrganizationId = 1 },
				new RoleGrade {Id = 4, EmployeeTypeId = 2, Name = "Senior Developer", Order = 4, OrganizationId = 1 },
				new RoleGrade {Id = 5, EmployeeTypeId = 2, Name = "Tech/Team Lead", Order = 5, OrganizationId = 1 },
				new RoleGrade {Id = 6, EmployeeTypeId = 2, Name = "Architect", Order = 6, OrganizationId = 1 },
				new RoleGrade {Id = 8, EmployeeTypeId = 3, Name = "Intern QA", Order = 1, OrganizationId = 1 },
				new RoleGrade {Id = 9, EmployeeTypeId = 3, Name = "Junior QA", Order = 2, OrganizationId = 1 },
				new RoleGrade {Id = 10, EmployeeTypeId = 3, Name = "Middle QA", Order = 3, OrganizationId = 1 },
				new RoleGrade {Id = 11, EmployeeTypeId = 3, Name = "Serior QA", Order = 4, OrganizationId = 1 },
				new RoleGrade {Id = 12, EmployeeTypeId = 8, Name = "Junior Performance Engineer", Order = 1, OrganizationId = 1 },
				new RoleGrade {Id = 13, EmployeeTypeId = 8, Name = "Middle Performance Engineer", Order = 2, OrganizationId = 1 },
				new RoleGrade {Id = 14, EmployeeTypeId = 9, Name = "Some grade", Order = 1, OrganizationId = 1 },
				new RoleGrade {Id = 15, EmployeeTypeId = 9, Name = "Some other grade", Order = 2, OrganizationId = 1 },

            };
            context.RoleGrade.AddRange(items);

            context.SaveChanges();
			context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [RoleGrade] OFF");
            trans.Commit();
            return true;
        }
    }
}
