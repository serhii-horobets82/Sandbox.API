using System;
using System.Collections.Generic;

namespace Evoflare.API.Models
{
    public partial class Idea
    {
        public Idea()
        {
            IdeaComment = new HashSet<IdeaComment>();
            IdeaLike = new HashSet<IdeaLike>();
            IdeaTagRef = new HashSet<IdeaTagRef>();
            IdeaView = new HashSet<IdeaView>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CreatedById { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? Price { get; set; }
        public int OrganizationId { get; set; }

        public virtual Employee CreatedBy { get; set; }
        public virtual Organization Organization { get; set; }
        public virtual ICollection<IdeaComment> IdeaComment { get; set; }
        public virtual ICollection<IdeaLike> IdeaLike { get; set; }
        public virtual ICollection<IdeaTagRef> IdeaTagRef { get; set; }
        public virtual ICollection<IdeaView> IdeaView { get; set; }
    }
}
