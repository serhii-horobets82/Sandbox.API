using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
            Idea = new HashSet<Idea>();
            IdeaComment = new HashSet<IdeaComment>();
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
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [InverseProperty("Organization")]
        public virtual ICollection<CompetenceCertificate> CompetenceCertificate { get; set; }
        [InverseProperty("Organization")]
        public virtual ICollection<CustomerContact> CustomerContact { get; set; }
        [InverseProperty("Organization")]
        public virtual ICollection<EcfEmployeeEvaluation> EcfEmployeeEvaluation { get; set; }
        [InverseProperty("Organization")]
        public virtual ICollection<Employee> Employee { get; set; }
        [InverseProperty("Organization")]
        public virtual ICollection<EmployeeEvaluation> EmployeeEvaluation { get; set; }
        [InverseProperty("Organization")]
        public virtual ICollection<EmployeeRelations> EmployeeRelations { get; set; }
        [InverseProperty("Organization")]
        public virtual ICollection<EmployeeType> EmployeeType { get; set; }
        [InverseProperty("Organization")]
        public virtual ICollection<EvaluationSchedule> EvaluationSchedule { get; set; }
        [InverseProperty("Organization")]
        public virtual ICollection<Idea> Idea { get; set; }
        [InverseProperty("Organization")]
        public virtual ICollection<IdeaComment> IdeaComment { get; set; }
        [InverseProperty("Organization")]
        public virtual ICollection<Pdp> Pdp { get; set; }
        [InverseProperty("Organization")]
        public virtual ICollection<ProjectCareerPath> ProjectCareerPath { get; set; }
        [InverseProperty("Organization")]
        public virtual ICollection<ProjectPosition> ProjectPosition { get; set; }
        [InverseProperty("Organization")]
        public virtual ICollection<RoleGrade> RoleGrade { get; set; }
        [InverseProperty("Organization")]
        public virtual ICollection<Team> Team { get; set; }
        [InverseProperty("Organization")]
        public virtual ICollection<_360employeeEvaluation> _360employeeEvaluation { get; set; }
        [InverseProperty("Organization")]
        public virtual ICollection<_360evaluation> _360evaluation { get; set; }
        [InverseProperty("Organization")]
        public virtual ICollection<_360evaluationComment> _360evaluationComment { get; set; }
        [InverseProperty("Organization")]
        public virtual ICollection<_360pendingEvaluator> _360pendingEvaluator { get; set; }
        [InverseProperty("Organization")]
        public virtual ICollection<_360question> _360question { get; set; }
        [InverseProperty("Organization")]
        public virtual ICollection<_360questionToMark> _360questionToMark { get; set; }
        [InverseProperty("Organization")]
        public virtual ICollection<_360questionarie> _360questionarie { get; set; }
    }
}
