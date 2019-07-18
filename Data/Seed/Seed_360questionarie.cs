using Evoflare.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using System.Globalization;


namespace Evoflare.API.Data
{
    public static partial class DbInitializer
    {
    
		public static bool Seed_360questionarie(EvoflareDbContext context)
        {
            if (context._360questionarie.Any()) return false;
            var trans = context.Database.BeginTransaction();
			try
            {
                if(context.Database.IsSqlServer())
                    context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [360questionarie] ON");
			}
            catch { trans.Rollback(); } // TODO find better solution 

            var items = new[]
            {
				new _360questionarie {Id = 1, Text = "Question 1", _360feedbackGroupId = 1, OrganizationId = 1 },
				new _360questionarie {Id = 2, Text = "Question 1", _360feedbackGroupId = 2, OrganizationId = 1 },
				new _360questionarie {Id = 4, Text = "Question 2", _360feedbackGroupId = 1, OrganizationId = 1 },
				new _360questionarie {Id = 5, Text = "Question 3", _360feedbackGroupId = 1, OrganizationId = 1 },
				new _360questionarie {Id = 8, Text = "Question 4", _360feedbackGroupId = 1, OrganizationId = 1 },
				new _360questionarie {Id = 9, Text = "Question 2", _360feedbackGroupId = 2, OrganizationId = 1 },
				new _360questionarie {Id = 10, Text = "Question 3", _360feedbackGroupId = 2, OrganizationId = 1 },
				new _360questionarie {Id = 11, Text = "Question 4", _360feedbackGroupId = 2, OrganizationId = 1 },

            };
            context._360questionarie.AddRange(items);

            context.SaveChanges();

			try
            {
				if(context.Database.IsSqlServer())
					context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [360questionarie] OFF");
				trans.Commit();
			}
            catch { } // TODO find better solution 
            
            return true;
        }
    }
}
