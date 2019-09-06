using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Evoflare.API.Models
{
    [Table("360QuestionnarieStatement")]
    public partial class _360questionnarieStatement
    {
        public _360questionnarieStatement()
        {
            _360evaluationResult = new HashSet<_360evaluationResult>();
        }

        public int Id { get; set; }
        public int QuestionnarieId { get; set; }
        public int Mark { get; set; }
        [Required]
        public string Text { get; set; }

        [ForeignKey("QuestionnarieId")]
        [InverseProperty("_360questionnarieStatement")]
        public virtual _360questionnarie Questionnarie { get; set; }
        [InverseProperty("_360questionnarieStatement")]
        public virtual ICollection<_360evaluationResult> _360evaluationResult { get; set; }
    }
}
