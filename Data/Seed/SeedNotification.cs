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
    
		//public static bool SeedNotification(EvoflareDbContext context)
  //      {
  //          if (context.Notification.Any()) return false;
  //          IDbContextTransaction trans = null;
            
		//	if(true && context.Database.IsSqlServer())
		//	{
		//	    trans = context.Database.BeginTransaction();
  //              context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [Notification] ON");
		//	}
  //          var items = new[]
  //          {
		//		new Notification {Id = 1, Type = 1, EmployeeId = 21, Data = @"{""name"": ""You"", ""project"":""Cool project""}", CreatedDate = DateTime.ParseExact("2019-07-12T19:26:00.0000000", "O", CultureInfo.InvariantCulture), CreatedBy = 1, Active = true, ViewDate = null },
		//		new Notification {Id = 3, Type = 3, EmployeeId = 21, Data = @"{""name"":""Some Manager"", ""project"":""Cool project"", ""managerId"":""1""}", CreatedDate = DateTime.ParseExact("2019-07-12T19:20:00.0000000", "O", CultureInfo.InvariantCulture), CreatedBy = 1, Active = true, ViewDate = null },

  //          };
  //          context.Notification.AddRange(items);

  //          context.SaveChanges();

		//	if(true && context.Database.IsSqlServer())
  //          {
		//		context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [Notification] OFF");
		//		trans.Commit();
		//	}
            
  //          return true;
  //      }
    }
}
