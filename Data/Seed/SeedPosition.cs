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
    
		public static bool SeedPosition(EvoflareDbContext context)
        {
            if (context.Position.Any()) return false;
            IDbContextTransaction trans = null;
            
			if(true && context.Database.IsSqlServer())
			{
			    trans = context.Database.BeginTransaction();
                context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [Position] ON");
			}
            var items = new[]
            {
				new Position {Id = 1, Name = @"First position", ProjectId = null, CreatedDate = DateTime.ParseExact("2019-02-26T18:14:57.6830000", "O", CultureInfo.InvariantCulture), CreatedBy = 1, UpdatedDate = null, UpdatedBy = null, IsDeleted = false, OrganizationId = 0 },
				new Position {Id = 2, Name = @"Developer 1", ProjectId = null, CreatedDate = DateTime.ParseExact("2019-02-26T19:18:03.2600000", "O", CultureInfo.InvariantCulture), CreatedBy = 1, UpdatedDate = null, UpdatedBy = null, IsDeleted = false, OrganizationId = 0 },
				new Position {Id = 3, Name = @"Developer 2", ProjectId = null, CreatedDate = DateTime.ParseExact("2019-02-26T19:19:59.9070000", "O", CultureInfo.InvariantCulture), CreatedBy = 1, UpdatedDate = null, UpdatedBy = null, IsDeleted = false, OrganizationId = 0 },
				new Position {Id = 4, Name = @"Middle Developer", ProjectId = null, CreatedDate = DateTime.ParseExact("2019-03-26T17:26:59.0230000", "O", CultureInfo.InvariantCulture), CreatedBy = 1, UpdatedDate = null, UpdatedBy = null, IsDeleted = false, OrganizationId = 0 },
				new Position {Id = 5, Name = @"Senior Developer", ProjectId = null, CreatedDate = DateTime.ParseExact("2019-03-26T17:27:36.5770000", "O", CultureInfo.InvariantCulture), CreatedBy = 1, UpdatedDate = null, UpdatedBy = null, IsDeleted = false, OrganizationId = 0 },
				new Position {Id = 6, Name = @"Junior project position (Smart product 1)", ProjectId = 1, CreatedDate = DateTime.ParseExact("2019-03-31T12:58:27.0700000", "O", CultureInfo.InvariantCulture), CreatedBy = 1, UpdatedDate = null, UpdatedBy = null, IsDeleted = false, OrganizationId = 0 },

            };
            context.Position.AddRange(items);

            context.SaveChanges();

			if(true && context.Database.IsSqlServer())
            {
				context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [Position] OFF");
				trans.Commit();
			}
            
            return true;
        }
    }
}
