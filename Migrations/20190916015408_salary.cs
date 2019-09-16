using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Evoflare.API.Migrations
{
    public partial class salary : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_360EmployeeEvaluation_360FeedbackGroup",
                table: "360EmployeeEvaluation");

            migrationBuilder.DropForeignKey(
                name: "FK_IdeaTagRef_Idea_IdeaId",
                table: "IdeaTagRef");

            migrationBuilder.DropForeignKey(
                name: "FK_IdeaTagRef_IdeaTag_TagId",
                table: "IdeaTagRef");

            migrationBuilder.DropForeignKey(
                name: "FK_Profile_AspNetUser_IdentityId",
                schema: "core",
                table: "Profile");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoleClaim_AspNetRole_RoleId",
                schema: "security",
                table: "AspNetRoleClaim");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaim_AspNetUser_UserId",
                schema: "security",
                table: "AspNetUserClaim");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogin_AspNetUser_UserId",
                schema: "security",
                table: "AspNetUserLogin");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRole_AspNetRole_RoleId",
                schema: "security",
                table: "AspNetUserRole");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRole_AspNetUser_UserId",
                schema: "security",
                table: "AspNetUserRole");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserToken_AspNetUser_UserId",
                schema: "security",
                table: "AspNetUserToken");

            migrationBuilder.DropTable(
                name: "360Evaluation");

            migrationBuilder.DropTable(
                name: "360EvaluationComment");

            migrationBuilder.DropTable(
                name: "360Question");

            migrationBuilder.DropTable(
                name: "360FeedbackMark");

            migrationBuilder.DropTable(
                name: "360QuestionToMark");

            migrationBuilder.DropTable(
                name: "360Questionarie");

            migrationBuilder.DropTable(
                name: "360FeedbackGroup");

            migrationBuilder.DropIndex(
                name: "IX_360EmployeeEvaluation_360FeedbackGroupId",
                table: "360EmployeeEvaluation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserToken",
                schema: "security",
                table: "AspNetUserToken");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserRole",
                schema: "security",
                table: "AspNetUserRole");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserLogin",
                schema: "security",
                table: "AspNetUserLogin");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserClaim",
                schema: "security",
                table: "AspNetUserClaim");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUser",
                schema: "security",
                table: "AspNetUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetRoleClaim",
                schema: "security",
                table: "AspNetRoleClaim");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetRole",
                schema: "security",
                table: "AspNetRole");

            migrationBuilder.DropColumn(
                name: "360FeedbackGroupId",
                table: "360EmployeeEvaluation");

            migrationBuilder.RenameTable(
                name: "AspNetUserToken",
                schema: "security",
                newName: "UserTokens",
                newSchema: "security");

            migrationBuilder.RenameTable(
                name: "AspNetUserRole",
                schema: "security",
                newName: "UserRoles",
                newSchema: "security");

            migrationBuilder.RenameTable(
                name: "AspNetUserLogin",
                schema: "security",
                newName: "UserLogins",
                newSchema: "security");

            migrationBuilder.RenameTable(
                name: "AspNetUserClaim",
                schema: "security",
                newName: "UserClaims",
                newSchema: "security");

            migrationBuilder.RenameTable(
                name: "AspNetUser",
                schema: "security",
                newName: "Users",
                newSchema: "security");

            migrationBuilder.RenameTable(
                name: "AspNetRoleClaim",
                schema: "security",
                newName: "RoleClaims",
                newSchema: "security");

            migrationBuilder.RenameTable(
                name: "AspNetRole",
                schema: "security",
                newName: "Roles",
                newSchema: "security");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserRole_RoleId",
                schema: "security",
                table: "UserRoles",
                newName: "IX_UserRoles_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserLogin_UserId",
                schema: "security",
                table: "UserLogins",
                newName: "IX_UserLogins_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserClaim_UserId",
                schema: "security",
                table: "UserClaims",
                newName: "IX_UserClaims_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetRoleClaim_RoleId",
                schema: "security",
                table: "RoleClaims",
                newName: "IX_RoleClaims_RoleId");

            migrationBuilder.RenameColumn(
                name: "AccessGroup",
                schema: "security",
                table: "Roles",
                newName: "PolicyName");

            migrationBuilder.AddColumn<string>(
                name: "DatabaseType",
                schema: "core",
                table: "AppVersion",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Organization",
                schema: "core",
                table: "AppVersion",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Project",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "IdeaTag",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Employee",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DefaultPermission",
                schema: "security",
                table: "Roles",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserTokens",
                schema: "security",
                table: "UserTokens",
                columns: new[] { "UserId", "LoginProvider", "Name" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRoles",
                schema: "security",
                table: "UserRoles",
                columns: new[] { "UserId", "RoleId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserLogins",
                schema: "security",
                table: "UserLogins",
                columns: new[] { "LoginProvider", "ProviderKey" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserClaims",
                schema: "security",
                table: "UserClaims",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                schema: "security",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoleClaims",
                schema: "security",
                table: "RoleClaims",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Roles",
                schema: "security",
                table: "Roles",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "360EvaluationSchedule",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PeriodMonths = table.Column<int>(nullable: false),
                    EvaluationWindowMonths = table.Column<int>(nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_360EvaluationSchedule", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "360Questionnarie",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    IsForManager = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_360Questionnarie", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeSalary",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EmployeeId = table.Column<int>(nullable: false),
                    Period = table.Column<DateTime>(type: "datetime", nullable: false),
                    Basic = table.Column<int>(nullable: false),
                    Bonus = table.Column<int>(nullable: false),
                    Archived = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeSalary", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeSalary_Employee",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EmployeeId = table.Column<int>(nullable: false),
                    Message = table.Column<string>(nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    ViewDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrganizationStructureType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationStructureType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "360QuestionnarieStatement",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    QuestionnarieId = table.Column<int>(nullable: false),
                    Mark = table.Column<int>(nullable: false),
                    Text = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_360QuestionnarieStatement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_360QuestionnarieStatement_360Questionnarie",
                        column: x => x.QuestionnarieId,
                        principalTable: "360Questionnarie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "360EvaluationResult",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EvaluationId = table.Column<int>(nullable: false),
                    _360QuestionnarieStatementId = table.Column<int>(name: "360QuestionnarieStatementId", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_360EvaluationResult", x => x.Id);
                    table.ForeignKey(
                        name: "FK_360EvaluationResult_360EmployeeEvaluation",
                        column: x => x.EvaluationId,
                        principalTable: "360EmployeeEvaluation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_360EvaluationResult_360QuestionnarieStatement",
                        column: x => x._360QuestionnarieStatementId,
                        principalTable: "360QuestionnarieStatement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employee_UserId",
                table: "Employee",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_360EvaluationResult_EvaluationId",
                table: "360EvaluationResult",
                column: "EvaluationId");

            migrationBuilder.CreateIndex(
                name: "IX_360EvaluationResult_360QuestionnarieStatementId",
                table: "360EvaluationResult",
                column: "360QuestionnarieStatementId");

            migrationBuilder.CreateIndex(
                name: "IX_360QuestionnarieStatement_QuestionnarieId",
                table: "360QuestionnarieStatement",
                column: "QuestionnarieId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeSalary_EmployeeId",
                table: "EmployeeSalary",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_Users_UserId",
                table: "Employee",
                column: "UserId",
                principalSchema: "security",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IdeaTagRef_Idea",
                table: "IdeaTagRef",
                column: "IdeaId",
                principalTable: "Idea",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_IdeaTagRef_IdeaTag",
                table: "IdeaTagRef",
                column: "TagId",
                principalTable: "IdeaTag",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Profile_Users_IdentityId",
                schema: "core",
                table: "Profile",
                column: "IdentityId",
                principalSchema: "security",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RoleClaims_Roles_RoleId",
                schema: "security",
                table: "RoleClaims",
                column: "RoleId",
                principalSchema: "security",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserClaims_Users_UserId",
                schema: "security",
                table: "UserClaims",
                column: "UserId",
                principalSchema: "security",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserLogins_Users_UserId",
                schema: "security",
                table: "UserLogins",
                column: "UserId",
                principalSchema: "security",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_Roles_RoleId",
                schema: "security",
                table: "UserRoles",
                column: "RoleId",
                principalSchema: "security",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_Users_UserId",
                schema: "security",
                table: "UserRoles",
                column: "UserId",
                principalSchema: "security",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserTokens_Users_UserId",
                schema: "security",
                table: "UserTokens",
                column: "UserId",
                principalSchema: "security",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employee_Users_UserId",
                table: "Employee");

            migrationBuilder.DropForeignKey(
                name: "FK_IdeaTagRef_Idea",
                table: "IdeaTagRef");

            migrationBuilder.DropForeignKey(
                name: "FK_IdeaTagRef_IdeaTag",
                table: "IdeaTagRef");

            migrationBuilder.DropForeignKey(
                name: "FK_Profile_Users_IdentityId",
                schema: "core",
                table: "Profile");

            migrationBuilder.DropForeignKey(
                name: "FK_RoleClaims_Roles_RoleId",
                schema: "security",
                table: "RoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_UserClaims_Users_UserId",
                schema: "security",
                table: "UserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_UserLogins_Users_UserId",
                schema: "security",
                table: "UserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_Roles_RoleId",
                schema: "security",
                table: "UserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_Users_UserId",
                schema: "security",
                table: "UserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTokens_Users_UserId",
                schema: "security",
                table: "UserTokens");

            migrationBuilder.DropTable(
                name: "360EvaluationResult");

            migrationBuilder.DropTable(
                name: "360EvaluationSchedule");

            migrationBuilder.DropTable(
                name: "EmployeeSalary");

            migrationBuilder.DropTable(
                name: "Notification");

            migrationBuilder.DropTable(
                name: "OrganizationStructureType");

            migrationBuilder.DropTable(
                name: "360QuestionnarieStatement");

            migrationBuilder.DropTable(
                name: "360Questionnarie");

            migrationBuilder.DropIndex(
                name: "IX_Employee_UserId",
                table: "Employee");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserTokens",
                schema: "security",
                table: "UserTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                schema: "security",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRoles",
                schema: "security",
                table: "UserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserLogins",
                schema: "security",
                table: "UserLogins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserClaims",
                schema: "security",
                table: "UserClaims");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Roles",
                schema: "security",
                table: "Roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoleClaims",
                schema: "security",
                table: "RoleClaims");

            migrationBuilder.DropColumn(
                name: "DatabaseType",
                schema: "core",
                table: "AppVersion");

            migrationBuilder.DropColumn(
                name: "Organization",
                schema: "core",
                table: "AppVersion");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "DefaultPermission",
                schema: "security",
                table: "Roles");

            migrationBuilder.RenameTable(
                name: "UserTokens",
                schema: "security",
                newName: "AspNetUserToken",
                newSchema: "security");

            migrationBuilder.RenameTable(
                name: "Users",
                schema: "security",
                newName: "AspNetUser",
                newSchema: "security");

            migrationBuilder.RenameTable(
                name: "UserRoles",
                schema: "security",
                newName: "AspNetUserRole",
                newSchema: "security");

            migrationBuilder.RenameTable(
                name: "UserLogins",
                schema: "security",
                newName: "AspNetUserLogin",
                newSchema: "security");

            migrationBuilder.RenameTable(
                name: "UserClaims",
                schema: "security",
                newName: "AspNetUserClaim",
                newSchema: "security");

            migrationBuilder.RenameTable(
                name: "Roles",
                schema: "security",
                newName: "AspNetRole",
                newSchema: "security");

            migrationBuilder.RenameTable(
                name: "RoleClaims",
                schema: "security",
                newName: "AspNetRoleClaim",
                newSchema: "security");

            migrationBuilder.RenameIndex(
                name: "IX_UserRoles_RoleId",
                schema: "security",
                table: "AspNetUserRole",
                newName: "IX_AspNetUserRole_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_UserLogins_UserId",
                schema: "security",
                table: "AspNetUserLogin",
                newName: "IX_AspNetUserLogin_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserClaims_UserId",
                schema: "security",
                table: "AspNetUserClaim",
                newName: "IX_AspNetUserClaim_UserId");

            migrationBuilder.RenameColumn(
                name: "PolicyName",
                schema: "security",
                table: "AspNetRole",
                newName: "AccessGroup");

            migrationBuilder.RenameIndex(
                name: "IX_RoleClaims_RoleId",
                schema: "security",
                table: "AspNetRoleClaim",
                newName: "IX_AspNetRoleClaim_RoleId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "IdeaTag",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Employee",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "360FeedbackGroupId",
                table: "360EmployeeEvaluation",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserToken",
                schema: "security",
                table: "AspNetUserToken",
                columns: new[] { "UserId", "LoginProvider", "Name" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUser",
                schema: "security",
                table: "AspNetUser",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserRole",
                schema: "security",
                table: "AspNetUserRole",
                columns: new[] { "UserId", "RoleId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserLogin",
                schema: "security",
                table: "AspNetUserLogin",
                columns: new[] { "LoginProvider", "ProviderKey" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserClaim",
                schema: "security",
                table: "AspNetUserClaim",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetRole",
                schema: "security",
                table: "AspNetRole",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetRoleClaim",
                schema: "security",
                table: "AspNetRoleClaim",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "360EvaluationComment",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EvaluationId = table.Column<int>(nullable: false),
                    OrganizationId = table.Column<int>(nullable: false),
                    OtherComments = table.Column<string>(nullable: true),
                    StartDoing = table.Column<string>(nullable: true),
                    StopDoing = table.Column<string>(nullable: true)
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
                name: "360Questionarie",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    OrganizationId = table.Column<int>(nullable: false),
                    Text = table.Column<string>(maxLength: 250, nullable: false),
                    _360FeedbackGroupId = table.Column<int>(name: "360FeedbackGroupId", nullable: false)
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
                name: "360Evaluation",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EvaluationId = table.Column<int>(nullable: false),
                    FeedbackMarkId = table.Column<int>(nullable: false),
                    OrganizationId = table.Column<int>(nullable: false),
                    QuestionId = table.Column<int>(nullable: false)
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
                name: "360QuestionToMark",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MarkId = table.Column<int>(nullable: false),
                    OrganizationId = table.Column<int>(nullable: false),
                    QuestionId = table.Column<int>(nullable: false)
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
                name: "360Question",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Order = table.Column<int>(nullable: false),
                    OrganizationId = table.Column<int>(nullable: false),
                    Question = table.Column<string>(maxLength: 250, nullable: false),
                    QuestionToMarkId = table.Column<int>(nullable: false)
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

            migrationBuilder.AddForeignKey(
                name: "FK_360EmployeeEvaluation_360FeedbackGroup",
                table: "360EmployeeEvaluation",
                column: "360FeedbackGroupId",
                principalTable: "360FeedbackGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_IdeaTagRef_Idea_IdeaId",
                table: "IdeaTagRef",
                column: "IdeaId",
                principalTable: "Idea",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IdeaTagRef_IdeaTag_TagId",
                table: "IdeaTagRef",
                column: "TagId",
                principalTable: "IdeaTag",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Profile_AspNetUser_IdentityId",
                schema: "core",
                table: "Profile",
                column: "IdentityId",
                principalSchema: "security",
                principalTable: "AspNetUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoleClaim_AspNetRole_RoleId",
                schema: "security",
                table: "AspNetRoleClaim",
                column: "RoleId",
                principalSchema: "security",
                principalTable: "AspNetRole",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaim_AspNetUser_UserId",
                schema: "security",
                table: "AspNetUserClaim",
                column: "UserId",
                principalSchema: "security",
                principalTable: "AspNetUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogin_AspNetUser_UserId",
                schema: "security",
                table: "AspNetUserLogin",
                column: "UserId",
                principalSchema: "security",
                principalTable: "AspNetUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRole_AspNetRole_RoleId",
                schema: "security",
                table: "AspNetUserRole",
                column: "RoleId",
                principalSchema: "security",
                principalTable: "AspNetRole",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRole_AspNetUser_UserId",
                schema: "security",
                table: "AspNetUserRole",
                column: "UserId",
                principalSchema: "security",
                principalTable: "AspNetUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserToken_AspNetUser_UserId",
                schema: "security",
                table: "AspNetUserToken",
                column: "UserId",
                principalSchema: "security",
                principalTable: "AspNetUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
