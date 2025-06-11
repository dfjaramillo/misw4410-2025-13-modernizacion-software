using Auth.Service;
using Common;
using Microsoft.AspNet.Identity.Owin;
using Model.Custom.EvaluacionGridView;
using Model.Custom.Utils;
using Model.Domain.Evaluacion;
using Model.Domain.Respuesta;
using Model.Domain.Usuario;
using Model.Domain.Utils;
using Service.Areas;
using Service.Cargo;
using Service.Evaluaciones;
using Service.Formularios;
using Service.Preguntas;
using Service.Respuestas;
using Service.Usuarios;
using Service.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace FrontEnd.Controllers
{
    [Authorize(Roles = "Administrador,Evaluador")]
    public class EvaluacionController : Controller
    {
        private readonly IEvaluacionService _evaluacionService = DependecyFactory.GetInstance<IEvaluacionService>();
        private readonly IFormularioService _formularioService = DependecyFactory.GetInstance<IFormularioService>();
        private readonly IPreguntaService _preguntaService = DependecyFactory.GetInstance<IPreguntaService>();
        private readonly IRespuestaService _respuestaService = DependecyFactory.GetInstance<IRespuestaService>();
        private readonly IUtilsService _utilsService = DependecyFactory.GetInstance<IUtilsService>();
        private readonly IUserService _userService = DependecyFactory.GetInstance<IUserService>();
        private readonly IAreaService _AreaService = DependecyFactory.GetInstance<IAreaService>();
        private readonly ICargoService _CargoService = DependecyFactory.GetInstance<ICargoService>();

        private static int _respuestaId;
        private static int _evAsignadaId;

        private ApplicationRoleManager RoleManager => HttpContext.GetOwinContext().Get<ApplicationRoleManager>();


        #region Evaluaciones

        [Authorize(Roles = "Administrador")]
        public ActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "Administrador")]
        public JsonResult Read()
        {
            return Json(_evaluacionService.GetAll(), JsonRequestBehavior.AllowGet);
        }
        [Authorize(Roles = "Administrador")]
        public JsonResult Crud(string op)
        {
            if (!string.IsNullOrEmpty(op) && op.Equals("Delete"))
            {
                var oCreate = new JavaScriptSerializer().Deserialize<List<Evaluacion>>(System.Web.HttpContext.Current.Request["models"]);
                if (oCreate.Any())
                {
                    oCreate.All(x =>
                    {
                        _evaluacionService.Delete(x.IdEvaluacion);
                        return true;
                    });
                }
            }
            else
            {
                var oCreate = new JavaScriptSerializer().Deserialize<List<Evaluacion>>(System.Web.HttpContext.Current.Request["models"]);
                if (oCreate.Any())
                {
                    oCreate.All(x =>
                    {
                        _evaluacionService.InsertOrUpdate(x);
                        return true;
                    });
                }
            }


            return Json(_evaluacionService.GetAll(), JsonRequestBehavior.AllowGet);
        }


        #endregion

        #region Evaluacion Detalles
        [Authorize(Roles = "Administrador")]
        public ActionResult EvalDetalles(int id)
        {
            var detalle = _utilsService.GetAllbyCatalogId((int)EnumEvaluacionCatalogo.EvaluacionDetalle);
            var oModel = new EvaluacionDetalleForGridView { DetallesItem = new List<CatalogModel>() };
            detalle.All(x =>
            {
                oModel.DetallesItem.Add(x.ItemType);
                return true;
            });

            ViewBag.IdEvaluacion = id;


            return View(oModel);
        }
        [Authorize(Roles = "Administrador")]
        public JsonResult ReadDet(int id)
        {
            return Json(_evaluacionService.GetAllEvaluacionDetalle(id), JsonRequestBehavior.AllowGet);
        }
        [Authorize(Roles = "Administrador")]
        public JsonResult CrudDet(string op, int id)
        {
            if (!string.IsNullOrEmpty(op) && op.Equals("Delete"))
            {
                var oCreate = new JavaScriptSerializer().Deserialize<List<EvaluacionDetalle>>(System.Web.HttpContext.Current.Request["models"]);
                if (oCreate.Any())
                {
                    oCreate.All(x =>
                    {
                        _evaluacionService.DeleteEvaluacionDetalle(x.IdEvaluacionDetalle);
                        return true;
                    });
                }
            }
            else
            {
                var oCreate = new JavaScriptSerializer().Deserialize<List<EvaluacionDetalle>>(System.Web.HttpContext.Current.Request["models"]);
                if (oCreate.Any())
                {
                    oCreate.All(x =>
                    {
                        x.IdEvaluacion = id;
                        _evaluacionService.InsertOrUpdateEvaluacionDetalle(x);
                        return true;
                    });
                }
            }


            return Json(_evaluacionService.GetAllEvaluacionDetalle(id), JsonRequestBehavior.AllowGet);
            //return View("EvalDetalles");
        }

        #endregion

        #region Evaluacion Configuracion
        [Authorize(Roles = "Administrador")]
        public JsonResult ReadConf(int id)
        {

            return Json(_evaluacionService.GetAllEvaluacionConfig(id), JsonRequestBehavior.AllowGet);
        }
        [Authorize(Roles = "Administrador")]
        public ActionResult EvalConfig(int id)
        {
            var forms = _formularioService.GetAll();
            var evConfig = new EvaluacionConfigForGridView() { IdEvaluacion = id, Forms = new List<CatalogModel>() };
            if (forms.Any())
            {
                forms.All(x =>
                {
                    evConfig.Forms.Add(new CatalogModel()
                    {
                        ItemId = x.IdFormulario,
                        ItemName = x.Nombre,
                        CatalogId = (int)EnumEvaluacionCatalogo.EvaluacionConfig
                    });
                    return true;
                });
            }
            else
            {
                ViewBag.Message = "No existen formularios";
            }


            return View(evConfig);
        }
        [Authorize(Roles = "Administrador")]
        public JsonResult CrudConf(string op, int id)
        {
            if (!string.IsNullOrEmpty(op) && op.Equals("Delete"))
            {
                var oCreate = new JavaScriptSerializer().Deserialize<List<EvaluacionConfig>>(System.Web.HttpContext.Current.Request["models"]);
                if (oCreate.Any())
                {
                    oCreate.All(x =>
                    {
                        _evaluacionService.DeleteEvaluacionConfig(x.IdEvaluacionConfig);
                        return true;
                    });
                }
            }
            else
            {
                var oCreate = new JavaScriptSerializer().Deserialize<List<EvaluacionConfig>>(System.Web.HttpContext.Current.Request["models"]);
                if (oCreate.Any())
                {
                    oCreate.All(x =>
                    {
                        x.IdEvaluacion = id;
                        _evaluacionService.InsertOrUpdateEvaluacionConfig(x);
                        return true;
                    });
                }
            }


            return Json(_evaluacionService.GetAllEvaluacionConfig(id), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Evaluaciones Usuario
        [Authorize(Roles = "Administrador,Evaluador")]
        public ActionResult EvaluacionUsuario(ExampleViewModel model, EvaluacionUsuarioForGridView evaluacionUsuario)
        {
            var idE = Request.QueryString["idE"];
            var idEA = Request.QueryString["idEA"];
            ViewBag.IdEvaluacionAsignada = idEA;
            var idUser = Request.QueryString["idU"];
            ViewBag.IdEvaluado = idUser;
            var op = Request.QueryString["option"];
            var idUvAsignada = Request.QueryString["idA"];
            ResponseHelper<Respuesta> response = null;
            if (!string.IsNullOrEmpty(op) && op.Equals("Load"))
            {
                response = _respuestaService.InsertOrUpdate(new Respuesta()
                {
                    IdRespuesta = 0,
                    IdEvaluacion = Convert.ToInt16(idE),
                    IdEvaluador = CurrentUserHelper.Get.UserId,
                    IdEvaluado = idUser,
                });
            }

            if (response != null && !response.Message.Contains("Error"))
            {
                _respuestaId = response.Result.IdRespuesta;
                _evAsignadaId = Convert.ToInt16(idUvAsignada);
            }

            if (model.Answers.Any() && !string.IsNullOrEmpty(evaluacionUsuario.Identificacion))
            {
                model.Answers.All(x =>
                {
                    _respuestaService.InsertOrUpdateRespuestaDetalle(new RespuestaDetalle()
                    {
                        IdRespuestaDetalle = 0,
                        IdREspuesta = _respuestaId,
                        IdPreguntaDetalle = Convert.ToInt16(x.Key),
                        ValorRespuesta = x.Value
                    });
                    return true;
                });
                _userService.InsertOrUpdateUsuarioAsignado(new UsuariosAsignado()
                {
                    IdUsuarioAsignado = Convert.ToInt16(_evAsignadaId),
                    IdEvaluacionAsignada = Convert.ToInt16(evaluacionUsuario.IdEvaluacionAsignada),
                    IdEvaluado = evaluacionUsuario.IdEvaluado,
                    IsEvaluado = true
                });
                return RedirectToAction("IndexEvaluador", "Home", new { id = evaluacionUsuario.Identificacion });
            }

            var evaluado = _userService.GetUserById(idUser);
            var evaluacion = _evaluacionService.GetAllEvaluacionUsuariobyId(idE);
            evaluacion.Evaluado = evaluado;
            evaluacion.RespuestasModel = new ExampleViewModel();

            evaluacion.EvaluacionConfig.All(x =>
                        {
                            x.Formularios.All(y =>
                            {
                                y.Preguntas.All(z =>
                                {
                                    z.PreguntaDetalle.All(w =>
                                    {
                                        evaluacion.RespuestasModel.Add(w.IdPreguntaDetalle.ToString(),
                                            new string[] { "1", "2", "3", "4", "5" }, w.Valor);
                                        return true;
                                    });
                                    return true;
                                });
                                return true;
                            });
                            return true;
                        });


            return View(evaluacion);
        }

        //[Authorize(Roles = "Administrador,Evaluador")]
        //[HttpPost]
        //public ActionResult EvaluacionUsuario(int id,ExampleViewModel answer)
        //{
        //    ViewBag.Message = "Se ha guardado la evaluación con éxito";

        //    return RedirectToAction("IndexEvaluador", "Home");
        //}
        [Authorize(Roles = "Administrador")]
        public ActionResult AsignarEvaluacion()
        {
            var eval = _evaluacionService.GetAll();
            var usuarios = _userService.GetAll(true);
            var roles = RoleManager.Roles.Where(x => x.Name == "Evaluador").Select(x => x).First();
            var model = new EvaluacionAsignadaForGridView()
            {
                Evaluaciones = (from evaluacionForGridView in eval
                                select new CatalogModel()
                                {
                                    ItemId = evaluacionForGridView.IdEvaluacion,
                                    ItemName = evaluacionForGridView.Nombre,
                                    CatalogId = 1,
                                    CatalogName = "Evaluaciones"

                                }).ToList(),
                Evaluadores = (from usu in usuarios
                               where usu.Roles.Contains(roles.Name) && usu.Enable
                               select new CatalogModel()
                               {
                                   ItemIdString = usu.Id,
                                   ItemName = $"{usu.Name} {usu.LastName}",
                                   CatalogId = 2,
                                   CatalogName = "Evaluadores",
                               }).ToList(),
            };

            return View(model);
        }
        [Authorize(Roles = "Administrador")]
        public JsonResult ReadAsignado()
        {
            var model = _evaluacionService.GetAllEvaluacionAsignada();
            if (!model.Any())
            {
                model = new List<EvaluacionAsignadaForGridView>();
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        [Authorize(Roles = "Administrador")]
        public JsonResult CrudAsignado(string op)
        {
            if (!string.IsNullOrEmpty(op) && op.Equals("Delete"))
            {
                var oCreate = new JavaScriptSerializer().Deserialize<List<EvaluacionAsignada>>(System.Web.HttpContext.Current.Request["models"]);
                if (oCreate.Any())
                {
                    oCreate.All(x =>
                    {
                        _evaluacionService.DeleteEvaluacionAsignada(x.IdEvaluacionAsignada);
                        return true;
                    });
                }
            }
            else
            {
                var oCreate = new JavaScriptSerializer().Deserialize<List<EvaluacionAsignada>>(System.Web.HttpContext.Current.Request["models"]);
                if (oCreate.Any())
                {
                    oCreate.All(x =>
                    {
                        _evaluacionService.InsertOrUpdateEvaluacionAsignada(x);
                        return true;
                    });
                }
            }


            return Json(_evaluacionService.GetAllEvaluacionAsignada(), JsonRequestBehavior.AllowGet);
        }
        #endregion


    }
}