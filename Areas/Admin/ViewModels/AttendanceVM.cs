using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KS_Payroll.Areas.Admin.ViewModels
{
    public class AttendanceVM
    {
        public DateTime Date { get; set; }
        public string EmpInTime { get; set; }
        public string EmpOutTime { get; set; }
        public TimeSpan LateTime { get; set; }
       
        public TimeSpan EarlyoutTime	{ get; set; }
        public double OverTime	{ get; set; }
       

        //for short datetime date
        public string Ddate { get; set; }
        public string Earlyout { get; set; }
        public string Late { get; set; }

        //for all attendanceList
        public DateTime searchFromDate { get; set; }
        public DateTime searchToDate { get; set; }
        public string empIDCard { get; set; }
        public string empName { get; set; }
        public DateTime InTime { get; set; }


    }
}
