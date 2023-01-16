using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KS_Payroll.Areas.Admin.Data
{
    public class Designation
    {
        //Bonus
        [Key]
        public int DesigID { get; set; }
        public string DesigName { get; set; }
        public int Grade { get; set; }
        public double AttenBonus { get; set; }
        public bool OverTime { get; set; }
        public double HolidayAllowance { get; set; }
        public double NightBill { get; set; }
        public double LunchBill { get; set; }
        public double TiffinBill { get; set; }
        public double IftarBill { get; set; }
        public int UserId { get; set; }
        public int ModifiedId { get; set; }
        public int? Created_By { get; set; }
        public int? Updated_By { get; set; }
        public DateTime? Created_At { get; set; }
        public DateTime? Updated_At { get; set; }
    }
}
