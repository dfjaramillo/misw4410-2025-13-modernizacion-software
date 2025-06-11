using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using Model.Auth;
using Model.Custom.PreguntaGridView;
using Model.Domain.Pegunta;
using NLog;
using Persistence.DbContextScope.Interfaces;
using Persistence.Repository;

namespace Service.Preguntas
{
    public class PreguntaService : IPreguntaService
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();
        private readonly IDbContextScopeFactory _dbContextScopeFactory;
        private readonly IRepository<Pregunta> _preguntaRepository;
        private readonly IRepository<PreguntaDetalle> _preguntaDetalleRepository;
        private readonly IRepository<ApplicationUser> _applicationUser;
        private readonly IRepository<ApplicationRole> _applicationRole;

        public PreguntaService(
            IDbContextScopeFactory dbContextScopeFactory,
            IRepository<Pregunta> preguntaRepository,
            IRepository<PreguntaDetalle> preguntaDetalleRepository,
            IRepository<ApplicationUser> applicationUser,
            IRepository<ApplicationRole> applicationRole
        )
        {
            _dbContextScopeFactory = dbContextScopeFactory;
            _preguntaRepository = preguntaRepository;
            _preguntaDetalleRepository = preguntaDetalleRepository;
            _applicationUser = applicationUser;
            _applicationRole = applicationRole;
        }

        public IEnumerable<PreguntaForGridView> GetAll()
        {
            var result = new List<PreguntaForGridView>();

            try
            {
                using (var ctx = _dbContextScopeFactory.CreateReadOnly())
                {
                    result = _preguntaRepository.GetAll()
                        .Select(x => new PreguntaForGridView()
                        {
                            IdPregunta = x.IdPregunta,
                            Titulo = x.Titulo,
                            Descripcion = x.Descripcion
                        }).ToList();
                }
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
            }

            return result;
        }
        public IEnumerable<PreguntaForGridView> GetbyFormId(int id)
        {
            var result = new List<PreguntaForGridView>();

            try
            {
                using (var ctx = _dbContextScopeFactory.CreateReadOnly())
                {
                    result = _preguntaRepository.Find(x => x.IdFormulario == id)
                        .Select(x => new PreguntaForGridView()
                        {
                            IdPregunta = x.IdPregunta,
                            Titulo = x.Titulo,
                            Descripcion = x.Descripcion
                        }).ToList();
                }
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
            }

            return result;
        }
        public Pregunta Get(int id)
        {
            var result = new Pregunta();

            try
            {
                using (var ctx = _dbContextScopeFactory.Create())
                {
                    result = _preguntaRepository.SingleOrDefault(x => x.IdPregunta == id);
                }
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
            }

            return result;
        }
        public ResponseHelper InsertOrUpdate(Pregunta model)
        {
            var rh = new ResponseHelper();

            try
            {
                using (var ctx = _dbContextScopeFactory.Create())
                {
                    if (model.IdPregunta == 0)
                    {
                        _preguntaRepository.Insert(model);
                    }
                    else
                    {
                        _preguntaRepository.Update(model);
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
                    var model = _preguntaRepository.SingleOrDefault(x => x.IdPregunta == id);
                    _preguntaRepository.Delete(model);

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

        public PreguntaDetalle GetPreguntaDetalle(int id)
        {
            var result = new PreguntaDetalle();

            try
            {
                using (var ctx = _dbContextScopeFactory.Create())
                {
                    result = _preguntaDetalleRepository.SingleOrDefault(x => x.IdPreguntaDetalle == id);
                }
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
            }

            return result;
        }
        public List<PreguntaDetalle> GetAllPreguntaDetalle(int id)
        {
            var result = new List<PreguntaDetalle>();

            try
            {
                using (var ctx = _dbContextScopeFactory.Create())
                {
                    result = _preguntaDetalleRepository.Find(x => x.IdPregunta == id).ToList();
                }
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
            }

            return result;
        }

        public ResponseHelper InsertOrUpdatePreguntaDetalle(PreguntaDetalle model)
        {
            var rh = new ResponseHelper();

            try
            {
                using (var ctx = _dbContextScopeFactory.Create())
                {
                    if (model.IdPreguntaDetalle == 0)
                    {
                        _preguntaDetalleRepository.Insert(model);
                    }
                    else
                    {
                        _preguntaDetalleRepository.Update(model);
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

        public ResponseHelper DeletePreguntaDetalle(int id)
        {
            var rh = new ResponseHelper();

            try
            {
                using (var ctx = _dbContextScopeFactory.Create())
                {
                    var model = _preguntaDetalleRepository.SingleOrDefault(x => x.IdPreguntaDetalle == id);
                    _preguntaDetalleRepository.Delete(model);

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
    }
}
