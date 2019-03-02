using System;
using System.Collections.Generic;

namespace Evoflare.API.Models
{
    public partial class Organization
    {
        public Organization()
        {
            Employee = new HashSet<Employee>();
            EmployeeEvaluation = new HashSet<EmployeeEvaluation>();
            EmployeeRelations = new HashSet<EmployeeRelations>();
            Team = new HashSet<Team>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Employee> Employee { get; set; }
        public virtual ICollection<EmployeeEvaluation> EmployeeEvaluation { get; set; }
        public virtual ICollection<EmployeeRelations> EmployeeRelations { get; set; }
        public virtual ICollection<Team> Team { get; set; }
    }
}
