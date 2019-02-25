using System;
using System.Collections.Generic;

namespace Evoflare.API.Models
{
    public partial class Team
    {
        public Team()
        {
            EmployeeRelations = new HashSet<EmployeeRelations>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int ProjectId { get; set; }
        public int OrganizationId { get; set; }

        public virtual Organization Organization { get; set; }
        public virtual Project Project { get; set; }
        public virtual ICollection<EmployeeRelations> EmployeeRelations { get; set; }
    }
}
