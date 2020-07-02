using Microsoft.EntityFrameworkCore.Migrations;

namespace si2.dal.Migrations
{
    public partial class Address_refactor_migration1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Longitude",
                table: "Address",
                type: "decimal(9,7)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(9,6)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Latitude",
                table: "Address",
                type: "decimal(9,7)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,6)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Longitude",
                table: "Address",
                type: "decimal(9,6)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(9,7)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Latitude",
                table: "Address",
                type: "decimal(8,6)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(9,7)");
        }
    }
}
