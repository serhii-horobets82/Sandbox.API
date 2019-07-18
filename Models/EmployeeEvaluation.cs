using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Evoflare.API.Models
{
    public partial class EmployeeEvaluation
    {
        public EmployeeEvaluation()
        {
            EcfEmployeeEvaluation = new HashSet<EcfEmployeeEvaluation>();
            _360employeeEvaluation = new HashSet<_360employeeEvaluation>();
        }

        public int Id { get; set; }
        public int EmployeeId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime StartDate { get; set; }
        public int StartedById { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? EndDate { get; set; }
        public int? EndedById { get; set; }
        public int OrganizationId { get; set; }
        public bool Archived { get; set; }

        [ForeignKey("EmployeeId")]
        [InverseProperty("EmployeeEvaluationEmployee")]
        public virtual Employee Employee { get; set; }
        [ForeignKey("EndedById")]
        [InverseProperty("EmployeeEvaluationEndedBy")]
        public virtual Employee EndedBy { get; set; }
        [ForeignKey("OrganizationId")]
        [InverseProperty("EmployeeEvaluation")]
        public virtual Organization Organization { get; set; }
        [ForeignKey("StartedById")]
        [InverseProperty("EmployeeEvaluationStartedBy")]
        public virtual Employee StartedBy { get; set; }
        [InverseProperty("Evaluation")]
        public virtual ICollection<EcfEmployeeEvaluation> EcfEmployeeEvaluation { get; set; }
        [InverseProperty("Evaluation")]
        public virtual ICollection<_360employeeEvaluation> _360employeeEvaluation { get; set; }
    }
}
