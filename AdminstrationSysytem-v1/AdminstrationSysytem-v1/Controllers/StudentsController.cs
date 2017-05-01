using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdminstrationSysytem_v1.Models;
using System.Data.Entity;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Excel;
using System.IO;
using System.Data;


namespace AdminstrationSysytem_v1.Controllers
{
    public class StudentsController : Controller
    {

        static ApplicationDbContext db = new ApplicationDbContext();
        UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));


        // GET: Students
        [Authorize(Roles = "Admin")]

        public ActionResult StudentsList()
        {
            var Students = db.Students.ToList();
            return PartialView(Students);
            //return View(Students);
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult ActivateAccounts()
        {
            var Students = db.Students.ToList();

            foreach (var item in Students)
            {
                if (item.Name == Request.Form[item.Name])
                {
                    if (item.IsActivated == false)
                    {
                        item.IsActivated = true;
                        db.Entry(item).State = EntityState.Modified;
                    }
                }
                else
                {
                    item.IsActivated = false;
                    db.Entry(item).State = EntityState.Modified;
                }
            }

            db.SaveChanges();

            return PartialView("List",Students); 
        }

        [Authorize(Roles = "Student")]
        public ActionResult UserProfile()
        {

            return View();
        }



        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult UpdateStudentData(string id)
        {
            Student student = db.Students.Find(id);
            //return Content(id);
            return PartialView("UpdateStudentData",student);
        }


        [Authorize(Roles ="Admin")]
        [HttpGet]
        public ActionResult CraeteStudentData()
        {
            var departments = db.Departments.ToList();
            ViewBag.Deps = new SelectList(departments, "DepartmentId", "Name");
            return PartialView();
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> CraeteStudentData(JoinViewModel model , int department)
        {
            var Students = db.Students.ToList();
            if (ModelState.IsValid)
            {
                var user = new Student { Name = model.Name, UserName = model.Name, Email = model.Email, Address = model.Address, BD = model.BirthDate, UserAccessType = "Student" , IsActivated = true , GradeOfAbsence = 600 , NoOfPermissions = 0 , NoOfAbsenceDay = 0 };
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {

                    string code = await userManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    await userManager.SendEmailAsync(user.Id, "Student , Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");
                    var RoleAssigner = userManager.AddToRole(user.Id, "Student");

                    TempData["Student"] = user;
                    return RedirectToAction("StudentsList");
                }
                AddErrors(result);

            }
            return PartialView("StudentsList");
        }



        [Authorize(Roles ="Admin")]
        [HttpGet]
        public ActionResult DeleteStudent()
        {
            return PartialView();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult DeleteStudent()
        {
            return PartialView();
        }


        //[Authorize(Roles = "Student")]
        [HttpGet]
        public ActionResult Evalute()
        {
            var StudentDepartmentId = (TempData["Student"] as Student).DepartmentId;
            var StudentDepartment = (TempData["Student"] as Student).Id;

            var chk = db.InstCrsStudent.Where(e => e.StudentId == StudentDepartment).Any();
            if (!chk) 
            {
                var CoursesInDepartment = db.InstCrsDep
                    .Include(g => g.Instructor)
                    .Include(f => f.Department)
                    .Include(u => u.Course)
                    .Where(m => m.DepartmentId == StudentDepartmentId).ToList();
                //return Content(CoursesInDepartment[0].Instructor.Name + CoursesInDepartment[0].Department.Name);
                return View(CoursesInDepartment);
            }
            else
                return RedirectToAction("Index", "Home");
        }




        [Authorize(Roles = "Student")]
        [HttpPost]
        public void EvaluteInstructors(List<EvaluationObject> obj)
        {
            var StudentDepartmentId = (TempData["Student"] as Student).Id;
            var Eval = new Course_Student_Instructor(); 

            foreach (var item in obj)
            {
                Eval = new Course_Student_Instructor()
                {
                    CoursId = item.CourseID
                    ,
                    InstructorId = item.InstructorID,
                    StudentId = StudentDepartmentId,
                    InstructorEvaluation = item.EvaluationDegree
                };

                db.InstCrsStudent.Add(Eval);

            }
            db.SaveChanges();
        }





        [Authorize(Roles ="Admin")]
        [HttpGet]
        public ActionResult SubmitToDepartment()
        {
            var departments = db.Departments.ToList();
            ViewBag.Deps = new SelectList(departments, "DepartmentId", "Name");
            return View();
        }


        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult GetStudents(int id)
        {
            var StudentInDepartments = db.Students
                       .Where(e=>e.DepartmentId == id || e.DepartmentId == null )
                       .ToList();

            return PartialView("GetStudents", StudentInDepartments);
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult ToggleStudentDepartmentState()
        {

            var depid = int.Parse(Request.Form["DeptId"]);

            var StudentInDepartments = db.Students
                       .Where(e => e.DepartmentId == depid || e.DepartmentId == null)
                       .ToList();

            foreach(var item in StudentInDepartments)
            {
                if(item.Id == Request.Form[item.Id])
                {
                    item.DepartmentId = depid;
                    db.Entry(item).State = EntityState.Modified;
                }
                else
                {
                    item.DepartmentId = null;
                    db.Entry(item).State = EntityState.Modified;
                }
            }

            db.SaveChanges();

            return RedirectToAction("SubmitToDepartment","Students");
        }



        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult UploadDataExcel()
        {
            return PartialView();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public  ActionResult UploadDataExcel(HttpPostedFileBase FileExcel)
        {
            List<JoinViewModel> DataToInsert = new List<JoinViewModel>();
            if (ModelState.IsValid)
            {
                if(FileExcel != null && FileExcel.ContentLength > 0)
                {
                    Stream stream = FileExcel.InputStream;
                    IExcelDataReader reader = null;
                    if (FileExcel.FileName.EndsWith(".xls"))
                    {
                        reader = ExcelReaderFactory.CreateBinaryReader(stream);
                    }
                    else if (FileExcel.FileName.EndsWith(".xlsx"))
                    {
                        reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                    }
                    else
                    {
                        return PartialView("UploadDataExcel");
                    }
                    reader.IsFirstRowAsColumnNames = true;
                    DataSet CommingData = reader.AsDataSet();
                    reader.Close();

                    DataTable Res = CommingData.Tables[0];

                    foreach (DataRow item in Res.Rows)
                    {
                         var s =   item;
                    }



                }
            }
            return PartialView("UploadDataExcel");
        }









        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }







    }
}