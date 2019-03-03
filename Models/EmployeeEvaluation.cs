﻿using System;
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
        public DateTime StartDate { get; set; }
        public int StartedById { get; set; }
        public DateTime? EndDate { get; set; }
        public int? EndedById { get; set; }
        public int OrganizationId { get; set; }
        public bool Archived { get; set; }
        public int? TechnicalEvaluatorId { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual Employee EndedBy { get; set; }
        public virtual Organization Organization { get; set; }
        public virtual Employee StartedBy { get; set; }
        public virtual ICollection<EcfEvaluation> EcfEvaluation { get; set; }
    }
}
