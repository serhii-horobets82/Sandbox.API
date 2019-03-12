using System;
using System.Collections.Generic;

namespace Evoflare.API.Models
{
    public partial class _360employeeEvaluation
    {
        public _360employeeEvaluation()
        {
            _360evaluation = new HashSet<_360evaluation>();
            _360pendingEvaluator = new HashSet<_360pendingEvaluator>();
        }

        public int Id { get; set; }
        public int EvaluatorEmployeeId { get; set; }
        public int EvaluationId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int OrganizationId { get; set; }
        public int _360feedbackGroupId { get; set; }

        public virtual EmployeeEvaluation Evaluation { get; set; }
        public virtual Employee IdNavigation { get; set; }
        public virtual Organization Organization { get; set; }
        public virtual _360feedbackGroup _360feedbackGroup { get; set; }
        public virtual ICollection<_360evaluation> _360evaluation { get; set; }
        public virtual ICollection<_360pendingEvaluator> _360pendingEvaluator { get; set; }
    }
}
