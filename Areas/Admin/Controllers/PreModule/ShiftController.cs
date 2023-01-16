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
    public class ShiftController : Controller
    {
        private readonly KS_PayrollContext _context;

        public ShiftController(KS_PayrollContext context)
        {
            _context = context;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create_Shift(Shift data)
        {
            if (data.ShiftTitle == null || data.ShiftTitle == " ")
            {
                return RedirectToAction("Shift", "PreModule", new { area = "Admin" });
            }

            Shift Shifts = new Shift
            {
                ShiftTitle = data.ShiftTitle, 
                StartTime = data.StartTime, 
                EndTime = data.EndTime, 
                BreakStartTime = data.BreakStartTime, 
                BreakEndTime = data.BreakEndTime, 
                Created_At = DateTime.Now
            };
            if (!_context.Shift.Any(t => t.ShiftTitle.Equals(data.ShiftTitle)))
            {
                _context.Shift.Add(Shifts);
                _context.SaveChanges();
            }


            var Companys = _context.Department.OrderByDescending(x => x.DepartmentID).ToList();
            ViewData["Departments"] = Companys;
            return RedirectToAction("Shift", "PreModule", new { area = "Admin" });
        }

        //Shift_Edit

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Shift_Edit(Shift model)
        {
            int id = Int16.Parse(model.ShiftId.ToString());
            Shift data = _context.Shift.Find(id);
            data.ShiftTitle = model.ShiftTitle;
            data.StartTime = model.StartTime;
            data.EndTime = model.EndTime;
            model.BreakStartTime = data.BreakStartTime;
            data.BreakEndTime = model.BreakEndTime;
            data.Updated_At = DateTime.Now;

            _context.Update(data);
            _context.SaveChanges();
            return RedirectToAction("Shift", "PreModule", new { area = "Admin" });

        }
    }
}
