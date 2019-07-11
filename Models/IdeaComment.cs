using System;
using System.Collections.Generic;

namespace Evoflare.API.Models
{
    public partial class IdeaComment
    {
        public IdeaComment()
        {
            InverseParentComment = new HashSet<IdeaComment>();
        }

        public int Id { get; set; }
        public int IdeaId { get; set; }
        public string Comment { get; set; }
        public int CreatedById { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ParentCommentId { get; set; }
        public int OrganizationId { get; set; }

        public virtual Employee CreatedBy { get; set; }
        public virtual Idea Idea { get; set; }
        public virtual Organization Organization { get; set; }
        public virtual IdeaComment ParentComment { get; set; }
        public virtual ICollection<IdeaComment> InverseParentComment { get; set; }
    }
}
