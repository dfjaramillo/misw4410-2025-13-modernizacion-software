using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Common;
using Model.Domain.Area;
using Service.Areas;

namespace FrontEnd.Controllers
{
    public class AreaController : Controller
    {
        private readonly IAreaService _areaService = DependecyFactory.GetInstance<IAreaService>();
        // GET: Area
        public ActionResult Index()
        {

            return View();
        }
        public JsonResult Read()
        {
            return Json(_areaService.GetAll(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Crud(string op)
        {
            if (!string.IsNullOrEmpty(op) && op.Equals("Delete"))
            {
                var oCreate = new JavaScriptSerializer().Deserialize<List<Area>>(System.Web.HttpContext.Current.Request["models"]);
                if (oCreate.Any())
                {
                    oCreate.All(x =>
                    {
                        _areaService.Delete(x.IdArea);
                        return true;
                    });
                }
            }
            else
            {
                var oCreate = new JavaScriptSerializer().Deserialize<List<Area>>(System.Web.HttpContext.Current.Request["models"]);
                if (oCreate.Any())
                {
                    oCreate.All(x =>
                    {
                        _areaService.InsertOrUpdateArea(x);
                        return true;
                    });
                }
            }


            return Json(_areaService.GetAll(), JsonRequestBehavior.AllowGet);
        }

    }
}