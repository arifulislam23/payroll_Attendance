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
    public class SectionController : Controller
    {
        private readonly KS_PayrollContext _context;

        public SectionController(KS_PayrollContext context)
        {
            _context = context;
        }
        public IActionResult Create_Section(Section data)
        {
            if (data.SectionName == null || data.SectionName == " ")
            {
                
                ViewBag.data = _context.Section.OrderByDescending(x => x.SectionId).ToList();

                return RedirectToAction("Section", "PreModule", new { area = "Admin" });
            }

            Section section = new Section
            {

                SectionId = data.SectionId,
                SectionName = data.SectionName,
                DepartmentID = data.DepartmentID,
                Created_At = DateTime.Now
            };
            if (!_context.Section.Any(t => t.SectionId.Equals(data.SectionId)))
            {
                _context.Section.Add(section);
                _context.SaveChanges();
            }


            var Companys = _context.Section.OrderByDescending(x => x.SectionId).ToList();
            ViewData["Sections"] = Companys;
            return RedirectToAction("Section", "PreModule", new { area = "Admin" });

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit_Section(Section model)
        {

            int id = Int16.Parse(model.SectionId.ToString());
            Section D = _context.Section.Find(id);
            D.SectionName = model.SectionName;
            D.DepartmentID = model.DepartmentID;
            D.Updated_At = DateTime.Now;
            _context.Update(D);
            _context.SaveChanges();
            return RedirectToAction("Section", "PreModule", new { area = "Admin" });

        }
    }
}
