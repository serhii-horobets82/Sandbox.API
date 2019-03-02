using System;
using System.Collections.Generic;

namespace Evoflare.API.Models
{
    public partial class EcfCompetence
    {
        public EcfCompetence()
        {
            EcfCompetenceLevel = new HashSet<EcfCompetenceLevel>();
            EcfEvaluation = new HashSet<EcfEvaluation>();
            EcfRoleCompetence = new HashSet<EcfRoleCompetence>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Summary { get; set; }

        public virtual ICollection<EcfCompetenceLevel> EcfCompetenceLevel { get; set; }
        public virtual ICollection<EcfEvaluation> EcfEvaluation { get; set; }
        public virtual ICollection<EcfRoleCompetence> EcfRoleCompetence { get; set; }
    }
}
