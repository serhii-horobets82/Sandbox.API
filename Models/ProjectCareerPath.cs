using System;
using System.Collections.Generic;

namespace Evoflare.API.Models
{
    public partial class ProjectCareerPath
    {
        public ProjectCareerPath()
        {
            ProjectPosition = new HashSet<ProjectPosition>();
        }

        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public int RoleId { get; set; }
        public int? TeamId { get; set; }
        public int OrganizationId { get; set; }

        public virtual Organization Organization { get; set; }
        public virtual Project Project { get; set; }
        public virtual EmployeeType Role { get; set; }
        public virtual Team Team { get; set; }
        public virtual ICollection<ProjectPosition> ProjectPosition { get; set; }
    }
}
