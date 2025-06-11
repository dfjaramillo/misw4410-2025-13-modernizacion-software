using Common;
using Model.Custom.EvaluacionGridView;
using Model.Custom.EvaluadorGridView;
using Model.Custom.UsuarioGridView;
using Service.Evaluaciones;
using Service.Usuarios;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace FrontEnd.Controllers
{
    [Authorize(Roles = "Administrador,Evaluador")]
    public class HomeController : Controller
    {
        private readonly IEvaluacionService _evaluacionService = DependecyFactory.GetInstance<IEvaluacionService>();
        private readonly IUserService _userService = DependecyFactory.GetInstance<IUserService>();


        [Authorize(Roles = "Administrador")]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Evaluador")]
        public ActionResult IndexEvaluador(string id)
        {            
            return View(GetEvaluadorForGrid(id));
        }
        private List<EvaluadorForGridView> GetEvaluadorForGrid(string idEvaluador)
        {
            var returnobj = new List<EvaluadorForGridView>();
            var EvaluacionesAsignadas = _evaluacionService.GetAllEvaluacionAsignada().Where(x => x.IdEvaluador.Equals(idEvaluador));
            var UsuariosAsignados = _userService.GetAllUsuarioAsignado().ToList();
            var Usuarios = _userService.GetAll(true).ToList();
            EvaluacionesAsignadas.All(x =>
            {
                var usersA = UsuariosAsignados.Where(y => y.IdEvaluacionAsignada.Equals(x.IdEvaluacionAsignada) && !y.IsEvaluado);
                var user = new List<UsuarioForGridView>();
                usersA.All(u =>
                {
                    user.Add(new UsuarioForGridView()
                    {
                        Id = Usuarios.FirstOrDefault(z => z.Id.Equals(u.IdEvaluado))?.Id,
                        Identificacion = Usuarios.FirstOrDefault(z => z.Id.Equals(u.IdEvaluado)).Identification,
                        Nombre = Usuarios.FirstOrDefault(z => z.Id.Equals(u.IdEvaluado))?.Name,
                        Apellido = Usuarios.FirstOrDefault(z => z.Id.Equals(u.IdEvaluado))?.LastName,
                        Email = Usuarios.FirstOrDefault(z => z.Id.Equals(u.IdEvaluado))?.Email,
                    });
                    return true;
                });

                returnobj.Add(new EvaluadorForGridView()
                {
                    Evaluacion = new EvaluacionForGridView(_evaluacionService.Get(x.IdEvaluacion)),
                    Usuarios = user,
                    UsuariosAsignados = UsuariosAsignados,
                    EvaluacionesAsignadas = new EvaluacionAsignadaForGridView()
                    {
                        IdEvaluacionAsignada = x.IdEvaluacionAsignada,
                        IdEvaluacion = x.IdEvaluacion,
                        IdEvaluador = x.IdEvaluador,
                        Deleted = x.Deleted
                    }                    
                });
                return true;
            });

            return returnobj;
        }

        [Authorize(Roles = "Administrador")]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [Authorize(Roles = "Administrador")]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}