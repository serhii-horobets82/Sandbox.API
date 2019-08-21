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
    
		//public static bool SeedNotificationType(EvoflareDbContext context)
  //      {
  //          if (context.NotificationType.Any()) return false;
  //          IDbContextTransaction trans = null;
            
		//	if(true && context.Database.IsSqlServer())
		//	{
		//	    trans = context.Database.BeginTransaction();
  //              context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [NotificationType] ON");
		//	}
  //          var items = new[]
  //          {
		//		new NotificationType {Id = 1, Name = @"ProjectJoin", Template = @"${name} has joined a project ${project}" },
		//		new NotificationType {Id = 4, Name = @"ProjectLeave", Template = @"${name} has left a project ${project}" },
		//		new NotificationType {Id = 5, Name = @"ProjectManagerAssignToTeam", Template = @"${name} has been assigned as a Manager to a project ${project}" },

  //          };
  //          context.NotificationType.AddRange(items);

  //          context.SaveChanges();

		//	if(true && context.Database.IsSqlServer())
  //          {
		//		context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [NotificationType] OFF");
		//		trans.Commit();
		//	}
            
  //          return true;
  //      }
    }
}
