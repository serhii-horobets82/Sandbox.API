using System;
using System.Collections.Generic;

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

        public string Id { get; set; }
        public string Name { get; set; }
        public string Summary { get; set; }

        public virtual ICollection<CompetenceCertificate> CompetenceCertificate { get; set; }
        public virtual ICollection<EcfCompetenceLevel> EcfCompetenceLevel { get; set; }
        public virtual ICollection<EcfEvaluationResult> EcfEvaluationResult { get; set; }
        public virtual ICollection<EcfRoleCompetence> EcfRoleCompetence { get; set; }
        public virtual ICollection<ProjectPositionCompetence> ProjectPositionCompetence { get; set; }
        public virtual ICollection<RoleGradeCompetence> RoleGradeCompetence { get; set; }
    }
}
