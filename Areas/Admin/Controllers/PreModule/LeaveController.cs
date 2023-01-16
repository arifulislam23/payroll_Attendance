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
    public class LeaveController : Controller
    {
        private readonly KS_PayrollContext _context;
        public LeaveController(KS_PayrollContext context)
        {
            _context = context;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create_Leave(Leave data)
        {
            if (data.LeaveTitle == null || data.LeaveTitle == " " || data.NoOfDays==0)
            {
                ViewBag.data = _context.Leave.OrderByDescending(x => x.LeaveId).ToList();

                return RedirectToAction("Leave", "PreModule", new { area = "Admin" });
            }

            Leave leave = new Leave
            {

                LeaveTitle = data.LeaveTitle,
                NoOfDays = data.NoOfDays,
                Created_At = DateTime.Now
            };
            if (!_context.Leave.Any(t => t.LeaveTitle.Equals(data.LeaveTitle)))
            {
                _context.Leave.Add(leave);
                _context.SaveChanges();
            }


            var leaves = _context.Leave.OrderByDescending(x => x.LeaveId).ToList();
            ViewData["leaves"] = leaves;
            return RedirectToAction("Leave", "PreModule", new { area = "Admin" });
        }

        //Leave_Edit

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Leave_Edit(Leave model)
        {
            int id = Int16.Parse(model.LeaveId.ToString());
            Leave Data = _context.Leave.Find(id);
            Data.LeaveTitle = model.LeaveTitle;
            Data.NoOfDays = model.NoOfDays;
            Data.Updated_At = DateTime.Now;
            _context.Update(Data);
            _context.SaveChanges();
            return RedirectToAction("Leave", "PreModule", new { area = "Admin" });

        }
    }
}
