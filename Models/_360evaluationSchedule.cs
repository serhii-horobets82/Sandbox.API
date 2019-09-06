using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Evoflare.API.Models
{
    [Table("360EvaluationSchedule")]
    public partial class _360evaluationSchedule
    {
        public int Id { get; set; }
        public int PeriodMonths { get; set; }
        public int EvaluationWindowMonths { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime StartDate { get; set; }
    }
}
