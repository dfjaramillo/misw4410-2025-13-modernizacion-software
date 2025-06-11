using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using Model.Auth;
using Model.Custom.EvaluacionGridView;
using Model.Custom.FormularioGridView;
using Model.Custom.PreguntaGridView;
using Model.Domain.Evaluacion;
using Model.Domain.Formulario;
using Model.Domain.Pegunta;
using NLog;
using Persistence.DbContextScope.Interfaces;
using Persistence.Repository;

namespace Service.Evaluaciones
{
    public class EvaluacionService : IEvaluacionService
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();
        private readonly IDbContextScopeFactory _dbContextScopeFactory;
        private readonly IRepository<Evaluacion> _evaluacionRepository;
        private readonly IRepository<EvaluacionDetalle> _evaluacionDetalleRepository;
        private readonly IRepository<EvaluacionConfig> _evaluacionConfigRepository;
        private readonly IRepository<EvaluacionUsuario> _evaluacionUsuarioRepository;
        private readonly IRepository<EvaluacionAsignada> _evaluacionAsignadaRepository;
        private readonly IRepository<Formulario> _formularioRepository;
        private readonly IRepository<FormularioDetalle> _formularioDetalleRepository;
        private readonly IRepository<FormularioConfig> _formularioConfigRepository;
        private readonly IRepository<Pregunta> _preguntaRepository;
        private readonly IRepository<PreguntaDetalle> _preguntaDetalleRepository;
        private readonly IRepository<ApplicationUser> _applicationUser;
        private readonly IRepository<ApplicationRole> _applicationRole;

        public EvaluacionService(
            IDbContextScopeFactory dbContextScopeFactory,
            IRepository<Evaluacion> evaluacionRepository,
            IRepository<EvaluacionDetalle> evaluacionDetalleRepository,
            IRepository<EvaluacionConfig> evaluacionConfigRepository,
            IRepository<ApplicationUser> applicationUser,
            IRepository<ApplicationRole> applicationRole,
            IRepository<EvaluacionUsuario> evaluacionUsuarioRepository,
            IRepository<Formulario> formularioRepository,
            IRepository<FormularioDetalle> formularioDetalleRepository,
            IRepository<FormularioConfig> formularioConfigRepository,
            IRepository<PreguntaDetalle> preguntaDetalleRepository,
            IRepository<Pregunta> preguntaRepository, IRepository<EvaluacionAsignada> evaluacionAsignadaRepository)
        {
            _dbContextScopeFactory = dbContextScopeFactory;
            _evaluacionRepository = evaluacionRepository;
            _evaluacionDetalleRepository = evaluacionDetalleRepository;
            _evaluacionConfigRepository = evaluacionConfigRepository;
            _applicationUser = applicationUser;
            _applicationRole = applicationRole;
            _evaluacionUsuarioRepository = evaluacionUsuarioRepository;
            _formularioRepository = formularioRepository;
            _formularioDetalleRepository = formularioDetalleRepository;
            _formularioConfigRepository = formularioConfigRepository;
            _preguntaDetalleRepository = preguntaDetalleRepository;
            _preguntaRepository = preguntaRepository;
            _evaluacionAsignadaRepository = evaluacionAsignadaRepository;
        }

        #region Evaluacion

