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
        public static bool SeedOrganization(EvoflareDbContext context, Organization[] items = null)
        {
            if (context.Organization.Any()) return false;
            IDbContextTransaction trans = null;

            if (true && context.Database.IsSqlServer())
            {
                trans = context.Database.BeginTransaction();
                context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [Organization] ON");
            }
            if (items == null)
            {
                items = new[]
                {
                    new Organization {Id = 1, Name = $"{context.Database.GetDbConnection().Database}" },
                };
            }
            context.Organization.AddRange(items);

            context.SaveChanges();

            if (true && context.Database.IsSqlServer())
            {
                context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [Organization] OFF");
                trans.Commit();
            }

            return true;
        }
    }
}
