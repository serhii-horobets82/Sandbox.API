using System;
using System.Collections.Generic;

namespace Evoflare.API.Models
{
    public partial class _360employeeEvaluation
    {
        public int Id { get; set; }
        public int EvaluatorEmployeeId { get; set; }
        public int EvaluationId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public virtual EmployeeEvaluation Evaluation { get; set; }
        public virtual Employee IdNavigation { get; set; }
    }
}
