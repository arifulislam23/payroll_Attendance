using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KS_Payroll.Areas.Admin.ViewModels
{
    public class LeaveMngVM
    {
        public int Leave_ID { get; set; }
        public string ComID { get; set; }
        public string EmpCardID { get; set; }
        public string Empdig { get; set; }
        public string EmpName { get; set; }
        public DateTime Empjoiningdate { get; set; }
        public string Leave_desc { get; set; }

        public DateTime Leave_Start { get; set; }
        public DateTime Leave_End { get; set; }
        public string Leave_Days { get; set; }
        public string Leave_Used { get; set; }
        public string Leave_Bonus { get; set; }
        public string Leave_Balance { get; set; }
        public string Leave_Type { get; set; }
    }
}
