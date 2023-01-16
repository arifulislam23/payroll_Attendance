using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KS_Payroll.Areas.Admin.Data
{
    public class Section
    {
        [Key]
        public int SectionId { get; set; }
        public string SectionName { get; set; }
        public int DepartmentID { get; set; }
        public int? Created_By { get; set; }
        public int? Updated_By { get; set; }
        public DateTime? Created_At { get; set; }
        public DateTime? Updated_At { get; set; }
        public int UserId { get; set; }
        public int ModifiedId { get; set; }
    }
}
