using Evoflare.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using System.Globalization;


namespace Evoflare.API.Data
{
    public static partial class DbInitializer
    {
    
		public static bool SeedCompetenceCertificate(EvoflareDbContext context)
        {
            if (context.CompetenceCertificate.Any()) return false;
            var trans = context.Database.BeginTransaction();
			try
            {
				context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [CompetenceCertificate] ON");
			}
            catch { trans.Rollback(); } // TODO find better solution 

            var items = new[]
            {
				new CompetenceCertificate {Id = 1, CompetenceId = "B1 ", CompetenceLevelId = 23, CertificateId = 1, OrganizationId = 1 },
				new CompetenceCertificate {Id = 2, CompetenceId = "B2 ", CompetenceLevelId = 24, CertificateId = 2, OrganizationId = 1 },

            };
            context.CompetenceCertificate.AddRange(items);

            context.SaveChanges();

			try
            {
				context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [CompetenceCertificate] OFF");
				trans.Commit();
			}
            catch { } // TODO find better solution 
            
            return true;
        }
    }
}
