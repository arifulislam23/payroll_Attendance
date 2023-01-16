using Microsoft.EntityFrameworkCore.Migrations;

namespace KS_Payroll.Migrations
{
    public partial class asdffdsgh : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Month",
                table: "AdvanceLoan",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Month",
                table: "AdvanceLoan");
        }
    }
}
