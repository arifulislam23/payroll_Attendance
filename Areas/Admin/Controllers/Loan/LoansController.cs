using KS_Payroll.Areas.Admin.Data;
using KS_Payroll.Areas.Admin.ViewModels;
using KS_Payroll.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KS_Payroll.Areas.Admin.Controllers.Loan
{
    [Area("Admin")]
    public class LoansController : Controller
    {
        private readonly KS_PayrollContext _context;
        public LoansController(KS_PayrollContext context)
        {
            _context = context;
        }
        public IActionResult GiveLoan()
        {
            ViewBag.companies = _context.Comapny.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult GiveLoan(Loans model)
        {
            if (model == null)
            {
                return View(model);
            }
            var emp = _context.Employeeinfo.Where(x => x.empid == model.empid && x.EmpStats == "REGULAR");
            if (emp != null)
            {

                Loans data = new Loans();
                data.empid = model.empid;
                data.EmpIDCard = model.EmpIDCard;
                data.comid = model.comid;
                data.Amount = model.Amount;
                data.TimePerioad = model.TimePerioad;
                data.Short_Description = model.Short_Description;
                data.Date = model.Date;
                data.Status = "Active";
                data.PerMonth = model.Amount / model.TimePerioad;
                data.Created_At = DateTime.Now;
                _context.Add(data);
                _context.SaveChanges();

            }
            ViewBag.companies = _context.Comapny.ToList();
            return View();
        }
        public JsonResult GetEmpInfo(string eidnum, string comid)
        {
            EmpinfoVM employee = new EmpinfoVM();
            if (eidnum != null && comid != null)
            {
                var empinfo = _context.Employeeinfo.Where(x => x.EmpIDCard == eidnum && x.EmpCompany == comid && x.EmpStats == "REGULAR").FirstOrDefault();
                if (empinfo != null)
                {
                    var empattendance = _context.FinalAttendance.Where(x => x.empid == empinfo.empid).OrderByDescending(x => x.Date).FirstOrDefault();
                    var desig = _context.Designation.Where(x => x.DesigID.ToString() == empinfo.EmpDesignationTitle).FirstOrDefault();

                    employee.EmpName = empinfo.EmpName;

                    employee.EmpDesignationTitle = desig.DesigName.ToString() == null ? "NO Designation" : desig.DesigName.ToString();
                    employee.Jdate = empinfo.EmpJoiningDate.ToShortDateString();

                    employee.EmpPhone = empinfo.EmpPhone;
                    employee.EmpStats = empinfo.EmpStats;
                    //employee.LastAttendance = empattendance.Date.ToShortDateString();
                    employee.LastAttendance = empattendance.Date.ToShortDateString() == null ? "Not Found" : empattendance.Date.ToShortDateString();
                    employee.empid = empinfo.empid;
                    employee.EmpBasic = empinfo.EmpBasic;
                    return Json(employee);
                }

            }
            return Json(0);
        }
    }
}
