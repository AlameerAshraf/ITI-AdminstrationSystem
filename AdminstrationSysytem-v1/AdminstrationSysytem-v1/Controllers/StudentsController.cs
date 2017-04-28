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
            return PartialView();
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> CraeteStudentData(JoinViewModel model)
        {
            var Students = db.Students.ToList();
            if (ModelState.IsValid)
            {
                var user = new Student { Name = model.Name, UserName = model.Name, Email = model.Email, Address = model.Address, BD = model.BirthDate, UserAccessType = "Student" , IsActivated = true };
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {

                    string code = await userManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    await userManager.SendEmailAsync(user.Id, "Student , Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");
                    var RoleAssigner = userManager.AddToRole(user.Id, "Student");

                    TempData["Student"] = user;
                    return PartialView("List", Students);
                }
                AddErrors(result);

            }
            return PartialView("CraeteStudentData");
        }



        [Authorize(Roles ="Admin")]
        [HttpGet]
        public ActionResult DeleteStudent()
        {
            return PartialView();
        }



        [Authorize(Roles = "Student")]
        [HttpGet]
        public ActionResult Evalute()
        {
            var StudentDepartmentId = (TempData["Student"] as Student).DepartmentId; 

            return View();
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