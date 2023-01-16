using KS_Payroll.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using CsvHelper;
using KS_Payroll.Areas.Admin.Data;

namespace KS_Payroll.Areas.Admin.Controllers.Attendance
{
    [Area("Admin")]
    public class AttendanceModuleController : Controller
    {
        private readonly KS_PayrollContext _context;
        
        private readonly IWebHostEnvironment _henv;

      
        public AttendanceModuleController(KS_PayrollContext context, IWebHostEnvironment henv)
        {
            _context = context;
            _henv = henv;
        }
        public IActionResult AttendanceModule()
        {
            var AttendanceModule = _context.AttendanceModule.OrderByDescending(x => x.AMid).ToList();
            ViewData["AttendanceModule"] = AttendanceModule;
            return View();
        }
         public IActionResult AttendanceModule_Create(AttendanceModule data)
        {

            if (data==null)
            {

                return RedirectToAction("AttendanceModule",data);
            }

            AttendanceModule AttendanceModule = new AttendanceModule
            {

                AModuleName = data.AModuleName,
                SecretCode = data.SecretCode,
                DateName = data.DateName,
                TimeName = data.TimeName,
                Time2Name = data.Time2Name,
                FileType = data.FileType,
                TableName = data.TableName,
            };
            if (!_context.AttendanceModule.Any(t => t.AModuleName.Equals(data.AModuleName)))
            {
                _context.AttendanceModule.Add(AttendanceModule);
                _context.SaveChanges();
            }

            return RedirectToAction("AttendanceModule");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AttendanceModule_Edit(AttendanceModule model)
        {
            int id = Int16.Parse(model.AMid.ToString());
            AttendanceModule Data = _context.AttendanceModule.Find(id);
            Data.AModuleName = model.AModuleName;
            Data.DateName = model.DateName;
            Data.TimeName = model.TimeName;
            Data.Time2Name = model.Time2Name;
            Data.SecretCode = model.SecretCode;
            Data.TableName = model.TableName;
            Data.FileType = model.FileType;
            _context.Update(Data);
            _context.SaveChanges();
            return RedirectToAction("AttendanceModule");

        }

    
        public IActionResult UploadAttendance()
        {
            return View();
        }


        public IActionResult RowAttendanceList()
        {
           
            return View(_context.RowAttendance.OrderByDescending(x => x.SL).ToList());
        }

    }//main
}
