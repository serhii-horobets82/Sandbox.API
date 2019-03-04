using System;
using System.Collections.Generic;

namespace Evoflare.API.Models
{
    public partial class _360question
    {
        public _360question()
        {
            _360evaluation = new HashSet<_360evaluation>();
        }

        public int Id { get; set; }
        public string Text { get; set; }
        public int _360feedbackGroupId { get; set; }

        public virtual _360feedbackGroup _360feedbackGroup { get; set; }
        public virtual ICollection<_360evaluation> _360evaluation { get; set; }
    }
}
