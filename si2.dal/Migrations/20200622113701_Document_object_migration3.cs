using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace si2.dal.Migrations
{
    public partial class Document_object_migration3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuditEntryProperties_AuditEntries_AuditEntryID",
                table: "AuditEntryProperties");

            migrationBuilder.DropForeignKey(
                name: "FK_Document_University_UniversityId",
                table: "Document");

            migrationBuilder.DropForeignKey(
                name: "FK_Program_Institution_InstitutionId",
                table: "Program");

            migrationBuilder.DropForeignKey(
                name: "FK_Program_ProgramLevel_ProgramLevelId",
                table: "Program");

            migrationBuilder.DropTable(
                name: "University");

            migrationBuilder.DropIndex(
                name: "IX_Document_UniversityId",
                table: "Document");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "ProgramLevel");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Institution");

            migrationBuilder.DropColumn(
                name: "UniversityId",
                table: "Document");

            migrationBuilder.AddColumn<decimal>(
                name: "Credits",
                table: "ProgramLevel",
                type: "decimal(5,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<Guid>(
                name: "InstitutionId",
                table: "ProgramLevel",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "NameAr",
                table: "ProgramLevel",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NameEn",
                table: "ProgramLevel",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NameFr",
                table: "ProgramLevel",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "AddressId",
                table: "Institution",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Institution",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "ContactInfoId",
                table: "Institution",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "NameAr",
                table: "Institution",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NameEn",
                table: "Institution",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NameFr",
                table: "Institution",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "ParentId",
                table: "Institution",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Cohort",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Promotion = table.Column<string>(maxLength: 20, nullable: false),
                    ProgramId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cohort", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cohort_Program_ProgramId",
                        column: x => x.ProgramId,
                        principalTable: "Program",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Course",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Code = table.Column<string>(maxLength: 10, nullable: false),
                    NameFr = table.Column<string>(maxLength: 200, nullable: false),
                    NameAr = table.Column<string>(maxLength: 200, nullable: false),
                    NameEn = table.Column<string>(maxLength: 200, nullable: false),
                    Credits = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    InstitutionId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Course", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Course_Institution_InstitutionId",
                        column: x => x.InstitutionId,
                        principalTable: "Institution",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserCohort",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    CohortId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCohort", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserCohort_Cohort_CohortId",
                        column: x => x.CohortId,
                        principalTable: "Cohort",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserCohort_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CourseCohort",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    CourseId = table.Column<Guid>(nullable: false),
                    CohortId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseCohort", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseCohort_Cohort_CohortId",
                        column: x => x.CohortId,
                        principalTable: "Cohort",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CourseCohort_Course_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Course",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserCourse",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    CourseId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCourse", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserCourse_Course_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Course",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserCourse_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProgramLevel_InstitutionId",
                table: "ProgramLevel",
                column: "InstitutionId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramLevel_NameAr",
                table: "ProgramLevel",
                column: "NameAr",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProgramLevel_NameEn",
                table: "ProgramLevel",
                column: "NameEn",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProgramLevel_NameFr",
                table: "ProgramLevel",
                column: "NameFr",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Institution_AddressId",
                table: "Institution",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Institution_Code",
                table: "Institution",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Institution_ContactInfoId",
                table: "Institution",
                column: "ContactInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Institution_ParentId",
                table: "Institution",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Cohort_ProgramId",
                table: "Cohort",
                column: "ProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_Cohort_Promotion",
                table: "Cohort",
                column: "Promotion",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Course_Code",
                table: "Course",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Course_InstitutionId",
                table: "Course",
                column: "InstitutionId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseCohort_CohortId",
                table: "CourseCohort",
                column: "CohortId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseCohort_CourseId_CohortId",
                table: "CourseCohort",
                columns: new[] { "CourseId", "CohortId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserCohort_CohortId",
                table: "UserCohort",
                column: "CohortId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCohort_UserId_CohortId",
                table: "UserCohort",
                columns: new[] { "UserId", "CohortId" },
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserCourse_CourseId",
                table: "UserCourse",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCourse_UserId_CourseId",
                table: "UserCourse",
                columns: new[] { "UserId", "CourseId" },
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_AuditEntryProperties_AuditEntries_AuditEntryID",
                table: "AuditEntryProperties",
                column: "AuditEntryID",
                principalTable: "AuditEntries",
                principalColumn: "AuditEntryID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Institution_Address_AddressId",
                table: "Institution",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Institution_ContactInfo_ContactInfoId",
                table: "Institution",
                column: "ContactInfoId",
                principalTable: "ContactInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Institution_Institution_ParentId",
                table: "Institution",
                column: "ParentId",
                principalTable: "Institution",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Program_Institution_InstitutionId",
                table: "Program",
                column: "InstitutionId",
                principalTable: "Institution",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Program_ProgramLevel_ProgramLevelId",
                table: "Program",
                column: "ProgramLevelId",
                principalTable: "ProgramLevel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProgramLevel_Institution_InstitutionId",
                table: "ProgramLevel",
                column: "InstitutionId",
                principalTable: "Institution",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuditEntryProperties_AuditEntries_AuditEntryID",
                table: "AuditEntryProperties");

            migrationBuilder.DropForeignKey(
                name: "FK_Institution_Address_AddressId",
                table: "Institution");

            migrationBuilder.DropForeignKey(
                name: "FK_Institution_ContactInfo_ContactInfoId",
                table: "Institution");

            migrationBuilder.DropForeignKey(
                name: "FK_Institution_Institution_ParentId",
                table: "Institution");

            migrationBuilder.DropForeignKey(
                name: "FK_Program_Institution_InstitutionId",
                table: "Program");

            migrationBuilder.DropForeignKey(
                name: "FK_Program_ProgramLevel_ProgramLevelId",
                table: "Program");

            migrationBuilder.DropForeignKey(
                name: "FK_ProgramLevel_Institution_InstitutionId",
                table: "ProgramLevel");

            migrationBuilder.DropTable(
                name: "CourseCohort");

            migrationBuilder.DropTable(
                name: "UserCohort");

            migrationBuilder.DropTable(
                name: "UserCourse");

            migrationBuilder.DropTable(
                name: "Cohort");

            migrationBuilder.DropTable(
                name: "Course");

            migrationBuilder.DropIndex(
                name: "IX_ProgramLevel_InstitutionId",
                table: "ProgramLevel");

            migrationBuilder.DropIndex(
                name: "IX_ProgramLevel_NameAr",
                table: "ProgramLevel");

            migrationBuilder.DropIndex(
                name: "IX_ProgramLevel_NameEn",
                table: "ProgramLevel");

            migrationBuilder.DropIndex(
                name: "IX_ProgramLevel_NameFr",
                table: "ProgramLevel");

            migrationBuilder.DropIndex(
                name: "IX_Institution_AddressId",
                table: "Institution");

            migrationBuilder.DropIndex(
                name: "IX_Institution_Code",
                table: "Institution");

            migrationBuilder.DropIndex(
                name: "IX_Institution_ContactInfoId",
                table: "Institution");

            migrationBuilder.DropIndex(
                name: "IX_Institution_ParentId",
                table: "Institution");

            migrationBuilder.DropColumn(
                name: "Credits",
                table: "ProgramLevel");

            migrationBuilder.DropColumn(
                name: "InstitutionId",
                table: "ProgramLevel");

            migrationBuilder.DropColumn(
                name: "NameAr",
                table: "ProgramLevel");

            migrationBuilder.DropColumn(
                name: "NameEn",
                table: "ProgramLevel");

            migrationBuilder.DropColumn(
                name: "NameFr",
                table: "ProgramLevel");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "Institution");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "Institution");

            migrationBuilder.DropColumn(
                name: "ContactInfoId",
                table: "Institution");

            migrationBuilder.DropColumn(
                name: "NameAr",
                table: "Institution");

            migrationBuilder.DropColumn(
                name: "NameEn",
                table: "Institution");

            migrationBuilder.DropColumn(
                name: "NameFr",
                table: "Institution");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Institution");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "ProgramLevel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Institution",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UniversityId",
                table: "Document",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "University",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    UniversityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_University", x => x.Id);
                    table.ForeignKey(
                        name: "FK_University_University_UniversityId",
                        column: x => x.UniversityId,
                        principalTable: "University",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Document_UniversityId",
                table: "Document",
                column: "UniversityId");

            migrationBuilder.CreateIndex(
                name: "IX_University_UniversityId",
                table: "University",
                column: "UniversityId");

            migrationBuilder.AddForeignKey(
                name: "FK_AuditEntryProperties_AuditEntries_AuditEntryID",
                table: "AuditEntryProperties",
                column: "AuditEntryID",
                principalTable: "AuditEntries",
                principalColumn: "AuditEntryID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Document_University_UniversityId",
                table: "Document",
                column: "UniversityId",
                principalTable: "University",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Program_Institution_InstitutionId",
                table: "Program",
                column: "InstitutionId",
                principalTable: "Institution",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Program_ProgramLevel_ProgramLevelId",
                table: "Program",
                column: "ProgramLevelId",
                principalTable: "ProgramLevel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
