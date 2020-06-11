using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace si2.dal.Migrations
{
    public partial class Updating_ParentInstId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Institution_Institution_InstitutionAttrId",
                table: "Institution");

            migrationBuilder.DropIndex(
                name: "IX_Institution_InstitutionAttrId",
                table: "Institution");

            migrationBuilder.DropColumn(
                name: "InstitutionAttrId",
                table: "Institution");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Institution",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "Index_Institution_Code",
                table: "Institution",
                column: "Code",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "Index_Institution_Code",
                table: "Institution");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Institution",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<Guid>(
                name: "InstitutionAttrId",
                table: "Institution",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Institution_InstitutionAttrId",
                table: "Institution",
                column: "InstitutionAttrId");

            migrationBuilder.AddForeignKey(
                name: "FK_Institution_Institution_InstitutionAttrId",
                table: "Institution",
                column: "InstitutionAttrId",
                principalTable: "Institution",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
