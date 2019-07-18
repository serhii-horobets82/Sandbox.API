using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Evoflare.API.Models
{
    [Table("360EvaluationComment")]
    public partial class _360evaluationComment
    {
        public int Id { get; set; }
        public int EvaluationId { get; set; }
        public string StartDoing { get; set; }
        public string StopDoing { get; set; }
        public string OtherComments { get; set; }
        public int OrganizationId { get; set; }

        [ForeignKey("EvaluationId")]
        [InverseProperty("_360evaluationComment")]
        public virtual _360employeeEvaluation Evaluation { get; set; }
        [ForeignKey("OrganizationId")]
        [InverseProperty("_360evaluationComment")]
        public virtual Organization Organization { get; set; }
    }
}
