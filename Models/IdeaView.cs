using System;
using System.Collections.Generic;

namespace Evoflare.API.Models
{
    public partial class IdeaView
    {
        public int Id { get; set; }
        public int IdeaId { get; set; }
        public int EmployeeId { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual Idea Idea { get; set; }
    }
}
