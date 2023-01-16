using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KS_Payroll.Areas.Admin.Data
{
    public class Leaveinfo
    {
        [Key]
        public int LID { get; set; }
       
        public string ComID { get; set; }
        public string EmpCardID { get; set; }
        public string Leave_desc { get; set; }
        public DateTime Leave_Start { get; set; }
        public DateTime Leave_End { get; set; }
        public string Leave_Days { get; set; }
        public string Leave_Bonus { get; set; }
        public string Leave_Type { get; set; }
        public int Leave_Stats { get; set; }

        public int UserId { get; set; }
        public int ModifiedId { get; set; }
        public int? Created_By { get; set; }
        public int? Updated_By { get; set; }
        public DateTime? Created_At { get; set; }
        public DateTime? Updated_At { get; set; }
    }
}
