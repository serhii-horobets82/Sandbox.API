using Evoflare.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using System.Globalization;


namespace Evoflare.API.Data
{
    public static partial class DbInitializer
    {
    
		public static bool SeedEcfEvaluationResult(EvoflareDbContext context)
        {
            if (context.EcfEvaluationResult.Any()) return false;
            var trans = context.Database.BeginTransaction();
			try
            {
                if(context.Database.IsSqlServer())
                    context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [EcfEvaluationResult] ON");
			}
            catch { trans.Rollback(); } // TODO find better solution 

            var items = new[]
            {
				new EcfEvaluationResult {Id = 1, EvaluationId = 1, Competence = "B1 ", CompetenceLevel = 2, Date = null },
				new EcfEvaluationResult {Id = 2, EvaluationId = 1, Competence = "B2 ", CompetenceLevel = 3, Date = null },
				new EcfEvaluationResult {Id = 3, EvaluationId = 1, Competence = "B3 ", CompetenceLevel = 4, Date = null },
				new EcfEvaluationResult {Id = 4, EvaluationId = 1, Competence = "B5 ", CompetenceLevel = 1, Date = null },
				new EcfEvaluationResult {Id = 5, EvaluationId = 1, Competence = "C4 ", CompetenceLevel = null, Date = null },
				new EcfEvaluationResult {Id = 6, EvaluationId = 1, Competence = "A6 ", CompetenceLevel = null, Date = null },
				new EcfEvaluationResult {Id = 7, EvaluationId = 1, Competence = "B4 ", CompetenceLevel = 2, Date = DateTime.ParseExact("2019-04-26T09:35:05.6030000", "O", CultureInfo.InvariantCulture) },
				new EcfEvaluationResult {Id = 8, EvaluationId = 1, Competence = "E8 ", CompetenceLevel = null, Date = null },
				new EcfEvaluationResult {Id = 9, EvaluationId = 1, Competence = "C1 ", CompetenceLevel = 3, Date = DateTime.ParseExact("2019-04-26T15:21:04.4230000", "O", CultureInfo.InvariantCulture) },
				new EcfEvaluationResult {Id = 10, EvaluationId = 1, Competence = "C2 ", CompetenceLevel = 2, Date = DateTime.ParseExact("2019-04-26T15:21:05.6630000", "O", CultureInfo.InvariantCulture) },
				new EcfEvaluationResult {Id = 11, EvaluationId = 1, Competence = "C3 ", CompetenceLevel = null, Date = null },
				new EcfEvaluationResult {Id = 12, EvaluationId = 1, Competence = "E3 ", CompetenceLevel = null, Date = null },
				new EcfEvaluationResult {Id = 13, EvaluationId = 1, Competence = "E6 ", CompetenceLevel = null, Date = null },
				new EcfEvaluationResult {Id = 14, EvaluationId = 3, Competence = "A6 ", CompetenceLevel = 2, Date = null },
				new EcfEvaluationResult {Id = 15, EvaluationId = 3, Competence = "B2 ", CompetenceLevel = 4, Date = null },
				new EcfEvaluationResult {Id = 16, EvaluationId = 3, Competence = "B4 ", CompetenceLevel = null, Date = null },
				new EcfEvaluationResult {Id = 17, EvaluationId = 3, Competence = "C4 ", CompetenceLevel = null, Date = null },
				new EcfEvaluationResult {Id = 18, EvaluationId = 3, Competence = "E8 ", CompetenceLevel = null, Date = null },
				new EcfEvaluationResult {Id = 19, EvaluationId = 3, Competence = "C2 ", CompetenceLevel = null, Date = null },
				new EcfEvaluationResult {Id = 20, EvaluationId = 3, Competence = "C3 ", CompetenceLevel = null, Date = null },
				new EcfEvaluationResult {Id = 21, EvaluationId = 3, Competence = "E3 ", CompetenceLevel = null, Date = null },
				new EcfEvaluationResult {Id = 22, EvaluationId = 3, Competence = "E6 ", CompetenceLevel = null, Date = null },
				new EcfEvaluationResult {Id = 23, EvaluationId = 3, Competence = "B3 ", CompetenceLevel = null, Date = null },
				new EcfEvaluationResult {Id = 24, EvaluationId = 3, Competence = "B5 ", CompetenceLevel = null, Date = null },
				new EcfEvaluationResult {Id = 49, EvaluationId = 4, Competence = "B1 ", CompetenceLevel = null, Date = null },
				new EcfEvaluationResult {Id = 50, EvaluationId = 4, Competence = "B2 ", CompetenceLevel = null, Date = null },
				new EcfEvaluationResult {Id = 51, EvaluationId = 4, Competence = "B3 ", CompetenceLevel = null, Date = null },
				new EcfEvaluationResult {Id = 52, EvaluationId = 4, Competence = "B5 ", CompetenceLevel = null, Date = null },
				new EcfEvaluationResult {Id = 53, EvaluationId = 4, Competence = "C4 ", CompetenceLevel = null, Date = null },
				new EcfEvaluationResult {Id = 54, EvaluationId = 4, Competence = "C1 ", CompetenceLevel = null, Date = null },
				new EcfEvaluationResult {Id = 55, EvaluationId = 4, Competence = "C2 ", CompetenceLevel = null, Date = null },
				new EcfEvaluationResult {Id = 56, EvaluationId = 4, Competence = "C3 ", CompetenceLevel = null, Date = null },

            };
            context.EcfEvaluationResult.AddRange(items);

            context.SaveChanges();

			try
            {
				if(context.Database.IsSqlServer())
					context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [EcfEvaluationResult] OFF");
				trans.Commit();
			}
            catch { } // TODO find better solution 
            
            return true;
        }
    }
}
