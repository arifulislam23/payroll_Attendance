using Microsoft.EntityFrameworkCore.Migrations;

namespace KS_Payroll.Migrations
{
    public partial class loadhjdasfhj : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Short_Description",
                table: "Loan",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Loan",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "comid",
                table: "Loan",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Short_Description",
                table: "Loan");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Loan");

            migrationBuilder.DropColumn(
                name: "comid",
                table: "Loan");
        }
    }
}
