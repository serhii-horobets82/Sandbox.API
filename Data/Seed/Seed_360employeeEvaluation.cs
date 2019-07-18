using Evoflare.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using System.Globalization;


namespace Evoflare.API.Data
{
    public static partial class DbInitializer
    {
    
		public static bool Seed_360employeeEvaluation(EvoflareDbContext context)
        {
            if (context._360employeeEvaluation.Any()) return false;
            var trans = context.Database.BeginTransaction();
			try
            {
                if(context.Database.IsSqlServer())
                    context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [360employeeEvaluation] ON");
			}
            catch { trans.Rollback(); } // TODO find better solution 

            var items = new[]
            {
				new _360employeeEvaluation {Id = 10, EvaluatorEmployeeId = 10, EvaluationId = 9, StartDate = DateTime.ParseExact("2019-03-12T20:56:06.9570000", "O", CultureInfo.InvariantCulture), EndDate = null, OrganizationId = 1, _360feedbackGroupId = 2, StartDoing = null, StopDoing = null, OtherComments = null },
				new _360employeeEvaluation {Id = 11, EvaluatorEmployeeId = 2, EvaluationId = 9, StartDate = DateTime.ParseExact("2019-03-12T20:56:06.9570000", "O", CultureInfo.InvariantCulture), EndDate = DateTime.ParseExact("2019-04-11T06:12:17.8230000", "O", CultureInfo.InvariantCulture), OrganizationId = 1, _360feedbackGroupId = 2, StartDoing = "Start doing things", StopDoing = "Stop doing smth", OtherComments = "Some comment" },
				new _360employeeEvaluation {Id = 12, EvaluatorEmployeeId = 16, EvaluationId = 9, StartDate = DateTime.ParseExact("2019-03-12T20:56:06.9570000", "O", CultureInfo.InvariantCulture), EndDate = null, OrganizationId = 1, _360feedbackGroupId = 2, StartDoing = null, StopDoing = null, OtherComments = null },
				new _360employeeEvaluation {Id = 13, EvaluatorEmployeeId = 21, EvaluationId = 9, StartDate = DateTime.ParseExact("2019-03-12T20:56:06.9570000", "O", CultureInfo.InvariantCulture), EndDate = null, OrganizationId = 1, _360feedbackGroupId = 2, StartDoing = null, StopDoing = null, OtherComments = null },
				new _360employeeEvaluation {Id = 14, EvaluatorEmployeeId = 12, EvaluationId = 9, StartDate = DateTime.ParseExact("2019-03-12T20:56:06.9570000", "O", CultureInfo.InvariantCulture), EndDate = null, OrganizationId = 1, _360feedbackGroupId = 2, StartDoing = null, StopDoing = null, OtherComments = null },

            };
            context._360employeeEvaluation.AddRange(items);

            context.SaveChanges();

			try
            {
				if(context.Database.IsSqlServer())
					context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [360employeeEvaluation] OFF");
				trans.Commit();
			}
            catch { } // TODO find better solution 
            
            return true;
        }
    }
}
