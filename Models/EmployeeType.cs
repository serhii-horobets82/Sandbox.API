using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Evoflare.API.Models
{
    public partial class EmployeeType
    {
        public EmployeeType()
        {
            Employee = new HashSet<Employee>();
            ProjectCareerPath = new HashSet<ProjectCareerPath>();
            RoleGrade = new HashSet<RoleGrade>();
        }

        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Type { get; set; }
        public int OrganizationId { get; set; }

        [ForeignKey("OrganizationId")]
        [InverseProperty("EmployeeType")]
        public virtual Organization Organization { get; set; }
        [InverseProperty("EmployeeType")]
        public virtual ICollection<Employee> Employee { get; set; }
        [InverseProperty("Role")]
        public virtual ICollection<ProjectCareerPath> ProjectCareerPath { get; set; }
        [InverseProperty("EmployeeType")]
        public virtual ICollection<RoleGrade> RoleGrade { get; set; }
    }
}
