using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KS_Payroll.Areas.Admin.Data
{
    public class Loans
    {
        [Key]
        public int ALid { get; set; }
        public string EmpIDCard { get; set; }
        public int empid { get; set; }
        public int comid { get; set; }
        public int Amount { get; set; }
        public float TimePerioad{ get; set; }
        public float PerMonth{ get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public string Short_Description { get; set; }

        //future work perpose
        public int UserId { get; set; }
        public int ModifiedId { get; set; }
        public int? Created_By { get; set; }
        public int? Updated_By { get; set; }
        public DateTime? Created_At { get; set; }
        public DateTime? Updated_At { get; set; }
    }
}
