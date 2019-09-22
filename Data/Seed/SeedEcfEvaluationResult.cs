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
		public static bool SeedEcfEvaluationResult(EvoflareDbContext context)
        {
            if (context.EcfEvaluationResult.Any()) return false;
            IDbContextTransaction trans = null;
            
			if(true && context.Database.IsSqlServer())
			{
			    trans = context.Database.BeginTransaction();
                context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [EcfEvaluationResult] ON");
			}
            var items = new[]
            {
				new EcfEvaluationResult {Id = 1, EvaluationId = 1, Competence = 10, CompetenceLevel = 2, Date = null },
				new EcfEvaluationResult {Id = 2, EvaluationId = 1, Competence = 11, CompetenceLevel = 3, Date = null },
				new EcfEvaluationResult {Id = 3, EvaluationId = 1, Competence = 12, CompetenceLevel = 4, Date = null },
				new EcfEvaluationResult {Id = 4, EvaluationId = 1, Competence = 14, CompetenceLevel = 1, Date = null },
				new EcfEvaluationResult {Id = 5, EvaluationId = 1, Competence = 19, CompetenceLevel = null, Date = null },
				new EcfEvaluationResult {Id = 6, EvaluationId = 1, Competence = 6, CompetenceLevel = null, Date = null },
				new EcfEvaluationResult {Id = 7, EvaluationId = 1, Competence = 13, CompetenceLevel = 2, Date = DateTime.ParseExact("2019-04-26T09:35:05.6030000", "O", CultureInfo.InvariantCulture) },
				new EcfEvaluationResult {Id = 8, EvaluationId = 1, Competence = 39, CompetenceLevel = null, Date = null },
				new EcfEvaluationResult {Id = 9, EvaluationId = 1, Competence = 16, CompetenceLevel = 3, Date = DateTime.ParseExact("2019-04-26T15:21:04.4230000", "O", CultureInfo.InvariantCulture) },
				new EcfEvaluationResult {Id = 10, EvaluationId = 1, Competence = 17, CompetenceLevel = 2, Date = DateTime.ParseExact("2019-04-26T15:21:05.6630000", "O", CultureInfo.InvariantCulture) },
				new EcfEvaluationResult {Id = 11, EvaluationId = 1, Competence = 18, CompetenceLevel = null, Date = null },
				new EcfEvaluationResult {Id = 12, EvaluationId = 1, Competence = 34, CompetenceLevel = null, Date = null },
				new EcfEvaluationResult {Id = 13, EvaluationId = 1, Competence = 37, CompetenceLevel = null, Date = null },
				new EcfEvaluationResult {Id = 14, EvaluationId = 3, Competence = 6, CompetenceLevel = 2, Date = null },
				new EcfEvaluationResult {Id = 15, EvaluationId = 3, Competence = 11, CompetenceLevel = 4, Date = null },
				new EcfEvaluationResult {Id = 16, EvaluationId = 3, Competence = 13, CompetenceLevel = null, Date = null },
				new EcfEvaluationResult {Id = 17, EvaluationId = 3, Competence = 19, CompetenceLevel = null, Date = null },
				new EcfEvaluationResult {Id = 18, EvaluationId = 3, Competence = 39, CompetenceLevel = null, Date = null },
				new EcfEvaluationResult {Id = 19, EvaluationId = 3, Competence = 17, CompetenceLevel = null, Date = null },
				new EcfEvaluationResult {Id = 20, EvaluationId = 3, Competence = 18, CompetenceLevel = null, Date = null },
				new EcfEvaluationResult {Id = 21, EvaluationId = 3, Competence = 34, CompetenceLevel = null, Date = null },
				new EcfEvaluationResult {Id = 22, EvaluationId = 3, Competence = 37, CompetenceLevel = null, Date = null },
				new EcfEvaluationResult {Id = 23, EvaluationId = 3, Competence = 12, CompetenceLevel = null, Date = null },
				new EcfEvaluationResult {Id = 24, EvaluationId = 3, Competence = 14, CompetenceLevel = null, Date = null },
				new EcfEvaluationResult {Id = 49, EvaluationId = 4, Competence = 10, CompetenceLevel = null, Date = null },
				new EcfEvaluationResult {Id = 50, EvaluationId = 4, Competence = 11, CompetenceLevel = null, Date = null },
				new EcfEvaluationResult {Id = 51, EvaluationId = 4, Competence = 12, CompetenceLevel = null, Date = null },
				new EcfEvaluationResult {Id = 52, EvaluationId = 4, Competence = 14, CompetenceLevel = null, Date = null },
				new EcfEvaluationResult {Id = 53, EvaluationId = 4, Competence = 19, CompetenceLevel = null, Date = null },
				new EcfEvaluationResult {Id = 54, EvaluationId = 4, Competence = 16, CompetenceLevel = null, Date = null },
				new EcfEvaluationResult {Id = 55, EvaluationId = 4, Competence = 17, CompetenceLevel = null, Date = null },
				new EcfEvaluationResult {Id = 56, EvaluationId = 4, Competence = 18, CompetenceLevel = null, Date = null },

            };
            context.EcfEvaluationResult.AddRange(items);

            context.SaveChanges();

			if(true && context.Database.IsSqlServer())
            {
				context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [EcfEvaluationResult] OFF");
				trans.Commit();
			}
            
            return true;
        }
    }
}
