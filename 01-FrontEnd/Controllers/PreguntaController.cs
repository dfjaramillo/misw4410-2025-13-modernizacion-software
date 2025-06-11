using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Common;
using Model.Custom.PreguntaGridView;
using Model.Custom.Utils;
using Model.Domain.Pegunta;
using Model.Domain.Utils;
using Service.Preguntas;
using Service.Utils;

namespace FrontEnd.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class PreguntaController : Controller
    {
        private readonly IPreguntaService _preguntaService = DependecyFactory.GetInstance<IPreguntaService>();
        private readonly IUtilsService _utilsService = DependecyFactory.GetInstance<IUtilsService>();

        #region Preguntas

        public ActionResult Index(int id)
        {
            ViewBag.FormId = id;
            return View();
        }
        public JsonResult Read(int id)
        {
            return Json(_preguntaService.GetbyFormId(id), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Crud(string op, int id)
        {
            if (!string.IsNullOrEmpty(op) && op.Equals("Delete"))
            {
                var oCreate = new JavaScriptSerializer().Deserialize<List<Pregunta>>(System.Web.HttpContext.Current.Request["models"]);
                if (oCreate.Any())
                {
                    oCreate.All(x =>
                    {
                        _preguntaService.Delete(x.IdPregunta);
                        return true;
                    });
                }
            }
            else
            {
                var oCreate = new JavaScriptSerializer().Deserialize<List<Pregunta>>(System.Web.HttpContext.Current.Request["models"]);
                
                if (oCreate.Any())
                {
                    oCreate.All(x =>
                    {
                        x.IdFormulario = id;
                        _preguntaService.InsertOrUpdate(x);
                        return true;
                    });
                }
            }


            return Json(_preguntaService.GetAll(), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region PreguntaDetalles

        public ActionResult DetPregunta(int id)
        {
            var detalle = _utilsService.GetAllbyCatalogId((int)EnumPreguntaCatalogo.PreguntaDetalle);
            var oModel = new PreguntaDetalleForGridView { DetallesItem = new List<CatalogModel>() };
            detalle.All(x =>
            {
                oModel.DetallesItem.Add(x.ItemType);
                return true;
            });

            ViewBag.Pregunta = id;


            return View(oModel);
        }
        public JsonResult ReadDet(int id)
        {
            return Json(_preguntaService.GetAllPreguntaDetalle(id), JsonRequestBehavior.AllowGet);
        }
        public JsonResult CrudDet(string op, int id)
        {
            if (!string.IsNullOrEmpty(op) && op.Equals("Delete"))
            {
                var oCreate = new JavaScriptSerializer().Deserialize<List<PreguntaDetalle>>(System.Web.HttpContext.Current.Request["models"]);
                if (oCreate.Any())
                {
                    oCreate.All(x =>
                    {
                        _preguntaService.DeletePreguntaDetalle(x.IdPreguntaDetalle);
                        return true;
                    });
                }
            }
            else
            {
                var oCreate = new JavaScriptSerializer().Deserialize<List<PreguntaDetalle>>(System.Web.HttpContext.Current.Request["models"]);
                if (oCreate.Any())
                {
                    oCreate.All(x =>
                    {
                        x.IdPregunta = id;
                        _preguntaService.InsertOrUpdatePreguntaDetalle(x);
                        return true;
                    });
                }
            }


            return Json(_preguntaService.GetAllPreguntaDetalle(id), JsonRequestBehavior.AllowGet);
            //return View("EvalDetalles");
        }

        #endregion
        
    }
}