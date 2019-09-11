using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [Required]
        public string UserId { get; set; }
        public bool IsManager { get; set; }
        public int EmployeeTypeId { get; set; }
        public int OrganizationId { get; set; }
        [Required]
        [StringLength(100)]
        public string NameTemp { get; set; }
        [Column(TypeName = "date")]
        public DateTime? HiringDate { get; set; }
        [StringLength(30)]
        public string Name { get; set; }
        [StringLength(30)]
        public string Surname { get; set; }

        [ForeignKey("EmployeeTypeId")]
        [InverseProperty("Employee")]
        public virtual EmployeeType EmployeeType { get; set; }
        [ForeignKey("OrganizationId")]
        [InverseProperty("Employee")]
        public virtual Organization Organization { get; set; }
        [InverseProperty("EndBy")]
        public virtual ICollection<EcfEmployeeEvaluation> EcfEmployeeEvaluationEndBy { get; set; }
        [InverseProperty("Evaluator")]
        public virtual ICollection<EcfEmployeeEvaluation> EcfEmployeeEvaluationEvaluator { get; set; }
        [InverseProperty("StartBy")]
        public virtual ICollection<EcfEmployeeEvaluation> EcfEmployeeEvaluationStartBy { get; set; }
        [InverseProperty("Employee")]
        public virtual ICollection<EmployeeEvaluation> EmployeeEvaluationEmployee { get; set; }
        [InverseProperty("EndedBy")]
        public virtual ICollection<EmployeeEvaluation> EmployeeEvaluationEndedBy { get; set; }
        [InverseProperty("StartedBy")]
        public virtual ICollection<EmployeeEvaluation> EmployeeEvaluationStartedBy { get; set; }
        [InverseProperty("Employee")]
        public virtual ICollection<EmployeeRelations> EmployeeRelationsEmployee { get; set; }
        [InverseProperty("Manager")]
        public virtual ICollection<EmployeeRelations> EmployeeRelationsManager { get; set; }
        [InverseProperty("Employee")]
        public virtual ICollection<EvaluationSchedule> EvaluationSchedule { get; set; }
        [InverseProperty("CreatedBy")]
        public virtual ICollection<Idea> Idea { get; set; }
        [InverseProperty("CreatedBy")]
        public virtual ICollection<IdeaComment> IdeaComment { get; set; }
        [InverseProperty("Employee")]
        public virtual ICollection<IdeaLike> IdeaLike { get; set; }
        [InverseProperty("Employee")]
        public virtual ICollection<IdeaView> IdeaView { get; set; }
        [InverseProperty("CreatedByNavigation")]
        public virtual ICollection<Position> PositionCreatedByNavigation { get; set; }
        [InverseProperty("UpdatedByNavigation")]
        public virtual ICollection<Position> PositionUpdatedByNavigation { get; set; }
        [InverseProperty("EvaluatorEmployee")]
        public virtual ICollection<_360employeeEvaluation> _360employeeEvaluation { get; set; }
        [InverseProperty("Evaluator")]
        public virtual ICollection<_360pendingEvaluator> _360pendingEvaluator { get; set; }
        [ForeignKey("UserId")]
        [InverseProperty("Employee")]
        public virtual Evoflare.API.Auth.Models.ApplicationUser Users { get; set; }
    }
}
