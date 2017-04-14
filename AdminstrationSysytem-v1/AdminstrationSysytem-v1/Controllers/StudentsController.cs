using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdminstrationSysytem_v1.Models;

namespace AdminstrationSysytem_v1.Controllers
{
    public class StudentsController : Controller
    {

        ApplicationDbContext db = new ApplicationDbContext();

        // GET: Students
        public ActionResult Users()
        {
            var users = db.Users;
            return View(users);
        }

        public ActionResult Profil()
        {
            return View();
        }
    }
}