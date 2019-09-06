using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Evoflare.API.Models
{
    [Table("360EvaluationResult")]
    public partial class _360evaluationResult
    {
        public int Id { get; set; }
        public int EvaluationId { get; set; }
        [Column("360QuestionnarieStatementId")]
        public int _360questionnarieStatementId { get; set; }

        [ForeignKey("EvaluationId")]
        [InverseProperty("_360evaluationResult")]
        public virtual _360employeeEvaluation Evaluation { get; set; }
        [ForeignKey("_360questionnarieStatementId")]
        [InverseProperty("_360evaluationResult")]
        public virtual _360questionnarieStatement _360questionnarieStatement { get; set; }
    }
}
