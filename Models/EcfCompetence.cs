using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Evoflare.API.Models
{
    public partial class EcfCompetence
    {
        public EcfCompetence()
        {
            CompetenceCertificate = new HashSet<CompetenceCertificate>();
            EcfCompetenceLevel = new HashSet<EcfCompetenceLevel>();
            EcfEvaluationResult = new HashSet<EcfEvaluationResult>();
            EcfRoleCompetence = new HashSet<EcfRoleCompetence>();
            ProjectPositionCompetence = new HashSet<ProjectPositionCompetence>();
            RoleGradeCompetence = new HashSet<RoleGradeCompetence>();
        }

        [StringLength(3)]
        public string Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public string Summary { get; set; }

        [InverseProperty("Competence")]
        public virtual ICollection<CompetenceCertificate> CompetenceCertificate { get; set; }
        [InverseProperty("Competence")]
        public virtual ICollection<EcfCompetenceLevel> EcfCompetenceLevel { get; set; }
        [InverseProperty("CompetenceNavigation")]
        public virtual ICollection<EcfEvaluationResult> EcfEvaluationResult { get; set; }
        [InverseProperty("Competence")]
        public virtual ICollection<EcfRoleCompetence> EcfRoleCompetence { get; set; }
        [InverseProperty("Competence")]
        public virtual ICollection<ProjectPositionCompetence> ProjectPositionCompetence { get; set; }
        [InverseProperty("Competence")]
        public virtual ICollection<RoleGradeCompetence> RoleGradeCompetence { get; set; }
    }
}
