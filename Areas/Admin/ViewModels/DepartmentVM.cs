using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KS_Payroll.Areas.Admin.ViewModels
{
    public class DepartmentVM
    {
        //department vm
        public int DepID { get; set; }
        public string DepartmentName { get; set; }

        //company vm
        public int ComID { get; set; }
        public string CompanyName { get; set; }

        //section VM
        public int SecID { get; set; }
        public string SectionName { get; set; }

        //Floor VM
        public int FloorID { get; set; }
        public string FloorName { get; set; }

        //Line VM
        public int LineID { get; set; }
        public string LineTitle { get; set; }
    }
}
