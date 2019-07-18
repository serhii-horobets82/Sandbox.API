using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Evoflare.API.Models
{
    public partial class RoleGrade
    {
        public RoleGrade()
        {
            ProjectPosition = new HashSet<ProjectPosition>();
            ProjectPositionCompetence = new HashSet<ProjectPositionCompetence>();
            RoleGradeCompetence = new HashSet<RoleGradeCompetence>();
        }

        public int Id { get; set; }
        public int EmployeeTypeId { get; set; }
        [Required]
        public string Name { get; set; }
        public int Order { get; set; }
        public int OrganizationId { get; set; }

        [ForeignKey("EmployeeTypeId")]
        [InverseProperty("RoleGrade")]
        public virtual EmployeeType EmployeeType { get; set; }
        [ForeignKey("OrganizationId")]
        [InverseProperty("RoleGrade")]
        public virtual Organization Organization { get; set; }
        [InverseProperty("RoleGrade")]
        public virtual ICollection<ProjectPosition> ProjectPosition { get; set; }
        [InverseProperty("RoleGrade")]
        public virtual ICollection<ProjectPositionCompetence> ProjectPositionCompetence { get; set; }
        [InverseProperty("RoleGrade")]
        public virtual ICollection<RoleGradeCompetence> RoleGradeCompetence { get; set; }
    }
}
