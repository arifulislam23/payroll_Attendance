using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KS_Payroll.Areas.Admin.Data
{
    public class Calendar
    {
        [Key]
        public int CalendarID { get; set; }
        public string Eventtype { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime FDate { get; set; }
        public string OccassionName { get; set; }
        public string Days { get; set; }
        public int Eventstats { get; set; }

        //future work perpose
        public int UserId { get; set; }
        public int ModifiedId { get; set; }
        public int? Created_By { get; set; }
        public int? Updated_By { get; set; }
        public DateTime? Created_At { get; set; }
        public DateTime? Updated_At { get; set; }
    }
}
