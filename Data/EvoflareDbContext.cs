using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Evoflare.API.Models
{
    public partial class EvoflareDbContext : BaseDbContext
    {
        public EvoflareDbContext()
        {
        }

        public EvoflareDbContext(DbContextOptions<EvoflareDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Certificate> Certificate { get; set; }
        public virtual DbSet<CertificationExam> CertificationExam { get; set; }
        public virtual DbSet<Competence> Competence { get; set; }
        public virtual DbSet<CompetenceArea> CompetenceArea { get; set; }
        public virtual DbSet<CompetenceCertificate> CompetenceCertificate { get; set; }
        public virtual DbSet<CompetenceLevel> CompetenceLevel { get; set; }
        public virtual DbSet<CustomerContact> CustomerContact { get; set; }
        public virtual DbSet<EcfEmployeeEvaluation> EcfEmployeeEvaluation { get; set; }
        public virtual DbSet<EcfEvaluationResult> EcfEvaluationResult { get; set; }
        public virtual DbSet<EcfRole> EcfRole { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<EmployeeEvaluation> EmployeeEvaluation { get; set; }
        public virtual DbSet<EmployeeRelations> EmployeeRelations { get; set; }
        public virtual DbSet<EmployeeSalary> EmployeeSalary { get; set; }
        public virtual DbSet<EmployeeType> EmployeeType { get; set; }
        public virtual DbSet<EvaluationSchedule> EvaluationSchedule { get; set; }
        public virtual DbSet<Idea> Idea { get; set; }
        public virtual DbSet<IdeaComment> IdeaComment { get; set; }
        public virtual DbSet<IdeaLike> IdeaLike { get; set; }
        public virtual DbSet<IdeaTag> IdeaTag { get; set; }
        public virtual DbSet<IdeaTagRef> IdeaTagRef { get; set; }
        public virtual DbSet<IdeaView> IdeaView { get; set; }
        public virtual DbSet<Notification> Notification { get; set; }
        public virtual DbSet<Organization> Organization { get; set; }
        public virtual DbSet<OrganizationStructureType> OrganizationStructureType { get; set; }
        public virtual DbSet<Pdp> Pdp { get; set; }
        public virtual DbSet<Position> Position { get; set; }
        public virtual DbSet<PositionRole> PositionRole { get; set; }
        public virtual DbSet<Project> Project { get; set; }
        public virtual DbSet<ProjectCareerPath> ProjectCareerPath { get; set; }
        public virtual DbSet<ProjectPosition> ProjectPosition { get; set; }
        public virtual DbSet<ProjectPositionCompetence> ProjectPositionCompetence { get; set; }
        public virtual DbSet<RoleCompetence> RoleCompetence { get; set; }
        public virtual DbSet<RoleGrade> RoleGrade { get; set; }
        public virtual DbSet<RoleGradeCompetence> RoleGradeCompetence { get; set; }
        public virtual DbSet<Team> Team { get; set; }
        public virtual DbSet<_360employeeEvaluation> _360employeeEvaluation { get; set; }
        public virtual DbSet<_360evaluationResult> _360evaluationResult { get; set; }
        public virtual DbSet<_360evaluationSchedule> _360evaluationSchedule { get; set; }
        public virtual DbSet<_360pendingEvaluator> _360pendingEvaluator { get; set; }
        public virtual DbSet<_360questionnarie> _360questionnarie { get; set; }
        public virtual DbSet<_360questionnarieStatement> _360questionnarieStatement { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseSqlServer("Server=localhost,14330;Database=EvoflareDB;User Id=sa;Password=DatgE66VbHy7");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity<Competence>(entity =>
            {
                entity.HasIndex(e => e.CompetenceAreaId)
                    .HasName("IX_EmpCompetence_CompetenceAreaId");

                entity.HasOne(d => d.CompetenceArea)
                    .WithMany(p => p.Competence)
                    .HasForeignKey(d => d.CompetenceAreaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmpCompetence_EmpCompetenceArea");
            });

            modelBuilder.Entity<CompetenceCertificate>(entity =>
            {
                entity.HasIndex(e => e.CertificateId);

                entity.HasIndex(e => e.CompetenceId);

                entity.HasIndex(e => e.CompetenceLevelId);

                entity.HasIndex(e => e.OrganizationId);

                entity.HasOne(d => d.Certificate)
                    .WithMany(p => p.CompetenceCertificate)
                    .HasForeignKey(d => d.CertificateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CompetenceCertificate_Certificate");

                entity.HasOne(d => d.Competence)
                    .WithMany(p => p.CompetenceCertificate)
                    .HasForeignKey(d => d.CompetenceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CompetenceCertificate_EmpCompetence");

                entity.HasOne(d => d.CompetenceLevel)
                    .WithMany(p => p.CompetenceCertificate)
                    .HasForeignKey(d => d.CompetenceLevelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CompetenceCertificate_EmpCompetenceLevel");

                entity.HasOne(d => d.Organization)
                    .WithMany(p => p.CompetenceCertificate)
                    .HasForeignKey(d => d.OrganizationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CompetenceCertificate_Organization");
            });

            modelBuilder.Entity<CompetenceLevel>(entity =>
            {
                entity.HasIndex(e => e.CompetenceId)
                    .HasName("IX_EmpCompetenceLevel_CompetenceId");

                entity.HasOne(d => d.Competence)
                    .WithMany(p => p.CompetenceLevel)
                    .HasForeignKey(d => d.CompetenceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmpCompetenceLevel_EmpCompetence");
            });

            modelBuilder.Entity<CustomerContact>(entity =>
            {
                entity.HasIndex(e => e.OrganizationId);

                entity.HasIndex(e => e.ProjectId);

                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.Phone).IsUnicode(false);

                entity.HasOne(d => d.Organization)
                    .WithMany(p => p.CustomerContact)
                    .HasForeignKey(d => d.OrganizationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerContact_Organization");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.CustomerContact)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerContact_Project");
            });

            modelBuilder.Entity<EcfEmployeeEvaluation>(entity =>
            {
                entity.HasIndex(e => e.EndById);

                entity.HasIndex(e => e.EvaluationId);

                entity.HasIndex(e => e.EvaluatorId);

                entity.HasIndex(e => e.OrganizationId);

                entity.HasIndex(e => e.StartById);

                entity.HasOne(d => d.EndBy)
                    .WithMany(p => p.EcfEmployeeEvaluationEndBy)
                    .HasForeignKey(d => d.EndById)
                    .HasConstraintName("FK_EcfEmployeeEvaluation_EndByEmployee");

                entity.HasOne(d => d.Evaluation)
                    .WithMany(p => p.EcfEmployeeEvaluation)
                    .HasForeignKey(d => d.EvaluationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EcfEmployeeEvaluation_EmployeeEvaluation");

                entity.HasOne(d => d.Evaluator)
                    .WithMany(p => p.EcfEmployeeEvaluationEvaluator)
                    .HasForeignKey(d => d.EvaluatorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EcfEmployeeEvaluation_Evaluator");

                entity.HasOne(d => d.Organization)
                    .WithMany(p => p.EcfEmployeeEvaluation)
                    .HasForeignKey(d => d.OrganizationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EcfEmployeeEvaluation_Organization");

                entity.HasOne(d => d.StartBy)
                    .WithMany(p => p.EcfEmployeeEvaluationStartBy)
                    .HasForeignKey(d => d.StartById)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EcfEmployeeEvaluation_StartByEmployee");
            });

            modelBuilder.Entity<EcfEvaluationResult>(entity =>
            {
                entity.HasIndex(e => e.Competence);

                entity.HasIndex(e => e.EvaluationId);

                entity.HasOne(d => d.CompetenceNavigation)
                    .WithMany(p => p.EcfEvaluationResult)
                    .HasForeignKey(d => d.Competence)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EcfEvaluationResult_EmpRoleCompetence");

                entity.HasOne(d => d.Evaluation)
                    .WithMany(p => p.EcfEvaluationResult)
                    .HasForeignKey(d => d.EvaluationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EcfEvaluationResults_EcfEmployeeEvaluation");
            });

            modelBuilder.Entity<EcfRole>(entity =>
            {
                entity.HasIndex(e => e.RoleId)
                    .HasName("IX_Role")
                    .IsUnique();
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasIndex(e => e.EmployeeTypeId);

                entity.HasIndex(e => e.OrganizationId);

                entity.HasIndex(e => e.UserId);

                entity.HasOne(d => d.EmployeeType)
                    .WithMany(p => p.Employee)
                    .HasForeignKey(d => d.EmployeeTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Employee_EmployeeType");

                entity.HasOne(d => d.Organization)
                    .WithMany(p => p.Employee)
                    .HasForeignKey(d => d.OrganizationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Employee_Organization");
            });

            modelBuilder.Entity<EmployeeEvaluation>(entity =>
            {
                entity.HasIndex(e => e.EmployeeId);

                entity.HasIndex(e => e.EndedById);

                entity.HasIndex(e => e.OrganizationId);

                entity.HasIndex(e => e.StartedById);

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.EmployeeEvaluationEmployee)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeeEvaluation_Employee");

                entity.HasOne(d => d.EndedBy)
                    .WithMany(p => p.EmployeeEvaluationEndedBy)
                    .HasForeignKey(d => d.EndedById)
                    .HasConstraintName("FK_EmployeeEvaluation_EmployeeEndedBy");

                entity.HasOne(d => d.Organization)
                    .WithMany(p => p.EmployeeEvaluation)
                    .HasForeignKey(d => d.OrganizationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeeEvaluation_Organization");

                entity.HasOne(d => d.StartedBy)
                    .WithMany(p => p.EmployeeEvaluationStartedBy)
                    .HasForeignKey(d => d.StartedById)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeeEvaluation_EmployeeStartedBy");
            });

            modelBuilder.Entity<EmployeeRelations>(entity =>
            {
                entity.HasIndex(e => e.EmployeeId);

                entity.HasIndex(e => e.ManagerId);

                entity.HasIndex(e => e.OrganizationId);

                entity.HasIndex(e => e.PositionId);

                entity.HasIndex(e => e.ProjectId);

                entity.HasIndex(e => e.TeamId);

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.EmployeeRelationsEmployee)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK_EmployeeRelations_Employee");

                entity.HasOne(d => d.Manager)
                    .WithMany(p => p.EmployeeRelationsManager)
                    .HasForeignKey(d => d.ManagerId)
                    .HasConstraintName("FK_EmployeeRelations_Manager");

                entity.HasOne(d => d.Organization)
                    .WithMany(p => p.EmployeeRelations)
                    .HasForeignKey(d => d.OrganizationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeeRelations_Organization");

                entity.HasOne(d => d.Position)
                    .WithMany(p => p.EmployeeRelations)
                    .HasForeignKey(d => d.PositionId)
                    .HasConstraintName("FK_EmployeeRelations_Position");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.EmployeeRelations)
                    .HasForeignKey(d => d.ProjectId)
                    .HasConstraintName("FK_EmployeeRelations_Project");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.EmployeeRelations)
                    .HasForeignKey(d => d.TeamId)
                    .HasConstraintName("FK_EmployeeRelations_Team");
            });

            modelBuilder.Entity<EmployeeSalary>(entity =>
            {
                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.EmployeeSalary)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeeSalary_Employee");
            });

            modelBuilder.Entity<EmployeeType>(entity =>
            {
                entity.HasIndex(e => e.OrganizationId);

                entity.HasOne(d => d.Organization)
                    .WithMany(p => p.EmployeeType)
                    .HasForeignKey(d => d.OrganizationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeeType_Organization");
            });

            modelBuilder.Entity<EvaluationSchedule>(entity =>
            {
                entity.HasIndex(e => e.EmployeeId);

                entity.HasIndex(e => e.OrganizationId);

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.EvaluationSchedule)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EvaluationSchedule_Employee");

                entity.HasOne(d => d.Organization)
                    .WithMany(p => p.EvaluationSchedule)
                    .HasForeignKey(d => d.OrganizationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EvaluationSchedule_Organization");
            });

            modelBuilder.Entity<Idea>(entity =>
            {
                entity.HasIndex(e => e.CreatedById);

                entity.HasIndex(e => e.OrganizationId);

                entity.HasOne(d => d.CreatedBy)
                    .WithMany(p => p.Idea)
                    .HasForeignKey(d => d.CreatedById)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Idea_Employee");

                entity.HasOne(d => d.Organization)
                    .WithMany(p => p.Idea)
                    .HasForeignKey(d => d.OrganizationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Idea_Organization");
            });

            modelBuilder.Entity<IdeaComment>(entity =>
            {
                entity.HasIndex(e => e.CreatedById);

                entity.HasIndex(e => e.IdeaId);

                entity.HasIndex(e => e.OrganizationId);

                entity.HasIndex(e => e.ParentCommentId);

                entity.HasOne(d => d.CreatedBy)
                    .WithMany(p => p.IdeaComment)
                    .HasForeignKey(d => d.CreatedById)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_IdeaComment_Employee");

                entity.HasOne(d => d.Idea)
                    .WithMany(p => p.IdeaComment)
                    .HasForeignKey(d => d.IdeaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_IdeaComment_Idea");

                entity.HasOne(d => d.Organization)
                    .WithMany(p => p.IdeaComment)
                    .HasForeignKey(d => d.OrganizationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_IdeaComment_Organization");

                entity.HasOne(d => d.ParentComment)
                    .WithMany(p => p.InverseParentComment)
                    .HasForeignKey(d => d.ParentCommentId)
                    .HasConstraintName("FK_IdeaComment_ParentIdeaComment");
            });

            modelBuilder.Entity<IdeaLike>(entity =>
            {
                entity.HasIndex(e => e.EmployeeId);

                entity.HasIndex(e => e.IdeaId);

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.IdeaLike)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_IdeaLike_Employee");

                entity.HasOne(d => d.Idea)
                    .WithMany(p => p.IdeaLike)
                    .HasForeignKey(d => d.IdeaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_IdeaLike_Idea");
            });

            modelBuilder.Entity<IdeaTagRef>(entity =>
            {
                entity.HasIndex(e => e.IdeaId);

                entity.HasIndex(e => e.TagId);

                entity.HasOne(d => d.Idea)
                    .WithMany(p => p.IdeaTagRef)
                    .HasForeignKey(d => d.IdeaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_IdeaTagRef_Idea");

                entity.HasOne(d => d.Tag)
                    .WithMany(p => p.IdeaTagRef)
                    .HasForeignKey(d => d.TagId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_IdeaTagRef_IdeaTag");
            });

            modelBuilder.Entity<IdeaView>(entity =>
            {
                entity.HasIndex(e => e.EmployeeId);

                entity.HasIndex(e => e.IdeaId);

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.IdeaView)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_IdeaView_Employee");

                entity.HasOne(d => d.Idea)
                    .WithMany(p => p.IdeaView)
                    .HasForeignKey(d => d.IdeaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_IdeaView_Idea");
            });

            modelBuilder.Entity<Pdp>(entity =>
            {
                entity.HasIndex(e => e.CertificateId);

                entity.HasIndex(e => e.OrganizationId);

                entity.HasOne(d => d.Certificate)
                    .WithMany(p => p.Pdp)
                    .HasForeignKey(d => d.CertificateId)
                    .HasConstraintName("FK_Pdp_Certificate");

                entity.HasOne(d => d.Organization)
                    .WithMany(p => p.Pdp)
                    .HasForeignKey(d => d.OrganizationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Pdp_Organization");
            });

            modelBuilder.Entity<Position>(entity =>
            {
                entity.HasIndex(e => e.CreatedBy);

                entity.HasIndex(e => e.ProjectId);

                entity.HasIndex(e => e.UpdatedBy);

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getutcdate())");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.PositionCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Position_EmployeeCreatedBy");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.Position)
                    .HasForeignKey(d => d.ProjectId)
                    .HasConstraintName("FK_Position_Project");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.PositionUpdatedByNavigation)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_Position_EmployeeUpdatedBy");
            });

            modelBuilder.Entity<PositionRole>(entity =>
            {
                entity.HasIndex(e => e.PositionId);

                entity.HasIndex(e => e.RoleId);

                entity.Property(e => e.DateTime).HasDefaultValueSql("(getutcdate())");

                entity.HasOne(d => d.Position)
                    .WithMany(p => p.PositionRole)
                    .HasForeignKey(d => d.PositionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PositionRole_Position");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.PositionRole)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PositionRole_EcfRole");
            });

            modelBuilder.Entity<ProjectCareerPath>(entity =>
            {
                entity.HasIndex(e => e.OrganizationId);

                entity.HasIndex(e => e.ProjectId);

                entity.HasIndex(e => e.RoleId);

                entity.HasIndex(e => e.TeamId);

                entity.HasOne(d => d.Organization)
                    .WithMany(p => p.ProjectCareerPath)
                    .HasForeignKey(d => d.OrganizationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProjectCareerPath_Organization");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.ProjectCareerPath)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProjectCareerPath_Project");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.ProjectCareerPath)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProjectCareerPath_Role");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.ProjectCareerPath)
                    .HasForeignKey(d => d.TeamId)
                    .HasConstraintName("FK_ProjectCareerPath_Team");
            });

            modelBuilder.Entity<ProjectPosition>(entity =>
            {
                entity.HasIndex(e => e.CareerPathId);

                entity.HasIndex(e => e.OrganizationId);

                entity.HasIndex(e => e.ProjectId);

                entity.HasIndex(e => e.RoleGradeId);

                entity.HasOne(d => d.CareerPath)
                    .WithMany(p => p.ProjectPosition)
                    .HasForeignKey(d => d.CareerPathId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProjectPosition_ProjectCareerPath");

                entity.HasOne(d => d.Organization)
                    .WithMany(p => p.ProjectPosition)
                    .HasForeignKey(d => d.OrganizationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProjectPosition_Organization");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.ProjectPosition)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProjectPosition_Project");

                entity.HasOne(d => d.RoleGrade)
                    .WithMany(p => p.ProjectPosition)
                    .HasForeignKey(d => d.RoleGradeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProjectPosition_RoleGrade");
            });

            modelBuilder.Entity<ProjectPositionCompetence>(entity =>
            {
                entity.HasIndex(e => e.CompetenceId);

                entity.HasIndex(e => e.CompetenceLevelId);

                entity.HasIndex(e => e.ProjectPositionId);

                entity.HasIndex(e => e.RoleGradeId);

                entity.HasOne(d => d.Competence)
                    .WithMany(p => p.ProjectPositionCompetence)
                    .HasForeignKey(d => d.CompetenceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProjectPositionCompetence_EmpCompetence");

                entity.HasOne(d => d.CompetenceLevel)
                    .WithMany(p => p.ProjectPositionCompetence)
                    .HasForeignKey(d => d.CompetenceLevelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProjectPositionCompetence_EmpCompetenceLevel");

                entity.HasOne(d => d.ProjectPosition)
                    .WithMany(p => p.ProjectPositionCompetence)
                    .HasForeignKey(d => d.ProjectPositionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProjectPositionCompetence_ProjectPosition");

                entity.HasOne(d => d.RoleGrade)
                    .WithMany(p => p.ProjectPositionCompetence)
                    .HasForeignKey(d => d.RoleGradeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProjectPositionCompetence_RoleGrade");
            });

            modelBuilder.Entity<RoleCompetence>(entity =>
            {
                entity.HasIndex(e => e.CompetenceId)
                    .HasName("IX_EmpRoleCompetence_CompetenceId");

                entity.HasIndex(e => e.RoleId)
                    .HasName("IX_EmpRoleCompetence_RoleId");

                entity.HasOne(d => d.Competence)
                    .WithMany(p => p.RoleCompetence)
                    .HasForeignKey(d => d.CompetenceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmpRoleCompetence_EmpCompetence");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.RoleCompetence)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmpRoleCompetence_EcfRole");
            });

            modelBuilder.Entity<RoleGrade>(entity =>
            {
                entity.HasIndex(e => e.EmployeeTypeId);

                entity.HasIndex(e => e.OrganizationId);

                entity.HasOne(d => d.EmployeeType)
                    .WithMany(p => p.RoleGrade)
                    .HasForeignKey(d => d.EmployeeTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CareerPath_EmployeeType");

                entity.HasOne(d => d.Organization)
                    .WithMany(p => p.RoleGrade)
                    .HasForeignKey(d => d.OrganizationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CareerPath_Organization");
            });

            modelBuilder.Entity<RoleGradeCompetence>(entity =>
            {
                entity.HasIndex(e => e.CompetenceId);

                entity.HasIndex(e => e.CompetenceLevelId);

                entity.HasIndex(e => e.RoleGradeId);

                entity.HasOne(d => d.Competence)
                    .WithMany(p => p.RoleGradeCompetence)
                    .HasForeignKey(d => d.CompetenceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RoleGradeCompetence_EmpCompetence");

                entity.HasOne(d => d.CompetenceLevel)
                    .WithMany(p => p.RoleGradeCompetence)
                    .HasForeignKey(d => d.CompetenceLevelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RoleGradeCompetence_EmpCompetenceLevel");

                entity.HasOne(d => d.RoleGrade)
                    .WithMany(p => p.RoleGradeCompetence)
                    .HasForeignKey(d => d.RoleGradeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CareerPathSkills_CareerPath");
            });

            modelBuilder.Entity<Team>(entity =>
            {
                entity.HasIndex(e => e.OrganizationId);

                entity.HasIndex(e => e.ProjectId);

                entity.HasOne(d => d.Organization)
                    .WithMany(p => p.Team)
                    .HasForeignKey(d => d.OrganizationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Team_Organization");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.Team)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Team_Project");
            });

            modelBuilder.Entity<_360employeeEvaluation>(entity =>
            {
                entity.HasIndex(e => e.EvaluationId);

                entity.HasIndex(e => e.EvaluatorEmployeeId);

                entity.HasIndex(e => e.OrganizationId);

                entity.HasOne(d => d.Evaluation)
                    .WithMany(p => p._360employeeEvaluation)
                    .HasForeignKey(d => d.EvaluationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_360EmployeeEvaluation_EmployeeEvaluation");

                entity.HasOne(d => d.EvaluatorEmployee)
                    .WithMany(p => p._360employeeEvaluation)
                    .HasForeignKey(d => d.EvaluatorEmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_360EmployeeEvaluation_Employee");

                entity.HasOne(d => d.Organization)
                    .WithMany(p => p._360employeeEvaluation)
                    .HasForeignKey(d => d.OrganizationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_360EmployeeEvaluation_Organization");
            });

            modelBuilder.Entity<_360evaluationResult>(entity =>
            {
                entity.HasIndex(e => e.EvaluationId);

                entity.HasIndex(e => e._360questionnarieStatementId);

                entity.HasOne(d => d.Evaluation)
                    .WithMany(p => p._360evaluationResult)
                    .HasForeignKey(d => d.EvaluationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_360EvaluationResult_360EmployeeEvaluation");

                entity.HasOne(d => d._360questionnarieStatement)
                    .WithMany(p => p._360evaluationResult)
                    .HasForeignKey(d => d._360questionnarieStatementId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_360EvaluationResult_360QuestionnarieStatement");
            });

            modelBuilder.Entity<_360pendingEvaluator>(entity =>
            {
                entity.HasIndex(e => e.EvaluatorId);

                entity.HasIndex(e => e.OrganizationId);

                entity.HasIndex(e => e._360employeeEvaluationId);

                entity.HasOne(d => d.Evaluator)
                    .WithMany(p => p._360pendingEvaluator)
                    .HasForeignKey(d => d.EvaluatorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_360PendingEvaluator_Employee");

                entity.HasOne(d => d.Organization)
                    .WithMany(p => p._360pendingEvaluator)
                    .HasForeignKey(d => d.OrganizationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_360PendingEvaluator_Organization");

                entity.HasOne(d => d._360employeeEvaluation)
                    .WithMany(p => p._360pendingEvaluator)
                    .HasForeignKey(d => d._360employeeEvaluationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_360PendingEvaluator_360PendingEvaluator");
            });

            modelBuilder.Entity<_360questionnarieStatement>(entity =>
            {
                entity.HasIndex(e => e.QuestionnarieId);

                entity.HasOne(d => d.Questionnarie)
                    .WithMany(p => p._360questionnarieStatement)
                    .HasForeignKey(d => d.QuestionnarieId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_360QuestionnarieStatement_360Questionnarie");
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
