using KS_Payroll.Areas.Admin.Data;
using KS_Payroll.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KS_Payroll.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DesignationTypeController : Controller
    {
        private readonly KS_PayrollContext _context;
        public DesignationTypeController(KS_PayrollContext context)
        {
            _context = context;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create_DesignationType(DesignationType data)
        {
            if (data.DesignationTypeName == null || data.DesignationTypeName == " ")
            {
                return RedirectToAction("DesignationType", "PreModule", new { area = "Admin" });
            }

            DesignationType DesignationType = new DesignationType
            {

                DesignationTypeName = data.DesignationTypeName,
                Created_At = DateTime.Now
            };
            if (!_context.DesignationType.Any(t => t.DesignationTypeName.Equals(data.DesignationTypeName)))
            {
                _context.DesignationType.Add(DesignationType);
                _context.SaveChanges();
            }


            var DesignationTypes = _context.DesignationType.OrderByDescending(x => x.DesignationTypeId).ToList();
            ViewData["DesignationTypes"] = DesignationTypes;
            return RedirectToAction("DesignationType", "PreModule", new { area = "Admin" });
        }

        //DesignationTypes Edit

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DesignationType_Edit(DesignationType model)
        {
            int id = Int16.Parse(model.DesignationTypeId.ToString());
            DesignationType Data = _context.DesignationType.Find(id);
            Data.DesignationTypeName = model.DesignationTypeName;
            Data.Updated_At = DateTime.Now;

            _context.Update(Data);
            _context.SaveChanges();

            return RedirectToAction("DesignationType", "PreModule", new { area = "Admin" });

        }
    }
}
