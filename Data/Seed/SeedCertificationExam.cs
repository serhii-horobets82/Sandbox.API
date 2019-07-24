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
    
		public static bool SeedCertificationExam(EvoflareDbContext context)
        {
            if (context.CertificationExam.Any()) return false;
            IDbContextTransaction trans = null;
            
			if(true && context.Database.IsSqlServer())
			{
			    trans = context.Database.BeginTransaction();
                context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [CertificationExam] ON");
			}
            var items = new[]
            {
				new CertificationExam {Id = 1, Code = @"AZ-900", Name = @"Microsoft Azure Fundamentals", Price = 100 },
				new CertificationExam {Id = 4, Code = @"AZ-203", Name = @"Developing Solutions for Microsoft Azure", Price = 165 },
				new CertificationExam {Id = 5, Code = @"AZ-103", Name = @"Microsoft Azure Administrator", Price = 165 },
				new CertificationExam {Id = 6, Code = @"AZ-300", Name = @"Microsoft Azure Architect Technologies", Price = 165 },
				new CertificationExam {Id = 7, Code = @"AZ-301", Name = @"Microsoft Azure Architect Design", Price = 165 },
				new CertificationExam {Id = 8, Code = @"AZ-400", Name = @"Microsoft Azure DevOps Solutions", Price = 165 },

            };
            context.CertificationExam.AddRange(items);

            context.SaveChanges();

			if(true && context.Database.IsSqlServer())
            {
				context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [CertificationExam] OFF");
				trans.Commit();
			}
            
            return true;
        }
    }
}
