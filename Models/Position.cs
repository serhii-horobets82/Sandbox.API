using System;
using System.Collections.Generic;

namespace Evoflare.API.Models
{
    public partial class Position
    {
        public Position()
        {
            EmployeeRelations = new HashSet<EmployeeRelations>();
            PositionRole = new HashSet<PositionRole>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? ProjectId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public int OrganizationId { get; set; }

        public virtual Employee CreatedByNavigation { get; set; }
        public virtual Project Project { get; set; }
        public virtual Employee UpdatedByNavigation { get; set; }
        public virtual ICollection<EmployeeRelations> EmployeeRelations { get; set; }
        public virtual ICollection<PositionRole> PositionRole { get; set; }
    }
}
