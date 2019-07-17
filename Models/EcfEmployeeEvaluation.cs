using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Evoflare.API.Models
{
    public partial class EcfEmployeeEvaluation
    {
        public EcfEmployeeEvaluation()
        {
            EcfEvaluationResult = new HashSet<EcfEvaluationResult>();
        }

        public int Id { get; set; }
        public int EvaluationId { get; set; }
        public int EvaluatorId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime StartDate { get; set; }
        public int StartById { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? EndDate { get; set; }
        public int? EndById { get; set; }
        public int OrganizationId { get; set; }

        [ForeignKey("EndById")]
        [InverseProperty("EcfEmployeeEvaluationEndBy")]
        public virtual Employee EndBy { get; set; }
        [ForeignKey("EvaluationId")]
        [InverseProperty("EcfEmployeeEvaluation")]
        public virtual EmployeeEvaluation Evaluation { get; set; }
        [ForeignKey("EvaluatorId")]
        [InverseProperty("EcfEmployeeEvaluationEvaluator")]
        public virtual Employee Evaluator { get; set; }
        [ForeignKey("OrganizationId")]
        [InverseProperty("EcfEmployeeEvaluation")]
        public virtual Organization Organization { get; set; }
        [ForeignKey("StartById")]
        [InverseProperty("EcfEmployeeEvaluationStartBy")]
        public virtual Employee StartBy { get; set; }
        [InverseProperty("Evaluation")]
        public virtual ICollection<EcfEvaluationResult> EcfEvaluationResult { get; set; }
    }
}
