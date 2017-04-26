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
                        StudentData = new Attendance() { ArrivalTime = hourse, StudentId = std.Id, Date = date, IsPermitted = false };
                        db.Attendance.Add(StudentData);
                    }
                }

            }

            db.SaveChanges();
            return View();
        }



        public void AttendanceReporter (DateTime DateOfDay)
        {
            //The day to calc the attendance 
            DateTime TodayAttendingSheet = DateOfDay.Date;
            //Ids of doesn't come students 
            var DoesntCome = db.Attendance.Where(m => m.IsAttended == false).Select(e => e.StudentId).ToList();


            foreach (string item in DoesntCome)
            {
                bool IsTheyPermitted = db.Attendance.Where(m => m.StudentId == item).Select(e => e.IsPermitted).SingleOrDefault();
                if (IsTheyPermitted == true)
                {
                    int? NumberOfPermissions = db.Students.Where(m => m.Id == item).Select(e => e.NoOfPermissions).SingleOrDefault();
                    if (NumberOfPermissions == 0)
                    {
                        var TargetStudent = db.Students.Where(m => m.Id == item).SingleOrDefault();
                        TargetStudent.NoOfPermissions = 1; 
                        db.Entry(TargetStudent).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                    else
                    {

                    }
                }
            }

        }
    }
}