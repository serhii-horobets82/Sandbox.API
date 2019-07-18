using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Evoflare.API.Models
{
    [Table("360Evaluation")]
    public partial class _360evaluation
    {
        public int Id { get; set; }
        public int EvaluationId { get; set; }
        public int QuestionId { get; set; }
        public int FeedbackMarkId { get; set; }
        public int OrganizationId { get; set; }

        [ForeignKey("EvaluationId")]
        [InverseProperty("_360evaluation")]
        public virtual _360employeeEvaluation Evaluation { get; set; }
        [ForeignKey("FeedbackMarkId")]
        [InverseProperty("_360evaluation")]
        public virtual _360feedbackMark FeedbackMark { get; set; }
        [ForeignKey("OrganizationId")]
        [InverseProperty("_360evaluation")]
        public virtual Organization Organization { get; set; }
        [ForeignKey("QuestionId")]
        [InverseProperty("_360evaluation")]
        public virtual _360questionarie Question { get; set; }
    }
}
