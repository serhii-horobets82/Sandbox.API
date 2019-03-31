using System;
using System.Collections.Generic;

namespace Evoflare.API.Models
{
    public partial class _360evaluationComment
    {
        public int Id { get; set; }
        public int EvaluationId { get; set; }
        public string StartDoing { get; set; }
        public string StopDoing { get; set; }
        public string OtherComments { get; set; }
        public int OrganizationId { get; set; }

        public virtual _360employeeEvaluation Evaluation { get; set; }
        public virtual Organization Organization { get; set; }
    }
}
