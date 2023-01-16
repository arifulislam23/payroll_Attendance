using KS_Payroll.Areas.Admin.Data;
using KS_Payroll.Areas.Admin.ViewModels;
using KS_Payroll.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KS_Payroll.Areas.Admin.Controllers.Attendance
{
    [Area("Admin")]
    public class AttendanceReportController : Controller
    {
        private readonly KS_PayrollContext _context;
        public AttendanceReportController(KS_PayrollContext context)
        {
            _context = context;
        }
        public IActionResult AttendanceList()
        {
            AttendanceVM model = new AttendanceVM();
            model.searchFromDate = DateTime.Today;
            model.searchToDate = DateTime.Today;
            
            return View(model);
        }

        [HttpPost]
        public IActionResult AttendanceList(AttendanceVM model)
        {

          
            if(model.searchFromDate != null && model.searchFromDate != null && model.empIDCard != null)
            {
                var attendanceList = (from final in _context.FinalAttendance
                                      
                                      join emp in _context.Employeeinfo
                                      on final.empid equals emp.empid
                                      where (final.Date >= model.searchFromDate && final.Date <= model.searchToDate && emp.EmpIDCard == model.empIDCard)
                                      select new AttendanceVM
                                      {
                                          empIDCard = emp.EmpIDCard,
                                          empName = emp.EmpName,
                                          Date = final.Date,
                                          EmpInTime = (final.InTime == null ? "0" : DateTime.Parse(final.InTime).ToShortTimeString()),
                                          EmpOutTime = (final.OutTime == null ? "0" : DateTime.Parse(final.OutTime).ToShortTimeString()),
                                          OverTime = final.OverTime,
                                      }).OrderBy(x => x.Date).ToList();


                ViewData["absentlist"] = attendanceList;
            }
            else if (model.searchFromDate != null && model.searchFromDate != null)
            {
                // var absentlist1 = _context.FinalAttendance.Where(x => x.Date >= model.searchFromDate && x.Date <= model.searchToDate).ToList();

                var attendanceList = (from final in _context.FinalAttendance
                                      
                                      join emp in _context.Employeeinfo
                                      on final.empid equals emp.empid
                                      where (final.Date >= model.searchFromDate && final.Date <= model.searchToDate)
                                      select new AttendanceVM
                                      {
                                          empIDCard = emp.EmpIDCard,
                                          empName = emp.EmpName,
                                          Date = final.Date,
                                          EmpInTime = (final.InTime == null ? "0" : DateTime.Parse(final.InTime).ToShortTimeString()),
                                          EmpOutTime = (final.OutTime == null ? "0" : DateTime.Parse(final.OutTime).ToShortTimeString()),
                                          OverTime = final.OverTime,
                                      }).OrderBy(x=>x.Date).ToList();


                ViewData["absentlist"] = attendanceList;
            }

            return View(model);
        }

        public IActionResult AttendanceMissingList()
        {
            AbsentList model = new AbsentList();
            model.StartDate = DateTime.Today;
            model.EndDate = DateTime.Today;

            return View(model);
        }

        [HttpPost]
        public IActionResult AttendanceMissingList(AbsentList model)
        {
            
            switch (model.type)
            {
                
                case "MissingOut":
                    var absentlist2 = (from Empinfo in _context.Employeeinfo
                                       join finalAttendance in _context.FinalAttendance
                                       on Empinfo.EmpSecrateCode equals finalAttendance.EmpSCode
                                      
                                       where (finalAttendance.Date >= model.StartDate && finalAttendance.Date <= model.EndDate && finalAttendance.OutTime==null)

                                       select new AbsentList
                                       {
                                           empIdCard = Empinfo.EmpIDCard,
                                           empName = Empinfo.EmpName,
                                           Date = finalAttendance.Date,
                                           EmpInTime = (finalAttendance.InTime == null ? "0" : DateTime.Parse(finalAttendance.InTime).ToShortTimeString()),
                                           EmpOutTime = (finalAttendance.OutTime == null ? "0" : DateTime.Parse(finalAttendance.OutTime).ToShortTimeString()),

                                       }).OrderBy(x => x.Date).ToList();

                    ViewData["absentlist"] = absentlist2;
                    break;
                case "MissingIn":
                  var   absentlist3 = (from Empinfo in _context.Employeeinfo
                                       join finalAttendance in _context.FinalAttendance
                                       on Empinfo.EmpSecrateCode equals finalAttendance.EmpSCode

                                       where (finalAttendance.Date >= model.StartDate && finalAttendance.Date <= model.EndDate && finalAttendance.InTime ==null)

                                       select new AbsentList
                                       {
                                           empIdCard = Empinfo.EmpIDCard,
                                           empName = Empinfo.EmpName,
                                           Date = finalAttendance.Date,
                                           EmpInTime = (finalAttendance.InTime == null ? "0" : DateTime.Parse(finalAttendance.InTime).ToShortTimeString()),
                                           EmpOutTime = (finalAttendance.OutTime == null ? "0" : DateTime.Parse(finalAttendance.OutTime).ToShortTimeString()),
                                           //Where(x => x.EmpInTime == "0" && x.EmpOutTime != "0")
                                       }).OrderBy(x => x.Date).ToList();


                    ViewData["absentlist"] = absentlist3;
                    break;

                case "Both":
                    var absentlist4 = (from Empinfo in _context.Employeeinfo
                                        join finalAttendance in _context.FinalAttendance
                                        on Empinfo.EmpSecrateCode equals finalAttendance.EmpSCode
                                     where ((finalAttendance.InTime == null || finalAttendance.OutTime == null) && (finalAttendance.Date>=model.StartDate && finalAttendance.Date<=model.EndDate))
               
                                       select new AbsentList
                                      {
                                           empIdCard = Empinfo.EmpIDCard,
                                           empName = Empinfo.EmpName,
                                           Date = finalAttendance.Date,
                                           EmpInTime = (finalAttendance.InTime == null ? "0" : DateTime.Parse(finalAttendance.InTime).ToShortTimeString()),
                                           EmpOutTime = (finalAttendance.OutTime == null ? "0" : DateTime.Parse(finalAttendance.OutTime).ToShortTimeString()),

                                       }).OrderBy(x => x.Date).ToList();
                    ViewData["absentlist"] = absentlist4;
                    break;
            }

            return View(model);
        }
        

    }
}
