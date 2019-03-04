using System;
using System.Collections.Generic;

namespace Evoflare.API.Models
{
    public partial class _360evaluation
    {
        public int Id { get; set; }
        public int EvaluationId { get; set; }
        public int QuestionId { get; set; }
        public int FeedbackMarkId { get; set; }

        public virtual EmployeeEvaluation Evaluation { get; set; }
        public virtual _360feedbackMark FeedbackMark { get; set; }
        public virtual _360question Question { get; set; }
    }
}
