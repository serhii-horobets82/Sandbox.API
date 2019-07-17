using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Evoflare.API.Models
{
    [Table("360Question")]
    public partial class _360question
    {
        public int Id { get; set; }
        public int QuestionToMarkId { get; set; }
        [Required]
        [StringLength(250)]
        public string Question { get; set; }
        public int Order { get; set; }
        public int OrganizationId { get; set; }

        [ForeignKey("OrganizationId")]
        [InverseProperty("_360question")]
        public virtual Organization Organization { get; set; }
        [ForeignKey("QuestionToMarkId")]
        [InverseProperty("_360question")]
        public virtual _360questionToMark QuestionToMark { get; set; }
    }
}
