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
    
		public static bool SeedEmployeeEvaluation(EvoflareDbContext context)
        {
            if (context.EmployeeEvaluation.Any()) return false;
            IDbContextTransaction trans = null;
            
			if(true && context.Database.IsSqlServer())
			{
			    trans = context.Database.BeginTransaction();
                context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [EmployeeEvaluation] ON");
			}
            var items = new[]
            {
				new EmployeeEvaluation {Id = 3, EmployeeId = 21, StartDate = DateTime.ParseExact("2019-03-02T13:53:39.6700000", "O", CultureInfo.InvariantCulture), StartedById = 11, EndDate = null, EndedById = null, OrganizationId = 1, Archived = false },
				new EmployeeEvaluation {Id = 4, EmployeeId = 7, StartDate = DateTime.ParseExact("2019-03-03T11:29:11.2670000", "O", CultureInfo.InvariantCulture), StartedById = 11, EndDate = null, EndedById = null, OrganizationId = 1, Archived = false },
				new EmployeeEvaluation {Id = 5, EmployeeId = 17, StartDate = DateTime.ParseExact("2019-03-05T17:32:01.6370000", "O", CultureInfo.InvariantCulture), StartedById = 11, EndDate = null, EndedById = null, OrganizationId = 1, Archived = false },
				new EmployeeEvaluation {Id = 9, EmployeeId = 18, StartDate = DateTime.ParseExact("2019-03-12T20:56:01.1400000", "O", CultureInfo.InvariantCulture), StartedById = 11, EndDate = null, EndedById = null, OrganizationId = 1, Archived = false },

            };
            context.EmployeeEvaluation.AddRange(items);

            context.SaveChanges();

			if(true && context.Database.IsSqlServer())
            {
				context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [EmployeeEvaluation] OFF");
				trans.Commit();
			}
            
            return true;
        }
    }
}
