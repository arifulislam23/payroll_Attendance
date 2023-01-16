using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KS_Payroll.Migrations
{
    public partial class safas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Uunemployees",
                columns: table => new
                {
                    sl = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    empid = table.Column<int>(nullable: false),
                    EmpIDCard = table.Column<string>(nullable: true),
                    Designation = table.Column<string>(nullable: true),
                    JoningDate = table.Column<string>(nullable: true),
                    LeftType = table.Column<string>(nullable: true),
                    LastAttendance = table.Column<string>(nullable: true),
                    Short_Description = table.Column<string>(nullable: true),
                    AsingDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uunemployees", x => x.sl);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Uunemployees");
        }
    }
}
