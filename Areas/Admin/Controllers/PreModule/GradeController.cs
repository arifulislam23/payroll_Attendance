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
    public class GradeController : Controller
    {
        private readonly KS_PayrollContext _context;

        public GradeController(KS_PayrollContext context)
        {
            _context = context;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create_Grade(Grade data)
        {
            if (data.GradeTitle == null || data.GradeTitle == " ")
            {
                return RedirectToAction("Grade", "PreModule", new { area = "Admin" });
            }

            Grade grad = new Grade
            {
                GradeTitle = data.GradeTitle,
                Created_At = DateTime.Now,
            };
            if (!_context.Grade.Any(t => t.GradeId.Equals(data.GradeId)))
            {
                _context.Grade.Add(grad);
                _context.SaveChanges();
            }

            var gds = _context.Grade.OrderByDescending(x => x.GradeId).ToList();
            ViewData["Grades"] = gds;
            return RedirectToAction("Grade", "PreModule", new { area = "Admin" });

        }


        //grade edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit_Grade(Grade model)
        {

            int id = Int16.Parse(model.GradeId.ToString());
            Grade D = _context.Grade.Find(id);
            D.GradeTitle = model.GradeTitle;
            D.Updated_At = DateTime.Now;
            _context.Update(D);
            _context.SaveChanges();

            return RedirectToAction("Grade", "PreModule", new { area = "Admin" });

        }
    }
}
