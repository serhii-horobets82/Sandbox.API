using Evoflare.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using System.Globalization;


namespace Evoflare.API.Data
{
    public static partial class DbInitializer
    {
    
		public static bool SeedIdeaTagRef(EvoflareDbContext context)
        {
            if (context.IdeaTagRef.Any()) return false;
            var trans = context.Database.BeginTransaction();
			try
            {
				context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [IdeaTagRef] ON");
                context.Database.ExecuteSqlCommand("INSERT [dbo].[IdeaTagRef] ([Id], [IdeaId], [TagId]) VALUES (2, 4, 7)");
                context.Database.ExecuteSqlCommand("INSERT [dbo].[IdeaTagRef] ([Id], [IdeaId], [TagId]) VALUES (3, 5, 8)");
                context.Database.ExecuteSqlCommand("INSERT [dbo].[IdeaTagRef] ([Id], [IdeaId], [TagId]) VALUES (4, 5, 9)");
            }
            catch { trans.Rollback(); } // TODO find better solution 

			try
            {
				context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [IdeaTagRef] OFF");
				trans.Commit();
			}
            catch { } // TODO find better solution 
            
            return true;
        }
    }
}
