using KS_Payroll.Areas.Admin.Data;
using KS_Payroll.Areas.Admin.ViewModels;
using KS_Payroll.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KS_Payroll.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DepartmentController : Controller
    {
        private readonly KS_PayrollContext _context;

        public DepartmentController(KS_PayrollContext context)
        {
            _context = context;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create_Department(Department data)
        {
            if (data == null)
            {
                ViewBag.data = _context.Department.OrderByDescending(x => x.DepartmentID).ToList();

                return RedirectToAction("Department", "PreModule", new { area = "Admin" });
            }

            Department department = new Department
            {

                DepartmentName = data.DepartmentName,
                CompanyId = data.CompanyId,
                Created_At = DateTime.Now
            };
            if (!_context.Department.Any(t => t.DepartmentID.Equals(data.DepartmentID)))
            {
                _context.Department.Add(department);
                _context.SaveChanges();
            }


            var Companys = _context.Department.OrderByDescending(x => x.DepartmentID).ToList();
            ViewData["Departments"] = Companys;
            return RedirectToAction("Department", "PreModule", new { area = "Admin" });
          
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit_Department(Department model)
        {
            
            int id = Int16.Parse(model.DepartmentID.ToString());
            Department D = _context.Department.Find(id);
            D.DepartmentName = model.DepartmentName;
            D.CompanyId = model.CompanyId;
            D.Updated_At = DateTime.Now;
            _context.Update(D);
            _context.SaveChanges();
            return RedirectToAction("Department", "PreModule", new { area = "Admin" });

        }
    }
}
