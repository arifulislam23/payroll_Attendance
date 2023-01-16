using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KS_Payroll.Areas.Admin.ViewModels
{
    public class AbsentList
    {

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate{ get; set; }
        public string empIdCard { get; set; }
        public string empName { get; set; }
        public DateTime? Date { get; set; }
        public string EmpInTime { get; set; }
        public string EmpOutTime { get; set; }
        public string type { get; set; }
    }
}
