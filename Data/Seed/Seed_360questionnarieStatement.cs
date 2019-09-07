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
    
		public static bool Seed_360questionnarieStatement(EvoflareDbContext context)
        {
            if (context._360questionnarieStatement.Any()) return false;
            IDbContextTransaction trans = null;
            
			if(true && context.Database.IsSqlServer())
			{
			    trans = context.Database.BeginTransaction();
                context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [360questionnarieStatement] ON");
			}
            var items = new[]
            {
				new _360questionnarieStatement {Id = 2, QuestionnarieId = 2, Mark = 1, Text = @" ff" },
				new _360questionnarieStatement {Id = 3, QuestionnarieId = 2, Mark = 2, Text = @" ss" },
				new _360questionnarieStatement {Id = 4, QuestionnarieId = 2, Mark = 3, Text = @" dd" },
				new _360questionnarieStatement {Id = 5, QuestionnarieId = 2, Mark = 4, Text = @" 33gg" },
				new _360questionnarieStatement {Id = 6, QuestionnarieId = 2, Mark = 5, Text = @" gg" },
				new _360questionnarieStatement {Id = 7, QuestionnarieId = 3, Mark = 1, Text = @" adsf" },
				new _360questionnarieStatement {Id = 8, QuestionnarieId = 3, Mark = 2, Text = @" 24" },
				new _360questionnarieStatement {Id = 9, QuestionnarieId = 3, Mark = 3, Text = @"gsdf" },
				new _360questionnarieStatement {Id = 10, QuestionnarieId = 3, Mark = 4, Text = @" asdf" },
				new _360questionnarieStatement {Id = 11, QuestionnarieId = 3, Mark = 5, Text = @" h5" },
				new _360questionnarieStatement {Id = 12, QuestionnarieId = 4, Mark = 1, Text = @" adf" },
				new _360questionnarieStatement {Id = 13, QuestionnarieId = 4, Mark = 2, Text = @" fawe" },
				new _360questionnarieStatement {Id = 14, QuestionnarieId = 4, Mark = 3, Text = @" a4g" },
				new _360questionnarieStatement {Id = 15, QuestionnarieId = 4, Mark = 4, Text = @" gae" },
				new _360questionnarieStatement {Id = 16, QuestionnarieId = 4, Mark = 5, Text = @" a4t" },

            };
            context._360questionnarieStatement.AddRange(items);

            context.SaveChanges();

			if(true && context.Database.IsSqlServer())
            {
				context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [360questionnarieStatement] OFF");
				trans.Commit();
			}
            
            return true;
        }
    }
}
