using System.Web.Mvc;
using AdminstrationSysytem_v1.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Web;

namespace AdminstrationSysytem_v1.Controllers
{
    [Authorize]
    public class AdminsController : Controller
    {
        static ApplicationDbContext dbContext = new ApplicationDbContext();
        RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(dbContext));
        UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(dbContext));

        private ApplicationSignInManager _signInManager;

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return SignInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

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


        [HttpGet]
        public ActionResult CreateAdmin()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateAdmin(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var Admin = new ApplicationUser() { UserName = model.Name, UserAccessType = "Admin", Email = model.Email, IsActivated = true };
                var result = await userManager.CreateAsync(Admin , model.Password);
                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(Admin, isPersistent: false, rememberBrowser: false);

                    var RoleAssigner = userManager.AddToRole(Admin.Id, "Admin");

                    TempData["Admin"] = Admin;
                    return RedirectToAction("Index", "Home");

                }
            }
            return View();
        }
    }
}