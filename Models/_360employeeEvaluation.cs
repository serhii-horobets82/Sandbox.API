using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Evoflare.API.Models
{
    [Table("360EmployeeEvaluation")]
    public partial class _360employeeEvaluation
    {
        public _360employeeEvaluation()
        {
            _360evaluationResult = new HashSet<_360evaluationResult>();
            _360pendingEvaluator = new HashSet<_360pendingEvaluator>();
        }

        public int Id { get; set; }
        public int EvaluatorEmployeeId { get; set; }
        public int EvaluationId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime StartDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? EndDate { get; set; }
        public int OrganizationId { get; set; }
        public string StartDoing { get; set; }
        public string StopDoing { get; set; }
        public string OtherComments { get; set; }

        [ForeignKey("EvaluationId")]
        [InverseProperty("_360employeeEvaluation")]
        public virtual EmployeeEvaluation Evaluation { get; set; }
        [ForeignKey("EvaluatorEmployeeId")]
        [InverseProperty("_360employeeEvaluation")]
        public virtual Employee EvaluatorEmployee { get; set; }
        [ForeignKey("OrganizationId")]
        [InverseProperty("_360employeeEvaluation")]
        public virtual Organization Organization { get; set; }
        [InverseProperty("Evaluation")]
        public virtual ICollection<_360evaluationResult> _360evaluationResult { get; set; }
        [InverseProperty("_360employeeEvaluation")]
        public virtual ICollection<_360pendingEvaluator> _360pendingEvaluator { get; set; }
    }
}
