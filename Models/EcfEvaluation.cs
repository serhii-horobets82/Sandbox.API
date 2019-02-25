using System;
using System.Collections.Generic;

namespace Evoflare.API.Models
{
    public partial class EcfEvaluation
    {
        public int Id { get; set; }
        public int EvaluationId { get; set; }
        public string Competence { get; set; }
        public int? CompetenceLevel { get; set; }

        public virtual EmployeeEvaluation Evaluation { get; set; }
    }
}
