using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KS_Payroll.Areas.Admin.ViewModels
{
    public class JobCardVm
    {
        public string  EmpIDCard { get; set; }
        public string EmpName { get; set; }
        public string EmpSection { get; set; }
        public DateTime EmpJoiningDate { get; set; }
        public string EmpDesignationTitle { get; set; }
        public string EmpGrade { get; set; }
        public string EmpLine { get; set; }
        public string EmpFloor { get; set; }
        public string EmpStats { get; set; }
        public int empid { get; set; }
        public DateTime Date { get; set; }
        public DateTime searchFromDate { get; set; }
        public DateTime searchToDate { get; set; }
        public TimeSpan EmpInTime { get; set; }
        public string InTime { get; set; }
        public string OutTime { get; set; }
        public TimeSpan SfitTime { get; set; }
        public TimeSpan LateTime { get; set; }
        public TimeSpan EmpOutTime { get; set; }
        public TimeSpan EarlyoutTime { get; set; }
        public string Status { get; set; }
        public string DaysType { get; set; }
        public int TotalOverTime { get; set; }
        public int totalPresent { get; set; }
        public int totalAbsent { get; set; }
        public int totalLeave { get; set; }
        public int totalEvent { get; set; }
        public int totalDays { get; set; }
        public string totalLate { get; set; }
        public string totalEarlyOut { get; set; }
    }
}
