using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public int CreatedById { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }
        public int? Price { get; set; }
        public int OrganizationId { get; set; }

        [ForeignKey("CreatedById")]
        [InverseProperty("Idea")]
        public virtual Employee CreatedBy { get; set; }
        [ForeignKey("OrganizationId")]
        [InverseProperty("Idea")]
        public virtual Organization Organization { get; set; }
        [InverseProperty("Idea")]
        public virtual ICollection<IdeaComment> IdeaComment { get; set; }
        [InverseProperty("Idea")]
        public virtual ICollection<IdeaLike> IdeaLike { get; set; }
        [InverseProperty("Idea")]
        public virtual ICollection<IdeaTagRef> IdeaTagRef { get; set; }
        [InverseProperty("Idea")]
        public virtual ICollection<IdeaView> IdeaView { get; set; }
    }
}
