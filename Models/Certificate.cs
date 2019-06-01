using System;
using System.Collections.Generic;

namespace Evoflare.API.Models
{
    public partial class Certificate
    {
        public Certificate()
        {
            CompetenceCertificate = new HashSet<CompetenceCertificate>();
            Pdp = new HashSet<Pdp>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Company { get; set; }
        public string Technology { get; set; }
        public string Stack { get; set; }
        public string CertificationLevel { get; set; }

        public virtual ICollection<CompetenceCertificate> CompetenceCertificate { get; set; }
        public virtual ICollection<Pdp> Pdp { get; set; }
    }
}
