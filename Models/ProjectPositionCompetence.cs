using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Evoflare.API.Models
{
    public partial class ProjectPositionCompetence
    {
        public int Id { get; set; }
        public int RoleGradeId { get; set; }
        public int ProjectPositionId { get; set; }
        [Required]
        [StringLength(3)]
        public string CompetenceId { get; set; }
        public int CompetenceLevelId { get; set; }

        [ForeignKey("CompetenceId")]
        [InverseProperty("ProjectPositionCompetence")]
        public virtual EcfCompetence Competence { get; set; }
        [ForeignKey("CompetenceLevelId")]
        [InverseProperty("ProjectPositionCompetence")]
        public virtual EcfCompetenceLevel CompetenceLevel { get; set; }
        [ForeignKey("ProjectPositionId")]
        [InverseProperty("ProjectPositionCompetence")]
        public virtual ProjectPosition ProjectPosition { get; set; }
        [ForeignKey("RoleGradeId")]
        [InverseProperty("ProjectPositionCompetence")]
        public virtual RoleGrade RoleGrade { get; set; }
    }
}
