using System;
using System.Collections.Generic;

namespace Evoflare.API.Models
{
    public partial class EcfCompetence
    {
        public EcfCompetence()
        {
            EcfCompetenceLevel = new HashSet<EcfCompetenceLevel>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Summary { get; set; }

        public virtual ICollection<EcfCompetenceLevel> EcfCompetenceLevel { get; set; }
    }
}
