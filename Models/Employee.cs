using System;
using System.Collections.Generic;

namespace Evoflare.API.Models
{
    public partial class Employee
    {
        public Employee()
        {
            EmployeeRelationsEmployee = new HashSet<EmployeeRelations>();
            EmployeeRelationsManager = new HashSet<EmployeeRelations>();
            PositionCreatedByNavigation = new HashSet<Position>();
            PositionUpdatedByNavigation = new HashSet<Position>();
        }

        public int Id { get; set; }
        public string UserId { get; set; }
        public bool IsManager { get; set; }
        public int EmployeeTypeId { get; set; }
        public int OrganizationId { get; set; }
        public string NameTemp { get; set; }

        public virtual EmployeeType EmployeeType { get; set; }
        public virtual Organization Organization { get; set; }
        public virtual ICollection<EmployeeRelations> EmployeeRelationsEmployee { get; set; }
        public virtual ICollection<EmployeeRelations> EmployeeRelationsManager { get; set; }
        public virtual ICollection<Position> PositionCreatedByNavigation { get; set; }
        public virtual ICollection<Position> PositionUpdatedByNavigation { get; set; }
    }
}
