using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KS_Payroll.Migrations
{
    public partial class safasfed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Created_At",
                table: "Uunemployees",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Created_By",
                table: "Uunemployees",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ModifiedId",
                table: "Uunemployees",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated_At",
                table: "Uunemployees",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Updated_By",
                table: "Uunemployees",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Uunemployees",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Created_At",
                table: "Uunemployees");

            migrationBuilder.DropColumn(
                name: "Created_By",
                table: "Uunemployees");

            migrationBuilder.DropColumn(
                name: "ModifiedId",
                table: "Uunemployees");

            migrationBuilder.DropColumn(
                name: "Updated_At",
                table: "Uunemployees");

            migrationBuilder.DropColumn(
                name: "Updated_By",
                table: "Uunemployees");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Uunemployees");
        }
    }
}
