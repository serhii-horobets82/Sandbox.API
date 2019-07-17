using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        public int? ProjectId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public int OrganizationId { get; set; }

        [ForeignKey("CreatedBy")]
        [InverseProperty("PositionCreatedByNavigation")]
        public virtual Employee CreatedByNavigation { get; set; }
        [ForeignKey("ProjectId")]
        [InverseProperty("Position")]
        public virtual Project Project { get; set; }
        [ForeignKey("UpdatedBy")]
        [InverseProperty("PositionUpdatedByNavigation")]
        public virtual Employee UpdatedByNavigation { get; set; }
        [InverseProperty("Position")]
        public virtual ICollection<EmployeeRelations> EmployeeRelations { get; set; }
        [InverseProperty("Position")]
        public virtual ICollection<PositionRole> PositionRole { get; set; }
    }
}
