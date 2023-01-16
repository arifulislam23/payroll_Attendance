using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KS_Payroll.Areas.Admin.Data
{
    public class Comapny
    {
        [Key]
        public int Cid { get; set; }
        public string CompanyName { get; set; }
        public string CompanyContact { get; set; }
        public string CompanyAddress { get; set; }
        public string CompanyDetails { get; set; }

        public int UserId { get; set; }
        public int ModifiedId { get; set; }
        public int? Created_By { get; set; }
        public int? Updated_By { get; set; }
        public DateTime? Created_At { get; set; }
        public DateTime? Updated_At { get; set; }
    }
}