        public IEnumerable<EvaluacionForGridView> GetAll()
        {
            var result = new List<EvaluacionForGridView>();

            try
            {
                using (var ctx = _dbContextScopeFactory.CreateReadOnly())
                {
                    result = _evaluacionRepository.GetAll()
                        .Select(x => new EvaluacionForGridView(x)).ToList();
                }
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
            }

            return result;
        }
        public Evaluacion Get(int id)
        {
            var result = new Evaluacion();

            try
            {
                using (var ctx = _dbContextScopeFactory.Create())
                {
                    result = _evaluacionRepository.SingleOrDefault(x => x.IdEvaluacion == id);
                }
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
            }

            return result;
        }
        public ResponseHelper InsertOrUpdate(Evaluacion model)
        {
            var rh = new ResponseHelper();

            try
            {
                using (var ctx = _dbContextScopeFactory.Create())
                {
                    if (model.IdEvaluacion == 0)
                    {
                        _evaluacionRepository.Insert(model);
                    }
                    else
                    {
                        _evaluacionRepository.Update(model);
                    }

                    ctx.SaveChanges();
                    rh.SetResponse(true);
                }
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
            }

            return rh;
        }
        public ResponseHelper Delete(int id)
        {
            var rh = new ResponseHelper();

            try
            {
                using (var ctx = _dbContextScopeFactory.Create())
                {
                    var model = _evaluacionRepository.SingleOrDefault(x => x.IdEvaluacion == id);
                    _evaluacionRepository.Delete(model);

                    ctx.SaveChanges();
                    rh.SetResponse(true);
                }
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
            }

            return rh;
        }

        #endregion

        #region Evaluacion Detalle

        public EvaluacionDetalle GetEvaluacionDetalle(int id)
        {
            var result = new EvaluacionDetalle();

            try
            {
                using (var ctx = _dbContextScopeFactory.Create())
                {
                    result = _evaluacionDetalleRepository.FirstOrDefault(x => x.IdEvaluacion == id);
                }
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
            }

            return result;
        }
        public IEnumerable<EvaluacionDetalleForGridView> GetAllEvaluacionDetalle(int id)
        {
            var result = new List<EvaluacionDetalleForGridView>();

            try
            {
                using (var ctx = _dbContextScopeFactory.CreateReadOnly())
                {
                    result = _evaluacionDetalleRepository.Find(x => x.IdEvaluacion == id).Select(x => new EvaluacionDetalleForGridView()
                    {
                        IdEvaluacionDetalle = x.IdEvaluacionDetalle,
                        IdEvaluacion = x.IdEvaluacion,
                        EvaluacionDetalleItem = x.EvaluacionDetalleItem,
                        Valor = x.Valor,
                    }).ToList();
                }
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
            }

            return result;
        }
        public ResponseHelper InsertOrUpdateEvaluacionDetalle(EvaluacionDetalle model)
        {
            var rh = new ResponseHelper();

            try
            {
                using (var ctx = _dbContextScopeFactory.Create())
                {
                    if (model.IdEvaluacionDetalle == 0)
                    {
                        _evaluacionDetalleRepository.Insert(model);
                    }
                    else
                    {
                        _evaluacionDetalleRepository.Update(model);
                    }

                    ctx.SaveChanges();
                    rh.SetResponse(true);
                }
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
            }

            return rh;
        }

        public ResponseHelper DeleteEvaluacionDetalle(int id)
        {
            var rh = new ResponseHelper();

            try
            {
                using (var ctx = _dbContextScopeFactory.Create())
                {
                    var model = _evaluacionDetalleRepository.SingleOrDefault(x => x.IdEvaluacionDetalle == id);
                    _evaluacionDetalleRepository.Delete(model);

                    ctx.SaveChanges();
                    rh.SetResponse(true);
                }
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
            }

            return rh;
        }

        #endregion

        #region Evaluacion Config

