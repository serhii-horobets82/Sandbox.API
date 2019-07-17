using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Evoflare.API.Models
{
    public partial class IdeaTagRef
    {
        public int Id { get; set; }
        public int IdeaId { get; set; }
        public int TagId { get; set; }

        [ForeignKey("IdeaId")]
        [InverseProperty("IdeaTagRef")]
        public virtual Idea Idea { get; set; }
        [ForeignKey("TagId")]
        [InverseProperty("IdeaTagRef")]
        public virtual IdeaTag Tag { get; set; }
    }
}
