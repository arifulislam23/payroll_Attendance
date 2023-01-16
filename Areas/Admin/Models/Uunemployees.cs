using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KS_Payroll.Areas.Admin.Data
{
    public class Uunemployees
    {
        [Key]
        public int sl { get; set; }
        public int empid{ get; set; }
        public int comid { get; set; }
        public int updateStatus { get; set; }
        public string EmpIDCard{ get; set; }
        public string Designation{ get; set; }
        public string JoningDate{ get; set; }
        public string LeftType{ get; set; }
        public string LastAttendance { get; set; }
        public string Short_Description { get; set; }
        public DateTime AsingDate{ get; set; }
        
        //for trace
        public int? Created_By { get; set; }
        public int? Updated_By { get; set; }
        public DateTime? Created_At { get; set; }
        public DateTime? Updated_At { get; set; }
        public int UserId { get; set; }
        public int ModifiedId { get; set; }
    }
}
