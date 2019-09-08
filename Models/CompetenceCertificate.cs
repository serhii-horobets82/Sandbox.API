using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Evoflare.API.Models
{
    public partial class CompetenceCertificate
    {
        public int Id { get; set; }
        public int CompetenceId { get; set; }
        public int CompetenceLevelId { get; set; }
        public int CertificateId { get; set; }
        public int OrganizationId { get; set; }

        [ForeignKey("CertificateId")]
        [InverseProperty("CompetenceCertificate")]
        public virtual Certificate Certificate { get; set; }
        [ForeignKey("CompetenceId")]
        [InverseProperty("CompetenceCertificate")]
        public virtual EmpCompetence Competence { get; set; }
        [ForeignKey("CompetenceLevelId")]
        [InverseProperty("CompetenceCertificate")]
        public virtual EmpCompetenceLevel CompetenceLevel { get; set; }
        [ForeignKey("OrganizationId")]
        [InverseProperty("CompetenceCertificate")]
        public virtual Organization Organization { get; set; }
    }
}