        public EvaluacionConfig GetEvaluacionConfig(int id)
        {
            var result = new EvaluacionConfig();

            try
            {
                using (var ctx = _dbContextScopeFactory.Create())
                {
                    result = _evaluacionConfigRepository.FirstOrDefault(x => x.IdEvaluacion == id);
                }
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
            }

            return result;
        }
        public IEnumerable<EvaluacionConfigForGridView> GetAllEvaluacionConfig(int id)
        {
            var result = new List<EvaluacionConfigForGridView>();

            try
            {
                using (var ctx = _dbContextScopeFactory.Create())
                {
                    result = _evaluacionConfigRepository.Find(x => x.IdEvaluacion == id).Select(x => new EvaluacionConfigForGridView()
                    {
                        IdEvaluacionConfig = x.IdEvaluacionConfig,
                        IdEvaluacion = x.IdEvaluacion,
                        IdFormulario = x.IdFormulario,
                    }).ToList();
                }
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
            }

            return result;
        }
        public ResponseHelper InsertOrUpdateEvaluacionConfig(EvaluacionConfig model)
        {
            var rh = new ResponseHelper();

            try
            {
                using (var ctx = _dbContextScopeFactory.Create())
                {
                    if (model.IdEvaluacionConfig == 0)
                    {
                        _evaluacionConfigRepository.Insert(model);
                    }
                    else
                    {
                        _evaluacionConfigRepository.Update(model);
                    }

                    ctx.SaveChanges();
                    rh.SetResponse(true);
                }
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
            }

            return rh;
        }

        public ResponseHelper DeleteEvaluacionConfig(int id)
        {
            var rh = new ResponseHelper();

            try
            {
                using (var ctx = _dbContextScopeFactory.Create())
                {
                    var model = _evaluacionConfigRepository.SingleOrDefault(x => x.IdEvaluacionConfig == id);
                    _evaluacionConfigRepository.Delete(model);

                    ctx.SaveChanges();
                    rh.SetResponse(true);
                }
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
            }

            return rh;
        }

        #endregion

        #region Evaluación Usuario
        public IEnumerable<EvaluacionForGridView> GetEvaluacionesbyUserId(string userId)
        {
            var result = new List<EvaluacionForGridView>();

            try
            {
                using (var ctx = _dbContextScopeFactory.Create())
                {
                    result = _evaluacionUsuarioRepository.Find(x => x.IdEvaluador == userId).Select(x => new EvaluacionForGridView()
                    {

                        IdEvaluacion = x.IdEvaluacion,

                    }).ToList();
                }
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
            }

            return result;
        }

        public EvaluacionUsuarioForGridView GetAllEvaluacionUsuariobyId(string id)
        {
            var result = new EvaluacionUsuarioForGridView();

            try
            {
                using (var ctx = _dbContextScopeFactory.CreateReadOnly())
                {
                    result = _evaluacionRepository.Find(x => x.IdEvaluacion.ToString() == id)
                        .Select(x => new EvaluacionUsuarioForGridView()
                        {
                            Evaluacion = new Evaluacion()
                            {
                                IdEvaluacion = x.IdEvaluacion,
                                Nombre = x.Nombre,

                            },
                            EvaluacionDetalles =
                                (from ed in _evaluacionDetalleRepository.Find(y => y.IdEvaluacion == x.IdEvaluacion)
                                 select new EvaluacionDetalleForGridView()
                                 {
                                     IdEvaluacion = ed.IdEvaluacion,
                                     EvaluacionDetalleItem = ed.EvaluacionDetalleItem,
                                     IdEvaluacionDetalle = ed.IdEvaluacionDetalle,
                                     Valor = ed.Valor
                                 }).ToList(),
                            EvaluacionConfig =
                                (from ec in _evaluacionConfigRepository.Find(z => z.IdEvaluacion == x.IdEvaluacion)
                                 select new EvaluacionConfigForGridView()
                                 {
                                     IdEvaluacion = ec.IdEvaluacion,
                                     IdEvaluacionConfig = ec.IdEvaluacionConfig,
                                     IdFormulario = ec.IdFormulario,
                                     Formularios =
                                         (from f in _formularioRepository.Find(
                                                 f => f.IdFormulario == ec.IdFormulario)
                                          select new FormularioForGridView()
                                          {
                                              IdFormulario = f.IdFormulario,
                                              Nombre = f.Nombre,
                                              FormDetalle =
                                                     (from fd in _formularioDetalleRepository.Find(fd =>
                                                             fd.IdFormulario == f.IdFormulario)
                                                      select new FormDetalleForGridView()
                                                      {
                                                          IdFormulario = fd.IdFormulario,
                                                          IdFormularioDetalle = fd.IdFormularioDetalle,
                                                          FormDetalleItem = fd.FormDetalleItem,
                                                          Valor = fd.Valor,
                                                      }).ToList(),
                                              Preguntas = (from p in _preguntaRepository.Find(p =>
                                                          p.IdFormulario == f.IdFormulario)
                                                           select new PreguntaForGridView()
                                                           {
                                                               IdPregunta = p.IdPregunta,
                                                               Titulo = p.Titulo,
                                                               Descripcion = p.Descripcion,
                                                               PreguntaDetalle =
                                                                      (from pd in _preguntaDetalleRepository.Find(
                                                                              pd => pd.IdPregunta == p.IdPregunta)
                                                                       select new PreguntaDetalleForGridView()
                                                                       {
                                                                           IdPreguntaDetalle =
                                                                                  pd.IdPreguntaDetalle,
                                                                           IdPregunta = pd.IdPregunta,
                                                                           DetalleItem = pd.DetalleItem,
                                                                           Valor = pd.Valor
                                                                       }).ToList(),
                                                           }).ToList()
                                          }).ToList(),
                                 }).ToList(),

                        }).FirstOrDefault();
                }
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
            }

            return result;
        }

