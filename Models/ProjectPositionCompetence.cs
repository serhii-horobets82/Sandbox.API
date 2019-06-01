using System;
using System.Collections.Generic;

namespace Evoflare.API.Models
{
    public partial class ProjectPositionCompetence
    {
        public int Id { get; set; }
        public int RoleGradeId { get; set; }
        public int ProjectPositionId { get; set; }
        public string CompetenceId { get; set; }
        public int CompetenceLevelId { get; set; }

        public virtual EcfCompetence Competence { get; set; }
        public virtual EcfCompetenceLevel CompetenceLevel { get; set; }
        public virtual ProjectPosition ProjectPosition { get; set; }
        public virtual RoleGrade RoleGrade { get; set; }
    }
}
