using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KS_Payroll.Migrations
{
    public partial class o : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AttendanceModule",
                columns: table => new
                {
                    AMid = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AModuleName = table.Column<string>(nullable: true),
                    TimeName = table.Column<string>(nullable: true),
                    Time2Name = table.Column<string>(nullable: true),
                    DateName = table.Column<string>(nullable: true),
                    SecretCode = table.Column<string>(nullable: true),
                    FileType = table.Column<string>(nullable: true),
                    TableName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttendanceModule", x => x.AMid);
                });

            migrationBuilder.CreateTable(
                name: "Calendar",
                columns: table => new
                {
                    CalendarID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Eventtype = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    FDate = table.Column<DateTime>(nullable: false),
                    OccassionName = table.Column<string>(nullable: true),
                    Days = table.Column<string>(nullable: true),
                    Eventstats = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    ModifiedId = table.Column<int>(nullable: false),
                    Created_By = table.Column<int>(nullable: true),
                    Updated_By = table.Column<int>(nullable: true),
                    Created_At = table.Column<DateTime>(nullable: true),
                    Updated_At = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calendar", x => x.CalendarID);
                });

            migrationBuilder.CreateTable(
                name: "Comapny",
                columns: table => new
                {
                    Cid = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(nullable: true),
                    CompanyContact = table.Column<string>(nullable: true),
                    CompanyAddress = table.Column<string>(nullable: true),
                    CompanyDetails = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false),
                    ModifiedId = table.Column<int>(nullable: false),
                    Created_By = table.Column<int>(nullable: true),
                    Updated_By = table.Column<int>(nullable: true),
                    Created_At = table.Column<DateTime>(nullable: true),
                    Updated_At = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comapny", x => x.Cid);
                });

            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    DepartmentID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentName = table.Column<string>(nullable: true),
                    CompanyId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    ModifiedId = table.Column<int>(nullable: false),
                    Created_By = table.Column<int>(nullable: true),
                    Updated_By = table.Column<int>(nullable: true),
                    Created_At = table.Column<DateTime>(nullable: true),
                    Updated_At = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.DepartmentID);
                });

            migrationBuilder.CreateTable(
                name: "Designation",
                columns: table => new
                {
                    DesigID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DesigName = table.Column<string>(nullable: true),
                    Grade = table.Column<int>(nullable: false),
                    AttenBonus = table.Column<double>(nullable: false),
                    OverTime = table.Column<bool>(nullable: false),
                    HolidayAllowance = table.Column<double>(nullable: false),
                    NightBill = table.Column<double>(nullable: false),
                    LunchBill = table.Column<double>(nullable: false),
                    TiffinBill = table.Column<double>(nullable: false),
                    IftarBill = table.Column<double>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    ModifiedId = table.Column<int>(nullable: false),
                    Created_By = table.Column<int>(nullable: true),
                    Updated_By = table.Column<int>(nullable: true),
                    Created_At = table.Column<DateTime>(nullable: true),
                    Updated_At = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Designation", x => x.DesigID);
                });

            migrationBuilder.CreateTable(
                name: "DesignationType",
                columns: table => new
                {
                    DesignationTypeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DesignationTypeName = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false),
                    ModifiedId = table.Column<int>(nullable: false),
                    Created_By = table.Column<int>(nullable: true),
                    Updated_By = table.Column<int>(nullable: true),
                    Created_At = table.Column<DateTime>(nullable: true),
                    Updated_At = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DesignationType", x => x.DesignationTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Employeeinfo",
                columns: table => new
                {
                    empid = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpStats = table.Column<string>(nullable: true),
                    EmpName = table.Column<string>(nullable: true),
                    EmpFName = table.Column<string>(nullable: true),
                    EmpMName = table.Column<string>(nullable: true),
                    EmpSpouseName = table.Column<string>(nullable: true),
                    EmpPhone = table.Column<string>(nullable: true),
                    EmpEmgPerson = table.Column<string>(nullable: true),
                    EmpEmgphone = table.Column<string>(nullable: true),
                    EmpDOB = table.Column<DateTime>(nullable: false),
                    EmpNID = table.Column<string>(nullable: true),
                    EmpBODCertificate = table.Column<string>(nullable: true),
                    EmpPassport = table.Column<string>(nullable: true),
                    EmpGender = table.Column<string>(nullable: true),
                    EmpMarriedStatus = table.Column<string>(nullable: true),
                    EmpChildNumber = table.Column<string>(nullable: true),
                    EmpNationality = table.Column<string>(nullable: true),
                    EmpReligion = table.Column<string>(nullable: true),
                    EmpBlood = table.Column<string>(nullable: true),
                    EmpNomineeName = table.Column<string>(nullable: true),
                    EmpNomineePhn = table.Column<string>(nullable: true),
                    EmpNomineeRlt = table.Column<string>(nullable: true),
                    EmpRefName = table.Column<string>(nullable: true),
                    EmpRefNumber = table.Column<int>(nullable: false),
                    EmpHeight = table.Column<int>(nullable: false),
                    EmpWeight = table.Column<int>(nullable: false),
                    EmpParmanentDistrict = table.Column<string>(nullable: true),
                    EmpParmanentZip = table.Column<string>(nullable: true),
                    EmpParmanentArea = table.Column<string>(nullable: true),
                    EmpParmanentAddress = table.Column<string>(nullable: true),
                    EmpPresentDistrict = table.Column<string>(nullable: true),
                    EmpPresentZip = table.Column<string>(nullable: true),
                    EmpPresentArea = table.Column<string>(nullable: true),
                    EmpPresentAddress = table.Column<string>(nullable: true),
                    EmpEduTitle = table.Column<string>(nullable: true),
                    EmpEduPassY = table.Column<int>(nullable: false),
                    EmpEduStatus = table.Column<string>(nullable: true),
                    EmpEduPassG = table.Column<string>(nullable: true),
                    EmpCompany = table.Column<string>(nullable: true),
                    EmpIDCard = table.Column<string>(nullable: true),
                    EmpSecrateCode = table.Column<string>(nullable: true),
                    EmpType = table.Column<string>(nullable: true),
                    EmpDepartment = table.Column<string>(nullable: true),
                    EmpSection = table.Column<string>(nullable: true),
                    EmpFloor = table.Column<string>(nullable: true),
                    EmpLine = table.Column<string>(nullable: true),
                    EmpJoiningDate = table.Column<DateTime>(nullable: false),
                    EmpShift = table.Column<string>(nullable: true),
                    EmpDesignationTitle = table.Column<string>(nullable: true),
                    EmpDesignationType = table.Column<string>(nullable: true),
                    EmpGrade = table.Column<string>(nullable: true),
                    EmpAttenBonus = table.Column<int>(nullable: false),
                    EmpOverTime = table.Column<bool>(nullable: false),
                    EmpHolidayAlw = table.Column<int>(nullable: false),
                    EmpNightBill = table.Column<int>(nullable: false),
                    EmpLunchBilll = table.Column<int>(nullable: false),
                    EmpTiffinBill = table.Column<int>(nullable: false),
                    EmpIftarBill = table.Column<int>(nullable: false),
                    EmpGross = table.Column<int>(nullable: false),
                    EmpBasic = table.Column<int>(nullable: false),
                    EmpHouse = table.Column<int>(nullable: false),
                    EmpMedical = table.Column<int>(nullable: false),
                    EmpConveyance = table.Column<int>(nullable: false),
                    EmpFood = table.Column<int>(nullable: false),
                    EmpPayMethodName = table.Column<string>(nullable: true),
                    EmpBankName = table.Column<string>(nullable: true),
                    EmpMFSName = table.Column<string>(nullable: true),
                    EmpBankBranch = table.Column<string>(nullable: true),
                    EmpBankAcc = table.Column<string>(nullable: true),
                    MFSnumber = table.Column<string>(nullable: true),
                    EmpPrfImg = table.Column<byte[]>(nullable: true),
                    EmpNIDImg = table.Column<byte[]>(nullable: true),
                    EmpBodImg = table.Column<byte[]>(nullable: true),
                    EmpPassportImg = table.Column<byte[]>(nullable: true),
                    EmpContractImg = table.Column<byte[]>(nullable: true),
                    EmpSigImg = table.Column<byte[]>(nullable: true),
                    UserId = table.Column<int>(nullable: false),
                    ModifiedId = table.Column<int>(nullable: false),
                    Created_By = table.Column<int>(nullable: true),
                    Updated_By = table.Column<int>(nullable: true),
                    Created_At = table.Column<DateTime>(nullable: true),
                    Updated_At = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employeeinfo", x => x.empid);
                });

            migrationBuilder.CreateTable(
                name: "FinalAttendance",
                columns: table => new
                {
                    sl = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    empid = table.Column<int>(nullable: false),
                    EmpSCode = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    InTime = table.Column<string>(nullable: true),
                    OutTime = table.Column<string>(nullable: true),
                    Late = table.Column<TimeSpan>(nullable: false),
                    EarlyOut = table.Column<TimeSpan>(nullable: false),
                    OverTime = table.Column<int>(nullable: false),
                    AttenStatus = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinalAttendance", x => x.sl);
                });

            migrationBuilder.CreateTable(
                name: "Floor",
                columns: table => new
                {
                    FloorId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FloorName = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false),
                    ModifiedId = table.Column<int>(nullable: false),
                    Created_By = table.Column<int>(nullable: true),
                    Updated_By = table.Column<int>(nullable: true),
                    Created_At = table.Column<DateTime>(nullable: true),
                    Updated_At = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Floor", x => x.FloorId);
                });

            migrationBuilder.CreateTable(
                name: "Grade",
                columns: table => new
                {
                    GradeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GradeTitle = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false),
                    ModifiedId = table.Column<int>(nullable: false),
                    Created_By = table.Column<int>(nullable: true),
                    Updated_By = table.Column<int>(nullable: true),
                    Created_At = table.Column<DateTime>(nullable: true),
                    Updated_At = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grade", x => x.GradeId);
                });

            migrationBuilder.CreateTable(
                name: "Leave",
                columns: table => new
                {
                    LeaveId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LeaveTitle = table.Column<string>(nullable: true),
                    NoOfDays = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    ModifiedId = table.Column<int>(nullable: false),
                    Created_By = table.Column<int>(nullable: true),
                    Updated_By = table.Column<int>(nullable: true),
                    Created_At = table.Column<DateTime>(nullable: true),
                    Updated_At = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leave", x => x.LeaveId);
                });

            migrationBuilder.CreateTable(
                name: "Leaveinfo",
                columns: table => new
                {
                    LID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ComID = table.Column<string>(nullable: true),
                    EmpCardID = table.Column<string>(nullable: true),
                    Leave_desc = table.Column<string>(nullable: true),
                    Leave_Start = table.Column<DateTime>(nullable: false),
                    Leave_End = table.Column<DateTime>(nullable: false),
                    Leave_Days = table.Column<string>(nullable: true),
                    Leave_Bonus = table.Column<string>(nullable: true),
                    Leave_Type = table.Column<string>(nullable: true),
                    Leave_Stats = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    ModifiedId = table.Column<int>(nullable: false),
                    Created_By = table.Column<int>(nullable: true),
                    Updated_By = table.Column<int>(nullable: true),
                    Created_At = table.Column<DateTime>(nullable: true),
                    Updated_At = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leaveinfo", x => x.LID);
                });

            migrationBuilder.CreateTable(
                name: "Line",
                columns: table => new
                {
                    LineId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LineTitle = table.Column<string>(nullable: true),
                    SectionId = table.Column<int>(nullable: false),
                    FloorId = table.Column<int>(nullable: false),
                    Created_By = table.Column<int>(nullable: true),
                    Updated_By = table.Column<int>(nullable: true),
                    Created_At = table.Column<DateTime>(nullable: true),
                    Updated_At = table.Column<DateTime>(nullable: true),
                    UserId = table.Column<int>(nullable: false),
                    ModifiedId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Line", x => x.LineId);
                });

            migrationBuilder.CreateTable(
                name: "RowAttendance",
                columns: table => new
                {
                    SL = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    empSCode = table.Column<double>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Frist_In_Time = table.Column<string>(nullable: true),
                    Last_Out_Time = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RowAttendance", x => x.SL);
                });

            migrationBuilder.CreateTable(
                name: "Section",
                columns: table => new
                {
                    SectionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SectionName = table.Column<string>(nullable: true),
                    DepartmentID = table.Column<int>(nullable: false),
                    Created_By = table.Column<int>(nullable: true),
                    Updated_By = table.Column<int>(nullable: true),
                    Created_At = table.Column<DateTime>(nullable: true),
                    Updated_At = table.Column<DateTime>(nullable: true),
                    UserId = table.Column<int>(nullable: false),
                    ModifiedId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Section", x => x.SectionId);
                });

            migrationBuilder.CreateTable(
                name: "Shift",
                columns: table => new
                {
                    ShiftId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShiftTitle = table.Column<string>(nullable: true),
                    StartTime = table.Column<string>(nullable: true),
                    EndTime = table.Column<string>(nullable: true),
                    BreakStartTime = table.Column<string>(nullable: true),
                    BreakEndTime = table.Column<string>(nullable: true),
                    Created_By = table.Column<int>(nullable: true),
                    Updated_By = table.Column<int>(nullable: true),
                    Created_At = table.Column<DateTime>(nullable: true),
                    Updated_At = table.Column<DateTime>(nullable: true),
                    UserId = table.Column<int>(nullable: false),
                    ModifiedId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shift", x => x.ShiftId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AttendanceModule");

            migrationBuilder.DropTable(
                name: "Calendar");

            migrationBuilder.DropTable(
                name: "Comapny");

            migrationBuilder.DropTable(
                name: "Department");

            migrationBuilder.DropTable(
                name: "Designation");

            migrationBuilder.DropTable(
                name: "DesignationType");

            migrationBuilder.DropTable(
                name: "Employeeinfo");

            migrationBuilder.DropTable(
                name: "FinalAttendance");

            migrationBuilder.DropTable(
                name: "Floor");

            migrationBuilder.DropTable(
                name: "Grade");

            migrationBuilder.DropTable(
                name: "Leave");

            migrationBuilder.DropTable(
                name: "Leaveinfo");

            migrationBuilder.DropTable(
                name: "Line");

            migrationBuilder.DropTable(
                name: "RowAttendance");

            migrationBuilder.DropTable(
                name: "Section");

            migrationBuilder.DropTable(
                name: "Shift");
        }
    }
}
