using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Evoflare.API.Models
{
    public partial class EcfEvaluationResult
    {
        public int Id { get; set; }
        public int EvaluationId { get; set; }
        [Required]
        [StringLength(3)]
        public string Competence { get; set; }
        public int? CompetenceLevel { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Date { get; set; }

        [ForeignKey("Competence")]
        [InverseProperty("EcfEvaluationResult")]
        public virtual EcfCompetence CompetenceNavigation { get; set; }
        [ForeignKey("EvaluationId")]
        [InverseProperty("EcfEvaluationResult")]
        public virtual EcfEmployeeEvaluation Evaluation { get; set; }
    }
}
