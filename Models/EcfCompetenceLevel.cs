using System;
using System.Collections.Generic;

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
        public string CompetenceId { get; set; }
        public int Level { get; set; }
        public string Description { get; set; }

        public virtual EcfCompetence Competence { get; set; }
        public virtual ICollection<CompetenceCertificate> CompetenceCertificate { get; set; }
        public virtual ICollection<ProjectPositionCompetence> ProjectPositionCompetence { get; set; }
        public virtual ICollection<RoleGradeCompetence> RoleGradeCompetence { get; set; }
    }
}
