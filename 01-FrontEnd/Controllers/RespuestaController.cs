using Common;
using Model.Custom.RespuestaGridView;
using Model.Domain.Respuesta;
using Rotativa.MVC;
using Service.Respuestas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace FrontEnd.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class RespuestaController : Controller
    {
        private const string path = @"C:\Users\DFZ-23\Downloads\mdb";
        private readonly IRespuestaService _respuestaService = DependecyFactory.GetInstance<IRespuestaService>();
        // GET: Respuesta
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Read()
        {
            return Json(_respuestaService.GetAll(), JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Administrador")]
        public JsonResult Crud(string op)
        {
            if (!string.IsNullOrEmpty(op) && op.Equals("Delete"))
            {
                var oCreate = new JavaScriptSerializer().Deserialize<List<Respuesta>>(System.Web.HttpContext.Current.Request["models"]);
                if (oCreate.Any())
                {
                    oCreate.All(x =>
                    {
                        _respuestaService.Delete(x.IdRespuesta);
                        return true;
                    });
                }
            }
            else
            {
                var oCreate = new JavaScriptSerializer().Deserialize<List<Respuesta>>(System.Web.HttpContext.Current.Request["models"]);
                if (oCreate.Any())
                {
                    oCreate.All(x =>
                    {
                        _respuestaService.InsertOrUpdate(x);
                        return true;
                    });
                }
            }


            return Json(_respuestaService.GetAll(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult DetRespuesta(int RespuestaId)
        {
            return View(_respuestaService.GetRespuestaDetalleByRespuestaId(RespuestaId));
        }

        [HttpPost]
        public ActionResult DownloadViewPDF(RespuestaViewModel objmodel)
        {
            var model = _respuestaService.GetRespuestaDetalleByRespuestaId(objmodel.IdRespuesta);
            model.Grafica = objmodel.Grafica;

            return new ViewAsPdf("Reporte", model)
            {
                FileName = model.Evaluado.Identification + ".pdf",                
                //CustomSwitches = "--header-spacing 2 --footer-spacing 2"
            };
        }
       
        public PartialViewResult _ReportePartial(RespuestaViewModel model)
        {
            var labels = new List<string>();
            var data = new List<string>();
            var dataRequerida = new List<string>();
            model.Detalles.All(x =>
            {
                labels.Add("\"Componente " + x.IdPreguntaDetalle + "\"");
                data.Add(x.ResultadoFinal);
                dataRequerida.Add("80");
                return true;
            });
            var lbl = string.Join(",", labels.ToArray());
            var dJoin = string.Join(",", data.ToArray());
            var drJoin = string.Join(",", dataRequerida.ToArray());

            Tuple<string, string, string> dataChart = new Tuple<string, string, string>(lbl, dJoin, drJoin);
            ViewBag.data = Json(dataChart, JsonRequestBehavior.AllowGet);

            return PartialView();
        }
    }
}