using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [Required]
        public string Comment { get; set; }
        public int CreatedById { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }
        public int? ParentCommentId { get; set; }
        public int OrganizationId { get; set; }

        [ForeignKey("CreatedById")]
        [InverseProperty("IdeaComment")]
        public virtual Employee CreatedBy { get; set; }
        [ForeignKey("IdeaId")]
        [InverseProperty("IdeaComment")]
        public virtual Idea Idea { get; set; }
        [ForeignKey("OrganizationId")]
        [InverseProperty("IdeaComment")]
        public virtual Organization Organization { get; set; }
        [ForeignKey("ParentCommentId")]
        [InverseProperty("InverseParentComment")]
        public virtual IdeaComment ParentComment { get; set; }
        [InverseProperty("ParentComment")]
        public virtual ICollection<IdeaComment> InverseParentComment { get; set; }
    }
}
