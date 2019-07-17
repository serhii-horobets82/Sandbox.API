using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Evoflare.API.Models
{
    public partial class IdeaView
    {
        public int Id { get; set; }
        public int IdeaId { get; set; }
        public int EmployeeId { get; set; }

        [ForeignKey("EmployeeId")]
        [InverseProperty("IdeaView")]
        public virtual Employee Employee { get; set; }
        [ForeignKey("IdeaId")]
        [InverseProperty("IdeaView")]
        public virtual Idea Idea { get; set; }
    }
}
