using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace si2.dal.Migrations
{
    public partial class v4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuditEntryProperties_AuditEntries_AuditEntryID",
                table: "AuditEntryProperties");

            migrationBuilder.DropColumn(
                name: "Tag",
                table: "Vehicle");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Dataflow");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Dataflow");

            migrationBuilder.DropColumn(
                name: "Tag",
                table: "Dataflow");

            migrationBuilder.AddColumn<string>(
                name: "FirstNameAr",
                table: "AspNetUsers",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstNameFr",
                table: "AspNetUsers",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastNameAr",
                table: "AspNetUsers",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastNameFr",
                table: "AspNetUsers",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    StreetFr = table.Column<string>(maxLength: 100, nullable: false),
                    StreetAr = table.Column<string>(maxLength: 100, nullable: true),
                    CityFr = table.Column<string>(maxLength: 50, nullable: false),
                    CityAr = table.Column<string>(maxLength: 50, nullable: true),
                    CountryFr = table.Column<string>(maxLength: 50, nullable: false),
                    CountryAr = table.Column<string>(maxLength: 50, nullable: true),
                    Longitude = table.Column<decimal>(type: "decimal(9,6)", nullable: false),
                    Latitude = table.Column<decimal>(type: "decimal(8,6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContactInfo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: false),
                    Phone = table.Column<string>(maxLength: 30, nullable: false),
                    Fax = table.Column<string>(maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DataflowVehicle",
                columns: table => new
                {
                    VehicleId = table.Column<Guid>(nullable: false),
                    DataflowId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataflowVehicle", x => new { x.DataflowId, x.VehicleId });
                    table.ForeignKey(
                        name: "FK_DataflowVehicle_Dataflow_DataflowId",
                        column: x => x.DataflowId,
                        principalTable: "Dataflow",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DataflowVehicle_Vehicle_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Institution",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Code = table.Column<string>(nullable: false),
                    NameFr = table.Column<string>(nullable: false),
                    NameAr = table.Column<string>(nullable: false),
                    NameEn = table.Column<string>(nullable: false),
                    AddressId = table.Column<Guid>(nullable: false),
                    ContactInfoId = table.Column<Guid>(nullable: false),
                    ParentId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Institution", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Institution_Address_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Institution_ContactInfo_ContactInfoId",
                        column: x => x.ContactInfoId,
                        principalTable: "ContactInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Institution_Institution_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Institution",
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
                name: "ProgramLevel",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Credits = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    NameFr = table.Column<string>(maxLength: 30, nullable: false),
                    NameAr = table.Column<string>(maxLength: 30, nullable: false),
                    NameEn = table.Column<string>(maxLength: 30, nullable: false),
                    InstitutionId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramLevel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProgramLevel_Institution_InstitutionId",
                        column: x => x.InstitutionId,
                        principalTable: "Institution",
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

            migrationBuilder.CreateTable(
                name: "Program",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Code = table.Column<string>(maxLength: 6, nullable: false),
                    NameFr = table.Column<string>(maxLength: 100, nullable: false),
                    NameAr = table.Column<string>(maxLength: 100, nullable: false),
                    NameEn = table.Column<string>(maxLength: 100, nullable: false),
                    ProgramLevelId = table.Column<Guid>(nullable: false),
                    InstitutionId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Program", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Program_Institution_InstitutionId",
                        column: x => x.InstitutionId,
                        principalTable: "Institution",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Program_ProgramLevel_ProgramLevelId",
                        column: x => x.ProgramLevelId,
                        principalTable: "ProgramLevel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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
                name: "Document",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    OriginalFileName = table.Column<string>(nullable: true),
                    NameFr = table.Column<string>(maxLength: 100, nullable: false),
                    NameAr = table.Column<string>(maxLength: 100, nullable: false),
                    NameEn = table.Column<string>(maxLength: 100, nullable: false),
                    DescriptionFr = table.Column<string>(maxLength: 100, nullable: false),
                    DescriptionAr = table.Column<string>(maxLength: 100, nullable: false),
                    DescriptionEn = table.Column<string>(maxLength: 100, nullable: false),
                    ContentType = table.Column<string>(nullable: false),
                    UploadedOn = table.Column<DateTime>(nullable: false),
                    UploadedBy = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    InstitutionId = table.Column<Guid>(nullable: true),
                    ProgramId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Document", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Document_Institution_InstitutionId",
                        column: x => x.InstitutionId,
                        principalTable: "Institution",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Document_Program_ProgramId",
                        column: x => x.ProgramId,
                        principalTable: "Program",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Document_AspNetUsers_UserId",
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
                name: "DocumentData",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    DocumentId = table.Column<Guid>(nullable: false),
                    FileBytes = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentData_Document_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "Document",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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
                name: "IX_DataflowVehicle_VehicleId",
                table: "DataflowVehicle",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_Document_InstitutionId",
                table: "Document",
                column: "InstitutionId");

            migrationBuilder.CreateIndex(
                name: "IX_Document_ProgramId",
                table: "Document",
                column: "ProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_Document_UserId",
                table: "Document",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentData_DocumentId",
                table: "DocumentData",
                column: "DocumentId",
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
                name: "IX_Program_Code",
                table: "Program",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Program_InstitutionId",
                table: "Program",
                column: "InstitutionId");

            migrationBuilder.CreateIndex(
                name: "IX_Program_ProgramLevelId",
                table: "Program",
                column: "ProgramLevelId");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuditEntryProperties_AuditEntries_AuditEntryID",
                table: "AuditEntryProperties");

            migrationBuilder.DropTable(
                name: "CourseCohort");

            migrationBuilder.DropTable(
                name: "DataflowVehicle");

            migrationBuilder.DropTable(
                name: "DocumentData");

            migrationBuilder.DropTable(
                name: "UserCohort");

            migrationBuilder.DropTable(
                name: "UserCourse");

            migrationBuilder.DropTable(
                name: "Document");

            migrationBuilder.DropTable(
                name: "Cohort");

            migrationBuilder.DropTable(
                name: "Course");

            migrationBuilder.DropTable(
                name: "Program");

            migrationBuilder.DropTable(
                name: "ProgramLevel");

            migrationBuilder.DropTable(
                name: "Institution");

            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "ContactInfo");

            migrationBuilder.DropColumn(
                name: "FirstNameAr",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FirstNameFr",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastNameAr",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastNameFr",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "Tag",
                table: "Vehicle",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Dataflow",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Dataflow",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Tag",
                table: "Dataflow",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_AuditEntryProperties_AuditEntries_AuditEntryID",
                table: "AuditEntryProperties",
                column: "AuditEntryID",
                principalTable: "AuditEntries",
                principalColumn: "AuditEntryID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
