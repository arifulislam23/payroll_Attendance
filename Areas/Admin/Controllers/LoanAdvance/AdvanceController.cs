using KS_Payroll.Areas.Admin.Data;
using KS_Payroll.Areas.Admin.ViewModels;
using KS_Payroll.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KS_Payroll.Areas.Admin.Controllers.LoanAdvance
{
    [Area("Admin")]
    public class AdvanceController : Controller
    {
        private readonly KS_PayrollContext _context;
        public AdvanceController(KS_PayrollContext context)
        {
            _context = context;
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
                    if (desig == null)
                    {
                        employee.EmpDesignationTitle = "No Found";
                    }
                    else
                    {
                        employee.EmpDesignationTitle = desig.DesigName.ToString() == null ? "NO Designation" : desig.DesigName.ToString();

                    }
                     if (empattendance == null)
                    {
                        employee.LastAttendance = "No Found";
                    }
                    else
                    {
                        employee.LastAttendance = empattendance.Date.ToShortDateString() == null ? "Not Found" : empattendance.Date.ToShortDateString();

                    }


                    employee.EmpName = empinfo.EmpName;

                    employee.Jdate = empinfo.EmpJoiningDate.ToShortDateString();

                    employee.EmpPhone = empinfo.EmpPhone;
                    employee.EmpStats = empinfo.EmpStats;
                    //employee.LastAttendance = empattendance.Date.ToShortDateString();
                   
                    employee.empid = empinfo.empid;
                    employee.EmpBasic = empinfo.EmpBasic;
                    return Json(employee);
                }

            }
            return Json(0);
        }
        public IActionResult GiveAdvance()
        {
            ViewBag.companies = _context.Comapny.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult GiveAdvance(AdvanceLoan model)
        {
            if(model == null)
            {
                return View(model);
            }
            var emp = _context.Employeeinfo.Where(x => x.empid == model.empid && x.EmpStats == "REGULAR");
            if (emp != null)
            {

                AdvanceLoan data = new AdvanceLoan();
                data.empid = model.empid;
                data.EmpIDCard = model.EmpIDCard;
                data.comid = model.comid;
                data.Amount = model.Amount;
                data.Month = model.Month;
                data.Short_Description = model.Short_Description;
                data.Date = model.Date;
                data.Status = "Active";
                data.Created_At = DateTime.Now;
                _context.Add(data);
                _context.SaveChanges();

            }
            ViewBag.companies = _context.Comapny.ToList();
            return View();
        }

        public IActionResult AdvanceHistory()
        {
            var advanceLoans = _context.AdvanceLoan.OrderBy(x=>x.ALid).ToList();
            return View(advanceLoans);
        }
         public IActionResult LoanHistory()
        {
            var advanceLoans = _context.Loan.OrderBy(x=>x.ALid).ToList();
            return View(advanceLoans);
        }
        

    }
}
