using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace si2.dal.Migrations
{
    public partial class Added_ParentChildren : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParentInstitutionId",
                table: "Institution");

            migrationBuilder.AddColumn<Guid>(
                name: "ParentId",
                table: "Institution",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Institution_ParentId",
                table: "Institution",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Institution_Institution_ParentId",
                table: "Institution",
                column: "ParentId",
                principalTable: "Institution",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Institution_Institution_ParentId",
                table: "Institution");

            migrationBuilder.DropIndex(
                name: "IX_Institution_ParentId",
                table: "Institution");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Institution");

            migrationBuilder.AddColumn<Guid>(
                name: "ParentInstitutionId",
                table: "Institution",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
