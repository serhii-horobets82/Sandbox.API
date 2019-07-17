using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Evoflare.API.Models
{
    public partial class ProjectPosition
    {
        public ProjectPosition()
        {
            ProjectPositionCompetence = new HashSet<ProjectPositionCompetence>();
        }

        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int CareerPathId { get; set; }
        public int RoleGradeId { get; set; }
        public int OrganizationId { get; set; }

        [ForeignKey("CareerPathId")]
        [InverseProperty("ProjectPosition")]
        public virtual ProjectCareerPath CareerPath { get; set; }
        [ForeignKey("OrganizationId")]
        [InverseProperty("ProjectPosition")]
        public virtual Organization Organization { get; set; }
        [ForeignKey("ProjectId")]
        [InverseProperty("ProjectPosition")]
        public virtual Project Project { get; set; }
        [ForeignKey("RoleGradeId")]
        [InverseProperty("ProjectPosition")]
        public virtual RoleGrade RoleGrade { get; set; }
        [InverseProperty("ProjectPosition")]
        public virtual ICollection<ProjectPositionCompetence> ProjectPositionCompetence { get; set; }
    }
}
