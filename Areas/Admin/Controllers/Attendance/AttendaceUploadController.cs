using KS_Payroll.Areas.Admin.Data;
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
using Microsoft.Extensions.Configuration;
using System.Configuration;
using Microsoft.Data.SqlClient;
using System.Globalization;

namespace KS_Payroll.Areas.Admin.Controllers.Attendance
{
    //AttendaceUpload
    [Area("Admin")]
    public class AttendaceUploadController : Controller
    {
        private readonly KS_PayrollContext _context;

        private readonly IWebHostEnvironment _henv;
        private readonly IConfiguration configuration;


        public AttendaceUploadController(KS_PayrollContext context, IWebHostEnvironment henv, IConfiguration configuration)
        {
            _context = context;
            _henv = henv;
            this.configuration = configuration;
        }
        public void upload2(IFormFile formfile)
        {
            try
            {
                var mainpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "UploadedAttendance");
                if (!Directory.Exists(mainpath))
                {
                    Directory.CreateDirectory(mainpath);
                }
                var filepath = Path.Combine(mainpath, formfile.FileName);
                using (FileStream stream = new FileStream(filepath, FileMode.Create))
                {
                    formfile.CopyTo(stream);
                }
            }
            catch (Exception ex)
            {
                var msg = ex;
            }

        }
        //frist upload the attendance file in server root folder
        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            try
            {
                if (file.Length > 0)
                {
                    string root = _henv.WebRootPath;
                    string dir = "UploadedAttendance";
                    string fname = Path.GetFileName(file.FileName);
                    string filename = Path.Combine(root, dir, fname);
                    using (var stream = new FileStream(filename, FileMode.Create))
                    {
                        //file.CopyTo(stream);
                        await file.CopyToAsync(stream);
                    }

                }

            }
            catch (Exception ex)
            {
                var msg = ex;
            }


