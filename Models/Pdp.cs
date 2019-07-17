using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Evoflare.API.Models
{
    public partial class Pdp
    {
        public int Id { get; set; }
        [Required]
        [StringLength(200)]
        public string Name { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? StudyStartDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ClassroomStartDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? AssessmentStartDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ExamDate { get; set; }
        public int? CertificateId { get; set; }
        public int OrganizationId { get; set; }

        [ForeignKey("CertificateId")]
        [InverseProperty("Pdp")]
        public virtual Certificate Certificate { get; set; }
        [ForeignKey("OrganizationId")]
        [InverseProperty("Pdp")]
        public virtual Organization Organization { get; set; }
    }
}
