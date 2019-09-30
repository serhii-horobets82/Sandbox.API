using System.Collections.Generic;
using System.Linq;
using Evoflare.API.Constants;

namespace Evoflare.API.Models
{
    public class UiAccessDescriptor
    {
        public string Name { get; }
        public bool IsActive { get; }

        public UiAccessDescriptor(string name, bool isActive = true)
        {
            Name = name;
            IsActive = isActive;
        }
    }

    public static class UiAccessMatrix
    {
        private static readonly UiAccessDescriptor[] Admin = new UiAccessDescriptor[]
        {
             new UiAccessDescriptor(NavigatioMenu.Configuration),
             new UiAccessDescriptor(NavigatioMenu.Employees),
             new UiAccessDescriptor(NavigatioMenu.PositionsGrade),
             new UiAccessDescriptor(NavigatioMenu.OrganizationStructure),
             new UiAccessDescriptor(NavigatioMenu.AdministrationProjects)
        };

        private static readonly UiAccessDescriptor[] SysAdmin = new UiAccessDescriptor[]
        {
             new UiAccessDescriptor(NavigatioMenu.SystemSettings),
        };

        private static readonly UiAccessDescriptor[] Manager = new UiAccessDescriptor[]
        {
             new UiAccessDescriptor(NavigatioMenu.Projects),
             new UiAccessDescriptor(NavigatioMenu.T360TeamReview),
             new UiAccessDescriptor(NavigatioMenu.T360Analytics),
             new UiAccessDescriptor(NavigatioMenu.SalaryReviewPlan),
             new UiAccessDescriptor(NavigatioMenu.EducationCubes),
             new UiAccessDescriptor(NavigatioMenu.Myprofile),
             new UiAccessDescriptor(NavigatioMenu.T360),
             new UiAccessDescriptor(NavigatioMenu.PDP),
             new UiAccessDescriptor(NavigatioMenu.IdeasPlatform),
        };

        private static readonly UiAccessDescriptor[] Hr = new UiAccessDescriptor[]
        {
             new UiAccessDescriptor(NavigatioMenu.EducationCubes),
             new UiAccessDescriptor(NavigatioMenu.Myprofile),
             new UiAccessDescriptor(NavigatioMenu.T360),
             new UiAccessDescriptor(NavigatioMenu.PDP),
             new UiAccessDescriptor(NavigatioMenu.IdeasPlatform),
        };

        private static readonly UiAccessDescriptor[] User = new UiAccessDescriptor[]
        {
             new UiAccessDescriptor(NavigatioMenu.Myprofile),
             new UiAccessDescriptor(NavigatioMenu.T360),
             new UiAccessDescriptor(NavigatioMenu.PDP),
             new UiAccessDescriptor(NavigatioMenu.IdeasPlatform),
        };

        public static Dictionary<string, UiAccessDescriptor> GetAccessRights(string role)
        {
            if (Equals(role, Roles.SysAdmin)) { return Export(SysAdmin); }
            if (Equals(role, Roles.Admin)) { return Export(Admin); }
            if (Equals(role, Roles.Manager)) { return Export(Manager); }
            if (Equals(role, Roles.HR)) { return Export(Hr); }

            return Export(User);
        }

        private static bool Equals(string v1, string v2)
        {
            return string.Equals(v1, v2, System.StringComparison.InvariantCultureIgnoreCase);
        }

        private static Dictionary<string, UiAccessDescriptor> Export(UiAccessDescriptor[] src)
        {
            return src.ToDictionary(x => x.Name.ToLowerInvariant());
        }
    }

    public static class NavigatioMenu
    {
        public const string Configuration = "configuration";

        public const string Employees = "employees";

        public const string Projects = "projects";

        public const string PositionsGrade = "positions-grade";

        public const string OrganizationStructure = "organization-structure";

        public const string T360Questionarie = "360-questionarie";

        public const string CompanyStrategy = "company-strategy";

        public const string T360Schedule = "360-schedule";

        public const string SystemSettings = "system-settings";

        public const string T360TeamReview = "360-team-review";

        public const string T360Analytics = "360-analytics";

        public const string SalaryReviewPlan = "salary-review-plan";

        public const string EducationCubes = "education-cubes";

        public const string Myprofile = "my-profile";

        public const string T360 = "360";

        public const string PDP = "pdp";

        public const string IdeasPlatform = "ideas-platform";

        public const string AdministrationProjects = "administration-projects";
    }
}
