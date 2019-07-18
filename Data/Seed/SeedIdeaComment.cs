using Evoflare.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using System.Globalization;


namespace Evoflare.API.Data
{
    public static partial class DbInitializer
    {
    
		public static bool SeedIdeaComment(EvoflareDbContext context)
        {
            if (context.IdeaComment.Any()) return false;
            var trans = context.Database.BeginTransaction();
			try
            {
				context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [IdeaComment] ON");
                context.Database.ExecuteSqlCommand("INSERT[dbo].[IdeaComment]([Id], [IdeaId], [Comment], [CreatedById], [CreatedDate], [ParentCommentId], [OrganizationId]) VALUES(1, 5, N'First comment', 1, CAST(N'2019-07-15T20:58:58.633' AS DateTime), NULL, 1)");
                context.Database.ExecuteSqlCommand("INSERT[dbo].[IdeaComment] ([Id], [IdeaId], [Comment], [CreatedById], [CreatedDate], [ParentCommentId], [OrganizationId]) VALUES(2, 5, N'Second!', 1, CAST(N'2019-07-15T21:06:23.377' AS DateTime), NULL, 1)");
                context.Database.ExecuteSqlCommand("INSERT[dbo].[IdeaComment] ([Id], [IdeaId], [Comment], [CreatedById], [CreatedDate], [ParentCommentId], [OrganizationId]) VALUES(3, 5, N'Third..', 1, CAST(N'2019-07-15T21:07:51.587' AS DateTime), NULL, 1)");
                context.Database.ExecuteSqlCommand("INSERT[dbo].[IdeaComment] ([Id], [IdeaId], [Comment], [CreatedById], [CreatedDate], [ParentCommentId], [OrganizationId]) VALUES(4, 5, N'another', 1, CAST(N'2019-07-15T21:09:51.157' AS DateTime), NULL, 1)");
                context.Database.ExecuteSqlCommand("INSERT[dbo].[IdeaComment] ([Id], [IdeaId], [Comment], [CreatedById], [CreatedDate], [ParentCommentId], [OrganizationId]) VALUES(5, 5, N'Second reply', 1, CAST(N'2019-07-15T21:30:39.167' AS DateTime), 2, 1)");
                context.Database.ExecuteSqlCommand("INSERT[dbo].[IdeaComment] ([Id], [IdeaId], [Comment], [CreatedById], [CreatedDate], [ParentCommentId], [OrganizationId]) VALUES(6, 5, N'Third reply', 1, CAST(N'2019-07-15T21:34:38.920' AS DateTime), 3, 1)");
                context.Database.ExecuteSqlCommand("INSERT[dbo].[IdeaComment] ([Id], [IdeaId], [Comment], [CreatedById], [CreatedDate], [ParentCommentId], [OrganizationId]) VALUES(7, 5, N'another reply', 1, CAST(N'2019-07-15T21:36:31.627' AS DateTime), 4, 1)");
                context.Database.ExecuteSqlCommand("INSERT[dbo].[IdeaComment] ([Id], [IdeaId], [Comment], [CreatedById], [CreatedDate], [ParentCommentId], [OrganizationId]) VALUES(8, 5, N'first reply', 1, CAST(N'2019-07-15T21:45:34.157' AS DateTime), 1, 1)");
                context.Database.ExecuteSqlCommand("INSERT[dbo].[IdeaComment] ([Id], [IdeaId], [Comment], [CreatedById], [CreatedDate], [ParentCommentId], [OrganizationId]) VALUES(9, 5, N'second reply 2', 1, CAST(N'2019-07-15T21:48:32.167' AS DateTime), 2, 1)");
                context.Database.ExecuteSqlCommand("INSERT[dbo].[IdeaComment] ([Id], [IdeaId], [Comment], [CreatedById], [CreatedDate], [ParentCommentId], [OrganizationId]) VALUES(10, 5, N'second reply to reply', 1, CAST(N'2019-07-15T21:50:56.790' AS DateTime), 5, 1)");
            }
            catch { trans.Rollback(); } // TODO find better solution 

			try
            {
				context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [IdeaComment] OFF");
				trans.Commit();
			}
            catch { } // TODO find better solution 
            
            return true;
        }
    }
}
