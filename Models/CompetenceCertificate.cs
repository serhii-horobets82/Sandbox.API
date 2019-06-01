using System;
using System.Collections.Generic;

namespace Evoflare.API.Models
{
    public partial class CompetenceCertificate
    {
        public int Id { get; set; }
        public string CompetenceId { get; set; }
        public int CompetenceLevelId { get; set; }
        public int CertificateId { get; set; }
        public int OrganizationId { get; set; }

        public virtual Certificate Certificate { get; set; }
        public virtual EcfCompetence Competence { get; set; }
        public virtual EcfCompetenceLevel CompetenceLevel { get; set; }
        public virtual Organization Organization { get; set; }
    }
}
