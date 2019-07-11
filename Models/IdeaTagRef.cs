using System;
using System.Collections.Generic;

namespace Evoflare.API.Models
{
    public partial class IdeaTagRef
    {
        public int Id { get; set; }
        public int IdeaId { get; set; }
        public int TagId { get; set; }

        public virtual Idea Idea { get; set; }
        public virtual IdeaTag Tag { get; set; }
    }
}
