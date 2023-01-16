using KS_Payroll.Areas.Admin.Data;
using KS_Payroll.Areas.Admin.ViewModels;
using KS_Payroll.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace KS_Payroll.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EmployeeController : Controller
    {
        private readonly KS_PayrollContext _context;

        public EmployeeController(KS_PayrollContext context)
        {
            _context = context;
        }
        private void takeData()
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
            
            List<Line> lines = new List<Line>();
            lines = _context.Line.ToList();
            ViewBag.lines = lines;
            
            List<Shift> Shifts = new List<Shift>();
            Shifts = _context.Shift.ToList();
            ViewBag.shifts = Shifts;
            
            List<DesignationType> DesignationTypes = new List<DesignationType>();
            DesignationTypes = _context.DesignationType.OrderBy(x=>x.DesignationTypeId).ToList();
            ViewBag.DesignationTypes = DesignationTypes;

            List<Designation> Designations = new List<Designation>();
            Designations = _context.Designation.ToList();
            ViewBag.designations = Designations;


        }
        //get department
        public JsonResult GetDept(int companyID)
        {
            var viewmodel = new List<KS_Payroll.Areas.Admin.Data.Department>();
            var model = new KS_Payroll.Areas.Admin.Data.Department();
            var depart = _context.Department.Where(x => x.CompanyId == companyID).ToList();

            for (int i = 0; i < depart.Count(); i++)
            {
                viewmodel.Add(depart[i]);
            }
            return Json(viewmodel);
        }

        //get section
        public JsonResult GetSec(int depID)
        {
            var viewmodel = new List<KS_Payroll.Areas.Admin.Data.Section>();

            var sect = _context.Section.Where(x => x.DepartmentID == depID).ToList();

            for (int i = 0; i < sect.Count(); i++)
            {
                viewmodel.Add(sect[i]);
            }

            return Json(viewmodel);
        }

        //get Line by (section + floor)
        public JsonResult GetLine(int flrID, int secID)
        {
            var viewmodel = new List<KS_Payroll.Areas.Admin.Data.Line>();

            var line = _context.Line.Where(x => x.SectionId == secID && x.FloorId == flrID).ToList();

            for (int i = 0; i < line.Count(); i++)
            {
                viewmodel.Add(line[i]);
            }

            return Json(viewmodel);
        }
        public JsonResult CheckDesignation(int designationId)
        {
            try
            {
                var Designation = _context.Designation.Where(x => x.DesigID == designationId).FirstOrDefault();
                return Json(Designation);
            }
            catch (Exception ex)
            {
                return Json(ex);
            }
        }

        //Employees Create From Start
        public IActionResult CreateEmployee()
        {
            takeData();
            var Companys = _context.Comapny.ToList();
            ViewData["Companys"] = Companys;
            return View();
        }
        //Employees Create From End


        public IActionResult EmployeeList()
        {
            //var allemployee = (from Empinfo in _context.Employeeinfo
            //                       //join Section in _context.Section
            //                       //on Empinfo.EmpSection equals Section.SectionId.ToString()
            //                       //join Designation in _context.Designation
            //                       //on Empinfo.EmpDesignationTitle equals Designation.DesigID.ToString()

            //                       //join line in _context.Line
            //                       //on Empinfo.EmpLine equals line.LineId.ToString()
            //                   select new EmpinfoVM
            //                   {
            //                       EmpIDCard = Empinfo.EmpIDCard,
            //                       EmpName = Empinfo.EmpName,
            //                       EmpSection = Empinfo.EmpSection.ToString(),
            //                       EmpJoiningDate = Empinfo.EmpJoiningDate,
            //                       EmpDesignationTitle = Empinfo.EmpDesignationTitle,
            //                       EmpGrade = Empinfo.EmpGender,
            //                       EmpLine = Empinfo.EmpLine,
            //                       EmpGender = Empinfo.EmpGender,
            //                       EmpPhone = Empinfo.EmpPhone,
            //                       empid = Empinfo.empid,
            //                   }).ToList();




            //return View(allemployee);
            return View(_context.Employeeinfo.ToList());
        }
        public IActionResult EmpDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
               
            }
            var empDetails = (from Empinfo in _context.Employeeinfo
                               //join Section in _context.Section
                               //on Empinfo.EmpSection equals Section.SectionId.ToString()
                               //join Designation in _context.Designation
                               //on Empinfo.EmpDesignationTitle equals Designation.DesigID.ToString()

                               //join line in _context.Line
                               //on Empinfo.EmpLine equals line.LineId.ToString()
                               select new EmpinfoVM
                               {
                                   EmpIDCard = Empinfo.EmpIDCard,
                                   EmpName = Empinfo.EmpName,
                                   EmpSection = Empinfo.EmpSection.ToString(),
                                   EmpJoiningDate = Empinfo.EmpJoiningDate,
                                   EmpDesignationTitle = Empinfo.EmpDesignationTitle,
                                   EmpGrade = Empinfo.EmpGender,
                                   EmpLine = Empinfo.EmpLine,
                                   EmpGender = Empinfo.EmpGender,
                                   EmpPhone = Empinfo.EmpPhone,
                                   EmpFName = Empinfo.EmpFName,
                                   EmpStats = Empinfo.EmpStats,
                                   empid = Empinfo.empid,
                               }).FirstOrDefault(m => m.empid == id);

            //var empDetails = _context.Employeeinfo.FirstOrDefault(m => m.empid == id);


            if (empDetails == null)
            {
                return NotFound();
            }

            return View(empDetails);

        }
        //emp update view page
         public IActionResult EmpUpdate(int? id)
        {
            if (id == null)
            {
                return NotFound();
               
            }

            takeData();
            var Companys = _context.Comapny.ToList();
            ViewData["Companys"] = Companys;
            var empDetails = _context.Employeeinfo.FirstOrDefault(m => m.empid == id);


            if (empDetails == null)
            {
                return NotFound();
            }

            return View(empDetails);

        }

        

    }
}
