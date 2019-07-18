using Evoflare.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using System.Globalization;


namespace Evoflare.API.Data
{
    public static partial class DbInitializer
    {
    
		public static bool SeedTeam(EvoflareDbContext context)
        {
            if (context.Team.Any()) return false;
            var trans = context.Database.BeginTransaction();
			try
            {
                if(context.Database.IsSqlServer())
                    context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [Team] ON");
			}
            catch { trans.Rollback(); } // TODO find better solution 

            var items = new[]
            {
				new Team {Id = 1, Name = "Smart team 1", ProjectId = 1, OrganizationId = 1 },
				new Team {Id = 2, Name = "Smart team 2", ProjectId = 1, OrganizationId = 1 },
				new Team {Id = 3, Name = "Usual team 1", ProjectId = 2, OrganizationId = 1 },
				new Team {Id = 4, Name = "First ui team", ProjectId = 1, OrganizationId = 1 },
				new Team {Id = 5, Name = "Second ui team", ProjectId = 2, OrganizationId = 1 },
				new Team {Id = 6, Name = "Another cool team", ProjectId = 1, OrganizationId = 1 },
				new Team {Id = 7, Name = "Another cool team 2", ProjectId = 1, OrganizationId = 1 },
				new Team {Id = 8, Name = "Mainenance team", ProjectId = 1, OrganizationId = 1 },

            };
            context.Team.AddRange(items);

            context.SaveChanges();

			try
            {
				if(context.Database.IsSqlServer())
					context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [Team] OFF");
				trans.Commit();
			}
            catch { } // TODO find better solution 
            
            return true;
        }
    }
}
