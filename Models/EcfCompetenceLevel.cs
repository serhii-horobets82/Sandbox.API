using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Evoflare.API.Models
{
    public partial class EcfCompetenceLevel
    {
        public EcfCompetenceLevel()
        {
            CompetenceCertificate = new HashSet<CompetenceCertificate>();
            ProjectPositionCompetence = new HashSet<ProjectPositionCompetence>();
            RoleGradeCompetence = new HashSet<RoleGradeCompetence>();
        }

        public int Id { get; set; }
        [Required]
        [StringLength(3)]
        public string CompetenceId { get; set; }
        public int Level { get; set; }
        public string Description { get; set; }

        [ForeignKey("CompetenceId")]
        [InverseProperty("EcfCompetenceLevel")]
        public virtual EcfCompetence Competence { get; set; }
        [InverseProperty("CompetenceLevel")]
        public virtual ICollection<CompetenceCertificate> CompetenceCertificate { get; set; }
        [InverseProperty("CompetenceLevel")]
        public virtual ICollection<ProjectPositionCompetence> ProjectPositionCompetence { get; set; }
        [InverseProperty("CompetenceLevel")]
        public virtual ICollection<RoleGradeCompetence> RoleGradeCompetence { get; set; }
    }
}
