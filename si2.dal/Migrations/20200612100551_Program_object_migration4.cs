using Microsoft.EntityFrameworkCore.Migrations;

namespace si2.dal.Migrations
{
    public partial class Program_object_migration4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Program_InstitutionId",
                table: "Program",
                column: "InstitutionId");

            migrationBuilder.CreateIndex(
                name: "IX_Program_ProgramLevelId",
                table: "Program",
                column: "ProgramLevelId");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Program_Institution_InstitutionId",
                table: "Program");

            migrationBuilder.DropForeignKey(
                name: "FK_Program_ProgramLevel_ProgramLevelId",
                table: "Program");

            migrationBuilder.DropIndex(
                name: "IX_Program_InstitutionId",
                table: "Program");

            migrationBuilder.DropIndex(
                name: "IX_Program_ProgramLevelId",
                table: "Program");
        }
    }
}
