using System;
using System.Collections.Generic;

namespace Evoflare.API.Models
{
    public partial class _360questionarie
    {
        public _360questionarie()
        {
            _360evaluation = new HashSet<_360evaluation>();
            _360questionToMark = new HashSet<_360questionToMark>();
        }

        public int Id { get; set; }
        public string Text { get; set; }
        public int _360feedbackGroupId { get; set; }
        public int OrganizationId { get; set; }

        public virtual Organization Organization { get; set; }
        public virtual _360feedbackGroup _360feedbackGroup { get; set; }
        public virtual ICollection<_360evaluation> _360evaluation { get; set; }
        public virtual ICollection<_360questionToMark> _360questionToMark { get; set; }
    }
}
