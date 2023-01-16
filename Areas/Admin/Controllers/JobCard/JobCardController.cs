using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KS_Payroll.Areas.Admin.Data;
using KS_Payroll.Areas.Admin.ViewModels;
using KS_Payroll.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KS_Payroll.Areas.Admin.Controllers.JobCard
{
    [Area("Admin")]
    public class JobCardController : Controller
    {
        private readonly KS_PayrollContext _context;
        public JobCardController(KS_PayrollContext context)
        {
            _context = context;
        }
        public IActionResult JobCardList()
        {
            JobCardVm model = new JobCardVm();

            var now = DateTime.Now;
            var startOfMonth = new DateTime(now.Year, now.Month, 1);
            var EndOfMonth = new DateTime(now.Year, now.Month, 1).AddMonths(1).AddDays(-1);
            model.searchFromDate = startOfMonth;
            model.searchToDate = EndOfMonth;
            return View(model);
        }

        [HttpPost]
        public IActionResult JobCardList(JobCardVm model)
        {

            if(model.searchFromDate != null && model.searchToDate !=null && model.EmpIDCard !=null)
            {
                string EmpidCard = model.EmpIDCard.ToString();
                int empid = _context.Employeeinfo.Where(x => x.EmpIDCard == EmpidCard).Select(c=>c.empid).FirstOrDefault();
                int totalDays = 0;
                int totalLeave = 0;
                int totalEvent = 0;
                int totalPresent = 0;
                int totalAbsent = 0;
                TimeSpan totallate = new TimeSpan(0,0,0,0,0);
                TimeSpan totalEarlyOut = new TimeSpan(0,0,0,0,0);
                _context.TempJobCard.RemoveRange(_context.TempJobCard);
                _context.SaveChanges();
                for (var date = model.searchFromDate; date <= model.searchToDate; date = date.AddDays(1))
                {
                    TempJobCard temjobcard = new TempJobCard();
                    temjobcard.Date = date;

                    var EventCalender = _context.Calendar.Where(x => x.FDate == date && x.Eventstats==0).FirstOrDefault();
                    var leave = _context.Leaveinfo.FirstOrDefault(x => x.EmpCardID == EmpidCard && x.Leave_Start>=date && x.Leave_End<=date && x.Leave_Stats ==0);
                    var attendance = _context.FinalAttendance.FirstOrDefault(x=>x.empid == empid && x.Date==date);


                    if (EventCalender != null)
                    {
                        temjobcard.status = EventCalender.Eventtype;
                        temjobcard.InTime = "0";
                        temjobcard.OutTime = "0";
                        temjobcard.EarlyOut = new TimeSpan(0, 0, 0, 0, 0);
                        temjobcard.LateTime = new TimeSpan(0, 0, 0, 0, 0);
                        temjobcard.OverTime = 0;
                        totalEvent++;

                    }
                    else if (leave !=null)
                    {
                        temjobcard.status = leave.Leave_desc;
                        temjobcard.InTime = "0";
                        temjobcard.OutTime = "0";
                        temjobcard.EarlyOut = new TimeSpan(0, 0, 0, 0, 0);
                        temjobcard.LateTime = new TimeSpan(0, 0, 0, 0, 0);
                        temjobcard.OverTime = 0;
                        totalLeave++;
                    }
                    else if(attendance != null)
                    {
                        temjobcard.status = "Present";
                        //temjobcard.InTime = attendance.InTime;
                        temjobcard.InTime = (attendance.InTime == null ? "0" : DateTime.Parse(attendance.InTime).ToShortTimeString());
                        temjobcard.OutTime = (attendance.OutTime == null ? "0" : DateTime.Parse(attendance.OutTime).ToShortTimeString());
                        
                        //temjobcard.OutTime = attendance.OutTime;
                        temjobcard.EarlyOut = attendance.EarlyOut;
                        temjobcard.LateTime = attendance.Late;
                        temjobcard.OverTime = attendance.OverTime;
                        totalPresent++;
                        totallate = totallate + attendance.Late;
                        totalEarlyOut = totalEarlyOut + attendance.EarlyOut;

                    }
                    else
                    {
                        temjobcard.status = "Absent";
                        temjobcard.InTime = "0";
                        temjobcard.OutTime = "0";
                        temjobcard.EarlyOut = new TimeSpan(0, 0, 0, 0, 0);
                        temjobcard.LateTime = new TimeSpan(0, 0, 0, 0, 0);
                        temjobcard.OverTime = 0;
                        totalAbsent++;
                    }

                    try
                    {
                       
                        
                        _context.TempJobCard.Add(temjobcard);

                        _context.SaveChanges();
                    }
                   catch(Exception ex)
                    {
                        var msg = ex;
                    }
                    totalDays++;
                }

                ViewData["attendanceList"] = _context.TempJobCard.ToList();

                


                int totalOT = _context.TempJobCard.Select(x => x.OverTime).Sum();

                var empDetails = (from Empinfo in _context.Employeeinfo
                                  //join Section in _context.Section
                                  //on Empinfo.EmpSection equals Section.SectionId.ToString()
                                  //join Designation in _context.Designation
                                  //on Empinfo.EmpDesignationTitle equals Designation.DesigID.ToString()

                                  //join line in _context.Line
                                  //on Empinfo.EmpLine equals line.LineId.ToString()
                                  select new JobCardVm
                                  {

                                      EmpIDCard = Empinfo.EmpIDCard,
                                      EmpName = Empinfo.EmpName,
                                      EmpSection = Empinfo.EmpSection,
                                      EmpJoiningDate = Empinfo.EmpJoiningDate,
                                      EmpDesignationTitle = Empinfo.EmpDesignationTitle,
                                      EmpGrade = Empinfo.EmpGender,
                                      EmpLine = Empinfo.EmpLine,
                                      EmpFloor = Empinfo.EmpFloor,
                                      EmpStats = Empinfo.EmpStats,
                                      TotalOverTime= totalOT,
                                      totalPresent = totalPresent,
                                      totalAbsent = totalAbsent,
                                      totalLeave= totalLeave,
                                      totalEvent = totalEvent,
                                      totalDays = totalDays,
                                      totalLate = totallate.ToString(@"hh\:mm"),
                                      totalEarlyOut = totalEarlyOut.ToString(@"hh\:mm"),
                                      empid = Empinfo.empid,
                                  }).FirstOrDefault(m => m.EmpIDCard == EmpidCard);

                ViewData["empDetails"] = empDetails;
                return View(empDetails);
            }

            return View();

        }

       
    }
}
