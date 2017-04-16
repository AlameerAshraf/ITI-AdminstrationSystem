using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdminstrationSysytem_v1.Models;

namespace AdminstrationSysytem_v1.Controllers
{
    [Authorize(Roles = "Admin")]
    public class StudentsController : Controller
    {

        ApplicationDbContext db = new ApplicationDbContext();

        // GET: Students
        public ActionResult StudentsList()
        {
            var Students = db.Students.ToList();
            return View(Students);
        }


        public ActionResult ActivateAccounts(string[] IsActivated)
        {
            ViewBag.del = IsActivated;


            return View();
        }

        public ActionResult UserProfile()
        {

            return View();
        }
    }
}