using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Evoflare.API.Models
{
    public partial class EmployeeRelations
    {
        public int Id { get; set; }
        public int? EmployeeId { get; set; }
        public int? ManagerId { get; set; }
        public int? TeamId { get; set; }
        public int? ProjectId { get; set; }
        public int? PositionId { get; set; }
        public int OrganizationId { get; set; }
        public bool Archived { get; set; }

        [ForeignKey("EmployeeId")]
        [InverseProperty("EmployeeRelationsEmployee")]
        public virtual Employee Employee { get; set; }
        [ForeignKey("ManagerId")]
        [InverseProperty("EmployeeRelationsManager")]
        public virtual Employee Manager { get; set; }
        [ForeignKey("OrganizationId")]
        [InverseProperty("EmployeeRelations")]
        public virtual Organization Organization { get; set; }
        [ForeignKey("PositionId")]
        [InverseProperty("EmployeeRelations")]
        public virtual Position Position { get; set; }
        [ForeignKey("ProjectId")]
        [InverseProperty("EmployeeRelations")]
        public virtual Project Project { get; set; }
        [ForeignKey("TeamId")]
        [InverseProperty("EmployeeRelations")]
        public virtual Team Team { get; set; }
    }
}
