using Microsoft.EntityFrameworkCore.Migrations;

namespace si2.dal.Migrations
{
    public partial class MyFirstMigration7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_ProgramLevel_nameEn",
                table: "ProgramLevel");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_ProgramLevel_nameFr",
                table: "ProgramLevel");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramLevel_nameAr",
                table: "ProgramLevel",
                column: "nameAr",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProgramLevel_nameEn",
                table: "ProgramLevel",
                column: "nameEn",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProgramLevel_nameFr",
                table: "ProgramLevel",
                column: "nameFr",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProgramLevel_nameAr",
                table: "ProgramLevel");

            migrationBuilder.DropIndex(
                name: "IX_ProgramLevel_nameEn",
                table: "ProgramLevel");

            migrationBuilder.DropIndex(
                name: "IX_ProgramLevel_nameFr",
                table: "ProgramLevel");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_ProgramLevel_nameEn",
                table: "ProgramLevel",
                column: "nameEn");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_ProgramLevel_nameFr",
                table: "ProgramLevel",
                column: "nameFr");
        }
    }
}
