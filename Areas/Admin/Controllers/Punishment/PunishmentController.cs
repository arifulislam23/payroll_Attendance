using KS_Payroll.Areas.Admin.Data;
using KS_Payroll.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KS_Payroll.Areas.Admin.Controllers.Punishment
{
    [Area("Admin")]
    public class PunishmentController : Controller
    {
        private readonly KS_PayrollContext _context;
        public PunishmentController(KS_PayrollContext context)
        {
            _context = context;
        }

        public IActionResult PunishmentCreate()
        {
            ViewBag.coimpany = _context.Comapny.ToList();
            return View();
        }
        

        [HttpPost]
        public IActionResult PunishmentCreate(PunishmentFine model)
        {
            if(model !=null)
            {

                var emp = _context.Employeeinfo.FirstOrDefault(x=>x.EmpIDCard == model.EmpIDCard && x.EmpCompany == model.comid.ToString());
                if(emp != null)
                {
                    if (emp!=null)
                    {
                        model.comid = int.Parse(emp.EmpCompany);
                        model.Created_At = DateTime.Now;
                        _context.Add(model);
                         _context.SaveChanges();
                        return RedirectToAction(nameof(PunishmentCreate));
                    }
                   
                }
                return View(model);
            }

            ViewBag.coimpany = _context.Comapny.ToList();
            return View();
        }
        public IActionResult History()
        {
            var history = _context.PunishmentFine.OrderByDescending(x=>x.PID).ToList();
            return View(history);
        }
    }
}
