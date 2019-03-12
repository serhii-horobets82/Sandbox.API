using System;
using System.Collections.Generic;

namespace Evoflare.API.Models
{
    public partial class _360pendingEvaluator
    {
        public int Id { get; set; }
        public int _360employeeEvaluationId { get; set; }
        public int EvaluatorId { get; set; }
        public int Action { get; set; }
        public int OrganizationId { get; set; }

        public virtual Employee Evaluator { get; set; }
        public virtual Organization Organization { get; set; }
        public virtual _360employeeEvaluation _360employeeEvaluation { get; set; }
    }
}
