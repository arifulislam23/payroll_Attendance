using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KS_Payroll.Migrations
{
    public partial class sadf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Punishment",
                columns: table => new
                {
                    PID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpIDCard = table.Column<string>(nullable: true),
                    empid = table.Column<int>(nullable: false),
                    comid = table.Column<int>(nullable: false),
                    short_desc = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    Amount = table.Column<int>(nullable: false),
                    Month = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    ModifiedId = table.Column<int>(nullable: false),
                    Created_By = table.Column<int>(nullable: true),
                    Updated_By = table.Column<int>(nullable: true),
                    Created_At = table.Column<DateTime>(nullable: true),
                    Updated_At = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Punishment", x => x.PID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Punishment");
        }
    }
}
