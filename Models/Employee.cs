using System;
using System.Collections.Generic;

namespace Evoflare.API.Models
{
    public partial class Employee
    {
        public Employee()
        {
            EcfEmployeeEvaluationEndBy = new HashSet<EcfEmployeeEvaluation>();
            EcfEmployeeEvaluationEvaluator = new HashSet<EcfEmployeeEvaluation>();
            EcfEmployeeEvaluationStartBy = new HashSet<EcfEmployeeEvaluation>();
            EmployeeEvaluationEmployee = new HashSet<EmployeeEvaluation>();
            EmployeeEvaluationEndedBy = new HashSet<EmployeeEvaluation>();
            EmployeeEvaluationStartedBy = new HashSet<EmployeeEvaluation>();
            EmployeeRelationsEmployee = new HashSet<EmployeeRelations>();
            EmployeeRelationsManager = new HashSet<EmployeeRelations>();
            EvaluationSchedule = new HashSet<EvaluationSchedule>();
            Idea = new HashSet<Idea>();
            IdeaComment = new HashSet<IdeaComment>();
            IdeaLike = new HashSet<IdeaLike>();
            IdeaView = new HashSet<IdeaView>();
            PositionCreatedByNavigation = new HashSet<Position>();
            PositionUpdatedByNavigation = new HashSet<Position>();
            _360employeeEvaluation = new HashSet<_360employeeEvaluation>();
            _360pendingEvaluator = new HashSet<_360pendingEvaluator>();
        }

        public int Id { get; set; }
        public string UserId { get; set; }
        public bool IsManager { get; set; }
        public int EmployeeTypeId { get; set; }
        public int OrganizationId { get; set; }
        public string NameTemp { get; set; }
        public DateTime? HiringDate { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public virtual EmployeeType EmployeeType { get; set; }
        public virtual Organization Organization { get; set; }
        public virtual ICollection<EcfEmployeeEvaluation> EcfEmployeeEvaluationEndBy { get; set; }
        public virtual ICollection<EcfEmployeeEvaluation> EcfEmployeeEvaluationEvaluator { get; set; }
        public virtual ICollection<EcfEmployeeEvaluation> EcfEmployeeEvaluationStartBy { get; set; }
        public virtual ICollection<EmployeeEvaluation> EmployeeEvaluationEmployee { get; set; }
        public virtual ICollection<EmployeeEvaluation> EmployeeEvaluationEndedBy { get; set; }
        public virtual ICollection<EmployeeEvaluation> EmployeeEvaluationStartedBy { get; set; }
        public virtual ICollection<EmployeeRelations> EmployeeRelationsEmployee { get; set; }
        public virtual ICollection<EmployeeRelations> EmployeeRelationsManager { get; set; }
        public virtual ICollection<EvaluationSchedule> EvaluationSchedule { get; set; }
        public virtual ICollection<Idea> Idea { get; set; }
        public virtual ICollection<IdeaComment> IdeaComment { get; set; }
        public virtual ICollection<IdeaLike> IdeaLike { get; set; }
        public virtual ICollection<IdeaView> IdeaView { get; set; }
        public virtual ICollection<Position> PositionCreatedByNavigation { get; set; }
        public virtual ICollection<Position> PositionUpdatedByNavigation { get; set; }
        public virtual ICollection<_360employeeEvaluation> _360employeeEvaluation { get; set; }
        public virtual ICollection<_360pendingEvaluator> _360pendingEvaluator { get; set; }
    }
}
