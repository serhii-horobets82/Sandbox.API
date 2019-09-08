using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Evoflare.API.Models
{
    public partial class EmpCompetence
    {
        public EmpCompetence()
        {
            CompetenceCertificate = new HashSet<CompetenceCertificate>();
            EmpCompetenceLevel = new HashSet<EmpCompetenceLevel>();
            EmpRoleCompetence = new HashSet<EmpRoleCompetence>();
            ProjectPositionCompetence = new HashSet<ProjectPositionCompetence>();
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
        [InverseProperty("EmpCompetence")]
        public virtual EmpCompetenceArea CompetenceArea { get; set; }
        [InverseProperty("Competence")]
        public virtual ICollection<CompetenceCertificate> CompetenceCertificate { get; set; }
        [InverseProperty("Competence")]
        public virtual ICollection<EmpCompetenceLevel> EmpCompetenceLevel { get; set; }
        [InverseProperty("Competence")]
        public virtual ICollection<EmpRoleCompetence> EmpRoleCompetence { get; set; }
        [InverseProperty("Competence")]
        public virtual ICollection<ProjectPositionCompetence> ProjectPositionCompetence { get; set; }
        [InverseProperty("Competence")]
        public virtual ICollection<RoleGradeCompetence> RoleGradeCompetence { get; set; }
    }
}
