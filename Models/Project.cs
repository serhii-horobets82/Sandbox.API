using System;
using System.Collections.Generic;

namespace Evoflare.API.Models
{
    public partial class Project
    {
        public Project()
        {
            CustomerContact = new HashSet<CustomerContact>();
            EmployeeRelations = new HashSet<EmployeeRelations>();
            Position = new HashSet<Position>();
            ProjectCareerPath = new HashSet<ProjectCareerPath>();
            ProjectPosition = new HashSet<ProjectPosition>();
            Team = new HashSet<Team>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int OrganizationId { get; set; }

        public virtual ICollection<CustomerContact> CustomerContact { get; set; }
        public virtual ICollection<EmployeeRelations> EmployeeRelations { get; set; }
        public virtual ICollection<Position> Position { get; set; }
        public virtual ICollection<ProjectCareerPath> ProjectCareerPath { get; set; }
        public virtual ICollection<ProjectPosition> ProjectPosition { get; set; }
        public virtual ICollection<Team> Team { get; set; }
    }
}
