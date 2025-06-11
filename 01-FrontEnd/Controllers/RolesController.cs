using Auth.Service;
using Microsoft.AspNet.Identity.Owin;
using Model.Auth;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FrontEnd.Controllers
{   
    public class RolesController : Controller
    {
        private ApplicationRoleManager RoleManager => HttpContext.GetOwinContext().Get<ApplicationRoleManager>();

        // GET: User
        public ActionResult Index()
        {
            var model = RoleManager.Roles.OrderBy(x => x.Name).ToList();

            return View(model);
        }

        public async Task<ActionResult> Add(ApplicationRole model)
        {
            var response = await RoleManager.CreateAsync(model);

            if (!response.Succeeded)
            {
                throw new Exception(response.Errors.First());
            }

            return RedirectToAction("Index");
        }
    }
}