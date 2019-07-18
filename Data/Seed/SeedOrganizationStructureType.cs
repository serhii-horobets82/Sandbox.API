using Evoflare.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using System.Globalization;


namespace Evoflare.API.Data
{
    public static partial class DbInitializer
    {
    
		public static bool SeedOrganizationStructureType(EvoflareDbContext context)
        {
            if (context.OrganizationStructureType.Any()) return false;
            var trans = context.Database.BeginTransaction();
			try
            {
				context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [OrganizationStructureType] ON");
            }
            catch { trans.Rollback(); } // TODO find better solution 

            context.Database.ExecuteSqlCommand("INSERT [dbo].[OrganizationStructureType] ([Id], [Name], [Description]) VALUES (1, N'Functional', N'In a functional organization, every employee is positioned within only one function and has one manager they report to, the Functional Manager. The Functional Manager assigns and manages the employees work and handles administrative tasks such as employee compensation.')");
            context.Database.ExecuteSqlCommand("INSERT [dbo].[OrganizationStructureType] ([Id], [Name], [Description]) VALUES (2, N'Project-Based', N'In a project-based organization most of the organization''s resources are involved in project work. Project Managers have high levels of independence and authority for the project and control the project resources.')");
            context.Database.ExecuteSqlCommand($@"INSERT [dbo].[OrganizationStructureType] ([Id], [Name], [Description]) VALUES (3, N'Matrix', N'Matrix organizations blend features of project-based and functional organizational structures.            

            The key challenge with a matrix organization is that every employee has two(or more) managers they report to, their Functional Manager and the Project Manager.If they are working on multiple projects, they may have even more managers to report to.')");

            try
            {
				context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [OrganizationStructureType] OFF");
				trans.Commit();
			}
            catch { } // TODO find better solution 
            
            return true;
        }
    }
}
