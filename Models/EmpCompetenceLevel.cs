using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Evoflare.API.Models
{
    public partial class EmpCompetenceLevel
    {
        public EmpCompetenceLevel()
        {
            CompetenceCertificate = new HashSet<CompetenceCertificate>();
            ProjectPositionCompetence = new HashSet<ProjectPositionCompetence>();
            RoleGradeCompetence = new HashSet<RoleGradeCompetence>();
        }

        public int Id { get; set; }
        public int CompetenceId { get; set; }
        public int Level { get; set; }
        [Required]
        [StringLength(600)]
        public string Description { get; set; }

        [ForeignKey("CompetenceId")]
        [InverseProperty("EmpCompetenceLevel")]
        public virtual EmpCompetence Competence { get; set; }
        [InverseProperty("CompetenceLevel")]
        public virtual ICollection<CompetenceCertificate> CompetenceCertificate { get; set; }
        [InverseProperty("CompetenceLevel")]
        public virtual ICollection<ProjectPositionCompetence> ProjectPositionCompetence { get; set; }
        [InverseProperty("CompetenceLevel")]
        public virtual ICollection<RoleGradeCompetence> RoleGradeCompetence { get; set; }
    }
}
