using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Evoflare.API.Models
{
    public partial class Competence
    {
        public Competence()
        {
            CompetenceCertificate = new HashSet<CompetenceCertificate>();
            CompetenceLevel = new HashSet<CompetenceLevel>();
            ProjectPositionCompetence = new HashSet<ProjectPositionCompetence>();
            RoleCompetence = new HashSet<RoleCompetence>();
            RoleGradeCompetence = new HashSet<RoleGradeCompetence>();
        }

        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public int CompetenceAreaId { get; set; }
        [Required]
        [StringLength(800)]
        public string Summary { get; set; }

        [ForeignKey("CompetenceAreaId")]
        [InverseProperty("Competence")]
        public virtual CompetenceArea CompetenceArea { get; set; }
        [InverseProperty("Competence")]
        public virtual ICollection<CompetenceCertificate> CompetenceCertificate { get; set; }
        [InverseProperty("Competence")]
        public virtual ICollection<CompetenceLevel> CompetenceLevel { get; set; }
        [InverseProperty("Competence")]
        public virtual ICollection<ProjectPositionCompetence> ProjectPositionCompetence { get; set; }
        [InverseProperty("Competence")]
        public virtual ICollection<RoleCompetence> RoleCompetence { get; set; }
        [InverseProperty("Competence")]
        public virtual ICollection<RoleGradeCompetence> RoleGradeCompetence { get; set; }
    }
}
