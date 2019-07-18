using Evoflare.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using System.Globalization;


namespace Evoflare.API.Data
{
    public static partial class DbInitializer
    {
    
		public static bool SeedIdeaTag(EvoflareDbContext context)
        {
            if (context.IdeaTag.Any()) return false;
            var trans = context.Database.BeginTransaction();
			try
            {
				context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [IdeaTag] ON");
                context.Database.ExecuteSqlCommand("INSERT [dbo].[IdeaTag] ([Id], [Name]) VALUES (1, N'office')");
                context.Database.ExecuteSqlCommand("INSERT [dbo].[IdeaTag] ([Id], [Name]) VALUES (2, N'lunch')");
                context.Database.ExecuteSqlCommand("INSERT [dbo].[IdeaTag] ([Id], [Name]) VALUES (3, N'business')");
                context.Database.ExecuteSqlCommand("INSERT [dbo].[IdeaTag] ([Id], [Name]) VALUES (4, N'tea')");
                context.Database.ExecuteSqlCommand("INSERT [dbo].[IdeaTag] ([Id], [Name]) VALUES (5, N'ice')");
                context.Database.ExecuteSqlCommand("INSERT [dbo].[IdeaTag] ([Id], [Name]) VALUES (6, N'office')");
                context.Database.ExecuteSqlCommand("INSERT [dbo].[IdeaTag] ([Id], [Name]) VALUES (7, N'office')");
                context.Database.ExecuteSqlCommand("INSERT [dbo].[IdeaTag] ([Id], [Name]) VALUES (8, N'table')");
                context.Database.ExecuteSqlCommand("INSERT [dbo].[IdeaTag] ([Id], [Name]) VALUES (9, N'trees')");
            }
            catch { trans.Rollback(); } // TODO find better solution 

			try
            {
				context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [IdeaTag] OFF");
				trans.Commit();
			}
            catch { } // TODO find better solution 
            
            return true;
        }
    }
}
