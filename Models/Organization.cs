using System;
using System.Collections.Generic;

namespace Evoflare.API.Models
{
    public partial class Organization
    {
        public Organization()
        {
            CompetenceCertificate = new HashSet<CompetenceCertificate>();
            CustomerContact = new HashSet<CustomerContact>();
            EcfEmployeeEvaluation = new HashSet<EcfEmployeeEvaluation>();
            Employee = new HashSet<Employee>();
            EmployeeEvaluation = new HashSet<EmployeeEvaluation>();
            EmployeeRelations = new HashSet<EmployeeRelations>();
            EmployeeType = new HashSet<EmployeeType>();
            EvaluationSchedule = new HashSet<EvaluationSchedule>();
            Pdp = new HashSet<Pdp>();
            ProjectCareerPath = new HashSet<ProjectCareerPath>();
            ProjectPosition = new HashSet<ProjectPosition>();
            RoleGrade = new HashSet<RoleGrade>();
            Team = new HashSet<Team>();
            _360employeeEvaluation = new HashSet<_360employeeEvaluation>();
            _360evaluation = new HashSet<_360evaluation>();
            _360evaluationComment = new HashSet<_360evaluationComment>();
            _360pendingEvaluator = new HashSet<_360pendingEvaluator>();
            _360question = new HashSet<_360question>();
            _360questionToMark = new HashSet<_360questionToMark>();
            _360questionarie = new HashSet<_360questionarie>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<CompetenceCertificate> CompetenceCertificate { get; set; }
        public virtual ICollection<CustomerContact> CustomerContact { get; set; }
        public virtual ICollection<EcfEmployeeEvaluation> EcfEmployeeEvaluation { get; set; }
        public virtual ICollection<Employee> Employee { get; set; }
        public virtual ICollection<EmployeeEvaluation> EmployeeEvaluation { get; set; }
        public virtual ICollection<EmployeeRelations> EmployeeRelations { get; set; }
        public virtual ICollection<EmployeeType> EmployeeType { get; set; }
        public virtual ICollection<EvaluationSchedule> EvaluationSchedule { get; set; }
        public virtual ICollection<Pdp> Pdp { get; set; }
        public virtual ICollection<ProjectCareerPath> ProjectCareerPath { get; set; }
        public virtual ICollection<ProjectPosition> ProjectPosition { get; set; }
        public virtual ICollection<RoleGrade> RoleGrade { get; set; }
        public virtual ICollection<Team> Team { get; set; }
        public virtual ICollection<_360employeeEvaluation> _360employeeEvaluation { get; set; }
        public virtual ICollection<_360evaluation> _360evaluation { get; set; }
        public virtual ICollection<_360evaluationComment> _360evaluationComment { get; set; }
        public virtual ICollection<_360pendingEvaluator> _360pendingEvaluator { get; set; }
        public virtual ICollection<_360question> _360question { get; set; }
        public virtual ICollection<_360questionToMark> _360questionToMark { get; set; }
        public virtual ICollection<_360questionarie> _360questionarie { get; set; }
    }
}
