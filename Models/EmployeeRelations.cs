using System;
using System.Collections.Generic;

namespace Evoflare.API.Models
{
    public partial class EmployeeRelations
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int? ManagerId { get; set; }
        public int? TeamId { get; set; }
        public int? ProjectId { get; set; }
        public int OrganizationId { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual Employee Manager { get; set; }
        public virtual Organization Organization { get; set; }
        public virtual Project Project { get; set; }
        public virtual Team Team { get; set; }
    }
}
