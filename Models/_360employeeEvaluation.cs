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
            _360evaluation = new HashSet<_360evaluation>();
            _360evaluationComment = new HashSet<_360evaluationComment>();
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
        [Column("360FeedbackGroupId")]
        public int _360feedbackGroupId { get; set; }
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
        [ForeignKey("_360feedbackGroupId")]
        [InverseProperty("_360employeeEvaluation")]
        public virtual _360feedbackGroup _360feedbackGroup { get; set; }
        [InverseProperty("Evaluation")]
        public virtual ICollection<_360evaluation> _360evaluation { get; set; }
        [InverseProperty("Evaluation")]
        public virtual ICollection<_360evaluationComment> _360evaluationComment { get; set; }
        [InverseProperty("_360employeeEvaluation")]
        public virtual ICollection<_360pendingEvaluator> _360pendingEvaluator { get; set; }
    }
}
