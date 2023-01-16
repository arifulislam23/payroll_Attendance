using Microsoft.EntityFrameworkCore.Migrations;

namespace KS_Payroll.Migrations
{
    public partial class loadhjdasf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "comid",
                table: "Uunemployees",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Short_Description",
                table: "AdvanceLoan",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "AdvanceLoan",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "comid",
                table: "AdvanceLoan",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "comid",
                table: "Uunemployees");

            migrationBuilder.DropColumn(
                name: "Short_Description",
                table: "AdvanceLoan");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "AdvanceLoan");

            migrationBuilder.DropColumn(
                name: "comid",
                table: "AdvanceLoan");
        }
    }
}
