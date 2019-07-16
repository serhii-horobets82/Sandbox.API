using Evoflare.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using System.Globalization;


namespace Evoflare.API.Data
{
    public static partial class DbInitializer
    {
    
		public static bool SeedEcfEmployeeEvaluation(EvoflareDbContext context)
        {
            if (context.EcfEmployeeEvaluation.Any()) return false;
            var trans = context.Database.BeginTransaction();
            context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [EcfEmployeeEvaluation] ON");

            var items = new[]
            {
				new EcfEmployeeEvaluation {Id = 1, EvaluationId = 3, EvaluatorId = 6, StartDate = DateTime.ParseExact("2019-03-02T13:53:39.6700000", "O", CultureInfo.InvariantCulture), StartById = 11, EndDate = null, EndById = null, OrganizationId = 1 },
				new EcfEmployeeEvaluation {Id = 2, EvaluationId = 4, EvaluatorId = 6, StartDate = DateTime.ParseExact("2019-03-03T11:29:11.2670000", "O", CultureInfo.InvariantCulture), StartById = 11, EndDate = null, EndById = null, OrganizationId = 1 },
				new EcfEmployeeEvaluation {Id = 3, EvaluationId = 5, EvaluatorId = 7, StartDate = DateTime.ParseExact("2019-03-05T17:32:01.6370000", "O", CultureInfo.InvariantCulture), StartById = 11, EndDate = null, EndById = null, OrganizationId = 1 },
				new EcfEmployeeEvaluation {Id = 4, EvaluationId = 9, EvaluatorId = 6, StartDate = DateTime.ParseExact("2019-03-12T20:56:01.1400000", "O", CultureInfo.InvariantCulture), StartById = 11, EndDate = null, EndById = null, OrganizationId = 1 },

            };
            context.EcfEmployeeEvaluation.AddRange(items);

            context.SaveChanges();
			context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [EcfEmployeeEvaluation] OFF");
            trans.Commit();
            return true;
        }
    }
}
