﻿using System;
using System.Collections.Generic;

namespace Evoflare.API.Models
{
    public partial class _360evaluation
    {
        public int Id { get; set; }
        public int EvaluationId { get; set; }
        public int QuestionId { get; set; }
        public int FeedbackMarkId { get; set; }
        public int OrganizationId { get; set; }

        public virtual _360employeeEvaluation Evaluation { get; set; }
        public virtual _360feedbackMark FeedbackMark { get; set; }
        public virtual Organization Organization { get; set; }
        public virtual _360questionarie Question { get; set; }
    }
}
