using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KS_Payroll.Areas.Admin.Data
{
    public class TempJobCard
    {
        [Key]
        public int id { get; set; }
        public DateTime Date { get; set; }
        public string InTime { get; set; }
        public string OutTime { get; set; }
        public TimeSpan LateTime { get; set; }
        public TimeSpan EarlyOut { get; set; }
        public int OverTime { get; set; }
        public string status { get; set; }

    }
}
