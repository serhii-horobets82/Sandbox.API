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
    
		public static bool SeedEmployeeRelations(EvoflareDbContext context)
        {
            if (context.EmployeeRelations.Any()) return false;
            IDbContextTransaction trans = null;
            
			if(true && context.Database.IsSqlServer())
			{
			    trans = context.Database.BeginTransaction();
                context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [EmployeeRelations] ON");
			}
            var items = new[]
            {
				new EmployeeRelations {Id = 1, EmployeeId = null, ManagerId = 1, TeamId = 1, ProjectId = 1, PositionId = null, OrganizationId = 1, Archived = false },
				new EmployeeRelations {Id = 2, EmployeeId = 6, ManagerId = null, TeamId = 1, ProjectId = 1, PositionId = null, OrganizationId = 1, Archived = false },
				new EmployeeRelations {Id = 3, EmployeeId = 21, ManagerId = null, TeamId = 4, ProjectId = 1, PositionId = 2, OrganizationId = 1, Archived = false },
				new EmployeeRelations {Id = 4, EmployeeId = 12, ManagerId = null, TeamId = 4, ProjectId = 1, PositionId = 1, OrganizationId = 1, Archived = false },
				new EmployeeRelations {Id = 5, EmployeeId = null, ManagerId = 10, TeamId = 5, ProjectId = 2, PositionId = null, OrganizationId = 1, Archived = false },
				new EmployeeRelations {Id = 6, EmployeeId = 18, ManagerId = null, TeamId = 5, ProjectId = 2, PositionId = 2, OrganizationId = 1, Archived = false },
				new EmployeeRelations {Id = 7, EmployeeId = null, ManagerId = 2, TeamId = 6, ProjectId = 1, PositionId = null, OrganizationId = 1, Archived = false },
				new EmployeeRelations {Id = 8, EmployeeId = 16, ManagerId = null, TeamId = 6, ProjectId = 1, PositionId = null, OrganizationId = 1, Archived = false },
				new EmployeeRelations {Id = 9, EmployeeId = 21, ManagerId = null, TeamId = 6, ProjectId = 1, PositionId = null, OrganizationId = 1, Archived = false },
				new EmployeeRelations {Id = 10, EmployeeId = 18, ManagerId = null, TeamId = 6, ProjectId = 1, PositionId = null, OrganizationId = 1, Archived = false },
				new EmployeeRelations {Id = 11, EmployeeId = 12, ManagerId = null, TeamId = 6, ProjectId = 1, PositionId = null, OrganizationId = 1, Archived = false },
				new EmployeeRelations {Id = 12, EmployeeId = null, ManagerId = 2, TeamId = 7, ProjectId = 1, PositionId = null, OrganizationId = 1, Archived = false },
				new EmployeeRelations {Id = 13, EmployeeId = 12, ManagerId = null, TeamId = 7, ProjectId = 1, PositionId = null, OrganizationId = 1, Archived = false },
				new EmployeeRelations {Id = 14, EmployeeId = 16, ManagerId = null, TeamId = 7, ProjectId = 1, PositionId = 3, OrganizationId = 1, Archived = false },
				new EmployeeRelations {Id = 15, EmployeeId = 18, ManagerId = null, TeamId = 7, ProjectId = 1, PositionId = 2, OrganizationId = 1, Archived = false },
				new EmployeeRelations {Id = 16, EmployeeId = 21, ManagerId = null, TeamId = 7, ProjectId = 1, PositionId = 3, OrganizationId = 1, Archived = false },
				new EmployeeRelations {Id = 17, EmployeeId = null, ManagerId = 2, TeamId = 8, ProjectId = 1, PositionId = null, OrganizationId = 1, Archived = false },
				new EmployeeRelations {Id = 18, EmployeeId = 7, ManagerId = null, TeamId = 8, ProjectId = 1, PositionId = 2, OrganizationId = 1, Archived = false },
				new EmployeeRelations {Id = 19, EmployeeId = 13, ManagerId = null, TeamId = 8, ProjectId = 1, PositionId = null, OrganizationId = 1, Archived = false },
				new EmployeeRelations {Id = 20, EmployeeId = 17, ManagerId = null, TeamId = 8, ProjectId = 1, PositionId = 3, OrganizationId = 1, Archived = false },
				new EmployeeRelations {Id = 21, EmployeeId = 21, ManagerId = null, TeamId = 8, ProjectId = 1, PositionId = 2, OrganizationId = 1, Archived = false },
				new EmployeeRelations {Id = 22, EmployeeId = null, ManagerId = 1, TeamId = null, ProjectId = 1, PositionId = null, OrganizationId = 1, Archived = false },
				new EmployeeRelations {Id = 23, EmployeeId = null, ManagerId = 10, TeamId = null, ProjectId = 2, PositionId = null, OrganizationId = 1, Archived = false },
				new EmployeeRelations {Id = 25, EmployeeId = null, ManagerId = 2, TeamId = null, ProjectId = 3, PositionId = null, OrganizationId = 1, Archived = false },
				new EmployeeRelations {Id = 26, EmployeeId = null, ManagerId = 1, TeamId = null, ProjectId = 4, PositionId = null, OrganizationId = 1, Archived = false },
				new EmployeeRelations {Id = 27, EmployeeId = null, ManagerId = 10, TeamId = null, ProjectId = 5, PositionId = null, OrganizationId = 1, Archived = false },
				new EmployeeRelations {Id = 28, EmployeeId = null, ManagerId = 1, TeamId = null, ProjectId = 6, PositionId = null, OrganizationId = 1, Archived = false },
				new EmployeeRelations {Id = 29, EmployeeId = null, ManagerId = 10, TeamId = null, ProjectId = 8, PositionId = null, OrganizationId = 1, Archived = false },
				new EmployeeRelations {Id = 31, EmployeeId = 3, ManagerId = null, TeamId = 10, ProjectId = 4, PositionId = null, OrganizationId = 1, Archived = false },
				new EmployeeRelations {Id = 32, EmployeeId = 4, ManagerId = null, TeamId = 10, ProjectId = 4, PositionId = null, OrganizationId = 1, Archived = false },
				new EmployeeRelations {Id = 33, EmployeeId = 6, ManagerId = null, TeamId = 10, ProjectId = 4, PositionId = null, OrganizationId = 1, Archived = false },
				new EmployeeRelations {Id = 34, EmployeeId = 7, ManagerId = null, TeamId = 10, ProjectId = 4, PositionId = null, OrganizationId = 1, Archived = false },
				new EmployeeRelations {Id = 35, EmployeeId = 16, ManagerId = null, TeamId = 10, ProjectId = 4, PositionId = null, OrganizationId = 1, Archived = false },
				new EmployeeRelations {Id = 36, EmployeeId = 17, ManagerId = null, TeamId = 10, ProjectId = 4, PositionId = null, OrganizationId = 1, Archived = false },

				new EmployeeRelations {Id = 37, EmployeeId = null, ManagerId = 24, TeamId = 5, ProjectId = 2, PositionId = null, OrganizationId = 1, Archived = false },
				new EmployeeRelations {Id = 38, EmployeeId = null, ManagerId = 24, TeamId = null, ProjectId = 2, PositionId = null, OrganizationId = 1, Archived = false },	
            };
            context.EmployeeRelations.AddRange(items);

            context.SaveChanges();

			if(true && context.Database.IsSqlServer())
            {
				context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [EmployeeRelations] OFF");
				trans.Commit();
			}
            
            return true;
        }
    }
}
