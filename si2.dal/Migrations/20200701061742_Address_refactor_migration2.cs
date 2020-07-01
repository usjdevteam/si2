using Microsoft.EntityFrameworkCore.Migrations;

namespace si2.dal.Migrations
{
    public partial class Address_refactor_migration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Longitude",
                table: "Address",
                type: "decimal(10,7)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(9,7)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Longitude",
                table: "Address",
                type: "decimal(9,7)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,7)");
        }
    }
}
