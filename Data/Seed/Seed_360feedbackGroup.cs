using Evoflare.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using System.Globalization;


namespace Evoflare.API.Data
{
    public static partial class DbInitializer
    {
    
		public static bool Seed_360feedbackGroup(EvoflareDbContext context)
        {
            if (context._360feedbackGroup.Any()) return false;
            var trans = context.Database.BeginTransaction();
            context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [360feedbackGroup] ON");

            var items = new[]
            {
				new _360feedbackGroup {Id = 1, Type = "Self" },
				new _360feedbackGroup {Id = 2, Type = "Peer" },
				new _360feedbackGroup {Id = 3, Type = "Customer" },
				new _360feedbackGroup {Id = 4, Type = "Subordinate" },

            };
            context._360feedbackGroup.AddRange(items);

            context.SaveChanges();
			context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [360feedbackGroup] OFF");
            trans.Commit();
            return true;
        }
    }
}
