using System;
using System.Collections.Generic;

namespace Evoflare.API.Models
{
    public partial class RoleGradeCompetence
    {
        public int Id { get; set; }
        public string CompetenceId { get; set; }
        public int CompetenceLevelId { get; set; }
        public int RoleGradeId { get; set; }

        public virtual EcfCompetence Competence { get; set; }
        public virtual EcfCompetenceLevel CompetenceLevel { get; set; }
        public virtual RoleGrade RoleGrade { get; set; }
    }
}
