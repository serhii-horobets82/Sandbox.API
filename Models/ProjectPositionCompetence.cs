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
        public int CompetenceId { get; set; }
        public int CompetenceLevelId { get; set; }

        [ForeignKey("CompetenceId")]
        [InverseProperty("ProjectPositionCompetence")]
        public virtual Competence Competence { get; set; }
        [ForeignKey("CompetenceLevelId")]
        [InverseProperty("ProjectPositionCompetence")]
        public virtual CompetenceLevel CompetenceLevel { get; set; }
        [ForeignKey("ProjectPositionId")]
        [InverseProperty("ProjectPositionCompetence")]
        public virtual ProjectPosition ProjectPosition { get; set; }
        [ForeignKey("RoleGradeId")]
        [InverseProperty("ProjectPositionCompetence")]
        public virtual RoleGrade RoleGrade { get; set; }
    }
}
