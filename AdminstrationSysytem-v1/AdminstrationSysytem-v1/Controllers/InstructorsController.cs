using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdminstrationSysytem_v1.Models;

namespace AdminstrationSysytem_v1.Controllers
{
    public class InstructorsController : Controller
    {
        // GET: Instructors
        public ActionResult UserProfile()
        {
            return View();
        }


        [Authorize(Roles = "Instructor")]
        [HttpGet]
        public ActionResult GivePermission()
        {
            var ins = TempData["Instructor"] as Instructors;
            return Content(ins.DepartmentId.ToString());
        }
    }
}