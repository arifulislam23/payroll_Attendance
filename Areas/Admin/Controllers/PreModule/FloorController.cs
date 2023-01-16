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
    public class FloorController : Controller
    {
        private readonly KS_PayrollContext _context;
        public FloorController(KS_PayrollContext context)
        {
            _context = context;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create_Floor(Floor data)
        {
            if (data.FloorName == null || data.FloorName == " ")
            {
                ViewBag.data = _context.Floor.OrderByDescending(x => x.FloorId).ToList();

                return RedirectToAction("Floor", "PreModule", new { area = "Admin" });
            }

            Floor floor = new Floor
            {

                FloorName = data.FloorName,
                Created_At = DateTime.Now
            };
            if (!_context.Floor.Any(t => t.FloorName.Equals(data.FloorName)))
            {
                _context.Floor.Add(floor);
                _context.SaveChanges();
            }


            var floors = _context.Floor.OrderByDescending(x => x.FloorId).ToList();
            ViewData["floors"] = floors;
            return RedirectToAction("Floor", "PreModule", new { area = "Admin" });
        }

        //Floor_Edit

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Floor_Edit(Floor model)
        {
            int id = Int16.Parse(model.FloorId.ToString());
            Floor Data = _context.Floor.Find(id);
            Data.FloorName = model.FloorName;
            Data.Updated_At = DateTime.Now;
            _context.Update(Data);
            _context.SaveChanges();
            return RedirectToAction("Floor", "PreModule", new { area = "Admin" });

        }
    }
}
