using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace si2.dal.Migrations
{
    public partial class added_vehicle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeleltedOn",
                table: "Dataflow");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Dataflow",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Dataflow");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleltedOn",
                table: "Dataflow",
                type: "datetime2",
                nullable: true);
        }
    }
}
