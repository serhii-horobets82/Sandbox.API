using Evoflare.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using System.Globalization;


namespace Evoflare.API.Data
{
    public static partial class DbInitializer
    {
    
		public static bool Seed_360question(EvoflareDbContext context)
        {
            if (context._360question.Any()) return false;
            var trans = context.Database.BeginTransaction();
			try
            {
				context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [360question] ON");
			}
            catch { trans.Rollback(); } // TODO find better solution 

            var items = new[]
            {
				new _360question {Id = 5, QuestionToMarkId = 4, Question = "wawaw `\nawaww 2", Order = 0, OrganizationId = 1 },
				new _360question {Id = 6, QuestionToMarkId = 5, Question = "tatat 1\ntatatata 2", Order = 0, OrganizationId = 1 },
				new _360question {Id = 7, QuestionToMarkId = 6, Question = "tgggggggggggg\ngadss\ndfgjfdh", Order = 0, OrganizationId = 1 },
				new _360question {Id = 8, QuestionToMarkId = 7, Question = "234234\nflskdfj", Order = 0, OrganizationId = 1 },
				new _360question {Id = 9, QuestionToMarkId = 8, Question = "auauauau\nsfjd999 999", Order = 0, OrganizationId = 1 },
				new _360question {Id = 10, QuestionToMarkId = 9, Question = "0a0a0a0a", Order = 0, OrganizationId = 1 },
				new _360question {Id = 14, QuestionToMarkId = 4, Question = "wawaw `\nawaww 2", Order = 0, OrganizationId = 1 },
				new _360question {Id = 15, QuestionToMarkId = 5, Question = "tatat 1\ntatatata 2\nsfdd", Order = 0, OrganizationId = 1 },
				new _360question {Id = 16, QuestionToMarkId = 6, Question = "tgggggggggggg\ngadss\ndfgjfdh", Order = 0, OrganizationId = 1 },
				new _360question {Id = 17, QuestionToMarkId = 15, Question = "adsdDAa", Order = 0, OrganizationId = 1 },
				new _360question {Id = 18, QuestionToMarkId = 16, Question = "asdad3", Order = 0, OrganizationId = 1 },
				new _360question {Id = 19, QuestionToMarkId = 17, Question = "adasd3", Order = 0, OrganizationId = 1 },
				new _360question {Id = 20, QuestionToMarkId = 12, Question = "sfsdfsdf\nasdfasedf\n45rawf4w", Order = 0, OrganizationId = 1 },
				new _360question {Id = 21, QuestionToMarkId = 13, Question = "waf4wa\n4warfr\n", Order = 0, OrganizationId = 1 },
				new _360question {Id = 22, QuestionToMarkId = 14, Question = "a3wdawf\nawf3faw4\nf4awf", Order = 0, OrganizationId = 1 },
				new _360question {Id = 23, QuestionToMarkId = 18, Question = "safdf\na\nf", Order = 0, OrganizationId = 1 },
				new _360question {Id = 24, QuestionToMarkId = 19, Question = "2f3\nf4was\nfsd\n", Order = 0, OrganizationId = 1 },
				new _360question {Id = 25, QuestionToMarkId = 20, Question = "fa3w\nfw3\n", Order = 0, OrganizationId = 1 },
				new _360question {Id = 26, QuestionToMarkId = 21, Question = "asdfds", Order = 0, OrganizationId = 1 },
				new _360question {Id = 27, QuestionToMarkId = 22, Question = "afsdfafsdf", Order = 0, OrganizationId = 1 },
				new _360question {Id = 28, QuestionToMarkId = 23, Question = "asdfsd", Order = 0, OrganizationId = 1 },
				new _360question {Id = 29, QuestionToMarkId = 24, Question = "as df aw\n4f awf", Order = 0, OrganizationId = 1 },
				new _360question {Id = 30, QuestionToMarkId = 25, Question = "waffwa \naw fds\nf as\ndfs ", Order = 0, OrganizationId = 1 },
				new _360question {Id = 31, QuestionToMarkId = 26, Question = "asfdwf3", Order = 0, OrganizationId = 1 },
				new _360question {Id = 32, QuestionToMarkId = 27, Question = "fsaw3f\nwafwe \n", Order = 0, OrganizationId = 1 },
				new _360question {Id = 33, QuestionToMarkId = 28, Question = "ffw4fe\nawfw\nafsf", Order = 0, OrganizationId = 1 },
				new _360question {Id = 34, QuestionToMarkId = 29, Question = "agw\nar23f\nwgsdfgs dasf", Order = 0, OrganizationId = 1 },

            };
            context._360question.AddRange(items);

            context.SaveChanges();

			try
            {
				context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [360question] OFF");
				trans.Commit();
			}
            catch { } // TODO find better solution 
            
            return true;
        }
    }
}
