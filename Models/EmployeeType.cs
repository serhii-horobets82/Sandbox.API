using System;
using System.Collections.Generic;

namespace Evoflare.API.Models
{
    public partial class EmployeeType
    {
        public EmployeeType()
        {
            Employee = new HashSet<Employee>();
            RoleGrade = new HashSet<RoleGrade>();
        }

        public int Id { get; set; }
        public string Type { get; set; }
        public int OrganizationId { get; set; }

        public virtual Organization Organization { get; set; }
        public virtual ICollection<Employee> Employee { get; set; }
        public virtual ICollection<RoleGrade> RoleGrade { get; set; }
    }
}
