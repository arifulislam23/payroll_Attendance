using KS_Payroll.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KS_Payroll.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        private readonly KS_PayrollContext _context;
        public DashboardController(KS_PayrollContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            ViewBag.RegularEmp = _context.Employeeinfo.Where(x => x.EmpStats == "REGULAR").Count();
            ViewBag.RegineEmp = _context.Employeeinfo.Where(x => x.EmpStats == "Regine").Count();
            ViewBag.LeftyEmp = _context.Employeeinfo.Where(x => x.EmpStats == "Lefty").Count();
            return View();
        }
    }
}
