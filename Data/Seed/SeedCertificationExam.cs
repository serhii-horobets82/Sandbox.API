using Evoflare.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using System.Globalization;


namespace Evoflare.API.Data
{
    public static partial class DbInitializer
    {
    
		public static bool SeedCertificationExam(EvoflareDbContext context)
        {
            if (context.CertificationExam.Any()) return false;
            var trans = context.Database.BeginTransaction();
			try
            {
				context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [CertificationExam] ON");
			}
            catch { trans.Rollback(); } // TODO find better solution 

            var items = new[]
            {
				new CertificationExam {Id = 1, Code = "AZ-900", Name = "Microsoft Azure Fundamentals", Price = 100 },
				new CertificationExam {Id = 4, Code = "AZ-203", Name = "Developing Solutions for Microsoft Azure", Price = 165 },
				new CertificationExam {Id = 5, Code = "AZ-103", Name = "Microsoft Azure Administrator", Price = 165 },
				new CertificationExam {Id = 6, Code = "AZ-300", Name = "Microsoft Azure Architect Technologies", Price = 165 },
				new CertificationExam {Id = 7, Code = "AZ-301", Name = "Microsoft Azure Architect Design", Price = 165 },
				new CertificationExam {Id = 8, Code = "AZ-400", Name = "Microsoft Azure DevOps Solutions", Price = 165 },

            };
            context.CertificationExam.AddRange(items);

            context.SaveChanges();

			try
            {
				context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [CertificationExam] OFF");
				trans.Commit();
			}
            catch { } // TODO find better solution 
            
            return true;
        }
    }
}
