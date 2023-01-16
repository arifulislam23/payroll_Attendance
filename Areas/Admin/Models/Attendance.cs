using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KS_Payroll.Areas.Admin.Data
{
    public class RowAttendance
    {
        [Key]
        public int SL { get; set; }
        public double empSCode { get; set; }
        public DateTime Date{ get; set; }
        public string Frist_In_Time{ get; set; }
        public string Last_Out_Time{ get; set; }
    }
}
