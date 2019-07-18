using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Evoflare.API.Models
{
    [Table("360Questionarie")]
    public partial class _360questionarie
    {
        public _360questionarie()
        {
            _360evaluation = new HashSet<_360evaluation>();
            _360questionToMark = new HashSet<_360questionToMark>();
        }

        public int Id { get; set; }
        [Required]
        [StringLength(250)]
        public string Text { get; set; }
        [Column("360FeedbackGroupId")]
        public int _360feedbackGroupId { get; set; }
        public int OrganizationId { get; set; }

        [ForeignKey("OrganizationId")]
        [InverseProperty("_360questionarie")]
        public virtual Organization Organization { get; set; }
        [ForeignKey("_360feedbackGroupId")]
        [InverseProperty("_360questionarie")]
        public virtual _360feedbackGroup _360feedbackGroup { get; set; }
        [InverseProperty("Question")]
        public virtual ICollection<_360evaluation> _360evaluation { get; set; }
        [InverseProperty("Question")]
        public virtual ICollection<_360questionToMark> _360questionToMark { get; set; }
    }
}
