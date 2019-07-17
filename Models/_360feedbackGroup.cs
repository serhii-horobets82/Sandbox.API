using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Evoflare.API.Models
{
    [Table("360FeedbackGroup")]
    public partial class _360feedbackGroup
    {
        public _360feedbackGroup()
        {
            _360employeeEvaluation = new HashSet<_360employeeEvaluation>();
            _360questionarie = new HashSet<_360questionarie>();
        }

        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Type { get; set; }

        [InverseProperty("_360feedbackGroup")]
        public virtual ICollection<_360employeeEvaluation> _360employeeEvaluation { get; set; }
        [InverseProperty("_360feedbackGroup")]
        public virtual ICollection<_360questionarie> _360questionarie { get; set; }
    }
}
