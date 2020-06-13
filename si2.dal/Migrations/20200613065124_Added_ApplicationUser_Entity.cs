﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace si2.dal.Migrations
{
    public partial class Added_ApplicationUser_Entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstNameAr",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstNameFr",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastNameAr",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastNameFr",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstNameAr",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FirstNameFr",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastNameAr",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastNameFr",
                table: "AspNetUsers");
        }
    }
}
