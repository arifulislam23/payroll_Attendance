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
    public class CompanyController : Controller
    {
        private readonly KS_PayrollContext _context;

        public CompanyController(KS_PayrollContext context)
        {
            _context = context;
        }
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create_Company(Comapny data)
        {
            if (data.CompanyName == null || data.CompanyName == " ")
            {
                ViewBag.data = _context.Comapny.OrderByDescending(x => x.Cid).ToList();
              
                return RedirectToAction("Company", "PreModule", new { area = "Admin" });
            }

            Comapny comapny = new Comapny
            {

                CompanyName = data.CompanyName,
                CompanyAddress = data.CompanyAddress,
                CompanyContact = data.CompanyContact,
                CompanyDetails = data.CompanyDetails,
                Created_At = DateTime.Now
            };
            if (!_context.Comapny.Any(t => t.CompanyName.Equals(data.CompanyName)))
            {
                _context.Comapny.Add(comapny);
                _context.SaveChanges();
            }


            var Companys = _context.Comapny.OrderByDescending(x => x.Cid).ToList();
            ViewData["Companys"] = Companys;
            return RedirectToAction("Company", "PreModule", new { area = "Admin" });
        }
        //Create new Company End
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Company_Edit(Comapny model)
        {
            int id = Int16.Parse(model.Cid.ToString());
            Comapny Data = _context.Comapny.Find(id);
            Data.CompanyAddress = model.CompanyAddress;
            Data.CompanyContact = model.CompanyContact;
            Data.CompanyDetails = model.CompanyDetails;
            Data.Updated_At = DateTime.Now;
            _context.Update(Data);
            _context.SaveChanges();
            return RedirectToAction("Company", "PreModule", new { area = "Admin" });

        }
    }
}
