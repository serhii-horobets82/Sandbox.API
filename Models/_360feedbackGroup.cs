using System;
using System.Collections.Generic;

namespace Evoflare.API.Models
{
    public partial class _360feedbackGroup
    {
        public _360feedbackGroup()
        {
            _360question = new HashSet<_360question>();
        }

        public int Id { get; set; }
        public string Type { get; set; }

        public virtual ICollection<_360question> _360question { get; set; }
    }
}
