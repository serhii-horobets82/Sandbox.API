using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Evoflare.API.Models
{
    public partial class IdeaLike
    {
        public int Id { get; set; }
        public int IdeaId { get; set; }
        public int EmployeeId { get; set; }

        [ForeignKey("EmployeeId")]
        [InverseProperty("IdeaLike")]
        public virtual Employee Employee { get; set; }
        [ForeignKey("IdeaId")]
        [InverseProperty("IdeaLike")]
        public virtual Idea Idea { get; set; }
    }
}
