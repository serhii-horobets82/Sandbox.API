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
    
		public static bool SeedIdeaView(EvoflareDbContext context)
        {
            if (context.IdeaView.Any()) return false;
            IDbContextTransaction trans = null;
            
			if(true && context.Database.IsSqlServer())
			{
			    trans = context.Database.BeginTransaction();
                context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [IdeaView] ON");
			}
            var items = new[]
            {
				new IdeaView {Id = 1, IdeaId = 1, EmployeeId = 1 },

            };
            context.IdeaView.AddRange(items);

            context.SaveChanges();

			if(true && context.Database.IsSqlServer())
            {
				context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [IdeaView] OFF");
				trans.Commit();
			}
            
            return true;
        }
    }
}
