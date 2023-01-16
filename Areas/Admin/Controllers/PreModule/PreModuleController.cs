using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KS_Payroll.Areas.Admin.Data;
using KS_Payroll.Data;
using KS_Payroll.Areas.Admin.ViewModels;

namespace KS_Payroll.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PreModuleController : Controller
    {
        private readonly KS_PayrollContext _context;

        public PreModuleController(KS_PayrollContext context)
        {
            _context = context;
        }

        private void LoadData()
        {
            List<Comapny> companies = new List<Comapny>();
            companies = _context.Comapny.ToList();
            ViewBag.companies = companies;

            List<Department> departments = new List<Department>();
            departments = _context.Department.ToList();
            ViewBag.departments = departments;


            List<Section> sections = new List<Section>();
            sections = _context.Section.ToList();
            ViewBag.sections = sections;


            List<Floor> floors = new List<Floor>();
            floors = _context.Floor.ToList();
            ViewBag.floors = floors;



        }
        public IActionResult Company()
        {
            var Companys = _context.Comapny.OrderByDescending(x => x.Cid).ToList();
            ViewData["Companys"] = Companys;
            return View();
        }

       

        public IActionResult Department()
        {
            LoadData();

            var cd = (from dept in _context.Department.ToList()
                       join comp in _context.Comapny.ToList()
                       on dept.CompanyId equals comp.Cid
                       select new DepartmentVM
                       {
                           DepID = dept.DepartmentID,
                           ComID = comp.Cid,
                           DepartmentName = dept.DepartmentName,
                           CompanyName = comp.CompanyName,
                           
                       });
            ViewData["Departments"] = cd;
            return View();
        }
        public IActionResult Section()
        {
            LoadData();

            var sections = (from dept in _context.Department.ToList()
                            join section in _context.Section.ToList()
                             on dept.DepartmentID equals section.DepartmentID
                            select new DepartmentVM
                            {
                                DepID = dept.DepartmentID,
                                SecID = section.SectionId,
                                DepartmentName = dept.DepartmentName,
                                SectionName = section.SectionName,
                            });
            ViewData["Sections"] = sections.OrderByDescending(x=>x.SecID);
            return View();
        }
        public IActionResult Floor()
        {
            var floors = _context.Floor.OrderByDescending(x => x.FloorId).ToList();
            ViewData["floors"] = floors;
            return View();
        }
         public IActionResult Leave()
        {
            var Leaves = _context.Leave.OrderByDescending(x => x.LeaveId).ToList();
            ViewData["leave"] = Leaves;
            return View();
        }
       
         public IActionResult Line()
        {
            LoadData();

            var line = (from lines in _context.Line.ToList()
                        join section in _context.Section.ToList() on lines.SectionId equals section.SectionId
                        join flr in _context.Floor.ToList() on lines.FloorId equals flr.FloorId

                        select new DepartmentVM
                        {
                            SecID = section.SectionId,
                            FloorID = flr.FloorId,
                            SectionName = section.SectionName,
                            FloorName = flr.FloorName,
                            LineID = lines.LineId,
                            LineTitle = lines.LineTitle,
                        });
            ViewData["Lines"] = line;
            return View();
        }

        public IActionResult Shift()
        {
            var Shifts = _context.Shift.ToList();
            ViewData["Shifts"] = Shifts;
            return View();
        }

         public IActionResult Grade()
        {
            var grade = _context.Grade.ToList();
            ViewData["Grades"] = grade;
            return View();
        }
         public IActionResult DesignationType()
        {
            var designationdype = _context.DesignationType.ToList();
            ViewData["DesignationTypes"] = designationdype;
            return View();
        }
         public IActionResult Designation()
        {
            var Designations = _context.Designation.OrderByDescending(x => x.DesigID).ToList();
            ViewData["Designations"] = Designations;

            var grade = _context.Grade.ToList();
            ViewData["Grades"] = grade;
            return View();
        }

    }
}
