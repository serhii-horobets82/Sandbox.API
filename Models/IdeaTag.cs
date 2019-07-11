using System;
using System.Collections.Generic;

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

        public virtual ICollection<IdeaTagRef> IdeaTagRef { get; set; }
    }
}
