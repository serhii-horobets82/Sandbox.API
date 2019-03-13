using System;
using System.Collections.Generic;

namespace Evoflare.API.Models
{
    public partial class EcfEmployeeEvaluator
    {
        public int Id { get; set; }
        public int EvaluatorId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int EvaluationId { get; set; }

        public virtual EmployeeEvaluation Evaluation { get; set; }
        public virtual Employee Evaluator { get; set; }
    }
}
