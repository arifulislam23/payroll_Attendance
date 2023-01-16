using KS_Payroll.Areas.Admin.Data;
using KS_Payroll.Areas.Admin.ViewModels;
using KS_Payroll.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace KS_Payroll.Areas.Admin.Controllers.Employee
{
    [Area("Admin")]
    public class EmployeeUpdateController : Controller
    {
        private readonly IWebHostEnvironment _henv;
        private readonly KS_PayrollContext _context;

        public EmployeeUpdateController(KS_PayrollContext context, IWebHostEnvironment henv)
        {
            _context = context;
            _henv = henv;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> EmployeeUpdat(EmpinfoVM model)
        {

            Employeeinfo addemp = new Employeeinfo();
            string root = _henv.WebRootPath;

            
            if (ModelState.IsValid)
            {
                var data = _context.Employeeinfo.Where(x => x.empid == model.empid).FirstOrDefault();

                //Profile Picture
                if (model.EmpPrfImg != null)
                {
                    string ext = Path.GetExtension(model.EmpPrfImg.FileName);
                   
                    string nm= Path.GetFileNameWithoutExtension(model.EmpPrfImg.FileName);

                    if (model.EmpIDCard ==  nm)
                    {
                        if (ext == ".jpg" || ext == ".png" || ext == ".pdf")
                        {
                            string dir = "UploadedProfile";
                            string fname = Path.GetFileName(model.EmpPrfImg.FileName);

                            string filename = Path.Combine(root, dir, fname);

                            using var stream = new FileStream(filename, FileMode.Create);
                            await model.EmpPrfImg.CopyToAsync(stream);
                        }


                    }
                }
                  //NID
                if (model.EmpNIDImg != null)
                {
                    string ext = Path.GetExtension(model.EmpNIDImg.FileName);
                    string nm = Path.GetFileNameWithoutExtension(model.EmpNIDImg.FileName);

                    if (model.EmpIDCard == nm)
                    {
                        if (ext == ".jpg" || ext == ".png" || ext == ".pdf")
                        {
                            string dir = "UploadedNID";
                            string fname = Path.GetFileName(model.EmpNIDImg.FileName);

                            string filename = Path.Combine(root, dir, fname);

                            using var stream = new FileStream(filename, FileMode.Create);
                            await model.EmpNIDImg.CopyToAsync(stream);
                        }

                    }
                        
                }
                //DOB 
                if (model.EmpBodImg != null)
                {
                    string ext = Path.GetExtension(model.EmpBodImg.FileName);
                    string nm = Path.GetFileNameWithoutExtension(model.EmpBodImg.FileName);

                    if (model.EmpIDCard  == nm)
                    {
                        if (ext == ".jpg" || ext == ".png" || ext == ".pdf")
                        {
                            string dir = "UploadedDOB";
                            string fname = Path.GetFileName(model.EmpBodImg.FileName);

                            string filename = Path.Combine(root, dir, fname);

                            using var stream = new FileStream(filename, FileMode.Create);
                            await model.EmpBodImg.CopyToAsync(stream);
                        }
                    }

                }

                //pasport img to byte[]
                if (model.EmpPassportImg != null)
                {
                    string ext = Path.GetExtension(model.EmpPassportImg.FileName);
                    string nm = Path.GetFileNameWithoutExtension(model.EmpPassportImg.FileName);

                    if (model.EmpIDCard == nm)
                    {
                        if (ext == ".jpg" || ext == ".png" || ext == ".pdf")
                        {
                            string dir = "UploadedPassport";
                            string fname = Path.GetFileName(model.EmpPassportImg.FileName);

                            string filename = Path.Combine(root, dir, fname);

                            using var stream = new FileStream(filename, FileMode.Create);
                            await model.EmpPassportImg.CopyToAsync(stream);
                        }
                    }

                }
                //Contract img to byte[]
              
                if (model.EmpContractImg != null)
                {
                    string ext = Path.GetExtension(model.EmpContractImg.FileName);
                    string nm = Path.GetFileNameWithoutExtension(model.EmpContractImg.FileName);

                    if (model.EmpIDCard == nm)
                    {
                        if (ext == ".jpg" || ext == ".png" || ext == ".pdf")
                        {
                            string dir = "UploadedContract";
                            string fname = Path.GetFileName(model.EmpContractImg.FileName);

                            string filename = Path.Combine(root, dir, fname);

                            using var stream = new FileStream(filename, FileMode.Create);
                            await model.EmpContractImg.CopyToAsync(stream);
                        }
                    }

                }
                //Contract img to byte[]
                if (model.EmpSigImg != null)
                {
                    string ext = Path.GetExtension(model.EmpSigImg.FileName);
                    string nm = Path.GetFileNameWithoutExtension(model.EmpSigImg.FileName);

                    if (model.EmpIDCard == nm)
                    {
                        if (ext == ".jpg" || ext == ".png" || ext == ".pdf")
                        {
                            string dir = "UploadedSing";
                            string fname = Path.GetFileName(model.EmpSigImg.FileName);

                            string filename = Path.Combine(root, dir, fname);

                            using var stream = new FileStream(filename, FileMode.Create);
                            await model.EmpSigImg.CopyToAsync(stream);
                        }
                    }

                }
              

                data.EmpName = model.EmpName;
                data.EmpFName = model.EmpFName;
                data.EmpMName = model.EmpMName;
                data.EmpSpouseName = model.EmpSpouseName;
                data.EmpPhone = model.EmpPhone;
                data.EmpEmgPerson = model.EmpEmgPerson;
                data.EmpEmgphone = model.EmpEmgphone;
                data.EmpDOB = model.EmpDOB;
                data.EmpNID = model.EmpNID;
                data.EmpBODCertificate = model.EmpBODCertificate;
                data.EmpPassport = model.EmpPassport;
                data.EmpGender = model.EmpGender;
                data.EmpMarriedStatus = model.EmpMarriedStatus;
                data.EmpChildNumber = model.EmpChildNumber;
                data.EmpNationality = model.EmpNationality;
                data.EmpReligion = model.EmpReligion;
                data.EmpBlood = model.EmpBlood;
                data.EmpNomineeName = model.EmpNomineeName;
           data.EmpNomineePhn = model.EmpNomineePhn;
          data.EmpNomineeRlt = model.EmpNomineeRlt;
          data.EmpRefName = model.EmpRefName;
          data.EmpRefNumber = model.EmpRefNumber;
          data.EmpHeight = model.EmpHeight;
           data.EmpWeight = model.EmpWeight;
           data.EmpParmanentDistrict = model.EmpParmanentDistrict;
           data.EmpParmanentZip = model.EmpParmanentZip;
           data.EmpParmanentArea = model.EmpParmanentArea;
           data.EmpParmanentAddress = model.EmpParmanentAddress;
           data.EmpPresentDistrict = model.EmpPresentDistrict;
           data.EmpPresentZip = model.EmpPresentZip;
           data.EmpPresentArea = model.EmpPresentArea;
       data.EmpPresentAddress = model.EmpPresentAddress;
        data.EmpEduTitle = model.EmpEduTitle;
            data.EmpEduPassY = model.EmpEduPassY;
          data.EmpEduStatus = model.EmpEduStatus;
           data.EmpEduPassG = model.EmpEduPassG;
            data.EmpCompany = model.EmpCompany;
           data.EmpIDCard = model.EmpIDCard;
           data.EmpSecrateCode = model.EmpSecrateCode;
           data.EmpType = model.EmpType;
           data.EmpDepartment = model.EmpDepartment;
            data.EmpSection = model.EmpSection;
            data.EmpFloor = model.EmpFloor;
              data.EmpLine = model.EmpLine;
             data.EmpJoiningDate = model.EmpJoiningDate;
             data.EmpShift = model.EmpShift;
             data.EmpDesignationTitle = model.EmpDesignationTitle;
             data.EmpDesignationType = model.EmpDesignationType;
            data.EmpGrade = model.EmpGrade;
           data.EmpAttenBonus = model.EmpAttenBonus;
              data.EmpOverTime = model.EmpOverTime;
              data.EmpHolidayAlw = model.EmpHolidayAlw;
              data.EmpNightBill = model.EmpNightBill;
              data.EmpLunchBilll = model.EmpLunchBilll;
             data.EmpTiffinBill = model.EmpTiffinBill;
             data.EmpIftarBill = model.EmpIftarBill;
             data.EmpGross = model.EmpGross;
            data.EmpBasic = model.EmpBasic;
           data.EmpHouse = model.EmpHouse;
           data.EmpMedical = model.EmpMedical;
           data.EmpConveyance = model.EmpConveyance;
            data.EmpFood = model.EmpFood;
            data.EmpPayMethodName = model.EmpPayMethodName;
            data.EmpBankName = model.EmpBankName;
             data.EmpMFSName = model.EmpMFSName;
             data.EmpBankBranch = model.EmpBankBranch;
             data.EmpBankAcc = model.EmpBankAcc;
             data.MFSnumber = model.MFSnumber;
                //EmpProfileImg = model.EmpProfileImg;
                //EmpNIDImg = model.EmpNIDImg;
                //EmpBodImg = model.EmpBodImg;
                //EmpPassportImg = model.EmpPassportImg;
                //EmpContractImg = model.EmpContractImg;
                //EmpSigImg = model.EmpSigImg;


                data.Updated_At = DateTime.Now;

                 _context.Employeeinfo.Update(data);
                await _context.SaveChangesAsync();
                ViewBag.msg = "updateOK";
                return RedirectToAction("EmployeeList", "Employee", new { area = "Admin" });
            }
            
            return View(model);

            
        }
    }
}
