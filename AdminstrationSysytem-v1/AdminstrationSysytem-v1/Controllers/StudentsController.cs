using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdminstrationSysytem_v1.Models;
using System.Data.Entity;

namespace AdminstrationSysytem_v1.Controllers
{
    public class StudentsController : Controller
    {

        ApplicationDbContext db = new ApplicationDbContext();

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
        //public ActionResult Test()
        //{
        //    ViewBag.tem = Request.Form;
        //    return View();
        //}



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




    }
}