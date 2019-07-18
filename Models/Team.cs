using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Evoflare.API.Models
{
    public partial class Team
    {
        public Team()
        {
            EmployeeRelations = new HashSet<EmployeeRelations>();
            ProjectCareerPath = new HashSet<ProjectCareerPath>();
        }

        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        public int ProjectId { get; set; }
        public int OrganizationId { get; set; }

        [ForeignKey("OrganizationId")]
        [InverseProperty("Team")]
        public virtual Organization Organization { get; set; }
        [ForeignKey("ProjectId")]
        [InverseProperty("Team")]
        public virtual Project Project { get; set; }
        [InverseProperty("Team")]
        public virtual ICollection<EmployeeRelations> EmployeeRelations { get; set; }
        [InverseProperty("Team")]
        public virtual ICollection<ProjectCareerPath> ProjectCareerPath { get; set; }
    }
}
