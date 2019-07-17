using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Evoflare.API.Models
{
    [Table("360QuestionToMark")]
    public partial class _360questionToMark
    {
        public _360questionToMark()
        {
            _360question = new HashSet<_360question>();
        }

        public int Id { get; set; }
        public int QuestionId { get; set; }
        public int MarkId { get; set; }
        public int OrganizationId { get; set; }

        [ForeignKey("OrganizationId")]
        [InverseProperty("_360questionToMark")]
        public virtual Organization Organization { get; set; }
        [ForeignKey("QuestionId")]
        [InverseProperty("_360questionToMark")]
        public virtual _360questionarie Question { get; set; }
        [InverseProperty("QuestionToMark")]
        public virtual ICollection<_360question> _360question { get; set; }
    }
}
