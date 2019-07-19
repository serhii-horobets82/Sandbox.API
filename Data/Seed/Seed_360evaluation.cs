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
    
		public static bool Seed_360evaluation(EvoflareDbContext context)
        {
            if (context._360evaluation.Any()) return false;
            IDbContextTransaction trans = null;
            
			if(true && context.Database.IsSqlServer())
			{
			    trans = context.Database.BeginTransaction();
                context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [360evaluation] ON");
			}
            var items = new[]
            {
				new _360evaluation {Id = 2, EvaluationId = 11, QuestionId = 2, FeedbackMarkId = 5, OrganizationId = 1 },
				new _360evaluation {Id = 3, EvaluationId = 11, QuestionId = 9, FeedbackMarkId = 4, OrganizationId = 1 },
				new _360evaluation {Id = 4, EvaluationId = 11, QuestionId = 10, FeedbackMarkId = 4, OrganizationId = 1 },
				new _360evaluation {Id = 5, EvaluationId = 11, QuestionId = 11, FeedbackMarkId = 5, OrganizationId = 1 },

            };
            context._360evaluation.AddRange(items);

            context.SaveChanges();

			if(true && context.Database.IsSqlServer())
            {
				context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [360evaluation] OFF");
				trans.Commit();
			}
            
            return true;
        }
    }
}
