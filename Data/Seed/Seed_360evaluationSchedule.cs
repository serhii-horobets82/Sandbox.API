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
    
		public static bool Seed_360evaluationSchedule(EvoflareDbContext context)
        {
            if (context._360evaluationSchedule.Any()) return false;
            IDbContextTransaction trans = null;
            
			if(true && context.Database.IsSqlServer())
			{
			    trans = context.Database.BeginTransaction();
                context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [360evaluationSchedule] ON");
			}
            var items = new[]
            {
				new _360evaluationSchedule {Id = 1, PeriodMonths = 3, EvaluationWindowMonths = 1, StartDate = DateTime.ParseExact("2019-01-01T00:00:00.0000000", "O", CultureInfo.InvariantCulture) },

            };
            context._360evaluationSchedule.AddRange(items);

            context.SaveChanges();

			if(true && context.Database.IsSqlServer())
            {
				context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [360evaluationSchedule] OFF");
				trans.Commit();
			}
            
            return true;
        }
    }
}
