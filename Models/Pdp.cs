using System;
using System.Collections.Generic;

namespace Evoflare.API.Models
{
    public partial class Pdp
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? StudyStartDate { get; set; }
        public DateTime? ClassroomStartDate { get; set; }
        public DateTime? AssessmentStartDate { get; set; }
        public DateTime ExamDate { get; set; }
        public int? CertificateId { get; set; }
        public int OrganizationId { get; set; }

        public virtual Certificate Certificate { get; set; }
        public virtual Organization Organization { get; set; }
    }
}
