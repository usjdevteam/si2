using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace si2.dal.Migrations
{
    public partial class Added_Audit_Concurrency : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Dataflow",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Dataflow",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleltedOn",
                table: "Dataflow",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "Dataflow",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "Dataflow",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedOn",
                table: "Dataflow",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Dataflow",
                rowVersion: true,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Dataflow");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Dataflow");

            migrationBuilder.DropColumn(
                name: "DeleltedOn",
                table: "Dataflow");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Dataflow");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "Dataflow");

            migrationBuilder.DropColumn(
                name: "LastModifiedOn",
                table: "Dataflow");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Dataflow");
        }
    }
}
