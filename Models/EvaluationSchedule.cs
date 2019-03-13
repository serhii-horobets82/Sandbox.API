using System;
using System.Collections.Generic;

namespace Evoflare.API.Models
{
    public partial class EvaluationSchedule
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public DateTime EvaluationDate { get; set; }
        public bool Archived { get; set; }
        public int OrganizationId { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual Organization Organization { get; set; }
    }
}
