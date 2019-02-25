using System;
using System.Collections.Generic;

namespace Evoflare.API.Models
{
    public partial class EmployeeEvaluation
    {
        public EmployeeEvaluation()
        {
            EcfEvaluation = new HashSet<EcfEvaluation>();
        }

        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int? Ecf { get; set; }
        public int? _360degree { get; set; }
        public DateTime DateTime { get; set; }

        public virtual ICollection<EcfEvaluation> EcfEvaluation { get; set; }
    }
}
