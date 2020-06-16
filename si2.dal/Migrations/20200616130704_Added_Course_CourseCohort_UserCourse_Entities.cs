using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace si2.dal.Migrations
{
    public partial class Added_Course_CourseCohort_UserCourse_Entities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    Credits = table.Column<float>(nullable: false),
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
                        onDelete: ReferentialAction.Cascade);
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
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseCohort_Course_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Course",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserCourse_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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
                name: "IX_UserCourse_CourseId",
                table: "UserCourse",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCourse_UserId_CourseId",
                table: "UserCourse",
                columns: new[] { "UserId", "CourseId" },
                unique: true,
                filter: "[UserId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseCohort");

            migrationBuilder.DropTable(
                name: "UserCourse");

            migrationBuilder.DropTable(
                name: "Course");
        }
    }
}
