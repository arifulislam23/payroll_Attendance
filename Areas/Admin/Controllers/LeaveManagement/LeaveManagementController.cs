using KS_Payroll.Areas.Admin.Data;
using KS_Payroll.Areas.Admin.ViewModels;
using KS_Payroll.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KS_Payroll.Areas.Admin.Controllers.LeaveMng
{
    [Area("Admin")]
    public class LeaveManagementController : Controller
    {
        private readonly KS_PayrollContext _context;

        public LeaveManagementController(KS_PayrollContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        } 

        public IActionResult CreateLeave()
        {
     
            ViewBag.companies = _context.Comapny.ToList();
            ViewBag.lines = _context.Leave.ToList();
            return View();
        }
     

        public IActionResult Create_Leave(Leaveinfo model)
        {
           
            Leaveinfo data = new Leaveinfo();
            data.ComID = model.ComID;
            data.EmpCardID = model.EmpCardID;
            data.Leave_Type = model.Leave_Type;
            data.Leave_desc = model.Leave_desc;
            data.Leave_Start = model.Leave_Start;
            data.Leave_End = model.Leave_End;
            data.Leave_Bonus = model.Leave_Bonus;
            data.Leave_Days = ((model.Leave_End - model.Leave_Start).Days + 1).ToString();

           




            _context.Leaveinfo.Add(data);
                _context.SaveChanges();


            return RedirectToAction("LeaveHistory");
        }
        public JsonResult GetEmpInfo(string eidnum, string comid, string ltype)
        {
           
            LeaveMngVM LeaveMng = new LeaveMngVM();

            if (eidnum!=null && comid != null)
            {
                 var empinfo = _context.Employeeinfo.Where(x => x.EmpIDCard == eidnum && x.EmpCompany == comid).FirstOrDefault();



                var Companys = _context.Leaveinfo.Where(x => x.EmpCardID == eidnum && x.ComID == comid).ToList();
                ViewData["leaveinfos"] = Companys;

                if (empinfo == null) 
                {
                    LeaveMng.EmpName = "No Employee Found";
                    LeaveMng.Empjoiningdate = DateTime.Today;
                    LeaveMng.Leave_Days = "0";
                    LeaveMng.Leave_Balance = "0";
                    LeaveMng.Leave_Used = "0";
                    return Json(LeaveMng);
                }

                if (ltype != null)
                {
                    var ldays = _context.Leave.Where(x => x.LeaveTitle == ltype).FirstOrDefault();
                    LeaveMng.Leave_Days = ldays.NoOfDays.ToString();

                    var ls = _context.Leaveinfo.Where(x => x.EmpCardID == eidnum && x.Leave_Type== ltype && x.Leave_Stats==0).ToList();

                    int total = 0;
                    foreach(var item in ls)
                    {
                        total = total+ Int32.Parse(item.Leave_Days);
                    }

                    LeaveMng.Leave_Used = total.ToString();
                    //LeaveMng.Leave_Used = ls.Count().ToString();

                    var Lhave = int.Parse(LeaveMng.Leave_Days) - int.Parse(LeaveMng.Leave_Used);
                    LeaveMng.Leave_Balance = Lhave.ToString();

                  
                }
                else
                {
                    LeaveMng.Leave_Days = "0";
                    LeaveMng.Leave_Balance = "0";
                    LeaveMng.Leave_Used = "0";
                }


                
                LeaveMng.EmpName = empinfo.EmpName;
                LeaveMng.Empjoiningdate = empinfo.EmpJoiningDate;
                LeaveMng.Empjoiningdate = empinfo.EmpJoiningDate;


                return Json(LeaveMng);
            }

            return Json(LeaveMng);

           
        }
        public IActionResult LeaveHistory()
        {
            
            return View(_context.Leaveinfo.Where(x => x.Leave_Stats ==0).OrderByDescending(x=>x.LID).ToList());
        }
         public IActionResult Update(int id)
        {
           
           _context.Leaveinfo.Where(w => w.LID == id).ToList().ForEach(w => w.Leave_Stats = 1);
          
            _context.SaveChanges();
            return RedirectToAction("LeaveHistory");
        }

    }//bdy
}
