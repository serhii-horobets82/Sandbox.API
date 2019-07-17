using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Evoflare.API.Models
{
    [Table("360FeedbackMark")]
    public partial class _360feedbackMark
    {
        public _360feedbackMark()
        {
            _360evaluation = new HashSet<_360evaluation>();
        }

        public int Id { get; set; }
        public int Mark { get; set; }
        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        [InverseProperty("FeedbackMark")]
        public virtual ICollection<_360evaluation> _360evaluation { get; set; }
    }
}
