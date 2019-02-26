using System;
using System.Collections.Generic;

namespace Evoflare.API.Models
{
    public partial class Position
    {
        public Position()
        {
            EmployeePosition = new HashSet<EmployeePosition>();
            PositionRole = new HashSet<PositionRole>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int OrganizationId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Employee CreatedByNavigation { get; set; }
        public virtual Employee UpdatedByNavigation { get; set; }
        public virtual ICollection<EmployeePosition> EmployeePosition { get; set; }
        public virtual ICollection<PositionRole> PositionRole { get; set; }
    }
}
