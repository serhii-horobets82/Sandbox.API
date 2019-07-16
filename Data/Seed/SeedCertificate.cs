using Evoflare.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using System.Globalization;


namespace Evoflare.API.Data
{
    public static partial class DbInitializer
    {
    
		public static bool SeedCertificate(EvoflareDbContext context)
        {
            if (context.Certificate.Any()) return false;
            var trans = context.Database.BeginTransaction();
            context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [Certificate] ON");

            var items = new[]
            {
				new Certificate {Id = 1, Name = "Microsoft Certified Azure Fundamentals", Company = "Microsoft", Technology = "Azure", Stack = null, CertificationLevel = "Foundation" },
				new Certificate {Id = 2, Name = "Microsoft Certified: Azure Developer Associate", Company = "Microsoft", Technology = "Azure", Stack = "Development", CertificationLevel = "Associate" },
				new Certificate {Id = 4, Name = "Microsoft Certified: Azure Administrator Associate", Company = "Microsoft", Technology = "Azure", Stack = "Administration", CertificationLevel = "Associate" },
				new Certificate {Id = 5, Name = "Microsoft Certified: Azure Solutions Architect Expert", Company = "Microsoft", Technology = "Azure", Stack = "Development", CertificationLevel = "Expert" },
				new Certificate {Id = 6, Name = "Microsoft Certified: Azure DevOps Engineer Expert", Company = "Microsoft", Technology = "Azure", Stack = "Administraion", CertificationLevel = "Expert" },
				new Certificate {Id = 7, Name = "AWS Certified Cloud Practitioner", Company = "Amazon", Technology = "AWS", Stack = null, CertificationLevel = "Foundation" },
				new Certificate {Id = 8, Name = "AWS Certified Developer – Associate", Company = "Amazon", Technology = "AWS", Stack = "Development", CertificationLevel = "Associate" },
				new Certificate {Id = 9, Name = "AWS Certified Solutions Architect – Associate", Company = "Amazon", Technology = "AWS", Stack = "Development", CertificationLevel = "Associate" },
				new Certificate {Id = 10, Name = "AWS Certified SysOps Administrator – Associate", Company = "Amazon", Technology = "AWS", Stack = "Administration", CertificationLevel = "Associate" },
				new Certificate {Id = 11, Name = "AWS Certified Solutions Architect – Professional", Company = "Amazon", Technology = "AWS", Stack = "Development", CertificationLevel = "Expert" },
				new Certificate {Id = 12, Name = "AWS Certified DevOps Engineer – Professional", Company = "Amazon", Technology = "AWS", Stack = "Administration", CertificationLevel = "Expert" },

            };
            context.Certificate.AddRange(items);

            context.SaveChanges();
			context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [Certificate] OFF");
            trans.Commit();
            return true;
        }
    }
}
