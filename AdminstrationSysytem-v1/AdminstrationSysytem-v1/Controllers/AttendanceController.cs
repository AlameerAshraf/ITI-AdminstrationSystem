using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdminstrationSysytem_v1.Models;

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
            return View();
        }




        [Authorize(Roles ="Admin")]
        [HttpGet]
        public ActionResult Report()
        {
            AttendanceReporter(DateTime.Now);
            return View();
        }


        public void AttendanceReporter (DateTime dateOfDay)
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
                        int punshment = db.AttendanceRules.Where(m => m.RuleCase == "2-5").Select(e => e.PunshimentNumber).SingleOrDefault();
                        targetStudent.GradeOfAbsence = degrees - punshment;
                        db.Entry(targetStudent).State = System.Data.Entity.EntityState.Modified;
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

        }
    }
}