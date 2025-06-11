using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Common;
using Model.Custom.FormularioGridView;
using Model.Custom.Utils;
using Model.Domain.Formulario;
using Model.Domain.Utils;
using Service.Formularios;
using Service.Utils;

namespace FrontEnd.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class FormularioController : Controller
    {
        // GET: Formulario
        private readonly IFormularioService _formularioService = DependecyFactory.GetInstance<IFormularioService>();
        private readonly IUtilsService _utilsService = DependecyFactory.GetInstance<IUtilsService>();

        #region Formularios    

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Read()
        {
            return Json(_formularioService.GetAll(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Crud(string op)
        {
            if (!string.IsNullOrEmpty(op) && op.Equals("Delete"))
            {
                var oCreate =
                    new JavaScriptSerializer().Deserialize<List<Formulario>>(
                        System.Web.HttpContext.Current.Request["models"]);
                if (oCreate.Any())
                {
                    oCreate.All(x =>
                    {
                        _formularioService.Delete(x.IdFormulario);
                        return true;
                    });
                }
            }
            else
            {
                var oCreate =
                    new JavaScriptSerializer().Deserialize<List<Formulario>>(
                        System.Web.HttpContext.Current.Request["models"]);
                if (oCreate.Any())
                {
                    oCreate.All(x =>
                    {
                        _formularioService.InsertOrUpdate(x);
                        return true;
                    });
                }
            }


            return Json(_formularioService.GetAll(), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Formulario Detalle

        public ActionResult FormDetalles(int id)
        {
            var detalle = _utilsService.GetAllbyCatalogId((int) EnumFormularioCatalogo.FormularioDetalle);
            var oModel = new FormDetalleForGridView {DetallesItem = new List<CatalogModel>()};
            detalle.All(x =>
            {
                oModel.DetallesItem.Add(x.ItemType);
                return true;
            });

            ViewBag.IdEvaluacion = id;

            return View(oModel);
        }

        public JsonResult ReadDet(int id)
        {
            return Json(_formularioService.GetAllFormDetalle(id), JsonRequestBehavior.AllowGet);
        }

        public JsonResult CrudDet(string op, int id)
        {
            if (!string.IsNullOrEmpty(op) && op.Equals("Delete"))
            {
                var oCreate = new JavaScriptSerializer().Deserialize<List<FormularioDetalle>>(System.Web.HttpContext.Current.Request["models"]);
                if (oCreate.Any())
                {
                    oCreate.All(x =>
                    {
                        _formularioService.DeleteFormDetalle(x.IdFormularioDetalle);
                        return true;
                    });
                }
            }
            else
            {
                var oCreate =
                    new JavaScriptSerializer().Deserialize<List<FormularioDetalle>>(
                        System.Web.HttpContext.Current.Request["models"]);
                if (oCreate.Any())
                {
                    oCreate.All(x =>
                    {
                        x.IdFormulario = id;
                        _formularioService.InsertOrUpdateFormDetalle(x);
                        return true;
                    });
                }
            }


            return Json(_formularioService.GetAllFormDetalle(id), JsonRequestBehavior.AllowGet);
            //return View("EvalDetalles");
        }

        #endregion
        


    }
}