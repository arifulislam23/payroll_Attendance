using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KS_Payroll.Areas.Admin.ViewModels
{
    public class ManualAttendanceVM
    {
       
        public int empid { get; set; }
        public string EmpIDCard { get; set; }
        public string InTime { get; set; }
        public string OutTime { get; set; }
        public string AttenStatus { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
