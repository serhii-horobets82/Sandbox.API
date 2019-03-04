using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Evoflare.API.Models
{
    public partial class TechnicalEvaluationContext : DbContext
    {
        public TechnicalEvaluationContext()
        {
        }

        public TechnicalEvaluationContext(DbContextOptions<TechnicalEvaluationContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AppVersion> AppVersion { get; set; }
        public virtual DbSet<EcfCompetence> EcfCompetence { get; set; }
        public virtual DbSet<EcfCompetenceLevel> EcfCompetenceLevel { get; set; }
        public virtual DbSet<EcfEvaluation> EcfEvaluation { get; set; }
        public virtual DbSet<EcfRole> EcfRole { get; set; }
        public virtual DbSet<EcfRoleCompetence> EcfRoleCompetence { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<EmployeeEvaluation> EmployeeEvaluation { get; set; }
        public virtual DbSet<EmployeeRelations> EmployeeRelations { get; set; }
        public virtual DbSet<EmployeeType> EmployeeType { get; set; }
        public virtual DbSet<Organization> Organization { get; set; }
        public virtual DbSet<Position> Position { get; set; }
        public virtual DbSet<PositionRole> PositionRole { get; set; }
        public virtual DbSet<Project> Project { get; set; }
        public virtual DbSet<Team> Team { get; set; }
        public virtual DbSet<_360employeeEvaluation> _360employeeEvaluation { get; set; }
        public virtual DbSet<_360evaluation> _360evaluation { get; set; }
        public virtual DbSet<_360feedbackGroup> _360feedbackGroup { get; set; }
        public virtual DbSet<_360feedbackMark> _360feedbackMark { get; set; }
        public virtual DbSet<_360question> _360question { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost;Database=TechnicalEvaluation;User ID=sa;Password=123");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.2-servicing-10034");

            modelBuilder.Entity<AppVersion>(entity =>
            {
                entity.HasKey(e => e.Name);

                entity.Property(e => e.Name).ValueGeneratedNever();
            });

            modelBuilder.Entity<EcfCompetence>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<EcfCompetenceLevel>(entity =>
            {
                entity.Property(e => e.CompetenceId)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.HasOne(d => d.Competence)
                    .WithMany(p => p.EcfCompetenceLevel)
                    .HasForeignKey(d => d.CompetenceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CompetenceLevel_Competence");
            });

            modelBuilder.Entity<EcfEvaluation>(entity =>
            {
                entity.Property(e => e.Competence)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.HasOne(d => d.CompetenceNavigation)
                    .WithMany(p => p.EcfEvaluation)
                    .HasForeignKey(d => d.Competence)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EcfEvaluation_EcfCompetence");

                entity.HasOne(d => d.Evaluation)
                    .WithMany(p => p.EcfEvaluation)
                    .HasForeignKey(d => d.EvaluationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EcfEvaluation_EmployeeEvaluation");
            });

            modelBuilder.Entity<EcfRole>(entity =>
            {
                entity.HasIndex(e => e.RoleId)
                    .HasName("IX_Role")
                    .IsUnique();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<EcfRoleCompetence>(entity =>
            {
                entity.Property(e => e.CompetenceId)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.HasOne(d => d.Competence)
                    .WithMany(p => p.EcfRoleCompetence)
                    .HasForeignKey(d => d.CompetenceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EcfRoleCompetence_EcfCompetence");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.EcfRoleCompetence)
                    .HasPrincipalKey(p => p.RoleId)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EcfRoleCompetence_EcfRole");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.Property(e => e.NameTemp)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.UserId).HasMaxLength(10);

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
                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.EmployeeEvaluationEmployee)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeeEvaluation_Employee");

                entity.HasOne(d => d.EndedBy)
                    .WithMany(p => p.EmployeeEvaluationEndedBy)
                    .HasForeignKey(d => d.EndedById)
                    .HasConstraintName("FK_EmployeeEvaluation_EmployeeEnded");

                entity.HasOne(d => d.Organization)
                    .WithMany(p => p.EmployeeEvaluation)
                    .HasForeignKey(d => d.OrganizationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeeEvaluation_Organization");

                entity.HasOne(d => d.StartedBy)
                    .WithMany(p => p.EmployeeEvaluationStartedBy)
                    .HasForeignKey(d => d.StartedById)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeeEvaluation_EmployeeStarted");
            });

            modelBuilder.Entity<EmployeeRelations>(entity =>
            {
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

            modelBuilder.Entity<EmployeeType>(entity =>
            {
                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Organization>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Position>(entity =>
            {
                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.PositionCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Position_EmployeeCreatedBy");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.PositionUpdatedByNavigation)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_Position_EmployeeUpdatedBy");
            });

            modelBuilder.Entity<PositionRole>(entity =>
            {
                entity.Property(e => e.DateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

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

            modelBuilder.Entity<Project>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Team>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

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
                entity.ToTable("360EmployeeEvaluation");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.HasOne(d => d.Evaluation)
                    .WithMany(p => p._360employeeEvaluation)
                    .HasForeignKey(d => d.EvaluationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_360EmployeeEvaluation_EmployeeEvaluation");

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p._360employeeEvaluation)
                    .HasForeignKey<_360employeeEvaluation>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_360EmployeeEvaluation_Employee");
            });

            modelBuilder.Entity<_360evaluation>(entity =>
            {
                entity.ToTable("360Evaluation");

                entity.HasOne(d => d.Evaluation)
                    .WithMany(p => p._360evaluation)
                    .HasForeignKey(d => d.EvaluationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_360Evaluation_EmployeeEvaluation");

                entity.HasOne(d => d.FeedbackMark)
                    .WithMany(p => p._360evaluation)
                    .HasForeignKey(d => d.FeedbackMarkId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_360Evaluation_360FeedbackMark");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p._360evaluation)
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_360Evaluation_360Question");
            });

            modelBuilder.Entity<_360feedbackGroup>(entity =>
            {
                entity.ToTable("360FeedbackGroup");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<_360feedbackMark>(entity =>
            {
                entity.ToTable("360FeedbackMark");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<_360question>(entity =>
            {
                entity.ToTable("360Question");

                entity.Property(e => e.Text)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e._360feedbackGroupId).HasColumnName("360FeedbackGroupId");

                entity.HasOne(d => d._360feedbackGroup)
                    .WithMany(p => p._360question)
                    .HasForeignKey(d => d._360feedbackGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_360Question_360FeedbackGroup");
            });
        }
    }
}
