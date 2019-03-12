using System;
using System.Collections.Generic;

namespace Evoflare.API.Models
{
    public partial class _360questionToMark
    {
        public _360questionToMark()
        {
            _360question = new HashSet<_360question>();
        }

        public int Id { get; set; }
        public int QuestionId { get; set; }
        public int MarkId { get; set; }
        public int OrganizationId { get; set; }

        public virtual Organization Organization { get; set; }
        public virtual _360questionarie Question { get; set; }
        public virtual ICollection<_360question> _360question { get; set; }
    }
}