        public EvaluacionUsuario GetEvaluacionUsuario(int id)
        {
            var result = new EvaluacionUsuario();

            try
            {
                using (var ctx = _dbContextScopeFactory.Create())
                {
                    result = _evaluacionUsuarioRepository.FirstOrDefault(x => x.IdEvaluacionUsuario == id);
                }
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
            }

            return result;
        }


        #endregion

        #region Evaluacion Asignada

        public IEnumerable<EvaluacionAsignadaForGridView> GetAllEvaluacionAsignada()
        {
            var result = new List<EvaluacionAsignadaForGridView>();

            try
            {
                using (var ctx = _dbContextScopeFactory.CreateReadOnly())
                {
                    result = _evaluacionAsignadaRepository.GetAll()                        
                        .Select(x => new EvaluacionAsignadaForGridView()
                        {
                            IdEvaluacionAsignada = x.IdEvaluacionAsignada,
                            IdEvaluacion = x.IdEvaluacion,
                            IdEvaluador = x.IdEvaluador,
                        }).ToList();
                }
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
            }

            return result;
        }
        public EvaluacionAsignada GetEvaluacionAsignada(int id)
        {
            var result = new EvaluacionAsignada();

            try
            {
                using (var ctx = _dbContextScopeFactory.Create())
                {
                    result = _evaluacionAsignadaRepository.SingleOrDefault(x => x.IdEvaluacionAsignada == id);
                }
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
            }

            return result;
        }
        public ResponseHelper InsertOrUpdateEvaluacionAsignada(EvaluacionAsignada model)
        {
            var rh = new ResponseHelper();

            try
            {
                using (var ctx = _dbContextScopeFactory.Create())
                {
                    if (model.IdEvaluacionAsignada == 0)
                    {
                        _evaluacionAsignadaRepository.Insert(model);
                    }
                    else
                    {
                        _evaluacionAsignadaRepository.Update(model);
                    }

                    ctx.SaveChanges();
                    rh.SetResponse(true);
                }
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
            }

            return rh;
        }
        public ResponseHelper DeleteEvaluacionAsignada(int id)
        {
            var rh = new ResponseHelper();

            try
            {
                using (var ctx = _dbContextScopeFactory.Create())
                {
                    var model = _evaluacionAsignadaRepository.SingleOrDefault(x => x.IdEvaluacionAsignada == id);
                    _evaluacionAsignadaRepository.Delete(model);

                    ctx.SaveChanges();
                    rh.SetResponse(true);
                }
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
            }

            return rh;
        }

        #endregion



    }
}
