 using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Common;
using Model.Domain.Cargo;
using Service.Areas;
 using Service.Cargo;

namespace FrontEnd.Controllers
{
    public class CargoController : Controller
    {
        private readonly ICargoService _cargoService = DependecyFactory.GetInstance<ICargoService>();
        // GET: Cargo
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Read()
        {
            return Json(_cargoService.GetAll(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Crud(string op)
        {
            if (!string.IsNullOrEmpty(op) && op.Equals("Delete"))
            {
                var oCreate = new JavaScriptSerializer().Deserialize<List<Cargo>>(System.Web.HttpContext.Current.Request["models"]);
                if (oCreate.Any())
                {
                    oCreate.All(x =>
                    {
                        _cargoService.Delete(x.CargoId);
                        return true;
                    });
                }
            }
            else
            {
                var oCreate = new JavaScriptSerializer().Deserialize<List<Cargo>>(System.Web.HttpContext.Current.Request["models"]);
                if (oCreate.Any())
                {
                    oCreate.All(x =>
                    {
                        _cargoService.InsertOrUpdateCargo(x);
                        return true;
                    });
                }
            }


            return Json(_cargoService.GetAll(), JsonRequestBehavior.AllowGet);
        }
    }
}