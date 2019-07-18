using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Evoflare.API.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "security");

            migrationBuilder.EnsureSchema(
                name: "core");

            migrationBuilder.CreateTable(
                name: "360FeedbackGroup",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_360FeedbackGroup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "360FeedbackMark",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Mark = table.Column<int>(nullable: false),
                    Title = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_360FeedbackMark", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Certificate",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Company = table.Column<string>(maxLength: 200, nullable: true),
                    Technology = table.Column<string>(maxLength: 200, nullable: true),
                    Stack = table.Column<string>(maxLength: 200, nullable: true),
                    CertificationLevel = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Certificate", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CertificationExam",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(maxLength: 50, nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Price = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CertificationExam", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EcfCompetence",
                columns: table => new
                {
                    Id = table.Column<string>(unicode: false, maxLength: 3, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Summary = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EcfCompetence", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EcfRole",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Summary = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EcfRole", x => x.Id);
                    table.UniqueConstraint("AK_EcfRole_RoleId", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "IdeaTag",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdeaTag", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Organization",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organization", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Project",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    OrganizationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ActivityLogs",
                schema: "core",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    User = table.Column<string>(nullable: true),
                    Action = table.Column<string>(nullable: true),
                    Level = table.Column<int>(nullable: false),
                    ActivityDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppVersion",
                schema: "core",
                columns: table => new
                {
                    Name = table.Column<string>(nullable: false),
                    Version = table.Column<string>(nullable: true),
                    Database = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppVersion", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                schema: "core",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRole",
                schema: "security",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    AccessGroup = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRole", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUser",
                schema: "security",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 50, nullable: true),
                    LastName = table.Column<string>(maxLength: 50, nullable: true),
                    FacebookId = table.Column<long>(nullable: true),
                    Age = table.Column<int>(nullable: false),
                    Gender = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EcfCompetenceLevel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CompetenceId = table.Column<string>(unicode: false, maxLength: 3, nullable: false),
                    Level = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EcfCompetenceLevel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompetenceLevel_Competence",
                        column: x => x.CompetenceId,
                        principalTable: "EcfCompetence",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EcfRoleCompetence",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<int>(nullable: false),
                    CompetenceId = table.Column<string>(unicode: false, maxLength: 3, nullable: false),
                    CompetenceLevel = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EcfRoleCompetence", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EcfRoleCompetence_EcfCompetence",
                        column: x => x.CompetenceId,
                        principalTable: "EcfCompetence",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EcfRoleCompetence_EcfRole",
                        column: x => x.RoleId,
                        principalTable: "EcfRole",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "360Questionarie",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Text = table.Column<string>(maxLength: 250, nullable: false),
                    _360FeedbackGroupId = table.Column<int>(name: "360FeedbackGroupId", nullable: false),
                    OrganizationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_360Questionarie", x => x.Id);
                    table.ForeignKey(
                        name: "FK_360Questionarie_Organization",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_360Question_360FeedbackGroup",
                        column: x => x._360FeedbackGroupId,
                        principalTable: "360FeedbackGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<string>(maxLength: 50, nullable: false),
                    OrganizationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeType_Organization",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Pdp",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    StudyStartDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ClassroomStartDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    AssessmentStartDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ExamDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    CertificateId = table.Column<int>(nullable: true),
                    OrganizationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pdp", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pdp_Certificate",
                        column: x => x.CertificateId,
                        principalTable: "Certificate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pdp_Organization",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomerContact",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 250, nullable: false),
                    Email = table.Column<string>(unicode: false, maxLength: 250, nullable: false),
                    Phone = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    ProjectId = table.Column<int>(nullable: false),
                    OrganizationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerContact", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerContact_Organization",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerContact_Project",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Team",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    ProjectId = table.Column<int>(nullable: false),
                    OrganizationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Team", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Team_Organization",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Team_Project",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaim",
                schema: "security",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaim_AspNetRole_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "security",
                        principalTable: "AspNetRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Profile",
                schema: "core",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdentityId = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: true),
                    Locale = table.Column<string>(nullable: true),
                    PictureUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Profile_AspNetUser_IdentityId",
                        column: x => x.IdentityId,
                        principalSchema: "security",
                        principalTable: "AspNetUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaim",
                schema: "security",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaim_AspNetUser_UserId",
                        column: x => x.UserId,
                        principalSchema: "security",
                        principalTable: "AspNetUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogin",
                schema: "security",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogin", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogin_AspNetUser_UserId",
                        column: x => x.UserId,
                        principalSchema: "security",
                        principalTable: "AspNetUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRole",
                schema: "security",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRole", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRole_AspNetRole_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "security",
                        principalTable: "AspNetRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRole_AspNetUser_UserId",
                        column: x => x.UserId,
                        principalSchema: "security",
                        principalTable: "AspNetUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserToken",
                schema: "security",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserToken", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserToken_AspNetUser_UserId",
                        column: x => x.UserId,
                        principalSchema: "security",
                        principalTable: "AspNetUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompetenceCertificate",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CompetenceId = table.Column<string>(unicode: false, maxLength: 3, nullable: false),
                    CompetenceLevelId = table.Column<int>(nullable: false),
                    CertificateId = table.Column<int>(nullable: false),
                    OrganizationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompetenceCertificate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompetenceCertificate_Certificate",
                        column: x => x.CertificateId,
                        principalTable: "Certificate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompetenceCertificate_EcfCompetence",
                        column: x => x.CompetenceId,
                        principalTable: "EcfCompetence",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompetenceCertificate_EcfCompetenceLevel",
                        column: x => x.CompetenceLevelId,
                        principalTable: "EcfCompetenceLevel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompetenceCertificate_Organization",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "360QuestionToMark",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    QuestionId = table.Column<int>(nullable: false),
                    MarkId = table.Column<int>(nullable: false),
                    OrganizationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_360QuestionToMark", x => x.Id);
                    table.ForeignKey(
                        name: "FK_360QuestionToMark_Organization",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_360QuestionToMark_360Questionarie",
                        column: x => x.QuestionId,
                        principalTable: "360Questionarie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(maxLength: 10, nullable: true),
                    IsManager = table.Column<bool>(nullable: false),
                    EmployeeTypeId = table.Column<int>(nullable: false),
                    OrganizationId = table.Column<int>(nullable: false),
                    NameTemp = table.Column<string>(maxLength: 100, nullable: false),
                    HiringDate = table.Column<DateTime>(type: "date", nullable: true),
                    Name = table.Column<string>(maxLength: 30, nullable: true),
                    Surname = table.Column<string>(maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employee_EmployeeType",
                        column: x => x.EmployeeTypeId,
                        principalTable: "EmployeeType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employee_Organization",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RoleGrade",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EmployeeTypeId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Order = table.Column<int>(nullable: false),
                    OrganizationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleGrade", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CareerPath_EmployeeType",
                        column: x => x.EmployeeTypeId,
                        principalTable: "EmployeeType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CareerPath_Organization",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProjectCareerPath",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProjectId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    RoleId = table.Column<int>(nullable: false),
                    TeamId = table.Column<int>(nullable: true),
                    OrganizationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectCareerPath", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectCareerPath_Organization",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectCareerPath_Project",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectCareerPath_Role",
                        column: x => x.RoleId,
                        principalTable: "EmployeeType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectCareerPath_Team",
                        column: x => x.TeamId,
                        principalTable: "Team",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "360Question",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    QuestionToMarkId = table.Column<int>(nullable: false),
                    Question = table.Column<string>(maxLength: 250, nullable: false),
                    Order = table.Column<int>(nullable: false),
                    OrganizationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_360Question", x => x.Id);
                    table.ForeignKey(
                        name: "FK_360Question_Organization",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_360Question_360QuestionToMark",
                        column: x => x.QuestionToMarkId,
                        principalTable: "360QuestionToMark",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeEvaluation",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EmployeeId = table.Column<int>(nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    StartedById = table.Column<int>(nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    EndedById = table.Column<int>(nullable: true),
                    OrganizationId = table.Column<int>(nullable: false),
                    Archived = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeEvaluation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeEvaluation_Employee",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeEvaluation_EmployeeEndedBy",
                        column: x => x.EndedById,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeEvaluation_Organization",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeEvaluation_EmployeeStartedBy",
                        column: x => x.StartedById,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EvaluationSchedule",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EmployeeId = table.Column<int>(nullable: false),
                    EvaluationDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Archived = table.Column<bool>(nullable: false),
                    OrganizationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluationSchedule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EvaluationSchedule_Employee",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EvaluationSchedule_Organization",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Idea",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    CreatedById = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Price = table.Column<int>(nullable: true),
                    OrganizationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Idea", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Idea_Employee",
                        column: x => x.CreatedById,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Idea_Organization",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Position",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    ProjectId = table.Column<int>(nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getutcdate())"),
                    CreatedBy = table.Column<int>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedBy = table.Column<int>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    OrganizationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Position", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Position_EmployeeCreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Position_Project",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Position_EmployeeUpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RoleGradeCompetence",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CompetenceId = table.Column<string>(unicode: false, maxLength: 3, nullable: false),
                    CompetenceLevelId = table.Column<int>(nullable: false),
                    RoleGradeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleGradeCompetence", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CareerPathSkills_EcfCompetence",
                        column: x => x.CompetenceId,
                        principalTable: "EcfCompetence",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CareerPathSkills_EcfCompetenceLevel",
                        column: x => x.CompetenceLevelId,
                        principalTable: "EcfCompetenceLevel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CareerPathSkills_CareerPath",
                        column: x => x.RoleGradeId,
                        principalTable: "RoleGrade",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProjectPosition",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProjectId = table.Column<int>(nullable: false),
                    CareerPathId = table.Column<int>(nullable: false),
                    RoleGradeId = table.Column<int>(nullable: false),
                    OrganizationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectPosition", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectPosition_ProjectCareerPath",
                        column: x => x.CareerPathId,
                        principalTable: "ProjectCareerPath",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectPosition_Organization",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectPosition_Project",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectPosition_RoleGrade",
                        column: x => x.RoleGradeId,
                        principalTable: "RoleGrade",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "360EmployeeEvaluation",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EvaluatorEmployeeId = table.Column<int>(nullable: false),
                    EvaluationId = table.Column<int>(nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    OrganizationId = table.Column<int>(nullable: false),
                    _360FeedbackGroupId = table.Column<int>(name: "360FeedbackGroupId", nullable: false),
                    StartDoing = table.Column<string>(nullable: true),
                    StopDoing = table.Column<string>(nullable: true),
                    OtherComments = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_360EmployeeEvaluation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_360EmployeeEvaluation_EmployeeEvaluation",
                        column: x => x.EvaluationId,
                        principalTable: "EmployeeEvaluation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_360EmployeeEvaluation_Employee",
                        column: x => x.EvaluatorEmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_360EmployeeEvaluation_Organization",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_360EmployeeEvaluation_360FeedbackGroup",
                        column: x => x._360FeedbackGroupId,
                        principalTable: "360FeedbackGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EcfEmployeeEvaluation",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EvaluationId = table.Column<int>(nullable: false),
                    EvaluatorId = table.Column<int>(nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    StartById = table.Column<int>(nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    EndById = table.Column<int>(nullable: true),
                    OrganizationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EcfEmployeeEvaluation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EcfEmployeeEvaluation_EndByEmployee",
                        column: x => x.EndById,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EcfEmployeeEvaluation_EmployeeEvaluation",
                        column: x => x.EvaluationId,
                        principalTable: "EmployeeEvaluation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EcfEmployeeEvaluation_Evaluator",
                        column: x => x.EvaluatorId,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EcfEmployeeEvaluation_Organization",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EcfEmployeeEvaluation_StartByEmployee",
                        column: x => x.StartById,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IdeaComment",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdeaId = table.Column<int>(nullable: false),
                    Comment = table.Column<string>(nullable: false),
                    CreatedById = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ParentCommentId = table.Column<int>(nullable: true),
                    OrganizationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdeaComment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IdeaComment_Employee",
                        column: x => x.CreatedById,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IdeaComment_Idea",
                        column: x => x.IdeaId,
                        principalTable: "Idea",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IdeaComment_Organization",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IdeaComment_ParentIdeaComment",
                        column: x => x.ParentCommentId,
                        principalTable: "IdeaComment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IdeaLike",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdeaId = table.Column<int>(nullable: false),
                    EmployeeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdeaLike", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IdeaLike_Employee",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IdeaLike_Idea",
                        column: x => x.IdeaId,
                        principalTable: "Idea",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IdeaTagRef",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdeaId = table.Column<int>(nullable: false),
                    TagId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdeaTagRef", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IdeaTagRef_Idea_IdeaId",
                        column: x => x.IdeaId,
                        principalTable: "Idea",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IdeaTagRef_IdeaTag_TagId",
                        column: x => x.TagId,
                        principalTable: "IdeaTag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IdeaView",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdeaId = table.Column<int>(nullable: false),
                    EmployeeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdeaView", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IdeaView_Employee",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IdeaView_Idea",
                        column: x => x.IdeaId,
                        principalTable: "Idea",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeRelations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EmployeeId = table.Column<int>(nullable: true),
                    ManagerId = table.Column<int>(nullable: true),
                    TeamId = table.Column<int>(nullable: true),
                    ProjectId = table.Column<int>(nullable: true),
                    PositionId = table.Column<int>(nullable: true),
                    OrganizationId = table.Column<int>(nullable: false),
                    Archived = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeRelations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeRelations_Employee",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeRelations_Manager",
                        column: x => x.ManagerId,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeRelations_Organization",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeRelations_Position",
                        column: x => x.PositionId,
                        principalTable: "Position",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeRelations_Project",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeRelations_Team",
                        column: x => x.TeamId,
                        principalTable: "Team",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PositionRole",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PositionId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getutcdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PositionRole", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PositionRole_Position",
                        column: x => x.PositionId,
                        principalTable: "Position",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PositionRole_EcfRole",
                        column: x => x.RoleId,
                        principalTable: "EcfRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProjectPositionCompetence",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleGradeId = table.Column<int>(nullable: false),
                    ProjectPositionId = table.Column<int>(nullable: false),
                    CompetenceId = table.Column<string>(unicode: false, maxLength: 3, nullable: false),
                    CompetenceLevelId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectPositionCompetence", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectPositionCompetence_EcfCompetence",
                        column: x => x.CompetenceId,
                        principalTable: "EcfCompetence",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectPositionCompetence_EcfCompetenceLevel",
                        column: x => x.CompetenceLevelId,
                        principalTable: "EcfCompetenceLevel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectPositionCompetence_ProjectPosition",
                        column: x => x.ProjectPositionId,
                        principalTable: "ProjectPosition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectPositionCompetence_RoleGrade",
                        column: x => x.RoleGradeId,
                        principalTable: "RoleGrade",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "360Evaluation",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EvaluationId = table.Column<int>(nullable: false),
                    QuestionId = table.Column<int>(nullable: false),
                    FeedbackMarkId = table.Column<int>(nullable: false),
                    OrganizationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_360Evaluation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_360Evaluation_EmployeeEvaluation",
                        column: x => x.EvaluationId,
                        principalTable: "360EmployeeEvaluation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_360Evaluation_360FeedbackMark",
                        column: x => x.FeedbackMarkId,
                        principalTable: "360FeedbackMark",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_360Evaluation_Organization",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_360Evaluation_360Question",
                        column: x => x.QuestionId,
                        principalTable: "360Questionarie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "360EvaluationComment",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EvaluationId = table.Column<int>(nullable: false),
                    StartDoing = table.Column<string>(nullable: true),
                    StopDoing = table.Column<string>(nullable: true),
                    OtherComments = table.Column<string>(nullable: true),
                    OrganizationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_360EvaluationComment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_360EvaluationComment_360EmployeeEvaluation",
                        column: x => x.EvaluationId,
                        principalTable: "360EmployeeEvaluation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_360EvaluationComment_Organization",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "360PendingEvaluator",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    _360EmployeeEvaluationId = table.Column<int>(name: "360EmployeeEvaluationId", nullable: false),
                    EvaluatorId = table.Column<int>(nullable: false),
                    Action = table.Column<int>(nullable: false),
                    OrganizationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_360PendingEvaluator", x => x.Id);
                    table.ForeignKey(
                        name: "FK_360PendingEvaluator_Employee",
                        column: x => x.EvaluatorId,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_360PendingEvaluator_Organization",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_360PendingEvaluator_360PendingEvaluator",
                        column: x => x._360EmployeeEvaluationId,
                        principalTable: "360EmployeeEvaluation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EcfEvaluationResult",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EvaluationId = table.Column<int>(nullable: false),
                    Competence = table.Column<string>(unicode: false, maxLength: 3, nullable: false),
                    CompetenceLevel = table.Column<int>(nullable: true),
                    Date = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EcfEvaluationResult", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EcfEvaluation_EcfCompetence",
                        column: x => x.Competence,
                        principalTable: "EcfCompetence",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EcfEvaluationResults_EcfEmployeeEvaluation",
                        column: x => x.EvaluationId,
                        principalTable: "EcfEmployeeEvaluation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_360EmployeeEvaluation_EvaluationId",
                table: "360EmployeeEvaluation",
                column: "EvaluationId");

            migrationBuilder.CreateIndex(
                name: "IX_360EmployeeEvaluation_EvaluatorEmployeeId",
                table: "360EmployeeEvaluation",
                column: "EvaluatorEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_360EmployeeEvaluation_OrganizationId",
                table: "360EmployeeEvaluation",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_360EmployeeEvaluation_360FeedbackGroupId",
                table: "360EmployeeEvaluation",
                column: "360FeedbackGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_360Evaluation_EvaluationId",
                table: "360Evaluation",
                column: "EvaluationId");

            migrationBuilder.CreateIndex(
                name: "IX_360Evaluation_FeedbackMarkId",
                table: "360Evaluation",
                column: "FeedbackMarkId");

            migrationBuilder.CreateIndex(
                name: "IX_360Evaluation_OrganizationId",
                table: "360Evaluation",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_360Evaluation_QuestionId",
                table: "360Evaluation",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_360EvaluationComment_EvaluationId",
                table: "360EvaluationComment",
                column: "EvaluationId");

            migrationBuilder.CreateIndex(
                name: "IX_360EvaluationComment_OrganizationId",
                table: "360EvaluationComment",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_360PendingEvaluator_EvaluatorId",
                table: "360PendingEvaluator",
                column: "EvaluatorId");

            migrationBuilder.CreateIndex(
                name: "IX_360PendingEvaluator_OrganizationId",
                table: "360PendingEvaluator",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_360PendingEvaluator_360EmployeeEvaluationId",
                table: "360PendingEvaluator",
                column: "360EmployeeEvaluationId");

            migrationBuilder.CreateIndex(
                name: "IX_360Question_OrganizationId",
                table: "360Question",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_360Question_QuestionToMarkId",
                table: "360Question",
                column: "QuestionToMarkId");

            migrationBuilder.CreateIndex(
                name: "IX_360Questionarie_OrganizationId",
                table: "360Questionarie",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_360Questionarie_360FeedbackGroupId",
                table: "360Questionarie",
                column: "360FeedbackGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_360QuestionToMark_OrganizationId",
                table: "360QuestionToMark",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_360QuestionToMark_QuestionId",
                table: "360QuestionToMark",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_CompetenceCertificate_CertificateId",
                table: "CompetenceCertificate",
                column: "CertificateId");

            migrationBuilder.CreateIndex(
                name: "IX_CompetenceCertificate_CompetenceId",
                table: "CompetenceCertificate",
                column: "CompetenceId");

            migrationBuilder.CreateIndex(
                name: "IX_CompetenceCertificate_CompetenceLevelId",
                table: "CompetenceCertificate",
                column: "CompetenceLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_CompetenceCertificate_OrganizationId",
                table: "CompetenceCertificate",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerContact_OrganizationId",
                table: "CustomerContact",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerContact_ProjectId",
                table: "CustomerContact",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_EcfCompetenceLevel_CompetenceId",
                table: "EcfCompetenceLevel",
                column: "CompetenceId");

            migrationBuilder.CreateIndex(
                name: "IX_EcfEmployeeEvaluation_EndById",
                table: "EcfEmployeeEvaluation",
                column: "EndById");

            migrationBuilder.CreateIndex(
                name: "IX_EcfEmployeeEvaluation_EvaluationId",
                table: "EcfEmployeeEvaluation",
                column: "EvaluationId");

            migrationBuilder.CreateIndex(
                name: "IX_EcfEmployeeEvaluation_EvaluatorId",
                table: "EcfEmployeeEvaluation",
                column: "EvaluatorId");

            migrationBuilder.CreateIndex(
                name: "IX_EcfEmployeeEvaluation_OrganizationId",
                table: "EcfEmployeeEvaluation",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_EcfEmployeeEvaluation_StartById",
                table: "EcfEmployeeEvaluation",
                column: "StartById");

            migrationBuilder.CreateIndex(
                name: "IX_EcfEvaluationResult_Competence",
                table: "EcfEvaluationResult",
                column: "Competence");

            migrationBuilder.CreateIndex(
                name: "IX_EcfEvaluationResult_EvaluationId",
                table: "EcfEvaluationResult",
                column: "EvaluationId");

            migrationBuilder.CreateIndex(
                name: "IX_Role",
                table: "EcfRole",
                column: "RoleId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EcfRoleCompetence_CompetenceId",
                table: "EcfRoleCompetence",
                column: "CompetenceId");

            migrationBuilder.CreateIndex(
                name: "IX_EcfRoleCompetence_RoleId",
                table: "EcfRoleCompetence",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_EmployeeTypeId",
                table: "Employee",
                column: "EmployeeTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_OrganizationId",
                table: "Employee",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeEvaluation_EmployeeId",
                table: "EmployeeEvaluation",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeEvaluation_EndedById",
                table: "EmployeeEvaluation",
                column: "EndedById");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeEvaluation_OrganizationId",
                table: "EmployeeEvaluation",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeEvaluation_StartedById",
                table: "EmployeeEvaluation",
                column: "StartedById");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeRelations_EmployeeId",
                table: "EmployeeRelations",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeRelations_ManagerId",
                table: "EmployeeRelations",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeRelations_OrganizationId",
                table: "EmployeeRelations",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeRelations_PositionId",
                table: "EmployeeRelations",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeRelations_ProjectId",
                table: "EmployeeRelations",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeRelations_TeamId",
                table: "EmployeeRelations",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeType_OrganizationId",
                table: "EmployeeType",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationSchedule_EmployeeId",
                table: "EvaluationSchedule",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationSchedule_OrganizationId",
                table: "EvaluationSchedule",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Idea_CreatedById",
                table: "Idea",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Idea_OrganizationId",
                table: "Idea",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_IdeaComment_CreatedById",
                table: "IdeaComment",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_IdeaComment_IdeaId",
                table: "IdeaComment",
                column: "IdeaId");

            migrationBuilder.CreateIndex(
                name: "IX_IdeaComment_OrganizationId",
                table: "IdeaComment",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_IdeaComment_ParentCommentId",
                table: "IdeaComment",
                column: "ParentCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_IdeaLike_EmployeeId",
                table: "IdeaLike",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_IdeaLike_IdeaId",
                table: "IdeaLike",
                column: "IdeaId");

            migrationBuilder.CreateIndex(
                name: "IX_IdeaTagRef_IdeaId",
                table: "IdeaTagRef",
                column: "IdeaId");

            migrationBuilder.CreateIndex(
                name: "IX_IdeaTagRef_TagId",
                table: "IdeaTagRef",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_IdeaView_EmployeeId",
                table: "IdeaView",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_IdeaView_IdeaId",
                table: "IdeaView",
                column: "IdeaId");

            migrationBuilder.CreateIndex(
                name: "IX_Pdp_CertificateId",
                table: "Pdp",
                column: "CertificateId");

            migrationBuilder.CreateIndex(
                name: "IX_Pdp_OrganizationId",
                table: "Pdp",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Position_CreatedBy",
                table: "Position",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Position_ProjectId",
                table: "Position",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Position_UpdatedBy",
                table: "Position",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PositionRole_PositionId",
                table: "PositionRole",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_PositionRole_RoleId",
                table: "PositionRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectCareerPath_OrganizationId",
                table: "ProjectCareerPath",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectCareerPath_ProjectId",
                table: "ProjectCareerPath",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectCareerPath_RoleId",
                table: "ProjectCareerPath",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectCareerPath_TeamId",
                table: "ProjectCareerPath",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectPosition_CareerPathId",
                table: "ProjectPosition",
                column: "CareerPathId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectPosition_OrganizationId",
                table: "ProjectPosition",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectPosition_ProjectId",
                table: "ProjectPosition",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectPosition_RoleGradeId",
                table: "ProjectPosition",
                column: "RoleGradeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectPositionCompetence_CompetenceId",
                table: "ProjectPositionCompetence",
                column: "CompetenceId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectPositionCompetence_CompetenceLevelId",
                table: "ProjectPositionCompetence",
                column: "CompetenceLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectPositionCompetence_ProjectPositionId",
                table: "ProjectPositionCompetence",
                column: "ProjectPositionId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectPositionCompetence_RoleGradeId",
                table: "ProjectPositionCompetence",
                column: "RoleGradeId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleGrade_EmployeeTypeId",
                table: "RoleGrade",
                column: "EmployeeTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleGrade_OrganizationId",
                table: "RoleGrade",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleGradeCompetence_CompetenceId",
                table: "RoleGradeCompetence",
                column: "CompetenceId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleGradeCompetence_CompetenceLevelId",
                table: "RoleGradeCompetence",
                column: "CompetenceLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleGradeCompetence_RoleGradeId",
                table: "RoleGradeCompetence",
                column: "RoleGradeId");

            migrationBuilder.CreateIndex(
                name: "IX_Team_OrganizationId",
                table: "Team",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Team_ProjectId",
                table: "Team",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Profile_IdentityId",
                schema: "core",
                table: "Profile",
                column: "IdentityId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "security",
                table: "AspNetRole",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaim_RoleId",
                schema: "security",
                table: "AspNetRoleClaim",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "security",
                table: "AspNetUser",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "security",
                table: "AspNetUser",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaim_UserId",
                schema: "security",
                table: "AspNetUserClaim",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogin_UserId",
                schema: "security",
                table: "AspNetUserLogin",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRole_RoleId",
                schema: "security",
                table: "AspNetUserRole",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "360Evaluation");

            migrationBuilder.DropTable(
                name: "360EvaluationComment");

            migrationBuilder.DropTable(
                name: "360PendingEvaluator");

            migrationBuilder.DropTable(
                name: "360Question");

            migrationBuilder.DropTable(
                name: "CertificationExam");

            migrationBuilder.DropTable(
                name: "CompetenceCertificate");

            migrationBuilder.DropTable(
                name: "CustomerContact");

            migrationBuilder.DropTable(
                name: "EcfEvaluationResult");

            migrationBuilder.DropTable(
                name: "EcfRoleCompetence");

            migrationBuilder.DropTable(
                name: "EmployeeRelations");

            migrationBuilder.DropTable(
                name: "EvaluationSchedule");

            migrationBuilder.DropTable(
                name: "IdeaComment");

            migrationBuilder.DropTable(
                name: "IdeaLike");

            migrationBuilder.DropTable(
                name: "IdeaTagRef");

            migrationBuilder.DropTable(
                name: "IdeaView");

            migrationBuilder.DropTable(
                name: "Pdp");

            migrationBuilder.DropTable(
                name: "PositionRole");

            migrationBuilder.DropTable(
                name: "ProjectPositionCompetence");

            migrationBuilder.DropTable(
                name: "RoleGradeCompetence");

            migrationBuilder.DropTable(
                name: "ActivityLogs",
                schema: "core");

            migrationBuilder.DropTable(
                name: "AppVersion",
                schema: "core");

            migrationBuilder.DropTable(
                name: "Groups",
                schema: "core");

            migrationBuilder.DropTable(
                name: "Profile",
                schema: "core");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaim",
                schema: "security");

            migrationBuilder.DropTable(
                name: "AspNetUserClaim",
                schema: "security");

            migrationBuilder.DropTable(
                name: "AspNetUserLogin",
                schema: "security");

            migrationBuilder.DropTable(
                name: "AspNetUserRole",
                schema: "security");

            migrationBuilder.DropTable(
                name: "AspNetUserToken",
                schema: "security");

            migrationBuilder.DropTable(
                name: "360FeedbackMark");

            migrationBuilder.DropTable(
                name: "360EmployeeEvaluation");

            migrationBuilder.DropTable(
                name: "360QuestionToMark");

            migrationBuilder.DropTable(
                name: "EcfEmployeeEvaluation");

            migrationBuilder.DropTable(
                name: "IdeaTag");

            migrationBuilder.DropTable(
                name: "Idea");

            migrationBuilder.DropTable(
                name: "Certificate");

            migrationBuilder.DropTable(
                name: "Position");

            migrationBuilder.DropTable(
                name: "EcfRole");

            migrationBuilder.DropTable(
                name: "ProjectPosition");

            migrationBuilder.DropTable(
                name: "EcfCompetenceLevel");

            migrationBuilder.DropTable(
                name: "AspNetRole",
                schema: "security");

            migrationBuilder.DropTable(
                name: "AspNetUser",
                schema: "security");

            migrationBuilder.DropTable(
                name: "360Questionarie");

            migrationBuilder.DropTable(
                name: "EmployeeEvaluation");

            migrationBuilder.DropTable(
                name: "ProjectCareerPath");

            migrationBuilder.DropTable(
                name: "RoleGrade");

            migrationBuilder.DropTable(
                name: "EcfCompetence");

            migrationBuilder.DropTable(
                name: "360FeedbackGroup");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "Team");

            migrationBuilder.DropTable(
                name: "EmployeeType");

            migrationBuilder.DropTable(
                name: "Project");

            migrationBuilder.DropTable(
                name: "Organization");
        }
    }
}
