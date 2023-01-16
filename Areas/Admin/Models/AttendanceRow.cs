using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KS_Payroll.Areas.Admin.Data
{
    public class AttendanceRow
    {
       
        public int serial { get; set; }
        public string FileName { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int Moduletye { get; set; }
        public string First_In_Time { get; set; }
        public string Last_Out_Time { get; set; }

    }
}
