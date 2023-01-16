using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using KS_Payroll.Areas.Admin.Data;
using KS_Payroll.Areas.Admin.ViewModels;

namespace KS_Payroll.Data
{
    public class KS_PayrollContext : DbContext
    {
        public KS_PayrollContext (DbContextOptions<KS_PayrollContext> options)
            : base(options)
        {
        }

        public DbSet<KS_Payroll.Areas.Admin.Data.Comapny> Comapny { get; set; }
        public DbSet<KS_Payroll.Areas.Admin.Data.Department> Department { get; set; }
        public DbSet<KS_Payroll.Areas.Admin.Data.Section> Section { get; set; }
        public DbSet<KS_Payroll.Areas.Admin.Data.Floor> Floor { get; set; }
        public DbSet<KS_Payroll.Areas.Admin.Data.Leave> Leave { get; set; }
        public DbSet<KS_Payroll.Areas.Admin.Data.Line> Line { get; set; }
        public DbSet<KS_Payroll.Areas.Admin.Data.Shift> Shift { get; set; }
        public DbSet<KS_Payroll.Areas.Admin.Data.Grade> Grade { get; set; }
        public DbSet<KS_Payroll.Areas.Admin.Data.DesignationType> DesignationType { get; set; }
        public DbSet<KS_Payroll.Areas.Admin.Data.Designation> Designation { get; set; }
        public DbSet<KS_Payroll.Areas.Admin.Data.Employeeinfo> Employeeinfo { get; set; }
        public DbSet<KS_Payroll.Areas.Admin.Data.Calendar> Calendar { get; set; }
        public DbSet<KS_Payroll.Areas.Admin.Data.Leaveinfo> Leaveinfo { get; set; }
        public DbSet<KS_Payroll.Areas.Admin.Data.AttendanceModule> AttendanceModule { get; set; }
        public DbSet<KS_Payroll.Areas.Admin.Data.RowAttendance> RowAttendance { get; set; }
        public DbSet<KS_Payroll.Areas.Admin.Data.FinalAttendance> FinalAttendance { get; set; }
        public DbSet<KS_Payroll.Areas.Admin.Data.TempJobCard> TempJobCard { get; set; }
        public DbSet<KS_Payroll.Areas.Admin.Data.Uunemployees> Uunemployees { get; set; }
        public DbSet<KS_Payroll.Areas.Admin.Data.Loans> Loan { get; set; }
        public DbSet<KS_Payroll.Areas.Admin.Data.AdvanceLoan> AdvanceLoan { get; set; }
        public DbSet<KS_Payroll.Areas.Admin.Data.PunishmentFine> PunishmentFine { get; set; }


        


    }//ks_payroll context
}
