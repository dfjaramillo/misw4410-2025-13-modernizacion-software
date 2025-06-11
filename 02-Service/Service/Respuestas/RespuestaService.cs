using Common;
using Model.Auth;
using Model.Custom.EvaluacionGridView;
using Model.Custom.RespuestaGridView;
using Model.Custom.UsuarioGridView;
using Model.Domain.Area;
using Model.Domain.Evaluacion;
using Model.Domain.Pegunta;
using Model.Domain.Respuesta;
using Model.Domain.Formulario;
using NLog;
using Persistence.DbContextScope.Interfaces;
using Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Respuestas
{
    public class RespuestaService : IRespuestaService
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();
        private readonly IDbContextScopeFactory _dbContextScopeFactory;
        private readonly IRepository<Respuesta> _respuestaRepository;
        private readonly IRepository<Formulario> _formularioRepository;
        private readonly IRepository<FormularioDetalle> _formularioDetalleRepository;
        private readonly IRepository<RespuestaDetalle> _respuestaDetalleRepository;
        private readonly IRepository<PreguntaDetalle> _preguntaDetalleRepository;
        private readonly IRepository<Evaluacion> _evaluacionRepository;
        private readonly IRepository<EvaluacionConfig> _evaluacionConfigRepository;
        private readonly IRepository<Model.Domain.Cargo.Cargo> _cargoRepository;
        private readonly IRepository<Model.Domain.Area.Area> _areaRepository;
        private readonly IRepository<ApplicationUser> _applicationUser;
        private readonly IRepository<ApplicationRole> _applicationRole;

        public RespuestaService(
            IDbContextScopeFactory dbContextScopeFactory,
            IRepository<Respuesta> respuestaRepository,
            IRepository<RespuestaDetalle> respuestaDetalleRepository,
            IRepository<PreguntaDetalle> preguntaDetalleRepository,
            IRepository<Formulario> formularioRepository,
            IRepository<FormularioDetalle> formularioDetalleRepository,
            IRepository<ApplicationUser> applicationUser,
            IRepository<ApplicationRole> applicationRole,
            IRepository<Evaluacion> evaluacionRepository,
            IRepository<EvaluacionConfig> evaluacionConfigRepository,
            IRepository<Model.Domain.Cargo.Cargo> cargoRepository,
            IRepository<Area> areaRepository)
        {
            _dbContextScopeFactory = dbContextScopeFactory;
            _respuestaRepository = respuestaRepository;
            _respuestaDetalleRepository = respuestaDetalleRepository;
            _preguntaDetalleRepository = preguntaDetalleRepository;
            _formularioRepository = formularioRepository;
            _formularioDetalleRepository = formularioDetalleRepository;
            _applicationUser = applicationUser;
            _applicationRole = applicationRole;
            _evaluacionRepository = evaluacionRepository;
            _evaluacionConfigRepository = evaluacionConfigRepository;
            _cargoRepository = cargoRepository;
            _areaRepository = areaRepository;
        }

        #region Respuesta

        public IEnumerable<RespuestaForGridView> GetAll()
        {
            var result = new List<RespuestaForGridView>();

            try
            {
                using (var ctx = _dbContextScopeFactory.CreateReadOnly())
                {
                    var respuestas = _respuestaRepository.GetAll().ToList();
                    var evaluaciones = _evaluacionRepository.GetAll().ToList();
                    var usuarios = _applicationUser.GetAll().ToList();
                    evaluaciones.All(e =>
                    {
                        respuestas.All(rt =>
                        {

                            if (e.IdEvaluacion.Equals(rt.IdEvaluacion))
                            {
                                result.Add(new RespuestaForGridView()
                                {
                                    IdRespuesta = rt.IdRespuesta,
                                    IdEvaluacion = rt.IdEvaluacion,
                                    IdEvaluador = rt.IdEvaluador,
                                    IdEvaluado = rt.IdEvaluado,
                                    EmailEvaluador = usuarios.FirstOrDefault(x => x.Id.Equals(rt.IdEvaluador))?.Email,
                                    EmailEvaluado = usuarios.FirstOrDefault(x => x.Id.Equals(rt.IdEvaluado))?.Email,
                                    NombreEvaluacion = e.IdEvaluacion.Equals(rt.IdEvaluacion) ? e.Nombre : string.Empty,
                                    NombreEvaluado = usuarios.FirstOrDefault(x => x.Id.Equals(rt.IdEvaluado))?.Name + usuarios.FirstOrDefault(x => x.Id.Equals(rt.IdEvaluado))?.LastName,
                                    NombreEvaluador = usuarios.FirstOrDefault(x => x.Id.Equals(rt.IdEvaluador))?.Name + usuarios.FirstOrDefault(x => x.Id.Equals(rt.IdEvaluador))?.LastName,
                                    Deleted = rt.Deleted
                                });
                            }
                            return true;
                        });

                        return true;
                    });

                }
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
            }

            return result;
        }
        public Respuesta Get(int id)
        {
            var result = new Respuesta();

            try
            {
                using (var ctx = _dbContextScopeFactory.Create())
                {
                    result = _respuestaRepository.SingleOrDefault(x => x.IdRespuesta == id);
                }
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
            }

            return result;
        }
        public ResponseHelper<Respuesta> InsertOrUpdate(Respuesta model)
        {
            var rh = new ResponseHelper<Respuesta>();

            try
            {
                using (var ctx = _dbContextScopeFactory.Create())
                {
                    if (model.IdRespuesta == 0)
                    {
                        _respuestaRepository.Insert(model);
                    }
                    else
                    {
                        _respuestaRepository.Update(model);
                    }

                    ctx.SaveChanges();
                    rh.Result = new Respuesta()
                    {
                        IdRespuesta = model.IdRespuesta,
                        IdEvaluacion = model.IdEvaluacion,
                        IdEvaluador = model.IdEvaluador,
                        IdEvaluado = model.IdEvaluado,
                        Deleted = model.Deleted
                    };
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
                    var model = _respuestaRepository.SingleOrDefault(x => x.IdRespuesta == id);
                    _respuestaRepository.Delete(model);

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

        #region RespuestaDetalle

        public IEnumerable<RespuestaDetalleForGridView> GetAllRespuestaDetalle()
        {
            var result = new List<RespuestaDetalleForGridView>();

            try
            {
                using (var ctx = _dbContextScopeFactory.CreateReadOnly())
                {
                    result = _respuestaDetalleRepository.GetAll()
                        .Select(x => new RespuestaDetalleForGridView()
                        {
                            IdPreguntaDetalle = x.IdPreguntaDetalle,
                            IdREspuesta = x.IdREspuesta,
                            IdRespuestaDetalle = x.IdPreguntaDetalle,
                            ValorRespuesta = x.ValorRespuesta,
                            Deleted = x.Deleted
                        }).ToList();
                }
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
            }

            return result;
        }
        //To-do:remove hard code values
        public RespuestaViewModel GetRespuestaDetalleByRespuestaId(int idRespuesta)
        {
            var result = new RespuestaViewModel();

            try
            {
                using (var ctx = _dbContextScopeFactory.CreateReadOnly())
                {
                    var respuesta = _respuestaRepository.FirstOrDefault(x => x.IdRespuesta.Equals(idRespuesta));
                    var evaluacion =
                        _evaluacionRepository.FirstOrDefault(x => x.IdEvaluacion.Equals(respuesta.IdEvaluacion));
                    var evaConfig = _evaluacionConfigRepository.Find(x => x.IdEvaluacion.Equals(evaluacion.IdEvaluacion));
                    var evaluador = _applicationUser.FirstOrDefault(x => x.Id.Equals(respuesta.IdEvaluador));
                    var evaluado = _applicationUser.FirstOrDefault(x => x.Id.Equals(respuesta.IdEvaluado));
                    var detallesRespuesta = _respuestaDetalleRepository.Find(x => x.IdREspuesta.Equals(idRespuesta)).ToList();
                    var formulario = new Formulario();
                    var cargos = _cargoRepository.GetAll().ToList();
                    var areas = _areaRepository.GetAll().ToList();

                    result.Respuesta = new RespuestaForGridView(respuesta);
                    result.Evaluacion = new EvaluacionForGridView(evaluacion);
                    result.Evaluado = new UserForGridView(evaluado)
                    {
                        NombreArea = areas.FirstOrDefault(x => x.IdArea.Equals(evaluado.Area))?.Name,
                        NombreCargo = cargos.FirstOrDefault(x => x.CargoId.Equals(evaluado.Cargo))?.Nombre
                    };
                    result.Evaluador = new UserForGridView(evaluador)
                    {
                        NombreArea = areas.FirstOrDefault(x => x.IdArea.Equals(evaluador.Area))?.Name,
                        NombreCargo = cargos.FirstOrDefault(x => x.CargoId.Equals(evaluador.Cargo))?.Nombre
                    };
                    result.Detalles = new List<RespuestaDetalleForGridView>();

                    detallesRespuesta.All(dr =>
                    {
                        bool intValido = false;
                        int intResult;
                        intValido = int.TryParse(dr.ValorRespuesta, out intResult);
                        var brecha = intValido ? 80 - (intResult * 20) : 0;
                        result.Detalles.Add(new RespuestaDetalleForGridView()
                        {
                            IdRespuestaDetalle = dr.IdRespuestaDetalle,
                            IdREspuesta = dr.IdREspuesta,
                            ValorRequerido = "80",
                            ResultadoFinal = Convert.ToString(intResult * 20),
                            Brecha = brecha.ToString(),
                            IdPreguntaDetalle = dr.IdPreguntaDetalle,
                            EnunciadoPregunta = _preguntaDetalleRepository.FirstOrDefault(x => x.IdPreguntaDetalle.Equals(dr.IdPreguntaDetalle)).Valor,
                            ValorRespuesta = dr.ValorRespuesta,
                            Deleted = dr.Deleted
                        });

                        return true;
                    });
                }
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
            }

            return result;
        }

        public RespuestaDetalle GetRespuestaDetalle(int id)
        {
            var result = new RespuestaDetalle();

            try
            {
                using (var ctx = _dbContextScopeFactory.Create())
                {
                    result = _respuestaDetalleRepository.SingleOrDefault(x => x.IdRespuestaDetalle == id);
                }
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
            }

            return result;
        }

        public ResponseHelper InsertOrUpdateRespuestaDetalle(RespuestaDetalle model)
        {
            var rh = new ResponseHelper();

            try
            {
                using (var ctx = _dbContextScopeFactory.Create())
                {
                    if (model.IdRespuestaDetalle == 0)
                    {
                        _respuestaDetalleRepository.Insert(model);
                    }
                    else
                    {
                        _respuestaDetalleRepository.Update(model);
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

        public ResponseHelper DeleteRespuestaDealle(int id)
        {
            var rh = new ResponseHelper();

            try
            {
                using (var ctx = _dbContextScopeFactory.Create())
                {
                    var model = _respuestaDetalleRepository.SingleOrDefault(x => x.IdRespuestaDetalle == id);
                    _respuestaDetalleRepository.Delete(model);

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
