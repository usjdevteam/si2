using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace si2.dal.Migrations
{
    public partial class Document_object_migration3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Document_University_UniversityId",
                table: "Document");

            migrationBuilder.DropForeignKey(
                name: "FK_University_University_UniversityId",
                table: "University");

            migrationBuilder.DropIndex(
                name: "IX_UserCohort_UniversityId",
                table: "UserCohort");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_University_TempId",
                table: "University");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_University_TempId1",
                table: "University");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "UserCohort");

            migrationBuilder.DropColumn(
                name: "UniversityId",
                table: "UserCohort");

            migrationBuilder.DropColumn(
                name: "TempId",
                table: "University");

            migrationBuilder.DropColumn(
                name: "TempId1",
                table: "University");

            migrationBuilder.AlterColumn<Guid>(
                name: "UniversityId",
                table: "University",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "University",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "University",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "University",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_University",
                table: "University",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_University_UniversityId",
                table: "University",
                column: "UniversityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Document_University_UniversityId",
                table: "Document",
                column: "UniversityId",
                principalTable: "University",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_University_University_UniversityId",
                table: "University",
                column: "UniversityId",
                principalTable: "University",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Document_University_UniversityId",
                table: "Document");

            migrationBuilder.DropForeignKey(
                name: "FK_University_University_UniversityId",
                table: "University");

            migrationBuilder.DropPrimaryKey(
                name: "PK_University",
                table: "University");

            migrationBuilder.DropIndex(
                name: "IX_University_UniversityId",
                table: "University");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "University");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "University");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "University");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "UserCohort",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UniversityId",
                table: "UserCohort",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UniversityId",
                table: "University",
                nullable: true,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TempId",
                table: "University",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "TempId1",
                table: "University",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddUniqueConstraint(
                name: "AK_University_TempId",
                table: "University",
                column: "TempId");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_University_TempId1",
                table: "University",
                column: "TempId1");

            migrationBuilder.CreateIndex(
                name: "IX_UserCohort_UniversityId",
                table: "UserCohort",
                column: "UniversityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Document_University_UniversityId",
                table: "Document",
                column: "UniversityId",
                principalTable: "University",
                principalColumn: "TempId1",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_University_University_UniversityId",
                table: "University",
                column: "UniversityId",
                principalTable: "University",
                principalColumn: "TempId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
