using Evoflare.API.Core.Models;
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

        public static bool SeedInstallation(EvoflareDbContext context)
        {
            if (context.Installations.Any()) return false;

            var items = new[]
            {
                new Installation{ Id = Guid.Parse("77593175-5a9f-4b60-ba9e-aad800a698b3"), Email = "dev01.evoflare@gmail.com",Key = "5QYXmaf9LeqnijEwA6xq", Enabled = true}
            };
            context.Installations.AddRange(items);

            context.SaveChanges();

            return true;
        }
    }
}
