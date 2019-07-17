using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Evoflare.API.Models
{
    public partial class IdeaTag
    {
        public IdeaTag()
        {
            IdeaTagRef = new HashSet<IdeaTagRef>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        [InverseProperty("Tag")]
        public virtual ICollection<IdeaTagRef> IdeaTagRef { get; set; }
    }
}
