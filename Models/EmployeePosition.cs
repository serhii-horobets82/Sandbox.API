using System;
using System.Collections.Generic;

namespace Evoflare.API.Models
{
    public partial class EmployeePosition
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int PositionId { get; set; }
        public int RelationId { get; set; }
        public int OrganizationId { get; set; }

        public virtual Organization Organization { get; set; }
    }
}
