using Evoflare.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using System.Globalization;


namespace Evoflare.API.Data
{
    public static partial class DbInitializer
    {
    
		public static bool SeedEmployeeRelations(EvoflareDbContext context)
        {
            if (context.EmployeeRelations.Any()) return false;
            var trans = context.Database.BeginTransaction();
			try
            {
				context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [EmployeeRelations] ON");
			}
            catch { trans.Rollback(); } // TODO find better solution 

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

            };
            context.EmployeeRelations.AddRange(items);

            context.SaveChanges();

			try
            {
				context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [EmployeeRelations] OFF");
				trans.Commit();
			}
            catch { } // TODO find better solution 
            
            return true;
        }
    }
}
