using KS_Payroll.Areas.Admin.Data;
using KS_Payroll.Areas.Admin.ViewModels;
using KS_Payroll.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KS_Payroll.Areas.Admin.Controllers.Regine
{
    [Area("Admin")]
    public class RegineController : Controller
    {
        private readonly KS_PayrollContext _context;
        public RegineController(KS_PayrollContext context)
        {
            _context = context;
        }
        public IActionResult CreateRegine()
        {
            ViewBag.companies = _context.Comapny.ToList();

            return View();
        }
        [HttpPost]
        public IActionResult CreateRegine(Uunemployees model)
        {
            if(model == null)
            {
                return View(model);
            }
            var emp = _context.Employeeinfo.Where(x => x.empid == model.empid && x.EmpStats == "REGULAR");
            if (emp !=null)
            {
                Employeeinfo Data = _context.Employeeinfo.Find(model.empid);
                Data.EmpStats = model.LeftType;
                Data.Updated_At = DateTime.Now;
                _context.Update(Data);
                _context.SaveChanges();

                Uunemployees data = new Uunemployees();
                data.empid = model.empid;
                data.comid = model.comid;
                data.EmpIDCard = model.EmpIDCard;
                data.Designation = model.Designation;
                data.JoningDate = model.JoningDate;
                data.LeftType = model.LeftType;
                data.LastAttendance = model.LastAttendance;
                data.Short_Description = model.Short_Description;
                data.AsingDate = model.AsingDate;
                data.Created_At = DateTime.Now;
                _context.Add(data);
                _context.SaveChanges();
                
            }


            ViewBag.companies = _context.Comapny.ToList();
            return View(model);
        }
        public JsonResult GetEmpInfo(string eidnum, string comid)
        {
            EmpinfoVM employee = new EmpinfoVM();
            if (eidnum != null && comid != null)
            {
                var empinfo = _context.Employeeinfo.Where(x => x.EmpIDCard == eidnum && x.EmpCompany == comid).FirstOrDefault();
                if(empinfo != null)
                {
                    try
                    {
                        var empattendance = _context.FinalAttendance.Where(x => x.empid == empinfo.empid).OrderByDescending(x => x.Date).FirstOrDefault();
                        
                        if(empattendance != null)
                        {
                            employee.LastAttendance = empattendance.Date.ToShortDateString() == "" ? "Not Found" : empattendance.Date.ToShortDateString();
                        }
                        else
                        {
                            employee.LastAttendance = "No Attendance Found";
                        }
                        var desig = _context.Designation.Where(x => x.DesigID.ToString() == empinfo.EmpDesignationTitle).FirstOrDefault();
                        if(desig != null)
                        {
                            employee.EmpDesignationTitle = desig.DesigName.ToString() == "" ? "NO Designation" : desig.DesigName.ToString();
                        }
                        else
                        {
                            employee.EmpDesignationTitle = "No Designation Found";
                        }
                        employee.EmpName = empinfo.EmpName;

                        
                        employee.Jdate = empinfo.EmpJoiningDate.ToShortDateString();

                        employee.EmpPhone = empinfo.EmpPhone;
                        employee.EmpStats = empinfo.EmpStats;
                        //employee.LastAttendance = empattendance.Date.ToShortDateString();
                        
                        employee.empid = empinfo.empid;
                        return Json(employee);

                    }
                    catch(Exception ex)
                    {
                        var exmsg = ex;
                        return Json(0);
                    }
                   
                }
               
            }
                return Json(0);
        }
        public IActionResult RegineList()
        {
            List<Comapny> companies = new List<Comapny>();
            companies = _context.Comapny.ToList();
            ViewBag.companies = companies;
            return View();
        }
        public IActionResult Details(EmpinfoVM model)
        {
            if(model == null)
            {
                return NotFound();
            }
            if(model.EmpIDCard !=null && model.EmpCompany != null && model.EmpPhone !=null)
            {
                var empDetails = _context.Employeeinfo.FirstOrDefault(x=>x.EmpCompany == model.EmpCompany && x.EmpPhone==model.EmpPhone && x.EmpIDCard == model.EmpIDCard);
                var Regineinfo = _context.Uunemployees.FirstOrDefault(x => x.empid == empDetails.empid && x.updateStatus == 0);
                if(Regineinfo !=null)
                return View(Regineinfo);
            }
            else if (model.EmpIDCard != null && model.EmpCompany != null)
            {
                var empDetails = _context.Employeeinfo.FirstOrDefault(x => x.EmpCompany == model.EmpCompany && x.EmpIDCard == model.EmpIDCard);

                var Regineinfo = _context.Uunemployees.FirstOrDefault(x=>x.empid == empDetails.empid && x.updateStatus ==0);
                //ViewData["Regineinfo"] = Regineinfo;
                if (Regineinfo != null)
                    return View(Regineinfo);
            }
           // return NotFound();
           return RedirectToAction("RegineList");
        }

        public IActionResult Active(Uunemployees model)
        {
            if (model == null)
            {
                return View(model);
            }

            

            Employeeinfo Data = _context.Employeeinfo.Find(model.empid);
            Data.EmpStats = "REGULAR";
            Data.Updated_At = DateTime.Now;
            _context.Update(Data);
            _context.SaveChanges();

            Uunemployees data2 = _context.Uunemployees.FirstOrDefault(x => x.empid == model.empid);
            data2.updateStatus = 1;
            data2.Updated_At = DateTime.Now;
            _context.Update(data2);
            _context.SaveChanges();



            return RedirectToAction("RegineList");
        }
    }
}
