using System;
using System.Collections.Generic;

namespace Evoflare.API.Models
{
    public partial class EcfEvaluationResult
    {
        public int Id { get; set; }
        public int EvaluationId { get; set; }
        public string Competence { get; set; }
        public int? CompetenceLevel { get; set; }
        public DateTime? Date { get; set; }

        public virtual EcfCompetence CompetenceNavigation { get; set; }
        public virtual EcfEmployeeEvaluation Evaluation { get; set; }
    }
}
