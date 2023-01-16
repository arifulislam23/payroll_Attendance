using KS_Payroll.Areas.Admin.Data;
using KS_Payroll.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KS_Payroll.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DesignationController : Controller
    {
        private readonly KS_PayrollContext _context;

        public DesignationController(KS_PayrollContext context)
        {
            _context = context;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create_Designation(Designation data)
        {
            if (data.DesigName == null || data.DesigName == " ")
            {
                return RedirectToAction("Designation", "PreModule", new { area = "Admin" });
            }

            Designation designation = new Designation
            {
                DesigName = data.DesigName,
                Grade = data.Grade,
                AttenBonus = data.AttenBonus,
                OverTime = data.OverTime,
                HolidayAllowance = data.HolidayAllowance,
                NightBill = data.NightBill,
                TiffinBill = data.TiffinBill,
                LunchBill = data.LunchBill,
                IftarBill = data.IftarBill,
                Created_At = DateTime.Now
            };
            if (!_context.Designation.Any(t => t.DesigID.Equals(data.DesigID)))
            {
                _context.Designation.Add(designation);
                _context.SaveChanges();
            }


            var Designation = _context.Designation.OrderByDescending(x => x.DesigID).ToList();
            ViewData["Designation"] = Designation;
            return RedirectToAction("Designation", "PreModule", new { area = "Admin" });

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit_Designation(Designation model)
        {

            int id = Int16.Parse(model.DesigID.ToString());
            Designation D = _context.Designation.Find(id);
            D.DesigName = model.DesigName;
            D.Grade = model.Grade;
            D.AttenBonus = model.AttenBonus;
            D.OverTime = model.OverTime;
            D.HolidayAllowance = model.HolidayAllowance;
            D.NightBill = model.NightBill;
            D.TiffinBill = model.TiffinBill;
            D.LunchBill = model.LunchBill;
            D.IftarBill = model.IftarBill;
            D.Updated_At = DateTime.Now;

            _context.Update(D);
            _context.SaveChanges();
            return RedirectToAction("Designation", "PreModule", new { area = "Admin" });

        }
    }
}
