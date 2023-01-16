using KS_Payroll.Areas.Admin.Data;
using KS_Payroll.Areas.Admin.ViewModels;
using KS_Payroll.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace KS_Payroll.Areas.Admin.Controllers.Employee
{
    [Area("Admin")]
    public class EmpDocumentController : Controller
    {
        

        private readonly IWebHostEnvironment _henv;

        public EmpDocumentController(IWebHostEnvironment henv)
        {
            _henv = henv;
        }

       

        //emp multipule picture List View Page
        public IActionResult MultiPicIndex()
        {
            
            return View();
        }

        //public IActionResult UploadFile(IEnumerable<HttpPosted> filetoupload)
        //{
        //    string msg = "";

        //    if (filetoupload != null)
        //    {

        //        string root = _henv.WebRootPath;
        //        string dir = "UploadedProfile";
        //        string fname = Path.GetFileName(filetoupload.FileName);

        //        string filename = Path.Combine(root, dir, fname);

        //        using (var stream = new FileStream(filename, FileMode.Create))
        //        {
        //             filetoupload.CopyTo(stream);
        //        }
        //        TempData["msg"] = msg;
        //        return RedirectToAction("MultiPicIndex");
        //    }


        //    return RedirectToAction("MultiPicIndex", "MultipleImgUpld");
        //}



        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            
            if (file.Length > 0)
            {
                string root = _henv.WebRootPath;
                string dir = "UploadedProfile";
                string fname = Path.GetFileName(file.FileName);
                string filename = Path.Combine(root, dir, fname);
                using (var stream = new FileStream(filename, FileMode.Create))
                {
                    //file.CopyTo(stream);
                    await file.CopyToAsync(stream);
                }
             
            }
            return RedirectToAction("MultiPicIndex");
        }
    }
}
