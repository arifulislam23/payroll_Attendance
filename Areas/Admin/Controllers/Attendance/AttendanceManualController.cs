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
    public class AttendanceManualController : Controller
    {
        private readonly KS_PayrollContext _context;
        public AttendanceManualController(KS_PayrollContext context)
        {
            _context = context;
        }
        public IActionResult TakeManualAttendance()
        {
            ManualAttendanceVM model = new ManualAttendanceVM();
            var now = DateTime.Now;
            var startOfMonth = new DateTime(now.Year, now.Month, 1);
            model.StartDate = DateTime.Now;
            model.EndDate = DateTime.Now;
            return View(model);

        }
        public JsonResult GetEmpInfo(string eidnum)
        {


            if (eidnum != null && eidnum != "0")
            {
                var allemployee = (from Empinfo in _context.Employeeinfo
                                   
                                   
                                   //join Designation in _context.Designation
                                   //on Empinfo.EmpDesignationTitle equals Designation.DesigID.ToString()

                                  

                                   join shiftTime in _context.Shift
                                   on Empinfo.EmpShift equals shiftTime.ShiftId.ToString()

                                   where(Empinfo.EmpIDCard == eidnum)
                                   select new EmpinfoVM
                                   {
                                       empid = Empinfo.empid,
                                       EmpIDCard = Empinfo.EmpIDCard,
                                       EmpName = Empinfo.EmpName,
                                       Jdate = Empinfo.EmpJoiningDate.ToString("dd-MM-yyyy"),
                                       //EmpDesignationTitle = Designation.DesigName,
                                       InTime = DateTime.Parse(shiftTime.StartTime).ToShortTimeString(),
                                       OutTime = DateTime.Parse(shiftTime.EndTime).ToShortTimeString()

                                   }).FirstOrDefault();


                return Json(allemployee);
            }

            return Json(0);


        }
        public JsonResult GetAttendanceSingel(string eidnum, DateTime date_start, DateTime date_end)
        {
            if (eidnum != null && date_start != null && date_end != null)
            {
               
                    var Attendance = (from attendance in _context.FinalAttendance
                                      join empinfo in _context.Employeeinfo
                                      on attendance.empid equals empinfo.empid
                                      where (empinfo.EmpIDCard == eidnum && attendance.Date >= date_start && attendance.Date <= date_end)
                                      select new AttendanceVM
                                      {
                                          Ddate = attendance.Date.ToShortDateString(),
                                          EmpInTime = (attendance.InTime == null ? "0" : DateTime.Parse(attendance.InTime).ToShortTimeString()),
                                          EmpOutTime = (attendance.OutTime == null ? "0" : DateTime.Parse(attendance.OutTime).ToShortTimeString()),
                                          Late = attendance.Late.ToString(@"hh\:mm"),
                                          Earlyout = attendance.EarlyOut.ToString(@"hh\:mm"),
                                          OverTime = attendance.OverTime,

                                      }).ToList();

                    return Json(Attendance);
                
             
            }
            return Json(0);
        }

   
        public JsonResult AddManualAttendance(string empidcardnum, DateTime InDate, DateTime date_end, string dayinTime, string dayoutTime)
        {

            if (InDate <= date_end)
            {
                
             
               
                    var emp = _context.Employeeinfo.FirstOrDefault(x => x.EmpIDCard == empidcardnum && x.EmpStats == "REGULAR");
                    //var emp = _context.Employeeinfo.FirstOrDefault(x => x.EmpIDCard == empidcardnum);
                    if (emp == null)
                    {
                        return Json(0);
                    }
                if (dayoutTime != null)
                {
                    double tout = double.Parse(dayoutTime.ToString().Replace(":", ""));

                    if (emp != null)
                    {

                        var shift_time = _context.Shift.FirstOrDefault(s => s.ShiftId.ToString() == emp.EmpShift);
                        if (shift_time != null)
                        {
                            string Shift_entry_time = shift_time.StartTime.ToString();
                            string Shift_out_time = shift_time.EndTime.ToString();


                            double Shift_In = double.Parse(Shift_entry_time.Replace(":", "")); //800
                            double Shift_Out = double.Parse(Shift_out_time.Replace(":", "")); //(155 for previos time 115 for next time) 


                            var find_emp = _context.FinalAttendance.FirstOrDefault(x => x.Date == InDate && x.empid == emp.empid);

                            if (find_emp == null) //if no emp foun in final attendance
                            {

                                //if 1
                                FinalAttendance atten = new FinalAttendance();
                                atten.EmpSCode = emp.EmpSecrateCode;
                                atten.empid = emp.empid;
                                //12/24/2022
                                atten.Date = InDate;

                                atten.OutTime = DateTime.Parse(dayoutTime.ToString()).ToString("HH:mm:ss");
                                //atten.InTime = dayinTime.ToString();
                                _context.FinalAttendance.Add(atten);
                                _context.SaveChanges();


                            }
                            else if (find_emp != null)  //if no emp foun in final attendance
                            {

                                //if 2


                                FinalAttendance Data = _context.FinalAttendance.Find(find_emp.sl);

                                Data.OutTime = DateTime.Parse(dayoutTime.ToString()).ToString("HH:mm:ss");
                                _context.FinalAttendance.Update(Data);
                                _context.SaveChanges();//else if 4

                            }
                        }
                    }
                }

                if (dayinTime != null )
                    {
                        double tin = double.Parse(dayinTime.ToString().Replace(":", ""));
                      
                        if (emp != null)
                        {
                            var shift_time = _context.Shift.FirstOrDefault(s => s.ShiftId.ToString() == emp.EmpShift);
                            if (shift_time != null)
                            {
                                string Shift_entry_time = shift_time.StartTime.ToString();
                                string Shift_out_time = shift_time.EndTime.ToString();


                                double Shift_In = double.Parse(Shift_entry_time.Replace(":", "")); //800
                                double Shift_Out = double.Parse(Shift_out_time.Replace(":", "")); //(155 for previos time 115 for next time) 


                                var find_emp = _context.FinalAttendance.FirstOrDefault(x => x.Date == InDate && x.empid == emp.empid);
                                
                                if (find_emp == null && tin >= (Shift_In - 155)) //if no emp foun in final attendance
                                {

                                    //if 1
                                    FinalAttendance atten = new FinalAttendance();
                                    atten.EmpSCode = emp.EmpSecrateCode;
                                    atten.empid = emp.empid;
                                    //12/24/2022
                                    atten.Date = InDate;

                                    atten.InTime = DateTime.Parse(dayinTime.ToString()).ToString("HH:mm:ss");
                                    //atten.InTime = dayinTime.ToString();
                                    _context.FinalAttendance.Add(atten);
                                    _context.SaveChanges();


                                }
                               else if (find_emp != null && tin >= (Shift_In - 155)) //if no emp foun in final attendance
                                {

                                    //if 2
                                 
                                    
                                    FinalAttendance Data = _context.FinalAttendance.Find(find_emp.sl);

                                    Data.InTime = DateTime.Parse(dayinTime.ToString()).ToString("HH:mm:ss");
                                    _context.FinalAttendance.Update(Data);
                                    _context.SaveChanges();//else if 4

                                }
                            }
                        }
                    }
                    AttendanceFinalize(emp.EmpSecrateCode , InDate);
                    return Json(12);
                
               

            }
            
                            return Json(0);
        }

        public JsonResult AddManualAttendance2(string empidcardnum, DateTime date_start, DateTime date_end, string dayinTime, string dayoutTime)
        {
            if (date_start <= date_end)
            {
                double tin = double.Parse(dayinTime.ToString().Replace(":", ""));
                double tout = double.Parse(dayoutTime.ToString().Replace(":", ""));



                if (tin != 0 && tout != 0)
                {
                    var emp = _context.Employeeinfo.FirstOrDefault(x => x.EmpIDCard == empidcardnum && x.EmpStats == "REGULAR");
                    if (emp != null)
                    {
                        var shift_time = _context.Shift.FirstOrDefault(s => s.ShiftId.ToString() == emp.EmpShift);
                        if (shift_time != null)
                        {
                            string entry_time = shift_time.StartTime.ToString();
                            string Shift_out_time = shift_time.EndTime.ToString();


                            double entrytime = double.Parse(entry_time.Replace(":", "")); //800
                            double outtime = double.Parse(Shift_out_time.Replace(":", "")); //(155 for previos time 115 for next time) 


                            var find_emp = _context.FinalAttendance.FirstOrDefault(x => x.Date == date_start && x.empid == emp.empid);
                            
                            FinalAttendance atten = new FinalAttendance();



                            if (find_emp == null && tin >= (entrytime - 155) && tin <= (entrytime + 115)) //if no emp foun in row attendance
                            {

                                //if 1

                                atten.EmpSCode = emp.EmpSecrateCode;

                                //12/24/2022
                                atten.Date = date_start;

                                atten.InTime = DateTime.Parse(dayinTime.ToString()).ToString("HH:mm:ss");

                                //atten.InTime = dayinTime.ToString();
                                _context.FinalAttendance.Add(atten);
                                _context.SaveChanges();


                            }
                            else if (find_emp != null && tin >= (entrytime - 155) && tin <= (entrytime + 115))
                            {
                                //else if 3
                                if (find_emp.InTime == null || find_emp.InTime == "00:00:00")
                                {
                                    FinalAttendance Data = _context.FinalAttendance.Find(find_emp.sl);
                                    Data.InTime = DateTime.Parse(dayinTime.ToString()).ToString("HH:mm:ss");
                                    // Data.InTime = dayinTime.ToString();
                                    _context.FinalAttendance.Update(Data);
                                    _context.SaveChanges();//else if 4

                                }
                                else if (find_emp.InTime != null || find_emp.InTime != "00:00:00")
                                {
                                    var ft = DateTime.Parse(dayinTime.ToString()).ToString("HH:mm:ss");
                                    //var st = DateTime.Parse(find_emp.ToString()).ToString("HH:mm:ss");
                                    var st = find_emp.InTime.ToString();
                                    double ftt = double.Parse(ft.Replace(":", "")); //8:00:00
                                    double stt = double.Parse(st.Replace(":", "")); //8:10:00
                                    if (stt > ftt)
                                    {
                                        FinalAttendance Data = _context.FinalAttendance.Find(find_emp.sl);
                                        Data.InTime = DateTime.Parse(dayinTime.ToString()).ToString("HH:mm:ss");
                                        _context.FinalAttendance.Update(Data);
                                        _context.SaveChanges();//else if 5

                                    }
                                }


                            }
                            else if (find_emp == null && tin < (entrytime - 155))
                            {//not checked
                                DateTime tempdate = date_start.AddDays(-1);
                                var P_find_emp = _context.FinalAttendance.FirstOrDefault(x => x.Date == tempdate && x.EmpSCode == emp.EmpSecrateCode);

                                if (P_find_emp == null)
                                {
                                    atten.EmpSCode = emp.EmpSecrateCode;

                                    //atten.Date = DateTime.Parse(filedate.ToString()).AddDays(-1);
                                    atten.Date = tempdate;
                                    atten.OutTime = DateTime.Parse(dayinTime.ToString()).ToString("HH:mm:ss");
                                    _context.FinalAttendance.Add(atten);
                                    _context.SaveChanges();
                                }
                                else
                                {
                                    FinalAttendance Data = _context.FinalAttendance.Find(P_find_emp.sl);
                                    Data.OutTime = DateTime.Parse(dayinTime.ToString()).ToString("HH:mm:ss");
                                    _context.FinalAttendance.Update(Data);
                                    _context.SaveChanges();
                                }

                            }

                            if (find_emp != null && find_emp.OutTime == null && date_start == date_end)
                            {
                                FinalAttendance Data = _context.FinalAttendance.Find(find_emp.sl);
                                Data.OutTime = Shift_out_time;
                                _context.FinalAttendance.Update(Data);
                                _context.SaveChanges();//else if 6



                            }
                            else if (find_emp != null && find_emp.OutTime != null && date_start == date_end)
                            {
                                var ft = Shift_out_time;
                                var st = find_emp.OutTime.ToString();
                                double ftt = double.Parse(ft.Replace(":", ""))*100; //8:00:00
                                double stt = double.Parse(st.Replace(":", "")); //8:10:00
                                if (stt < ftt)
                                {//else if 7
                                    FinalAttendance Data = _context.FinalAttendance.Find(find_emp.sl);
                                    Data.OutTime = Shift_out_time;
                                    _context.FinalAttendance.Update(Data);
                                    _context.SaveChanges();//else if 4

                                }
                            }
                            else if(find_emp != null && date_start < date_end && tout < (entrytime - 155) && find_emp.OutTime != null)
                            {
                                var ft = Shift_out_time;
                                var st = find_emp.OutTime.ToString();
                                double ftt = double.Parse(ft.Replace(":", "")); //8:00:00
                                double stt = double.Parse(st.Replace(":", "")); //8:10:00
                                if (stt < ftt)
                                {//else if 8
                                    FinalAttendance Data = _context.FinalAttendance.Find(find_emp.sl);
                                    Data.OutTime = Shift_out_time;
                                    _context.FinalAttendance.Update(Data);
                                    _context.SaveChanges();//else if 4

                                }
                            }
                            else if(find_emp != null && date_start < date_end && tout < (entrytime - 155) && find_emp.OutTime == null)
                            {
                                FinalAttendance Data = _context.FinalAttendance.Find(find_emp.sl);
                                Data.OutTime = Shift_out_time;
                                _context.FinalAttendance.Update(Data);
                                _context.SaveChanges();//else if 9
                            }

                        }
                    }
                    TempData["scode"] = emp.EmpSecrateCode;
                }

            }
            //empidcardnum
            AttendanceFinalize(TempData["scode"].ToString(), date_start);
            return Json(13);
        }



        public void AttendanceFinalize(string SCode, DateTime date)
        {
            
                var sa = _context.FinalAttendance.FirstOrDefault(x => x.Date == date && x.EmpSCode == SCode);
                if (sa == null)
                {
                var emp = _context.Employeeinfo.FirstOrDefault(x => x.EmpSecrateCode == SCode);
                if (emp != null)
                {

                    var checkDesig = _context.Designation.FirstOrDefault(x => x.DesigID.ToString() == emp.EmpDesignationTitle);
                    var takeTime = _context.Shift.FirstOrDefault(x => x.ShiftId == int.Parse(emp.EmpShift));
                    if (takeTime != null)
                    {
                        FinalAttendance final = new FinalAttendance();
                        TimeSpan shiftStart = TimeSpan.Parse(takeTime.StartTime);
                        TimeSpan shiftEnd = TimeSpan.Parse(takeTime.EndTime);

                        if (sa.InTime != null)
                        {
                            TimeSpan rowInTime = TimeSpan.Parse(sa.InTime);
                            if (rowInTime > shiftStart)//8:10 8:00 
                            {
                                final.Late = rowInTime - shiftStart;
                            }
                        }

                        if (sa.OutTime != null)
                        {
                            TimeSpan rowOutTime = TimeSpan.Parse(sa.OutTime);


                            double outtime = double.Parse(shiftStart.ToString().Replace(":", "")); //(155 for previos time 115 for next time) 

                            if (shiftStart < rowOutTime && rowOutTime < shiftEnd)//5:00 4:30
                            {
                                final.EarlyOut = shiftEnd - rowOutTime;
                                final.OverTime = 0;
                            }

                            if (shiftEnd < rowOutTime)
                            {
                                TimeSpan temTime = rowOutTime - shiftEnd;

                                if (temTime.Minutes >= 46)
                                {
                                    final.OverTime = (temTime.Hours) + 1;
                                }
                                else
                                {
                                    final.OverTime = temTime.Hours;
                                }
                                TimeSpan breakeTimeS = new TimeSpan(0, 0, 46, 0, 0);
                                if (rowOutTime >= breakeTimeS)
                                {
                                    final.OverTime = final.OverTime - 1;//=====
                                }
                                else
                                {
                                    final.OverTime = final.OverTime;
                                }
                                if (checkDesig.OverTime == false)
                                {
                                    final.OverTime = 0;
                                }
                            }
                        }

                        final.InTime = sa.InTime;
                        final.OutTime = sa.OutTime;
                        final.EmpSCode = emp.EmpSecrateCode;
                        final.empid = emp.empid;
                        final.Date = sa.Date;
                        final.AttenStatus = "M";
                        _context.Add(final);
                        _context.SaveChanges();


                    }
                }
            }
                else
                {
                    var emp = _context.Employeeinfo.FirstOrDefault(x => x.EmpSecrateCode == sa.EmpSCode.ToString());
                
                    if (emp != null)
                    {
                    var checkDesig1 = _context.Designation.FirstOrDefault(x => x.DesigID.ToString() == emp.EmpDesignationTitle);
                    var takeTime = _context.Shift.FirstOrDefault(x => x.ShiftId == int.Parse(emp.EmpShift));
                        if (takeTime != null)
                        {
                            FinalAttendance final = _context.FinalAttendance.Find(sa.sl);
                            TimeSpan shiftStart = TimeSpan.Parse(takeTime.StartTime);
                            TimeSpan shiftEnd = TimeSpan.Parse(takeTime.EndTime);
                            TimeSpan shiftBreakS = TimeSpan.Parse(takeTime.BreakStartTime).Add(new TimeSpan(0, 0, 46, 0));
                        //shiftBreakS = shiftBreakS.Add(new TimeSpan(0,0,46,0));
                            TimeSpan shiftBreakE = TimeSpan.Parse(takeTime.BreakEndTime);

                        var checkHoliday = _context.Calendar.FirstOrDefault(x=>x.FDate == date && x.Eventstats ==0);
                        if(checkHoliday == null)
                        {
                            if (sa.InTime != null)
                            {
                                TimeSpan rowInTime = TimeSpan.Parse(sa.InTime);
                                if (rowInTime > shiftStart)//8:10 8:00 
                                {
                                    final.Late = rowInTime - shiftStart;
                                }
                            }
                            if (sa.OutTime != null)
                            {
                                TimeSpan rowOutTime = TimeSpan.Parse(sa.OutTime);
                                if (shiftEnd > rowOutTime && rowOutTime > shiftStart)//5:00 4:30
                                {
                                    final.EarlyOut = shiftEnd - rowOutTime; //this is early out cal
                                    final.OverTime = 0;
                                }


                                else if (shiftEnd < rowOutTime)
                                {
                                    TimeSpan temTime = rowOutTime - shiftEnd;

                                    if (temTime.Minutes >= 46)
                                    {
                                        final.OverTime = (temTime.Hours) + 1;
                                        final.EarlyOut = new TimeSpan(0, 0, 0, 0);
                                    }
                                    else
                                    {
                                        final.OverTime = temTime.Hours;
                                        final.EarlyOut = new TimeSpan(0, 0, 0, 0);
                                    }
                                    if (checkDesig1.OverTime == false)
                                    {
                                        final.OverTime = 0;
                                    }
                                }

                                else if (rowOutTime < shiftEnd)//------
                                {
                                    int finalOT = 0;
                                    int tem1 = (240000 - Int32.Parse(shiftEnd.ToString().Replace(":", ""))) / 10000;

                                    if (rowOutTime.Minutes >= 46)
                                    {
                                        finalOT = (rowOutTime.Hours) + 1 + tem1;
                                    }
                                    else
                                    {
                                        finalOT = rowOutTime.Hours + tem1;
                                    }
                                    TimeSpan breakeTimeE = new TimeSpan(0, 1, 45, 0, 0);
                                    TimeSpan breakeTimeS = new TimeSpan(0, 0, 46, 0, 0);
                                    if (rowOutTime >= breakeTimeS)
                                    {
                                        final.OverTime = finalOT - 1;//=====
                                    }
                                    else
                                    {
                                        final.OverTime = finalOT;
                                    }


                                    final.EarlyOut = new TimeSpan(0, 0, 0, 0);
                                }
                            }
                        }//holiday null end
                        else //if holiday found
                        {
                            if (sa.InTime != null)
                            {
                                TimeSpan rowInTime = TimeSpan.Parse(sa.InTime);
                                if (rowInTime > shiftStart)//8:10 8:00 
                                {
                                    final.Late = rowInTime - shiftStart;
                                }
                            }//late count


                            if (sa.OutTime != null)
                            {
                                TimeSpan rowOutTime = TimeSpan.Parse(sa.OutTime);
                                TimeSpan duityHour = rowOutTime - shiftStart;

                                if (shiftStart.Add(new TimeSpan(0,45,59))< rowOutTime && rowOutTime<shiftBreakS)//before shift end time
                                {
                                    final.EarlyOut = shiftEnd - rowOutTime; //this is early out cal
                                    if (duityHour.Minutes >= 46)
                                    {
                                        final.OverTime = ((duityHour.Hours) + 1);
                                    }
                                    else
                                    {
                                        final.OverTime = duityHour.Hours;
                                    }
                                }
                                else if (shiftBreakS.Add(new TimeSpan(0,45,59))< rowOutTime && rowOutTime <=shiftEnd)//before shift end time
                                {
                                    final.EarlyOut = shiftEnd - rowOutTime; //this is early out cal
                                    if (duityHour.Minutes >= 46)
                                    {
                                        final.OverTime = ((duityHour.Hours) + 1)-1;
                                    }
                                    else
                                    {
                                        final.OverTime = duityHour.Hours -1;
                                    }
                                }
                                else if(rowOutTime>shiftEnd && rowOutTime>shiftStart)
                                {
                                    final.EarlyOut = new TimeSpan(0, 0, 0, 0); //this is over time cal after shift end
                                    if (duityHour.Minutes >= 46)
                                    {
                                        final.OverTime = ((duityHour.Hours) + 1)-1;
                                    }
                                    else
                                    {
                                        final.OverTime = duityHour.Hours -1;
                                    }
                                }


                                else if (rowOutTime < shiftEnd && rowOutTime < shiftStart.Add(new TimeSpan(-1,-15,0)))//------
                                {
                                    int finalOT = 0;
                                    int tem1 = ((240000 - Int32.Parse(shiftStart.ToString().Replace(":", ""))) / 10000)-1;
                                    TimeSpan fd = shiftStart.Add(new TimeSpan(-1, -15, 0));
                                    if (rowOutTime.Minutes >= 46)
                                    {
                                        finalOT = ((rowOutTime.Hours) + 1) + tem1;
                                    }
                                    else
                                    {
                                        finalOT = ((rowOutTime.Hours) + 1);
                                    }
                                    TimeSpan breakeTimeEE = new TimeSpan(0, 1, 45, 0, 0);
                                    TimeSpan breakeTimeES = new TimeSpan(0, 0, 46, 0, 0);
                                    if (rowOutTime >= breakeTimeES)
                                    {
                                        final.OverTime = finalOT - 1;//=====
                                    }
                                    else
                                    {
                                        final.OverTime = finalOT;
                                    }


                                    final.EarlyOut = new TimeSpan(0, 0, 0, 0);
                                }
                            }
                        }    

                            

                        if (checkDesig1.OverTime == false)
                        {
                            final.OverTime = 0;
                        }

                        final.InTime = sa.InTime;
                            final.OutTime = sa.OutTime;
                            final.EmpSCode = emp.EmpSecrateCode;
                            final.empid = emp.empid;
                            final.Date = sa.Date;
                            final.AttenStatus = "M";

                            _context.FinalAttendance.Update(final);
                            _context.SaveChanges();


                        }
                    }
                }



            

           
        }

    }//main
}
