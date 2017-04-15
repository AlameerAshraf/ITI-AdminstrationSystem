using System.Web.Mvc;
using AdminstrationSysytem_v1.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace AdminstrationSysytem_v1.Controllers
{
    [Authorize]
    public class AdminsController : Controller
    {

        static ApplicationDbContext dbContext = new ApplicationDbContext();
        RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(dbContext));



        // GET: Admins
        [HttpGet]
        public ActionResult CreateRoles()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateRoles(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                if(!roleManager.RoleExists(model.Name))
                {
                    var role = new IdentityRole();
                    role.Name = model.Name;
                    roleManager.Create(role);
                }
            }
            return View();
        }
    }
}