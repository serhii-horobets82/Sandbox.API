using System;
using System.Collections.Generic;

namespace Evoflare.API.Models
{
    public partial class Organization
    {
        public Organization()
        {
            CustomerContact = new HashSet<CustomerContact>();
            EcfEmployeeEvaluation = new HashSet<EcfEmployeeEvaluation>();
            Employee = new HashSet<Employee>();
            EmployeeEvaluation = new HashSet<EmployeeEvaluation>();
            EmployeeRelations = new HashSet<EmployeeRelations>();
            EvaluationSchedule = new HashSet<EvaluationSchedule>();
            Team = new HashSet<Team>();
            _360employeeEvaluation = new HashSet<_360employeeEvaluation>();
            _360evaluation = new HashSet<_360evaluation>();
            _360pendingEvaluator = new HashSet<_360pendingEvaluator>();
            _360question = new HashSet<_360question>();
            _360questionToMark = new HashSet<_360questionToMark>();
            _360questionarie = new HashSet<_360questionarie>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<CustomerContact> CustomerContact { get; set; }
        public virtual ICollection<EcfEmployeeEvaluation> EcfEmployeeEvaluation { get; set; }
        public virtual ICollection<Employee> Employee { get; set; }
        public virtual ICollection<EmployeeEvaluation> EmployeeEvaluation { get; set; }
        public virtual ICollection<EmployeeRelations> EmployeeRelations { get; set; }
        public virtual ICollection<EvaluationSchedule> EvaluationSchedule { get; set; }
        public virtual ICollection<Team> Team { get; set; }
        public virtual ICollection<_360employeeEvaluation> _360employeeEvaluation { get; set; }
        public virtual ICollection<_360evaluation> _360evaluation { get; set; }
        public virtual ICollection<_360pendingEvaluator> _360pendingEvaluator { get; set; }
        public virtual ICollection<_360question> _360question { get; set; }
        public virtual ICollection<_360questionToMark> _360questionToMark { get; set; }
        public virtual ICollection<_360questionarie> _360questionarie { get; set; }
    }
}
