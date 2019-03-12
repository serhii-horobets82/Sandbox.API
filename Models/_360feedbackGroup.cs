using System;
using System.Collections.Generic;

namespace Evoflare.API.Models
{
    public partial class _360feedbackGroup
    {
        public _360feedbackGroup()
        {
            _360employeeEvaluation = new HashSet<_360employeeEvaluation>();
            _360questionarie = new HashSet<_360questionarie>();
        }

        public int Id { get; set; }
        public string Type { get; set; }

        public virtual ICollection<_360employeeEvaluation> _360employeeEvaluation { get; set; }
        public virtual ICollection<_360questionarie> _360questionarie { get; set; }
    }
}
