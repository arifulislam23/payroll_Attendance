using KS_Payroll.Areas.Admin.Data;
using KS_Payroll.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KS_Payroll.Areas.Admin
{
    [Area("Admin")]
    public class CalendarController : Controller
    {

        private readonly KS_PayrollContext _context;

        public CalendarController(KS_PayrollContext context)
        {
            _context = context;
        }

        // GET: CalendarController/Create view page
        public ActionResult EventCreate()
        {
            // 0 = not deleted
            var calender = _context.Calendar.Where(x => x.Eventstats == 0).OrderByDescending(m=>m.CalendarID).ToList();
            //var calender = _context.Calendar.ToList();
            ViewData["Events"] = calender;
            return View();
        }

        static public List<string> GetDates(DateTime start_date, DateTime end_date,string days)
        {
            List<string> days_list = new List<string>();
            for (DateTime date = start_date; date <= end_date; date = date.AddDays(1))
            {
                //if(days == "Monday")
                //{
                //    if (date.DayOfWeek == DayOfWeek.Monday)
                //        days_list.Add(date.ToShortDateString());
                //}
                switch (days)
                {

                    case "Friday":
                        if (date.DayOfWeek == DayOfWeek.Friday)
                            days_list.Add(date.ToShortDateString());
                        break;
                    case "Saturday":
                        if (date.DayOfWeek == DayOfWeek.Saturday)
                            days_list.Add(date.ToShortDateString());
                        break;
                    case "Sunday":
                        if (date.DayOfWeek == DayOfWeek.Sunday)
                            days_list.Add(date.ToShortDateString());
                        break;
                        case "Monday":
                        if (date.DayOfWeek == DayOfWeek.Monday)
                            days_list.Add(date.ToShortDateString());
                        break;
                    case "Tuesday":
                        if (date.DayOfWeek == DayOfWeek.Monday)
                            days_list.Add(date.ToShortDateString());
                        break;
                        case "Wednesday":
                        if (date.DayOfWeek == DayOfWeek.Wednesday)
                            days_list.Add(date.ToShortDateString());
                        break;
                    case "Thursday":
                        if (date.DayOfWeek == DayOfWeek.Thursday)
                            days_list.Add(date.ToShortDateString());
                        break; 

                }


            }

            return days_list;
        }
            // POST: CalendarController/Create action
            [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EventCreate(Calendar model)
        {
           
            try
            {
                if (model.Days == " " || model.Days == "Select Day" ||model.Days==null)
                {
                    // set holidays event
                    var selectedDates = new List<DateTime?>();
                    for (var date = model.StartDate; date <= model.EndDate; date = date.AddDays(1))
                    {
                        selectedDates.Add(date);

                    }
                    //list all the date between start to end date
                    foreach (var item in selectedDates)
                    {
                        Calendar clendar = new Calendar
                        {
                            OccassionName = model.OccassionName,
                            FDate = (DateTime)item,
                            Created_At = DateTime.Now,
                            Eventtype = "General Holiday",
                            Eventstats = 0
                        };
                        if (!_context.Calendar.Any(x => x.FDate == (DateTime)item && x.Eventstats == 0))
                        {
                            _context.Calendar.Add(clendar);
                            _context.SaveChanges();
                        }
                       
                    }
                    
                }
                else
                {
                    var date = GetDates(model.StartDate, model.EndDate,model.Days);
                    //list all the date between start to end date
                    foreach (var item in date)
                    {
                        Calendar clendar = new Calendar
                        {
                            OccassionName = model.OccassionName,
                            FDate = DateTime.Parse(item),
                            Created_At = DateTime.Now,//Weekend
                            Eventtype = "Weekend",
                            Eventstats = 0
                        };
                        if (!_context.Calendar.Any(x => x.FDate == DateTime.Parse(item) && x.Eventstats ==0))
                        {
                            _context.Calendar.Add(clendar);
                            _context.SaveChanges();
                        }
                            
                    }
                }
                
                return RedirectToAction(nameof(EventCreate));

               
            }
            catch
            {
                return View(model);
            }
        }

        //Make the date inactive by 0 convert 1 (delete)

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Event_Edit(Calendar model)
        {
            int id = Int16.Parse(model.CalendarID.ToString());
            Calendar Data = _context.Calendar.Find(id);
            Data.Eventstats = 1;
            Data.Updated_At = DateTime.Now;
            _context.Update(Data);
            _context.SaveChanges();

            return RedirectToAction(nameof(EventCreate));

        }


    }
}
