using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Evoflare.API.Models
{
    public partial class RoleGradeCompetence
    {
        public int Id { get; set; }
        public int CompetenceId { get; set; }
        public int CompetenceLevelId { get; set; }
        public int RoleGradeId { get; set; }

        [ForeignKey("CompetenceId")]
        [InverseProperty("RoleGradeCompetence")]
        public virtual EmpCompetence Competence { get; set; }
        [ForeignKey("CompetenceLevelId")]
        [InverseProperty("RoleGradeCompetence")]
        public virtual EmpCompetenceLevel CompetenceLevel { get; set; }
        [ForeignKey("RoleGradeId")]
        [InverseProperty("RoleGradeCompetence")]
        public virtual RoleGrade RoleGrade { get; set; }
    }
}
