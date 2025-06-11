using LightInject;
using Model.Auth;
using Model.Domain;
using Model.Domain.Area;
using Model.Domain.Evaluacion;
using Model.Domain.Formulario;
using Model.Domain.Pegunta;
using Model.Domain.Respuesta;
using Model.Domain.Usuario;
using Model.Domain.Utils;
using Persistence.DbContextScope.Implementations;
using Persistence.DbContextScope.Interfaces;
using Persistence.Repository;
using Service.Areas;
using Service.Cargo;
using Service.Evaluaciones;
using Service.Formularios;
using Service.Preguntas;
using Service.Respuestas;
using Service.Usuarios;
using Service.Utils;

namespace Service.Config
{
    public class ServiceRegister : ICompositionRoot
    {               
        public void Compose(IServiceRegistry container)
        {
            var ambientDbContextLocator = new AmbientDbContextLocator();

            #region Config Context

            container.Register<IDbContextScopeFactory>((x) => new DbContextScopeFactory(null));
            container.Register<IAmbientDbContextLocator, AmbientDbContextLocator>(new PerScopeLifetime());

            #endregion

            #region Usuario

            container.Register<IRepository<ApplicationUser>>((x) => new Repository<ApplicationUser>(ambientDbContextLocator));
            container.Register<IRepository<UsuariosAsignado>>((x) => new Repository<UsuariosAsignado>(ambientDbContextLocator));

            #endregion

            #region Role

            container.Register<IRepository<ApplicationRole>>((x) => new Repository<ApplicationRole>(ambientDbContextLocator));

            #endregion

            #region Curso

            container.Register<IRepository<Course>>((x) => new Repository<Course>(ambientDbContextLocator));
            #endregion

            #region Estudiante

            container.Register<IRepository<Student>>((x) => new Repository<Student>(ambientDbContextLocator));
            container.Register<IRepository<StudentPerCourse>>((x) => new Repository<StudentPerCourse>(ambientDbContextLocator));

            #endregion

            #region Pregunta

            container.Register<IRepository<Pregunta>>((x) => new Repository<Pregunta>(ambientDbContextLocator));
            container.Register<IRepository<PreguntaDetalle>>((x) => new Repository<PreguntaDetalle>(ambientDbContextLocator));

            #endregion

            #region Respuesta

            container.Register<IRepository<Respuesta>>((x) => new Repository<Respuesta>(ambientDbContextLocator));
            container.Register<IRepository<RespuestaDetalle>>((x) => new Repository<RespuestaDetalle>(ambientDbContextLocator));            

            #endregion

            #region Formulario

            container.Register<IRepository<Formulario>>((x) => new Repository<Formulario>(ambientDbContextLocator));
            container.Register<IRepository<FormularioDetalle>>((x) => new Repository<FormularioDetalle>(ambientDbContextLocator));
            container.Register<IRepository<FormularioConfig>>((x) => new Repository<FormularioConfig>(ambientDbContextLocator));

            #endregion

            #region Evaluacion

            container.Register<IRepository<Evaluacion>>((x) => new Repository<Evaluacion>(ambientDbContextLocator));
            container.Register<IRepository<EvaluacionDetalle>>((x) => new Repository<EvaluacionDetalle>(ambientDbContextLocator));
            container.Register<IRepository<EvaluacionConfig>>((x) => new Repository<EvaluacionConfig>(ambientDbContextLocator));
            container.Register<IRepository<EvaluacionUsuario>>((x) => new Repository<EvaluacionUsuario>(ambientDbContextLocator));
            container.Register<IRepository<EvaluacionAsignada>>((x) => new Repository<EvaluacionAsignada>(ambientDbContextLocator));

            #endregion

            #region Area

            container.Register<IRepository<Area>>((x) => new Repository<Area>(ambientDbContextLocator));

            #endregion

            #region Cargo

            container.Register<IRepository<Model.Domain.Cargo.Cargo>>((x) => new Repository<Model.Domain.Cargo.Cargo>(ambientDbContextLocator));

            #endregion

            #region Utils

            container.Register<IRepository<Item>>((x) => new Repository<Item>(ambientDbContextLocator));
            container.Register<IRepository<Catalogo>>((x) => new Repository<Catalogo>(ambientDbContextLocator));

            #endregion

            #region Interfaces


            container.Register<IStudentService, StudentService>();
            container.Register<IStudentPerCourseService, StudentPerCourseService>();
            container.Register<ICourseService, CourseService>();

            container.Register<IPreguntaService, PreguntaService>();
            container.Register<IAreaService, AreaService>();
            container.Register<ICargoService, CargoService>();
            container.Register<IRespuestaService, RespuestaService>();
            container.Register<IEvaluacionService, EvaluacionService>();
            container.Register<IFormularioService, FormularioService>();
            container.Register<IUtilsService, UtilsService>();
            container.Register<IUserService, UserService>();

            #endregion

        }
    }
}