            return RedirectToAction("AttenFileUpload");
        }
        public IActionResult AttenFileUpload()
        {

            return View();

        }


        public IActionResult TakeAttendance()
        {
            //Fetch all files in the Folder (Directory).
            string[] filePaths = Directory.GetFiles(Path.Combine(this._henv.WebRootPath, "UploadedAttendance/"));

            //Copy File names to Model collection.
            List<AttendanceRow> files = new List<AttendanceRow>();
            foreach (string filePath in filePaths)
            {
                files.Add(new AttendanceRow { FileName = Path.GetFileName(filePath) });
            }
            ViewBag.filesname = files;//get files name list 

            ViewBag.modulelist = _context.AttendanceModule.OrderBy(x => x.AMid).ToList();
            return View();
        }

        public IActionResult AttendanceProcess(AttendanceRow model)//filename, startDate, endDate
        {
            var info = _context.AttendanceModule.FirstOrDefault(x => x.AMid == model.Moduletye);//file type, time1, time2, code, date;

            if (info.FileType == "mdb")
            {
                MDB_attendence_process(model.FileName, model.StartDate, model.EndDate, info.TimeName, info.SecretCode, info.DateName, info.TableName);
            }
            if (info.FileType == "csv")
            {
                //CSV_attendence_process(model.FileName, model.StartDate, model.EndDate, info.TimeName, info.Time2Name, info.SecretCode, info.DateName);
                CSV_attendence_process(model.FileName, model.StartDate, model.EndDate, info.TimeName, info.Time2Name, info.SecretCode, info.DateName, info.TableName);
            }

            return RedirectToAction("TakeAttendance");
        }



        public int MDB_attendence_process(string filename, string startDate, string endDate, string time, string code, string date, string table)
        {
            // Connection string and SQL query    
            var mainpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "UploadedAttendance");
            var filepath = Path.Combine(mainpath, filename);//Provider=Microsoft.ACE.OLEDB.12.0
            string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filepath + "";
            //string connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=E:\HAMS_2022.mdb";
            //PubEvent
            string strSQL = "SELECT " + date + "," + time + "," + code + " FROM " + table + "";


            // Create a connection    
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                // Create a command and set its connection    
                OleDbCommand command = new OleDbCommand(strSQL, connection);
                // Open the connection and execute the select command.    
                try
                {
                    // Open connecton    
                    connection.Open();
                    // Execute command    
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {

                        //startDate = yyyy-mm-dd
                        int i = 0;
                        DateTime sDate = DateTime.Parse(startDate);
                        DateTime eDate = DateTime.Parse(endDate);
                        while (reader.Read())
                        {


                            DateTime filedate = DateTime.Parse(reader[date].ToString());


                            //string times = reader[time].ToString();
                            double filetime = double.Parse(reader[time].ToString().Replace(":", "")); //file time


                            if (filedate >= sDate && filedate <= eDate)
                            {
                                DateTime nowdate = filedate;
                                string empscode = reader[code].ToString();//emp secrt code
                                var emp = _context.Employeeinfo.FirstOrDefault(x => x.EmpSecrateCode == empscode && x.EmpStats== "REGULAR");
                                if (emp != null)
                                {
                                    var shift_time = _context.Shift.FirstOrDefault(s => s.ShiftId.ToString() == emp.EmpShift);
                                    if (shift_time != null)
                                    {

                                        string entry_time = shift_time.StartTime.ToString();
                                        string out_time = shift_time.EndTime.ToString();


                                        double entrytime = double.Parse(entry_time.Replace(":", "")) * 100; //80000
                                        double outtime = double.Parse(out_time.Replace(":", "")) * 100; //(155 for previos time 115 for next time) 


                                        var find_emp = _context.RowAttendance.FirstOrDefault(x => x.Date == filedate && x.empSCode == double.Parse(reader[code].ToString()));
                                        RowAttendance atten = new RowAttendance();

                                        if (find_emp == null && filetime >= (entrytime - 15500) && filetime <= (entrytime + 11500)) //if no emp foun in row attendance
                                        {

                                            //if 1

                                            atten.empSCode = double.Parse(reader[code].ToString());
                                            atten.Date = DateTime.Parse(reader[date].ToString());
                                            atten.Frist_In_Time = reader[time].ToString();

                                            _context.RowAttendance.Add(atten);
                                            _context.SaveChanges();


                                        }
                                        else if (find_emp == null && filetime < (entrytime - 15500))
                                        {

                                            var P_find_emp = _context.RowAttendance.FirstOrDefault(x => x.Date == DateTime.Parse(reader[date].ToString()).AddDays(-1) && x.empSCode == double.Parse(reader[code].ToString()));

                                            if (P_find_emp == null)
                                            {
                                                atten.empSCode = double.Parse(reader[code].ToString());
                                                atten.Date = DateTime.Parse(reader[date].ToString()).AddDays(-1);
                                                atten.Last_Out_Time = reader[time].ToString();
                                                _context.RowAttendance.Add(atten);
                                                _context.SaveChanges();
                                            }
                                            else
                                            {
                                                RowAttendance Data = _context.RowAttendance.Find(P_find_emp.SL);
                                                Data.Last_Out_Time = reader[time].ToString();
                                                _context.RowAttendance.Update(Data);
                                                _context.SaveChanges();
                                            }


                                        }
                                        else if (find_emp == null)// emp == null and time not match with entry time cond.
                                        {

                                            // else if 2
                                            atten.empSCode = double.Parse(reader[code].ToString());
                                            atten.Date = DateTime.Parse(reader[date].ToString());
                                            atten.Last_Out_Time = reader[time].ToString();

                                            _context.RowAttendance.Add(atten);
                                            _context.SaveChanges();

                                        }
                                        else if (find_emp != null && filetime >= (entrytime - 15500) && filetime <= (entrytime + 11500))
                                        {
                                            //else if 3
                                            if (find_emp.Frist_In_Time == null)
                                            {
                                                RowAttendance Data = _context.RowAttendance.Find(find_emp.SL);
                                                Data.Frist_In_Time = reader[time].ToString();
                                                _context.RowAttendance.Update(Data);
                                                _context.SaveChanges();//else if 4

                                            }
                                            if (find_emp.Frist_In_Time != null)
                                            {
                                                var ft = reader[time].ToString();
                                                var st = find_emp.Frist_In_Time.ToString();
                                                double ftt = double.Parse(ft.Replace(":", "")) * 100; //8:00:00
                                                double stt = double.Parse(st.Replace(":", "")) * 100; //8:10:00
                                                if (stt > ftt)
                                                {
                                                    RowAttendance Data = _context.RowAttendance.Find(find_emp.SL);
                                                    Data.Frist_In_Time = reader[time].ToString();
                                                    _context.RowAttendance.Update(Data);
                                                    _context.SaveChanges();//else if 5

                                                }
                                            }


                                        }
                                        else if (find_emp != null && find_emp.Last_Out_Time == null)
                                        {
                                            RowAttendance Data = _context.RowAttendance.Find(find_emp.SL);
                                            Data.Last_Out_Time = reader[time].ToString();
                                            _context.RowAttendance.Update(Data);
                                            _context.SaveChanges();//else if 6



                                        }
                                        else if (find_emp != null && find_emp.Last_Out_Time != null)
                                        {
                                            var ft = reader[time].ToString();
                                            var st = find_emp.Last_Out_Time.ToString();
                                            double ftt = double.Parse(ft.Replace(":", "")) * 100; //8:00:00
                                            double stt = double.Parse(st.Replace(":", "")) * 100; //8:10:00
                                            if (stt < ftt)
                                            {//else if 7
                                                RowAttendance Data = _context.RowAttendance.Find(find_emp.SL);
                                                Data.Last_Out_Time = reader[time].ToString();
                                                _context.RowAttendance.Update(Data);
                                                _context.SaveChanges();//else if 4

                                            }
                                        }


                                    }
                                }
                                //0800 => 0645 = 


                            }


                        }
                        int asdf = i;

                    }


                    //_context.SaveChanges();


                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                // The connection is automatically closed becasuse of using block.    
            }
            AttendanceFinalize(startDate, endDate);
            return 0;
        }


        public int CSV_attendence_process(string filename, string startDate, string endDate, string timeIN, string timeOut, string code, string date, string table)
        {
            // Connection string and SQL query    
            var mainpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "UploadedAttendance");
            var filepath = Path.Combine(mainpath, filename);//Provider=Microsoft.ACE.OLEDB.12.0
            string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filepath + "";
            //string connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=E:\HAMS_2022.mdb";
            //PubEvent
            string strSQL = "SELECT " + date + "," + timeIN + "," + timeOut + "," + code + " FROM " + table + "";

            // Create a connection    
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                // Create a command and set its connection    
                OleDbCommand command = new OleDbCommand(strSQL, connection);
                // Open the connection and execute the select command.    
                try
                {
                    // Open connecton    
                    connection.Open();
                    // Execute command    
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {

                        //startDate = yyyy-mm-dd
                        
                        DateTime sDate = DateTime.Parse(startDate);
                        DateTime eDate = DateTime.Parse(endDate);
                        while (reader.Read())
                        {
                            // DateTime filedate = DateTime.Parse(reader[date].ToString());

                            DateTime filedate = DateTime.ParseExact(reader[date].ToString(), "MM/dd/yyyy", CultureInfo.InvariantCulture);

                            if (filedate >= sDate && filedate <= eDate)
                            {
                                double tin = double.Parse((DateTime.Parse(reader[timeIN].ToString()).ToString("HH:mm")).Replace(":", ""));
                                double tout = double.Parse((DateTime.Parse(reader[timeOut].ToString()).ToString("HH:mm")).Replace(":", ""));

                                if (tin != 0 || tout != 0)
                                {
                                    var emp = _context.Employeeinfo.FirstOrDefault(x => x.EmpSecrateCode == reader[code].ToString() &&  x.EmpStats== "REGULAR");
                                    if (emp != null)
                                    {
                                        var shift_time = _context.Shift.FirstOrDefault(s => s.ShiftId.ToString() == emp.EmpShift);
                                        if (shift_time != null)
                                        {
                                            string entry_time = shift_time.StartTime.ToString();
                                            string out_time = shift_time.EndTime.ToString();


                                            double entrytime = double.Parse(entry_time.Replace(":", "")); //800
                                            double outtime = double.Parse(out_time.Replace(":", "")); //(155 for previos time 115 for next time) 


                                            var find_emp = _context.RowAttendance.FirstOrDefault(x => x.Date == filedate && x.empSCode == double.Parse(reader[code].ToString()));
                                            RowAttendance atten = new RowAttendance();



                                            if (find_emp == null && tin >= (entrytime - 155) && tin <= (entrytime + 115)) //if no emp foun in row attendance
                                            {

                                                //if 1

                                                atten.empSCode = double.Parse(reader[code].ToString());

                                                //12/24/2022
                                                atten.Date = DateTime.ParseExact(reader[date].ToString(), "MM/dd/yyyy", CultureInfo.InvariantCulture);
                                                atten.Frist_In_Time = DateTime.Parse(reader[timeIN].ToString()).ToString("HH:mm:ss");
                                                _context.RowAttendance.Add(atten);
                                                _context.SaveChanges();


                                            }
                                            else if (find_emp != null && tin >= (entrytime - 155) && tin <= (entrytime + 115))
                                            {
                                                //else if 3
                                                if (find_emp.Frist_In_Time == null || find_emp.Frist_In_Time == "00:00:00")
                                                {
                                                    RowAttendance Data = _context.RowAttendance.Find(find_emp.SL);
                                                    Data.Frist_In_Time = reader[timeIN].ToString();
                                                    _context.RowAttendance.Update(Data);
                                                    _context.SaveChanges();//else if 4

                                                }
                                                if (find_emp.Frist_In_Time != null || find_emp.Frist_In_Time != "00:00:00")
                                                {
                                                    var ft = DateTime.Parse(reader[timeIN].ToString()).ToString("HH:mm:ss");
                                                    var st = find_emp.Frist_In_Time.ToString();
                                                    double ftt = double.Parse(ft.Replace(":", "")); //8:00:00
                                                    double stt = double.Parse(st.Replace(":", "")); //8:10:00
                                                    if (stt > ftt)
                                                    {
                                                        RowAttendance Data = _context.RowAttendance.Find(find_emp.SL);
                                                        Data.Frist_In_Time = DateTime.Parse(reader[timeIN].ToString()).ToString("HH:mm:ss");
                                                        _context.RowAttendance.Update(Data);
                                                        _context.SaveChanges();//else if 5

                                                    }
                                                }


                                            }
                                            else if (find_emp == null && tin < (entrytime - 155))
                                            {//not checked
                                                DateTime tempdate = DateTime.ParseExact(reader[date].ToString(), "MM/dd/yyyy", CultureInfo.InvariantCulture).AddDays(-1);
                                                var P_find_emp = _context.RowAttendance.FirstOrDefault(x => x.Date == tempdate && x.empSCode == double.Parse(reader[code].ToString()));

                                                if (P_find_emp == null)
                                                {
                                                    atten.empSCode = double.Parse(reader[code].ToString());

                                                    //atten.Date = DateTime.Parse(filedate.ToString()).AddDays(-1);
                                                    atten.Date = tempdate;
                                                    atten.Last_Out_Time = DateTime.Parse(reader[timeIN].ToString()).ToString("HH:mm:ss");
                                                    _context.RowAttendance.Add(atten);
                                                    _context.SaveChanges();
                                                }
                                                else
                                                {
                                                    RowAttendance Data = _context.RowAttendance.Find(P_find_emp.SL);
                                                    Data.Last_Out_Time = DateTime.Parse(reader[timeIN].ToString()).ToString("HH:mm:ss");
                                                    _context.RowAttendance.Update(Data);
                                                    _context.SaveChanges();
                                                }

                                            }

                                            if (find_emp != null && find_emp.Last_Out_Time == null)
                                            {
                                                RowAttendance Data = _context.RowAttendance.Find(find_emp.SL);
                                                Data.Last_Out_Time = DateTime.Parse(reader[timeOut].ToString()).ToString("HH:mm:ss");
                                                _context.RowAttendance.Update(Data);
                                                _context.SaveChanges();//else if 6



                                            }
                                            else if (find_emp != null && find_emp.Last_Out_Time != null)
                                            {
                                                var ft = DateTime.Parse(reader[timeOut].ToString()).ToString("HH:mm:ss");
                                                var st = find_emp.Last_Out_Time.ToString();
                                                double ftt = double.Parse(ft.Replace(":", "")); //8:00:00
                                                double stt = double.Parse(st.Replace(":", "")); //8:10:00
                                                if (stt < ftt)
                                                {//else if 7
                                                    RowAttendance Data = _context.RowAttendance.Find(find_emp.SL);
                                                    Data.Last_Out_Time = DateTime.Parse(reader[timeOut].ToString()).ToString("HH:mm:ss");
                                                    _context.RowAttendance.Update(Data);
                                                    _context.SaveChanges();//else if 4

                                                }
                                            }

                                        }
                                    }
                                }

                            }






                        }

                    }
                }

                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                // The connection is automatically closed becasuse of using block.    
            }
            AttendanceFinalize(startDate, endDate);
            return 0;
        }


        public int CSV_OLD_attendence_process(string filename, string startDate, string endDate, string timeIN, string timeOUT, string code, string date)
        { //use 4.7.1 sql nuget
            try
            {

                var mainpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "UploadedAttendance");
                var filepath = Path.Combine(mainpath, filename);

                var fileName = filename;
                string extesion = Path.GetExtension(fileName);
                string conString = string.Empty;
                switch (extesion)
                {
                    case ".xls":
                        conString = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + filepath + ";Extended Properties='Excel 8.0; HDR=YES'";
                        break;
                    case ".xlsx":

                        conString = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + filepath + ";Extended Properties='Excel 8.0; HDR=YES'";

                        break;
                    case ".csv":

                        conString = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + filepath + ";Extended Properties='Excel 8.0; HDR=YES'";

                        break;
                }

                DataTable dt = new DataTable();
                conString = String.Format(conString, filepath);
                using (OleDbConnection connExcel = new OleDbConnection(conString))
                {
                    using (OleDbCommand cmdExcel = new OleDbCommand())
                    {
                        using (OleDbDataAdapter odaExcel = new OleDbDataAdapter())
                        {
                            cmdExcel.Connection = connExcel;
                            connExcel.Open();
                            DataTable dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                            string sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                            cmdExcel.CommandText = "SELECT * FROM [" + sheetName + "]";

                            odaExcel.SelectCommand = cmdExcel;

                            odaExcel.Fill(dt);
                            connExcel.Close();

                        }
                    }
                }
                int i = 0;
                conString = configuration.GetConnectionString("KS_PayrollContext");
                using (SqlConnection con = new SqlConnection(conString))
                {
                    using (SqlBulkCopy SqlBulkCopy = new SqlBulkCopy(con))
                    {


                        SqlBulkCopy.DestinationTableName = "RowAttendance";
                        SqlBulkCopy.ColumnMappings.Add(code, "empSCode");
                        SqlBulkCopy.ColumnMappings.Add(date, "Date");
                        SqlBulkCopy.ColumnMappings.Add(timeIN, "Frist_In_Time");//file, db model
                        SqlBulkCopy.ColumnMappings.Add(timeOUT, "Last_Out_Time");
                        i++;
                        con.Open();

                        SqlBulkCopy.WriteToServer(dt);
                        con.Close();

                        DateTime d1 = DateTime.Parse("1899-12-30 00:00:00.0000000");
                        DateTime d2 = DateTime.Parse("1899-12-30 00:00:00.0000000");


                    }
                }

                return i;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
            return 0;
        }

    
    
        public void AttendanceFinalize(string startDate, string endDate)
        {
            DateTime sdate = DateTime.Parse(startDate);
            DateTime edate = DateTime.Parse(endDate);
            var rowAttendance = _context.RowAttendance.Where(x => x.Date >= sdate && x.Date <= edate).ToList();

            
            int i = 0;
            foreach(var item in rowAttendance)
            {
                var sa = _context.FinalAttendance.FirstOrDefault(x => x.Date == item.Date && x.EmpSCode == item.empSCode.ToString());
                if (sa == null)
                {//if date is not present in final attendance table
                    var emp = _context.Employeeinfo.FirstOrDefault(x => x.EmpSecrateCode == item.empSCode.ToString());
                    if (emp != null)
                    {
                        var checkDesig = _context.Designation.FirstOrDefault(x => x.DesigID.ToString() == emp.EmpDesignationTitle);
                        var takeTime = _context.Shift.FirstOrDefault(x => x.ShiftId == int.Parse(emp.EmpShift));
                        if (takeTime != null)
                        {
                            FinalAttendance final = new FinalAttendance();
                            TimeSpan shiftStart = TimeSpan.Parse(takeTime.StartTime);
                            TimeSpan shiftEnd = TimeSpan.Parse(takeTime.EndTime);
                            TimeSpan shiftBreakS = TimeSpan.Parse(takeTime.BreakStartTime).Add(new TimeSpan(0, 0, 46, 0));
                            var checkHoliday = _context.Calendar.FirstOrDefault(x => x.FDate == item.Date && x.Eventstats == 0);
                            if(checkHoliday == null)
                            {
                                if (item.Frist_In_Time != null)
                                {
                                    TimeSpan rowInTime = TimeSpan.Parse(item.Frist_In_Time);
                                    if (rowInTime > shiftStart)//8:10 8:00 
                                    {
                                        final.Late = rowInTime - shiftStart;
                                    }
                                }
                                if (item.Last_Out_Time != null)
                                {
                                    TimeSpan rowOutTime = TimeSpan.Parse(item.Last_Out_Time);
                                    if (shiftEnd > rowOutTime)//5:00 4:30
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
                                        TimeSpan breakeTimeS = new TimeSpan(0, 46, 0);
                                        if (rowOutTime <= breakeTimeS)
                                        {
                                            final.OverTime = final.OverTime - 1;//=====
                                        }
                                        else
                                        {
                                            final.OverTime = final.OverTime;
                                        }

                                        

                                    }
                                }
                            }
                            else ///holiday found
                            {
                                if (item.Frist_In_Time!= null)
                                {
                                    TimeSpan rowInTime = TimeSpan.Parse(item.Frist_In_Time);
                                    if (rowInTime > shiftStart)//8:10 8:00 
                                    {
                                        final.Late = rowInTime - shiftStart;
                                    }
                                }//late count


                                if (item.Last_Out_Time != null)
                                {
                                    TimeSpan rowOutTime = TimeSpan.Parse(item.Last_Out_Time);
                                    TimeSpan duityHour = rowOutTime - shiftStart;

                                    if (shiftStart.Add(new TimeSpan(0, 45, 59)) < rowOutTime && rowOutTime < shiftBreakS)//before shift end time
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
                                    else if (shiftBreakS.Add(new TimeSpan(0, 45, 59)) < rowOutTime && rowOutTime <= shiftEnd)//before shift end time
                                    {
                                        final.EarlyOut = shiftEnd - rowOutTime; //this is early out cal
                                        if (duityHour.Minutes >= 46)
                                        {
                                            final.OverTime = ((duityHour.Hours) + 1) - 1;
                                        }
                                        else
                                        {
                                            final.OverTime = duityHour.Hours - 1;
                                        }
                                    }
                                    else if (rowOutTime > shiftEnd && rowOutTime > shiftStart)
                                    {
                                        final.EarlyOut = new TimeSpan(0, 0, 0, 0); //this is over time cal after shift end
                                        if (duityHour.Minutes >= 46)
                                        {
                                            final.OverTime = ((duityHour.Hours) + 1) - 1;
                                        }
                                        else
                                        {
                                            final.OverTime = duityHour.Hours - 1;
                                        }
                                    }


                                    else if (rowOutTime < shiftEnd && rowOutTime < shiftStart.Add(new TimeSpan(-1, -15, 0)))//------
                                    {
                                        int finalOT = 0;
                                        int tem1 = ((240000 - Int32.Parse(shiftStart.ToString().Replace(":", ""))) / 10000) - 1;
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

                            if (checkDesig.OverTime == false)
                            {
                                final.OverTime = 0;
                            }

                            final.InTime = item.Frist_In_Time;
                            final.OutTime = item.Last_Out_Time;
                            final.EmpSCode = emp.EmpSecrateCode;
                            final.empid = emp.empid;
                            final.Date = item.Date;
                            final.AttenStatus = "P";
                            _context.Add(final);
                            _context.SaveChanges();


                        }
                    }
                }
                else
                {//sa!=null
                    var emp = _context.Employeeinfo.FirstOrDefault(x => x.EmpSecrateCode == item.empSCode.ToString());
                    if (emp != null)
                    {
                        var takeTime = _context.Shift.FirstOrDefault(x => x.ShiftId == int.Parse(emp.EmpShift));
                        var checkDesig1 = _context.Designation.FirstOrDefault(x => x.DesigID.ToString() == emp.EmpDesignationTitle);
                        if (takeTime != null)
                        {
                            FinalAttendance final = _context.FinalAttendance.Find(sa.sl);
                            TimeSpan shiftStart = TimeSpan.Parse(takeTime.StartTime);
                            TimeSpan shiftEnd = TimeSpan.Parse(takeTime.EndTime);
                            TimeSpan shiftBreakS = TimeSpan.Parse(takeTime.BreakStartTime).Add(new TimeSpan(0, 0, 46, 0));
                          
                            var checkHoliday = _context.Calendar.FirstOrDefault(x => x.FDate == item.Date && x.Eventstats == 0);
                            if(checkHoliday ==null)
                            {
                                if (item.Frist_In_Time != null)
                                {
                                    TimeSpan rowInTime = TimeSpan.Parse(item.Frist_In_Time);
                                    if (rowInTime > shiftStart)//8:10 8:00 
                                    {
                                        final.Late = rowInTime - shiftStart;
                                    }
                                }
                                if (item.Last_Out_Time != null)
                                {
                                    TimeSpan rowOutTime = TimeSpan.Parse(sa.OutTime);
                                    if (shiftEnd > rowOutTime && rowOutTime > shiftStart)//5:00 4:30
                                    {
                                        final.EarlyOut = shiftEnd - rowOutTime;
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
                                    else if (rowOutTime < shiftEnd)
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
                            }
                            else ///holiday !=null
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

                                    if (shiftStart.Add(new TimeSpan(0, 45, 59)) < rowOutTime && rowOutTime < shiftBreakS)//before shift end time
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
                                    else if (shiftBreakS.Add(new TimeSpan(0, 45, 59)) < rowOutTime && rowOutTime <= shiftEnd)//before shift end time
                                    {
                                        final.EarlyOut = shiftEnd - rowOutTime; //this is early out cal
                                        if (duityHour.Minutes >= 46)
                                        {
                                            final.OverTime = ((duityHour.Hours) + 1) - 1;
                                        }
                                        else
                                        {
                                            final.OverTime = duityHour.Hours - 1;
                                        }
                                    }
                                    else if (rowOutTime > shiftEnd && rowOutTime > shiftStart)
                                    {
                                        final.EarlyOut = new TimeSpan(0, 0, 0, 0); //this is over time cal after shift end
                                        if (duityHour.Minutes >= 46)
                                        {
                                            final.OverTime = ((duityHour.Hours) + 1) - 1;
                                        }
                                        else
                                        {
                                            final.OverTime = duityHour.Hours - 1;
                                        }
                                    }


                                    else if (rowOutTime < shiftEnd && rowOutTime < shiftStart.Add(new TimeSpan(-1, -15, 0)))//------
                                    {
                                        int finalOT = 0;
                                        int tem1 = ((240000 - Int32.Parse(shiftStart.ToString().Replace(":", ""))) / 10000) - 1;
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

                            final.InTime = item.Frist_In_Time;
                            final.OutTime = item.Last_Out_Time;
                            final.EmpSCode = emp.EmpSecrateCode;
                            final.empid = emp.empid;
                            final.Date = item.Date;
                            final.AttenStatus = "P";

                            _context.FinalAttendance.Update(final);
                            _context.SaveChanges();


                        }
                    }
                }
                    

                
            }
            
            var asdf = i;
        }
    
    }//main
}
