using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KS_Payroll.Areas.Admin.Data

{
    public class PunishmentFine
    {
        [Key]
        public int PID { get; set; }
        public String EmpIDCard { get; set; }
        public int empid { get; set; }
        public int comid { get; set; }
        public string short_desc { get; set; }
        public DateTime Date { get; set; }
        public int Amount { get; set; }
        public int Month { get; set; }

        //for future work 
        public int UserId { get; set; }
        public int ModifiedId { get; set; }
        public int? Created_By { get; set; }
        public int? Updated_By { get; set; }
        public DateTime? Created_At { get; set; }
        public DateTime? Updated_At { get; set; }
    }
}
