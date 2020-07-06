using Microsoft.EntityFrameworkCore.Migrations;

namespace si2.dal.Migrations
{
    public partial class Cohort_refactor_migration1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Cohort_Promotion",
                table: "Cohort");

            migrationBuilder.CreateIndex(
                name: "IX_Cohort_Promotion_ProgramId",
                table: "Cohort",
                columns: new[] { "Promotion", "ProgramId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Cohort_Promotion_ProgramId",
                table: "Cohort");

            migrationBuilder.CreateIndex(
                name: "IX_Cohort_Promotion",
                table: "Cohort",
                column: "Promotion",
                unique: true);
        }
    }
}
