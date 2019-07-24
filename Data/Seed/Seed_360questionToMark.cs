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
    
		public static bool Seed_360questionToMark(EvoflareDbContext context)
        {
            if (context._360questionToMark.Any()) return false;
            IDbContextTransaction trans = null;
            
			if(true && context.Database.IsSqlServer())
			{
			    trans = context.Database.BeginTransaction();
                context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [360questionToMark] ON");
			}
            var items = new[]
            {
				new _360questionToMark {Id = 4, QuestionId = 4, MarkId = 1, OrganizationId = 1 },
				new _360questionToMark {Id = 5, QuestionId = 4, MarkId = 3, OrganizationId = 1 },
				new _360questionToMark {Id = 6, QuestionId = 4, MarkId = 5, OrganizationId = 1 },
				new _360questionToMark {Id = 7, QuestionId = 5, MarkId = 1, OrganizationId = 1 },
				new _360questionToMark {Id = 8, QuestionId = 5, MarkId = 3, OrganizationId = 1 },
				new _360questionToMark {Id = 9, QuestionId = 5, MarkId = 5, OrganizationId = 1 },
				new _360questionToMark {Id = 12, QuestionId = 8, MarkId = 1, OrganizationId = 1 },
				new _360questionToMark {Id = 13, QuestionId = 8, MarkId = 3, OrganizationId = 1 },
				new _360questionToMark {Id = 14, QuestionId = 8, MarkId = 5, OrganizationId = 1 },
				new _360questionToMark {Id = 15, QuestionId = 1, MarkId = 1, OrganizationId = 1 },
				new _360questionToMark {Id = 16, QuestionId = 1, MarkId = 3, OrganizationId = 1 },
				new _360questionToMark {Id = 17, QuestionId = 1, MarkId = 5, OrganizationId = 1 },
				new _360questionToMark {Id = 18, QuestionId = 2, MarkId = 1, OrganizationId = 1 },
				new _360questionToMark {Id = 19, QuestionId = 2, MarkId = 3, OrganizationId = 1 },
				new _360questionToMark {Id = 20, QuestionId = 2, MarkId = 5, OrganizationId = 1 },
				new _360questionToMark {Id = 21, QuestionId = 9, MarkId = 1, OrganizationId = 1 },
				new _360questionToMark {Id = 22, QuestionId = 9, MarkId = 3, OrganizationId = 1 },
				new _360questionToMark {Id = 23, QuestionId = 9, MarkId = 5, OrganizationId = 1 },
				new _360questionToMark {Id = 24, QuestionId = 10, MarkId = 1, OrganizationId = 1 },
				new _360questionToMark {Id = 25, QuestionId = 10, MarkId = 3, OrganizationId = 1 },
				new _360questionToMark {Id = 26, QuestionId = 10, MarkId = 5, OrganizationId = 1 },
				new _360questionToMark {Id = 27, QuestionId = 11, MarkId = 1, OrganizationId = 1 },
				new _360questionToMark {Id = 28, QuestionId = 11, MarkId = 3, OrganizationId = 1 },
				new _360questionToMark {Id = 29, QuestionId = 11, MarkId = 5, OrganizationId = 1 },

            };
            context._360questionToMark.AddRange(items);

            context.SaveChanges();

			if(true && context.Database.IsSqlServer())
            {
				context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [360questionToMark] OFF");
				trans.Commit();
			}
            
            return true;
        }
    }
}
