using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KS_Payroll.Areas.Admin.Data
{
    public class AttendanceModule
    {
        [Key]
        public int AMid { get; set; }
        public string AModuleName { get; set; }
        public string TimeName { get; set; }
        public string Time2Name { get; set; }
        public string DateName { get; set; }
        public string SecretCode { get; set; }
        public string FileType { get; set; }
        public string TableName { get; set; }
    }
}
