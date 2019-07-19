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
    
		public static bool SeedOrganizationStructureType(EvoflareDbContext context)
        {
            if (context.OrganizationStructureType.Any()) return false;
            IDbContextTransaction trans = null;
            
			if(true && context.Database.IsSqlServer())
			{
			    trans = context.Database.BeginTransaction();
                context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [OrganizationStructureType] ON");
			}
            var items = new[]
            {
				new OrganizationStructureType {Id = 1, Name = @"Functional", Description = @"In a functional organization, every employee is positioned within only one function and has one manager they report to, the Functional Manager. The Functional Manager assigns and manages the employees work and handles administrative tasks such as employee compensation." },
				new OrganizationStructureType {Id = 2, Name = @"Project-Based", Description = @"In a project-based organization most of the organization's resources are involved in project work. Project Managers have high levels of independence and authority for the project and control the project resources." },
				new OrganizationStructureType {Id = 3, Name = @"Matrix", Description = @"Matrix organizations blend features of project-based and functional organizational structures.            

            The key challenge with a matrix organization is that every employee has two(or more) managers they report to, their Functional Manager and the Project Manager.If they are working on multiple projects, they may have even more managers to report to." },

            };
            context.OrganizationStructureType.AddRange(items);

            context.SaveChanges();

			if(true && context.Database.IsSqlServer())
            {
				context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [OrganizationStructureType] OFF");
				trans.Commit();
			}
            
            return true;
        }
    }
}
