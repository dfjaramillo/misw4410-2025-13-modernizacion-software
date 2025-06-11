using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using Model.Auth;
using Model.Custom.FormularioGridView;
using Model.Domain.Formulario;
using NLog;
using Persistence.DbContextScope.Interfaces;
using Persistence.Repository;

namespace Service.Formularios
{
    public class FormularioService : IFormularioService
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();
        private readonly IDbContextScopeFactory _dbContextScopeFactory;
        private readonly IRepository<Formulario> _formularioRepository;
        private readonly IRepository<FormularioDetalle> _formularioDetalleRepository;
        private readonly IRepository<ApplicationUser> _applicationUser;
        private readonly IRepository<ApplicationRole> _applicationRole;

        public FormularioService(
            IDbContextScopeFactory dbContextScopeFactory,
            IRepository<Formulario> formularioRepository,
            IRepository<FormularioDetalle> formularioDetalleRepository,
            IRepository<ApplicationUser> applicationUser,
            IRepository<ApplicationRole> applicationRole
        )
        {
            _dbContextScopeFactory = dbContextScopeFactory;
            _formularioRepository = formularioRepository;
            _formularioDetalleRepository = formularioDetalleRepository;
            _applicationUser = applicationUser;
            _applicationRole = applicationRole;
        }

        public IEnumerable<FormularioForGridView> GetAll()
        {
            var result = new List<FormularioForGridView>();

            try
            {
                using (var ctx = _dbContextScopeFactory.CreateReadOnly())
                {
                    result = _formularioRepository.GetAll()
                        .Select(x => new FormularioForGridView()
                        {
                            IdFormulario = x.IdFormulario,
                            Nombre = x.Nombre,                            
                        }).ToList();
                }
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
            }

            return result;
        }

        public Formulario Get(int id)
        {
            var result = new Formulario();

            try
            {
                using (var ctx = _dbContextScopeFactory.Create())
                {
                    result = _formularioRepository.SingleOrDefault(x => x.IdFormulario == id);
                }
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
            }

            return result;
        }
        public ResponseHelper InsertOrUpdate(Formulario model)
        {
            var rh = new ResponseHelper();

            try
            {
                using (var ctx = _dbContextScopeFactory.Create())
                {
                    if (model.IdFormulario == 0)
                    {
                        _formularioRepository.Insert(model);
                    }
                    else
                    {
                        _formularioRepository.Update(model);
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
                    var model = _formularioRepository.SingleOrDefault(x => x.IdFormulario == id);
                    _formularioRepository.Delete(model);

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

        public FormularioDetalle GetFormDetalle(int id)
        {
            var result = new FormularioDetalle();

            try
            {
                using (var ctx = _dbContextScopeFactory.Create())
                {
                    result = _formularioDetalleRepository.SingleOrDefault(x => x.IdFormularioDetalle == id);
                }
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
            }

            return result;
        }
        public List<FormDetalleForGridView> GetAllFormDetalle(int id)
        {
            var result = new List<FormDetalleForGridView>();

            try
            {
                using (var ctx = _dbContextScopeFactory.Create())
                {
                    result = _formularioDetalleRepository.Find(x => x.IdFormulario == id).Select(x=> new FormDetalleForGridView()
                    {
                        IdFormulario = x.IdFormulario,
                        IdFormularioDetalle = x.IdFormularioDetalle,
                        FormDetalleItem = x.FormDetalleItem,
                        Valor = x.Valor

                    }) .ToList();
                }                
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
            }

            return result;
        }

        public ResponseHelper InsertOrUpdateFormDetalle(FormularioDetalle model)
        {
            var rh = new ResponseHelper();

            try
            {
                using (var ctx = _dbContextScopeFactory.Create())
                {
                    if (model.IdFormularioDetalle == 0)
                    {
                        _formularioDetalleRepository.Insert(model);
                    }
                    else
                    {
                        _formularioDetalleRepository.Update(model);
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

        public ResponseHelper DeleteFormDetalle(int id)
        {
            var rh = new ResponseHelper();

            try
            {
                using (var ctx = _dbContextScopeFactory.Create())
                {
                    var model = _formularioDetalleRepository.SingleOrDefault(x => x.IdFormularioDetalle == id);
                    _formularioDetalleRepository.Delete(model);

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
