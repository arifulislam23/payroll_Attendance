using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KS_Payroll.Migrations
{
    public partial class loadhj : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdvanceLoan",
                columns: table => new
                {
                    ALid = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpIDCard = table.Column<string>(nullable: true),
                    empid = table.Column<int>(nullable: false),
                    Amount = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    ModifiedId = table.Column<int>(nullable: false),
                    Created_By = table.Column<int>(nullable: true),
                    Updated_By = table.Column<int>(nullable: true),
                    Created_At = table.Column<DateTime>(nullable: true),
                    Updated_At = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdvanceLoan", x => x.ALid);
                });

            migrationBuilder.CreateTable(
                name: "Loan",
                columns: table => new
                {
                    ALid = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpIDCard = table.Column<string>(nullable: true),
                    empid = table.Column<int>(nullable: false),
                    Amount = table.Column<int>(nullable: false),
                    TimePerioad = table.Column<float>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    ModifiedId = table.Column<int>(nullable: false),
                    Created_By = table.Column<int>(nullable: true),
                    Updated_By = table.Column<int>(nullable: true),
                    Created_At = table.Column<DateTime>(nullable: true),
                    Updated_At = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loan", x => x.ALid);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdvanceLoan");

            migrationBuilder.DropTable(
                name: "Loan");
        }
    }
}
