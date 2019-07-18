using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [Required]
        [StringLength(200)]
        public string Name { get; set; }
        [StringLength(200)]
        public string Company { get; set; }
        [StringLength(200)]
        public string Technology { get; set; }
        [StringLength(200)]
        public string Stack { get; set; }
        [StringLength(200)]
        public string CertificationLevel { get; set; }

        [InverseProperty("Certificate")]
        public virtual ICollection<CompetenceCertificate> CompetenceCertificate { get; set; }
        [InverseProperty("Certificate")]
        public virtual ICollection<Pdp> Pdp { get; set; }
    }
}
