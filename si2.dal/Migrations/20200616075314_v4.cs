using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace si2.dal.Migrations
{
    public partial class v4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "ProgramLevel",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramLevel", x => x.Id);
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
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserCohort_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
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
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Institution_ContactInfo_ContactInfoId",
                        column: x => x.ContactInfoId,
                        principalTable: "ContactInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Institution_Institution_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Institution",
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
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Program_ProgramLevel_ProgramLevelId",
                        column: x => x.ProgramLevelId,
                        principalTable: "ProgramLevel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cohort_Promotion",
                table: "Cohort",
                column: "Promotion",
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
                name: "IX_UserCohort_CohortId",
                table: "UserCohort",
                column: "CohortId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCohort_UserId_CohortId",
                table: "UserCohort",
                columns: new[] { "UserId", "CohortId" },
                unique: true,
                filter: "[UserId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Program");

            migrationBuilder.DropTable(
                name: "UserCohort");

            migrationBuilder.DropTable(
                name: "Institution");

            migrationBuilder.DropTable(
                name: "ProgramLevel");

            migrationBuilder.DropTable(
                name: "Cohort");

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
        }
    }
}
