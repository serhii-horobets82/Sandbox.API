using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Evoflare.API.Models
{
    public partial class ProjectCareerPath
    {
        public ProjectCareerPath()
        {
            ProjectPosition = new HashSet<ProjectPosition>();
        }

        public int Id { get; set; }
        public int ProjectId { get; set; }
        [Required]
        [StringLength(200)]
        public string Name { get; set; }
        public int RoleId { get; set; }
        public int? TeamId { get; set; }
        public int OrganizationId { get; set; }

        [ForeignKey("OrganizationId")]
        [InverseProperty("ProjectCareerPath")]
        public virtual Organization Organization { get; set; }
        [ForeignKey("ProjectId")]
        [InverseProperty("ProjectCareerPath")]
        public virtual Project Project { get; set; }
        [ForeignKey("RoleId")]
        [InverseProperty("ProjectCareerPath")]
        public virtual EmployeeType Role { get; set; }
        [ForeignKey("TeamId")]
        [InverseProperty("ProjectCareerPath")]
        public virtual Team Team { get; set; }
        [InverseProperty("CareerPath")]
        public virtual ICollection<ProjectPosition> ProjectPosition { get; set; }
    }
}
