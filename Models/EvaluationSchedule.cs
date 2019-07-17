using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Evoflare.API.Models
{
    public partial class EvaluationSchedule
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime EvaluationDate { get; set; }
        public bool Archived { get; set; }
        public int OrganizationId { get; set; }

        [ForeignKey("EmployeeId")]
        [InverseProperty("EvaluationSchedule")]
        public virtual Employee Employee { get; set; }
        [ForeignKey("OrganizationId")]
        [InverseProperty("EvaluationSchedule")]
        public virtual Organization Organization { get; set; }
    }
}
