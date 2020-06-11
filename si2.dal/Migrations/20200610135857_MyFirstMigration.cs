using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace si2.dal.Migrations
{
    public partial class MyFirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProgramLevel",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    credits = table.Column<float>(nullable: false),
                    nameFr = table.Column<string>(maxLength: 30, nullable: false),
                    nameAr = table.Column<string>(maxLength: 30, nullable: false),
                    nameEn = table.Column<string>(maxLength: 30, nullable: false),
                    universityId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramLevel", x => x.Id);
                    table.UniqueConstraint("AK_ProgramLevel_nameAr", x => x.nameAr);
                    table.UniqueConstraint("AK_ProgramLevel_nameEn", x => x.nameEn);
                    table.UniqueConstraint("AK_ProgramLevel_nameFr", x => x.nameFr);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProgramLevel");
        }
    }
}
