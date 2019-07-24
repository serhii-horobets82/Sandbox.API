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
    
		public static bool SeedTeam(EvoflareDbContext context)
        {
            if (context.Team.Any()) return false;
            IDbContextTransaction trans = null;
            
			if(true && context.Database.IsSqlServer())
			{
			    trans = context.Database.BeginTransaction();
                context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [Team] ON");
			}
            var items = new[]
            {
				new Team {Id = 1, Name = @"Smart team 1", ProjectId = 1, OrganizationId = 1 },
				new Team {Id = 2, Name = @"Smart team 2", ProjectId = 1, OrganizationId = 1 },
				new Team {Id = 3, Name = @"Usual team 1", ProjectId = 2, OrganizationId = 1 },
				new Team {Id = 4, Name = @"First ui team", ProjectId = 1, OrganizationId = 1 },
				new Team {Id = 5, Name = @"Second ui team", ProjectId = 2, OrganizationId = 1 },
				new Team {Id = 6, Name = @"Another cool team", ProjectId = 1, OrganizationId = 1 },
				new Team {Id = 7, Name = @"Another cool team 2", ProjectId = 1, OrganizationId = 1 },
				new Team {Id = 8, Name = @"Mainenance team", ProjectId = 1, OrganizationId = 1 },

            };
            context.Team.AddRange(items);

            context.SaveChanges();

			if(true && context.Database.IsSqlServer())
            {
				context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [Team] OFF");
				trans.Commit();
			}
            
            return true;
        }
    }
}
