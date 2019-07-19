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
    
		public static bool SeedIdeaComment(EvoflareDbContext context)
        {
            if (context.IdeaComment.Any()) return false;
            IDbContextTransaction trans = null;
            
			if(true && context.Database.IsSqlServer())
			{
			    trans = context.Database.BeginTransaction();
                context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [IdeaComment] ON");
			}
            var items = new[]
            {
				new IdeaComment {Id = 1, IdeaId = 5, Comment = @"First comment", CreatedById = 1, CreatedDate = DateTime.ParseExact("2019-07-15T20:58:58.6330000", "O", CultureInfo.InvariantCulture), ParentCommentId = null, OrganizationId = 1 },
				new IdeaComment {Id = 2, IdeaId = 5, Comment = @"Second!", CreatedById = 1, CreatedDate = DateTime.ParseExact("2019-07-15T21:06:23.3770000", "O", CultureInfo.InvariantCulture), ParentCommentId = null, OrganizationId = 1 },
				new IdeaComment {Id = 3, IdeaId = 5, Comment = @"Third..", CreatedById = 1, CreatedDate = DateTime.ParseExact("2019-07-15T21:07:51.5870000", "O", CultureInfo.InvariantCulture), ParentCommentId = null, OrganizationId = 1 },
				new IdeaComment {Id = 4, IdeaId = 5, Comment = @"another", CreatedById = 1, CreatedDate = DateTime.ParseExact("2019-07-15T21:09:51.1570000", "O", CultureInfo.InvariantCulture), ParentCommentId = null, OrganizationId = 1 },
				new IdeaComment {Id = 5, IdeaId = 5, Comment = @"Second reply", CreatedById = 1, CreatedDate = DateTime.ParseExact("2019-07-15T21:30:39.1670000", "O", CultureInfo.InvariantCulture), ParentCommentId = 2, OrganizationId = 1 },
				new IdeaComment {Id = 6, IdeaId = 5, Comment = @"Third reply", CreatedById = 1, CreatedDate = DateTime.ParseExact("2019-07-15T21:34:38.9200000", "O", CultureInfo.InvariantCulture), ParentCommentId = 3, OrganizationId = 1 },
				new IdeaComment {Id = 7, IdeaId = 5, Comment = @"another reply", CreatedById = 1, CreatedDate = DateTime.ParseExact("2019-07-15T21:36:31.6270000", "O", CultureInfo.InvariantCulture), ParentCommentId = 4, OrganizationId = 1 },
				new IdeaComment {Id = 8, IdeaId = 5, Comment = @"first reply", CreatedById = 1, CreatedDate = DateTime.ParseExact("2019-07-15T21:45:34.1570000", "O", CultureInfo.InvariantCulture), ParentCommentId = 1, OrganizationId = 1 },
				new IdeaComment {Id = 9, IdeaId = 5, Comment = @"second reply 2", CreatedById = 1, CreatedDate = DateTime.ParseExact("2019-07-15T21:48:32.1670000", "O", CultureInfo.InvariantCulture), ParentCommentId = 2, OrganizationId = 1 },
				new IdeaComment {Id = 10, IdeaId = 5, Comment = @"second reply to reply", CreatedById = 1, CreatedDate = DateTime.ParseExact("2019-07-15T21:50:56.7900000", "O", CultureInfo.InvariantCulture), ParentCommentId = 5, OrganizationId = 1 },

            };
            context.IdeaComment.AddRange(items);

            context.SaveChanges();

			if(true && context.Database.IsSqlServer())
            {
				context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [IdeaComment] OFF");
				trans.Commit();
			}
            
            return true;
        }
    }
}
