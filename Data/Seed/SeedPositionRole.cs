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
    
		public static bool SeedPositionRole(EvoflareDbContext context)
        {
            if (context.PositionRole.Any()) return false;
            IDbContextTransaction trans = null;
            
			if(true && context.Database.IsSqlServer())
			{
			    trans = context.Database.BeginTransaction();
                context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [PositionRole] ON");
			}
            var items = new[]
            {
				new PositionRole {Id = 1, PositionId = 1, RoleId = 1, DateTime = DateTime.ParseExact("2019-02-26T18:14:57.7170000", "O", CultureInfo.InvariantCulture) },
				new PositionRole {Id = 2, PositionId = 1, RoleId = 3, DateTime = DateTime.ParseExact("2019-02-26T18:14:57.7230000", "O", CultureInfo.InvariantCulture) },
				new PositionRole {Id = 3, PositionId = 2, RoleId = 1, DateTime = DateTime.ParseExact("2019-02-26T19:18:03.2930000", "O", CultureInfo.InvariantCulture) },
				new PositionRole {Id = 4, PositionId = 2, RoleId = 24, DateTime = DateTime.ParseExact("2019-02-26T19:18:03.3000000", "O", CultureInfo.InvariantCulture) },
				new PositionRole {Id = 5, PositionId = 3, RoleId = 2, DateTime = DateTime.ParseExact("2019-02-26T19:19:59.9430000", "O", CultureInfo.InvariantCulture) },
				new PositionRole {Id = 6, PositionId = 3, RoleId = 27, DateTime = DateTime.ParseExact("2019-02-26T19:19:59.9600000", "O", CultureInfo.InvariantCulture) },
				new PositionRole {Id = 7, PositionId = 3, RoleId = 22, DateTime = DateTime.ParseExact("2019-02-26T19:19:59.9670000", "O", CultureInfo.InvariantCulture) },
				new PositionRole {Id = 8, PositionId = 4, RoleId = 1, DateTime = DateTime.ParseExact("2019-03-26T17:26:59.0630000", "O", CultureInfo.InvariantCulture) },
				new PositionRole {Id = 9, PositionId = 4, RoleId = 4, DateTime = DateTime.ParseExact("2019-03-26T17:26:59.0730000", "O", CultureInfo.InvariantCulture) },
				new PositionRole {Id = 10, PositionId = 5, RoleId = 1, DateTime = DateTime.ParseExact("2019-03-26T17:27:36.6000000", "O", CultureInfo.InvariantCulture) },
				new PositionRole {Id = 11, PositionId = 5, RoleId = 2, DateTime = DateTime.ParseExact("2019-03-26T17:27:36.6000000", "O", CultureInfo.InvariantCulture) },
				new PositionRole {Id = 12, PositionId = 5, RoleId = 3, DateTime = DateTime.ParseExact("2019-03-26T17:27:36.6000000", "O", CultureInfo.InvariantCulture) },
				new PositionRole {Id = 13, PositionId = 5, RoleId = 4, DateTime = DateTime.ParseExact("2019-03-26T17:27:36.6000000", "O", CultureInfo.InvariantCulture) },
				new PositionRole {Id = 14, PositionId = 6, RoleId = 1, DateTime = DateTime.ParseExact("2019-03-31T12:58:27.0870000", "O", CultureInfo.InvariantCulture) },

            };
            context.PositionRole.AddRange(items);

            context.SaveChanges();

			if(true && context.Database.IsSqlServer())
            {
				context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [PositionRole] OFF");
				trans.Commit();
			}
            
            return true;
        }
    }
}
