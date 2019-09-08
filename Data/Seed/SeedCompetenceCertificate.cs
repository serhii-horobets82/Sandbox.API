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
    
		public static bool SeedCompetenceCertificate(EvoflareDbContext context)
        {
            if (context.CompetenceCertificate.Any()) return false;
            IDbContextTransaction trans = null;
            
			if(true && context.Database.IsSqlServer())
			{
			    trans = context.Database.BeginTransaction();
                context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [CompetenceCertificate] ON");
			}
            var items = new[]
            {
				new CompetenceCertificate {Id = 1, CompetenceId = 10, CompetenceLevelId = 23, CertificateId = 1, OrganizationId = 1 },
				new CompetenceCertificate {Id = 2, CompetenceId = 11, CompetenceLevelId = 24, CertificateId = 2, OrganizationId = 1 },

            };
            context.CompetenceCertificate.AddRange(items);

            context.SaveChanges();

			if(true && context.Database.IsSqlServer())
            {
				context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [CompetenceCertificate] OFF");
				trans.Commit();
			}
            
            return true;
        }
    }
}
