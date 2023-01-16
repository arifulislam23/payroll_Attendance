using KS_Payroll.Areas.Admin.Data;
using KS_Payroll.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using KS_Payroll.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

namespace KS_Payroll.Areas.Admin.Controllers.EmpInfo
{
    [Area("Admin")]
    public class EmpCreateController : Controller
    {
       
        

        private readonly KS_PayrollContext _context;
        private readonly IWebHostEnvironment _henv;
        public EmpCreateController(KS_PayrollContext context, IWebHostEnvironment henv)
        {
            _context = context;
            _henv = henv;
        }
        public JsonResult CheckIdCardNo(string idcard)
        {
            try
            {
                var official = _context.Employeeinfo.Where(c => c.EmpIDCard == idcard).FirstOrDefault();
                if (official == null)
                {
                    return Json(0);
                }
                else
                {
                    return Json(1);
                }

            }
            catch (Exception ex)
            {
                return Json(ex);
            }
        }
        public JsonResult CheckSecrateCode(string code)
        {
            try
            {
                var personal = _context.Employeeinfo.Where(c => c.EmpSecrateCode == code).FirstOrDefault();
                if (personal == null)
                {//not found
                    return Json(0);
                }
                else
                {
                    return Json(1);
                }
            }
            catch (Exception ex)
            {
                return Json(ex);
            }

        }
   

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Emp_Create(EmpinfoVM model)
        {
            Employeeinfo addemp = new Employeeinfo();
            string root = _henv.WebRootPath;

            //Profile Picture
            if (model.EmpPrfImg != null)
            {
                string ext = Path.GetExtension(model.EmpPrfImg.FileName);

                string nm = Path.GetFileNameWithoutExtension(model.EmpPrfImg.FileName);

                if (model.EmpIDCard == nm)
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

                if (model.EmpIDCard == nm)
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



            //profile img to byte[]
            //    if (model.EmpPrfImg != null)
            //{
            //    using (var ms = new MemoryStream())
            //    {
            //        await model.EmpPrfImg.CopyToAsync(ms);
            //        addemp.EmpPrfImg = ms.ToArray();
            //    }
            //}

            ////NID img to byte[]
            //if (model.EmpNIDImg != null)
            //{
            //    using (var ms = new MemoryStream())
            //    {
            //        await model.EmpNIDImg.CopyToAsync(ms);
            //        addemp.EmpNIDImg = ms.ToArray();
            //    }
            //}
            ////DOB img to byte[]
            //if (model.EmpBodImg != null)
            //{
            //    using (var ms = new MemoryStream())
            //    {
            //        await model.EmpBodImg.CopyToAsync(ms);
            //        addemp.EmpBodImg = ms.ToArray();
            //    }
            //}
            ////pasport img to byte[]
            //if (model.EmpPassportImg != null)
            //{
            //    using (var ms = new MemoryStream())
            //    {
            //        await model.EmpPassportImg.CopyToAsync(ms);
            //        addemp.EmpPassportImg = ms.ToArray();
            //    }
            //}
            ////Contract img to byte[]
            //if (model.EmpContractImg != null)
            //{
            //    using (var ms = new MemoryStream())
            //    {
            //        await model.EmpContractImg.CopyToAsync(ms);
            //        addemp.EmpContractImg = ms.ToArray();
            //    }
            //}
            ////Contract img to byte[]
            //if (model.EmpSigImg != null)
            //{
            //    using (var ms = new MemoryStream())
            //    {
            //        await model.EmpSigImg.CopyToAsync(ms);
            //        addemp.EmpSigImg = ms.ToArray();
            //    }
            //}


            addemp.empid = model.empid;
            addemp.EmpStats = "REGULAR";
            addemp.EmpName = model.EmpName;
             addemp.EmpFName = model.EmpFName;
              addemp.EmpMName = model.EmpMName;
              addemp.EmpSpouseName = model.EmpSpouseName;
              addemp.EmpPhone = model.EmpPhone;
               addemp.EmpEmgPerson = model.EmpEmgPerson;
               addemp.EmpEmgphone = model.EmpEmgphone;
              addemp.EmpDOB = model.EmpDOB;
              addemp.EmpNID = model.EmpNID;
              addemp.EmpBODCertificate = model.EmpBODCertificate;
              addemp.EmpPassport = model.EmpPassport;
              addemp.EmpGender = model.EmpGender;
              addemp.EmpMarriedStatus = model.EmpMarriedStatus;
              addemp.EmpChildNumber = model.EmpChildNumber;
              addemp.EmpNationality = model.EmpNationality;
              addemp.EmpReligion = model.EmpReligion;
             addemp.EmpBlood = model.EmpBlood;
             addemp.EmpNomineeName = model.EmpNomineeName;
             addemp.EmpNomineePhn = model.EmpNomineePhn;
             addemp.EmpNomineeRlt = model.EmpNomineeRlt;
              addemp.EmpRefName = model.EmpRefName;
              addemp.EmpRefNumber = model.EmpRefNumber;
               addemp.EmpHeight = model.EmpHeight;
              addemp.EmpWeight = model.EmpWeight;
              addemp.EmpParmanentDistrict = model.EmpParmanentDistrict;
              addemp.EmpParmanentZip = model.EmpParmanentZip;
              addemp.EmpParmanentArea = model.EmpParmanentArea;
              addemp.EmpParmanentAddress = model.EmpParmanentAddress;
              addemp.EmpPresentDistrict = model.EmpPresentDistrict;
              addemp.EmpPresentZip = model.EmpPresentZip;
              addemp.EmpPresentArea = model.EmpPresentArea;
              addemp.EmpPresentAddress = model.EmpPresentAddress;
              addemp.EmpEduTitle = model.EmpEduTitle;
              addemp.EmpEduPassY = model.EmpEduPassY;
              addemp.EmpEduStatus = model.EmpEduStatus;
              addemp.EmpEduPassG = model.EmpEduPassG;
              addemp.EmpCompany = model.EmpCompany;
              addemp.EmpIDCard = model.EmpIDCard;
             addemp.EmpSecrateCode = model.EmpSecrateCode;
             addemp.EmpType = model.EmpType;
              addemp.EmpDepartment = model.EmpDepartment;
              addemp.EmpSection = model.EmpSection;
              addemp.EmpFloor = model.EmpFloor;
              addemp.EmpLine = model.EmpLine;
              addemp.EmpJoiningDate = model.EmpJoiningDate;
               addemp.EmpShift = model.EmpShift;
               addemp.EmpDesignationTitle = model.EmpDesignationTitle;
               addemp.EmpDesignationType = model.EmpDesignationType;
              addemp.EmpGrade = model.EmpGrade;
              addemp.EmpAttenBonus = model.EmpAttenBonus;
              addemp.EmpOverTime = model.EmpOverTime;
              addemp.EmpHolidayAlw = model.EmpHolidayAlw;
              addemp.EmpNightBill = model.EmpNightBill;
              addemp.EmpLunchBilll = model.EmpLunchBilll;
              addemp.EmpTiffinBill = model.EmpTiffinBill;
               addemp.EmpIftarBill = model.EmpIftarBill;
               addemp.EmpGross = model.EmpGross;
               addemp.EmpBasic = model.EmpBasic;
              addemp.EmpHouse = model.EmpHouse;
              addemp.EmpMedical = model.EmpMedical;
              addemp.EmpConveyance = model.EmpConveyance;
              addemp.EmpFood = model.EmpFood;
              addemp.EmpPayMethodName = model.EmpPayMethodName;
              addemp.EmpBankName = model.EmpBankName;
              addemp.EmpMFSName = model.EmpMFSName;
             addemp.EmpBankBranch = model.EmpBankBranch;
             addemp.EmpBankAcc = model.EmpBankAcc;
              addemp.MFSnumber = model.MFSnumber;
            addemp.Created_At = DateTime.Now;
            //EmpProfileImg = model.EmpProfileImg;
            //EmpNIDImg = model.EmpNIDImg;
            //EmpBodImg = model.EmpBodImg;
            //EmpPassportImg = model.EmpPassportImg;
            //EmpContractImg = model.EmpContractImg;
            //EmpSigImg = model.EmpSigImg;





            if (!_context.Employeeinfo.Any(t => t.EmpSecrateCode.Equals(model.EmpSecrateCode)))
            {
                _context.Employeeinfo.Add(addemp);
                await _context.SaveChangesAsync();

            }
            return RedirectToAction("EmployeeList", "Employee", new { area = "Admin" });
        }
    }
}
