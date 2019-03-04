using System;
using System.Collections.Generic;

namespace Evoflare.API.Models
{
    public partial class _360feedbackMark
    {
        public _360feedbackMark()
        {
            _360evaluation = new HashSet<_360evaluation>();
        }

        public int Id { get; set; }
        public int Mark { get; set; }
        public string Title { get; set; }

        public virtual ICollection<_360evaluation> _360evaluation { get; set; }
    }
}
