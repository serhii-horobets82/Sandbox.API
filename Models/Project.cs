using System;
using System.Collections.Generic;

namespace Evoflare.API.Models
{
    public partial class Project
    {
        public Project()
        {
            EmployeeRelations = new HashSet<EmployeeRelations>();
            Team = new HashSet<Team>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int OrganizationId { get; set; }

        public virtual ICollection<EmployeeRelations> EmployeeRelations { get; set; }
        public virtual ICollection<Team> Team { get; set; }
    }
}
