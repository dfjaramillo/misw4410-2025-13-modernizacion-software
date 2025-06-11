using Auth.Service;
using Common;
using Microsoft.AspNet.Identity.Owin;
using Model.Auth;
using Model.Custom.UsuarioGridView;
using Model.Custom.Utils;
using Model.Domain.Usuario;
using Service.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace FrontEnd.Controllers
{
    
    public class UsersController : Controller
    {
        private ApplicationRoleManager _roleManager
        {
            get
            {
                return HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
        }

        private ApplicationUserManager _userManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        private readonly IUserService _userService = DependecyFactory.GetInstance<IUserService>();

        // GET: User
        public ActionResult Index()
        {
            var model = _userService.GetAll(null).ToList();

            return View(model);
        }


        public async Task<ActionResult> Get(string id)
        {
            var model = await _userManager.FindByIdAsync(id);
            ViewBag.Roles = _roleManager.Roles.OrderBy(x => x.Name).ToList();

            return View(model);
        }

        public async Task<ActionResult> AddRole(ApplicationUserRole role)
        {
            var hasRole = _userManager.GetRolesAsync(role.UserId);
            if (hasRole.Result.Any())
            {
                string[] strRoles = new string[hasRole.Result.Count()];
                int cont = 0;
                foreach (var item in hasRole.Result)
                {
                    strRoles[cont] = item.Id;
                    cont++;
                }
                var obj = _userManager.RemoveFromRolesAsync(role.UserId, strRoles);
            }

            if (!_userManager.IsInRoleAsync(role.UserId, role.RoleId).Result)
            {
                var result = await _userManager.AddToRoleAsync(role.UserId, role.RoleId);

                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First());
                }
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult HabilitarUsuario(string value)
        {
            _userService.DeactiveUser(value);

            return View("Index", _userService.GetAll(null).ToList());
        }
        /// <summary>
        /// Recibe el id de la evaluacion asignada
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult AsignarUsuario(int id,string IdEvaluador)
        {
            ViewBag.IdEvaluacionUsuario = id;           
            var usuarios = new UsuarioAsignadoForGridView()
            {
                Usuarios = (from usu in _userService.GetAll(true)
                            select new CatalogModel()
                            {
                                ItemIdString = usu.Id,
                                ItemName = $"{usu.Name} {usu.LastName}",
                                CatalogId = 1,
                                CatalogName = "Evaluados",
                            }).ToList(),
            };           
            return View(usuarios);
        }
        public JsonResult ReadAsignado(int id)
        {
            var model = _userService.GetUsuarioAsignadosByEvaluacion(id, null);
            if (!model.Any())
            {
                model = new List<UsuarioAsignadoForGridView>();
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CrudAsignado(string op, int id)
        {
            if (!string.IsNullOrEmpty(op) && op.Equals("Delete"))
            {
                var oCreate = new JavaScriptSerializer().Deserialize<List<UsuariosAsignado>>(System.Web.HttpContext.Current.Request["models"]);
                if (oCreate.Any())
                {
                    oCreate.All(x =>
                    {
                        _userService.DeleteUsuarioAsignado(x.IdUsuarioAsignado);
                        return true;
                    });
                }
            }
            else
            {
                var oCreate = new JavaScriptSerializer().Deserialize<List<UsuariosAsignado>>(System.Web.HttpContext.Current.Request["models"]);
                if (oCreate.Any())
                {
                    oCreate.All(x =>
                    {
                        x.IdEvaluacionAsignada = id;
                        _userService.InsertOrUpdateUsuarioAsignado(x);
                        return true;
                    });
                }
            }


            return Json(_userService.GetUsuarioAsignadosByEvaluacion(id, null), JsonRequestBehavior.AllowGet);
        }
    }
}