using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace si2.dal.Migrations
{
    public partial class v4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Document_Institution_UniversityId",
                table: "Document");

            migrationBuilder.DropForeignKey(
                name: "FK_Document_AspNetUsers_UploadedBy",
                table: "Document");

            migrationBuilder.DropIndex(
                name: "IX_Document_UniversityId",
                table: "Document");

            migrationBuilder.DropIndex(
                name: "IX_Document_UploadedBy",
                table: "Document");

            migrationBuilder.DropColumn(
                name: "UniversityId",
                table: "Document");

            migrationBuilder.AlterColumn<string>(
                name: "UploadedBy",
                table: "Document",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Document",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Document_InstitutionId",
                table: "Document",
                column: "InstitutionId");

            migrationBuilder.CreateIndex(
                name: "IX_Document_UserId",
                table: "Document",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Document_Institution_InstitutionId",
                table: "Document",
                column: "InstitutionId",
                principalTable: "Institution",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Document_AspNetUsers_UserId",
                table: "Document",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Document_Institution_InstitutionId",
                table: "Document");

            migrationBuilder.DropForeignKey(
                name: "FK_Document_AspNetUsers_UserId",
                table: "Document");

            migrationBuilder.DropIndex(
                name: "IX_Document_InstitutionId",
                table: "Document");

            migrationBuilder.DropIndex(
                name: "IX_Document_UserId",
                table: "Document");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Document");

            migrationBuilder.AlterColumn<string>(
                name: "UploadedBy",
                table: "Document",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<Guid>(
                name: "UniversityId",
                table: "Document",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Document_UniversityId",
                table: "Document",
                column: "UniversityId");

            migrationBuilder.CreateIndex(
                name: "IX_Document_UploadedBy",
                table: "Document",
                column: "UploadedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_Document_Institution_UniversityId",
                table: "Document",
                column: "UniversityId",
                principalTable: "Institution",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Document_AspNetUsers_UploadedBy",
                table: "Document",
                column: "UploadedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
