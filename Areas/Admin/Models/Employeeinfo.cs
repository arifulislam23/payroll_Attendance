using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KS_Payroll.Areas.Admin.Data
{
    public class Employeeinfo
    {
        [Key]
        public int empid { get; set; }
        public string EmpStats { get; set; }
        public string EmpName { get; set; }
        public string EmpFName { get; set; }
        public string EmpMName { get; set; }
        public string EmpSpouseName { get; set; }
        public string EmpPhone { get; set; }
        public string EmpEmgPerson { get; set; }
        public string EmpEmgphone { get; set; }
        public DateTime EmpDOB { get; set; }
        public string EmpNID { get; set; }
        public string EmpBODCertificate { get; set; }
        public string EmpPassport { get; set; }
        public string EmpGender { get; set; }
        public string EmpMarriedStatus { get; set; }
        public string EmpChildNumber { get; set; }
        public string EmpNationality { get; set; }
        public string EmpReligion { get; set; }
        public string EmpBlood { get; set; }
        public string EmpNomineeName { get; set; }
        public string EmpNomineePhn { get; set; }
        public string EmpNomineeRlt { get; set; }
        public string EmpRefName { get; set; }
        public int EmpRefNumber { get; set; }
        public int EmpHeight { get; set; }
        public int EmpWeight { get; set; }
        public string EmpParmanentDistrict { get; set; }
        public string EmpParmanentZip { get; set; }
        public string EmpParmanentArea { get; set; }
        public string EmpParmanentAddress { get; set; }
        public string EmpPresentDistrict { get; set; }
        public string EmpPresentZip { get; set; }
        public string EmpPresentArea { get; set; }
        public string EmpPresentAddress { get; set; }
        public string EmpEduTitle { get; set; }
        public int EmpEduPassY { get; set; }
        public string EmpEduStatus { get; set; }
        public string EmpEduPassG { get; set; }
        
        //End personal information part

        //start offical information part
        public string EmpCompany { get; set; }
        public string EmpIDCard { get; set; }
        public string EmpSecrateCode { get; set; }
        public string EmpType { get; set; }
        public string EmpDepartment { get; set; }
        public string EmpSection { get; set; }
        public string EmpFloor { get; set; }
        public string EmpLine { get; set; }
        public DateTime EmpJoiningDate { get; set; }
        public string EmpShift { get; set; }
        public string EmpDesignationTitle { get; set; }
        public string EmpDesignationType { get; set; }
        public string EmpGrade { get; set; }
        public int EmpAttenBonus { get; set; }
        public bool EmpOverTime { get; set; }
        public int EmpHolidayAlw { get; set; }
        public int EmpNightBill { get; set; }
        public int EmpLunchBilll { get; set; }
        public int EmpTiffinBill { get; set; }
        public int EmpIftarBill { get; set; }
        //end offical information

        //start salery information
        public int EmpGross { get; set; }
        public int EmpBasic { get; set; }
        public int EmpHouse { get; set; }
        public int EmpMedical { get; set; }
        public int EmpConveyance { get; set; }
        public int EmpFood { get; set; }

        //Payment Information
        public string EmpPayMethodName { get; set; }
        public string EmpBankName { get; set; }
        public string EmpMFSName { get; set; }
        public string EmpBankBranch { get; set; }
        public string EmpBankAcc { get; set; }
        public string MFSnumber { get; set; }

        //Documents Image version
        public byte[] EmpPrfImg { get; set; }
        public byte[] EmpNIDImg { get; set; }
        public byte[] EmpBodImg { get; set; }
        public byte[] EmpPassportImg { get; set; }
        public byte[] EmpContractImg { get; set; }
        public byte[] EmpSigImg { get; set; }

        //for super admin
        public int UserId { get; set; }
        public int ModifiedId { get; set; }
        public int? Created_By { get; set; }
        public int? Updated_By { get; set; }
        public DateTime? Created_At { get; set; }
        public DateTime? Updated_At { get; set; }
    }
}
