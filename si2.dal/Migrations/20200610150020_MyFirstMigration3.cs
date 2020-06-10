using Microsoft.EntityFrameworkCore.Migrations;

namespace si2.dal.Migrations
{
    public partial class MyFirstMigration3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddUniqueConstraint(
                name: "AK_ProgramLevel_nameAr",
                table: "ProgramLevel",
                column: "nameAr");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_ProgramLevel_nameEn",
                table: "ProgramLevel",
                column: "nameEn");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_ProgramLevel_nameFr",
                table: "ProgramLevel",
                column: "nameFr");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_ProgramLevel_nameAr",
                table: "ProgramLevel");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_ProgramLevel_nameEn",
                table: "ProgramLevel");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_ProgramLevel_nameFr",
                table: "ProgramLevel");
        }
    }
}
