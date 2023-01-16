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
    public class LineController : Controller
    {
        private readonly KS_PayrollContext _context;
        public LineController(KS_PayrollContext context)
        {
            _context = context;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create_Line(Line data)
        {
            if (data.LineTitle == null || data.LineTitle == " ")
            {
                return RedirectToAction("Line", "PreModule", new { area = "Admin" });
            }

            Line lines = new Line
            {

                LineTitle = data.LineTitle,
                SectionId = data.SectionId,
                FloorId = data.FloorId,
                Created_At = DateTime.Now
            };
            if (!_context.Line.Any(t => t.LineId.Equals(data.LineId)))
            {
                _context.Line.Add(lines);
                _context.SaveChanges();
            }


            var line = _context.Line.OrderByDescending(x => x.LineId).ToList();
            ViewData["Lines"] = line;
            return RedirectToAction("Line", "PreModule", new { area = "Admin" });
        }

        //Edit_line

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit_line(Line model)
        {
            int id = Int16.Parse(model.LineId.ToString());
            Line Data = _context.Line.Find(id);
            Data.LineTitle = model.LineTitle;
            Data.SectionId = model.SectionId;
            Data.FloorId = model.FloorId;
            Data.Updated_At = DateTime.Now;
            _context.Update(Data);
            _context.SaveChanges();
            return RedirectToAction("Line", "PreModule", new { area = "Admin" });

        }
    }
}
