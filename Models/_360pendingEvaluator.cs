using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Evoflare.API.Models
{
    [Table("360PendingEvaluator")]
    public partial class _360pendingEvaluator
    {
        public int Id { get; set; }
        [Column("360EmployeeEvaluationId")]
        public int _360employeeEvaluationId { get; set; }
        public int EvaluatorId { get; set; }
        public int Action { get; set; }
        public int OrganizationId { get; set; }

        [ForeignKey("EvaluatorId")]
        [InverseProperty("_360pendingEvaluator")]
        public virtual Employee Evaluator { get; set; }
        [ForeignKey("OrganizationId")]
        [InverseProperty("_360pendingEvaluator")]
        public virtual Organization Organization { get; set; }
        [ForeignKey("_360employeeEvaluationId")]
        [InverseProperty("_360pendingEvaluator")]
        public virtual _360employeeEvaluation _360employeeEvaluation { get; set; }
    }
}
