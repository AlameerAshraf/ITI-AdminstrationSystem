using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdminstrationSysytem_v1.Models;
using System.Net;
using System.Net.Mail;

namespace AdminstrationSysytem_v1.Controllers
{
    public class AttendanceController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View();
        }



        [Authorize(Roles ="Admin")]
        [HttpGet]
        public ActionResult AttendanceReception()
        {
            var departments = db.Departments.ToList();
            ViewBag.Deps = new SelectList(departments, "DepartmentId", "Name"); 
            var students = db.Students.ToList();
            return PartialView(students);
        }


        [Authorize(Roles ="Admin")]
        [HttpGet]
        public ActionResult GetStudents(int id)
        {
            var StudentInDepartments = db.Students.Where(dep => dep.DepartmentId == id).ToList();
            return PartialView("attList",StudentInDepartments);
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Attendance()
        {
            var TargetId = int.Parse(Request.Form["DeptId"]);
            var Students = db.Students.Where(M => M.DepartmentId == TargetId).ToList();
            var StudentData  = new Attendance();
            var date = DateTime.Now.Date;
            var hourse = DateTime.Now.TimeOfDay;
            var Adb = db.Attendance.ToList();


            foreach (var std in Students)
            {
                if (std.Name != null)
                {
                    if (std.Name == Request.Form[std.Name])
                    {
                        StudentData = new Attendance() { ArrivalTime = hourse, StudentId = std.Id, Date = date, IsPermitted = false , IsAttended = true};
                        db.Attendance.Add(StudentData);
                    }
                    else
                    {
                        StudentData = new Attendance() { ArrivalTime = hourse, StudentId = std.Id, Date = date, IsPermitted = false , IsAttended = false};
                        db.Attendance.Add(StudentData);
                    }
                }
                

            }

            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }




        [Authorize(Roles ="Admin")]
        [HttpGet]
        public ActionResult Report()
        {
            List<string> AbsenceStudents = new List<string>();
            var stds = new List<Student>();
            var d = DateTime.Now.Date;
            AbsenceStudents = db.Attendance.Where(m => m.IsAttended == false).Where(s => s.Date == d).Select(e => e.StudentId).ToList();

            foreach (var item in AbsenceStudents)
            {
                var std = db.Students.Where(m => m.Id == item).SingleOrDefault();
                stds.Add(std);
            }

            return View(stds);
        }

        [Authorize(Roles ="Admin")]
        [HttpGet]
        public ActionResult Claculatebsence()
        {
            AttendanceReporter(DateTime.Now);
            return Content("Hey,Admin this action should be calles automatically by the system Quartz Library");
        }

        [Authorize(Roles ="Admin")]
        [HttpGet]
        public ActionResult SendEmailToAbsence(string mail)
        {
            var message = "We Hope You're good " + "" + "You have counted absente today , without permissions";
            var Subject = "Absence Report - ITI" + DateTime.Now.ToString("dd/MM/yyyy");
            SmtpClient client = new SmtpClient()
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("AlameersDevelopers@gmail.com", "mvcsmtp2017")
            };
            MailMessage m = new MailMessage("AlameersDevelopers@gmail.com", mail)
            {
                Body = message,
                Subject = Subject
            };
            client.Send(m);
            return RedirectToAction("Index","Home");
        }


        [Authorize(Roles ="Admin")]
        [HttpGet]
        public ActionResult ReportOfAttendance()
        {
            List<string> AbsenceStudents = new List<string>();
            var stds = new List<Student>();
            var d = DateTime.Now.Date;
            AbsenceStudents = db.Attendance.Where(m => m.IsAttended == true).Where(s => s.Date == d).Select(e => e.StudentId).ToList();

            foreach (var item in AbsenceStudents)
            {
                var std = db.Students.Where(m => m.Id == item).SingleOrDefault();
                stds.Add(std);
            }
            return View(stds);
        }



        [Authorize(Roles ="Admin")]
        [HttpGet]
        public ActionResult Reportattendanceinperiod()
        {
            return View();
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Reportattendanceinperiod(string from , string to)
        {
            List<AttendanceModel> repos;

            DateTime start = Convert.ToDateTime(from);
            DateTime end = Convert.ToDateTime(to);
            
            repos = (from p in db.Attendance
                       where p.Date >= start.Date && p.Date <= end.Date && p.IsAttended == true
                       join s in db.Students on p.StudentId equals s.Id
                       select new AttendanceModel() { name = s.Name, GradeOfAbsence = s.GradeOfAbsence, NoOfPermissions = s.NoOfPermissions, Email = s.Email , Date = p.Date }).ToList();
        
            
            return PartialView("AttendanceStudentsList", repos);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult Reportabsenceinperiod()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Reportabsenceinperiod(string from, string to)
        {
            List<AttendanceModel> repos;

            DateTime start = Convert.ToDateTime(from);
            DateTime end = Convert.ToDateTime(to);

            repos = (from p in db.Attendance
                     where p.Date >= start.Date && p.Date <= end.Date && p.IsAttended == false
                     join s in db.Students on p.StudentId equals s.Id
                     select new AttendanceModel() { name = s.Name, GradeOfAbsence = s.GradeOfAbsence, NoOfPermissions = s.NoOfPermissions, Email = s.Email , Date = p.Date }).ToList();


            return PartialView("AbsenceStudentsList", repos);
        }





        public List<string> AttendanceReporter (DateTime dateOfDay)
        {
            //The day to calc the attendance 
            DateTime todayAttendingSheet = dateOfDay.Date;
            //Ids of doesn't come students 
            var doesntCome = db.Attendance.Where(m => m.IsAttended == false).Where(s=>s.Date == todayAttendingSheet).Select(e => e.StudentId).ToList();


            foreach (string item in doesntCome)
            {
                var targetStudent = db.Students.SingleOrDefault(m => m.Id == item);
                bool isTheyPermitted = db.Attendance.Where(m => m.StudentId == item)
                         .Where(m=>m.Date == todayAttendingSheet)
                         .Select(e => e.IsPermitted).SingleOrDefault();

                if (isTheyPermitted)
                {
                    targetStudent.NoOfPermissions += 1;
                    db.Entry(targetStudent).State = System.Data.Entity.EntityState.Modified;
                }
                else
                {
                    int? absenceDays = targetStudent.NoOfAbsenceDay;
                    int? degrees = targetStudent.GradeOfAbsence;
                    //1 - no , 2-5 - 5 , 6-9 - 10 , 10 - # - 25 ;
                    if (absenceDays == 0 || absenceDays == null)
                    {
                        if (absenceDays == null)
                        {
                            targetStudent.NoOfAbsenceDay = 1;
                        }
                        else
                        {
                            targetStudent.NoOfAbsenceDay = absenceDays + 1;
                        }
                        db.Entry(targetStudent).State = System.Data.Entity.EntityState.Modified;
                    }
                    else if (absenceDays <= 2 && absenceDays <= 5)
                    {
                        if (absenceDays != 1)
                        {
                            int punshment = db.AttendanceRules.Where(m => m.RuleCase == "2-5").Select(e => e.PunshimentNumber).SingleOrDefault();
                            targetStudent.GradeOfAbsence = degrees - punshment;
                            db.Entry(targetStudent).State = System.Data.Entity.EntityState.Modified;
                        }
                    }
                    else if (absenceDays >= 6 && absenceDays <= 9)
                    {
                        int punshment = db.AttendanceRules.Where(m => m.RuleCase == "6-9").Select(e => e.PunshimentNumber).SingleOrDefault();
                        targetStudent.GradeOfAbsence = degrees - punshment;
                        db.Entry(targetStudent).State = System.Data.Entity.EntityState.Modified;
                    }
                    else if (absenceDays >= 10)
                    {
                        int punshment = db.AttendanceRules.Where(m => m.RuleCase == "10-#").Select(e => e.PunshimentNumber).SingleOrDefault();
                        targetStudent.GradeOfAbsence = degrees - punshment;
                        db.Entry(targetStudent).State = System.Data.Entity.EntityState.Modified;
                    }
                    db.SaveChanges();
                }
            }


            return doesntCome;
        }
    }
}