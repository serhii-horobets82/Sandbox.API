using System;
using System.Collections.Generic;

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
        public DateTime StartDate { get; set; }
        public int StartById { get; set; }
        public DateTime? EndDate { get; set; }
        public int? EndById { get; set; }
        public int OrganizationId { get; set; }

        public virtual Employee EndBy { get; set; }
        public virtual EmployeeEvaluation Evaluation { get; set; }
        public virtual Employee Evaluator { get; set; }
        public virtual Organization Organization { get; set; }
        public virtual Employee StartBy { get; set; }
        public virtual ICollection<EcfEvaluationResult> EcfEvaluationResult { get; set; }
    }
}
