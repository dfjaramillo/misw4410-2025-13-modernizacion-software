using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using Model.Auth;
using Model.Custom;
using Model.Custom.UsuarioGridView;
using Model.Domain.Area;
using Model.Domain.Evaluacion;
using Model.Domain.Usuario;
using NLog;
using Persistence.DbContextScope.Extensions;
using Persistence.DbContextScope.Interfaces;
using Persistence.Repository;

namespace Service.Usuarios
{
    public class UserService : IUserService
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();
        private readonly IDbContextScopeFactory _dbContextScopeFactory;
        private readonly IRepository<ApplicationUser> _applicationUserRepo;
        private readonly IRepository<UsuariosAsignado> _usuarioAsignadoRepo;


        public UserService(
            IDbContextScopeFactory dbContextScopeFactory,
            IRepository<ApplicationUser> applicationUserRepo,
            IRepository<UsuariosAsignado> usuarioAsignadoRepo)
        {
            _dbContextScopeFactory = dbContextScopeFactory;
            _applicationUserRepo = applicationUserRepo;
            _usuarioAsignadoRepo = usuarioAsignadoRepo;
        }

        #region ApplicationUser

        public IEnumerable<UserForGridView> GetAll(bool? enable)
        {
            var result = new List<UserForGridView>();

            try
            {
                using (var ctx = _dbContextScopeFactory.CreateReadOnly())
                {                    
                    IQueryable<ApplicationUser> users = null;
                    users = enable != null ? ctx.GetEntity<ApplicationUser>().Where(x => x.Enable == enable) : ctx.GetEntity<ApplicationUser>();


                    var roles = ctx.GetEntity<ApplicationRole>();
                    var usersPerRoles = ctx.GetEntity<ApplicationUserRole>();

                    var queryUsersPerRoles = (
                        from upr in usersPerRoles
                        from r in roles.Where(x => x.Id == upr.RoleId)
                        select new
                        {
                            upr.UserId,
                            r.Name
                        }
                    ).AsQueryable();

                    result = (
                        from u in users
                        select new UserForGridView
                        {
                            Id = u.Id,
                            Email = u.Email,
                            Identification = u.Identification,
                            LastName = u.LastName,
                            Name = u.Name,
                            Area = u.Area,
                            Cargo = u.Cargo,
                            Enable = u.Enable,
                            Roles = queryUsersPerRoles.Where(x =>
                                x.UserId == u.Id
                            ).Select(x => x.Name).ToList()
                        }
                    ).ToList();

                }
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
            }

            return result;
        }

        public ApplicationUser GetUserByEmail(string email)
        {
            var oReturn = new ApplicationUser();
            try
            {
                using (var ctx = _dbContextScopeFactory.CreateReadOnly())
                {
                    var user = ctx.GetEntity<ApplicationUser>();
                    oReturn = user.First(x => x.Email == email);
                }
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
            }

            return oReturn;
        }
        public UserForGridView GetUserById(string id)
        {
            var oReturn = new UserForGridView();
            try
            {
                using (var ctx = _dbContextScopeFactory.CreateReadOnly())
                {
                    var user = ctx.GetEntity<ApplicationUser>().First(x => x.Id == id);

                    oReturn = new UserForGridView(user);
                    oReturn.NombreArea = ctx.GetEntity<Area>().First(x => x.IdArea == user.Area).Name;
                    oReturn.NombreCargo = ctx.GetEntity<Model.Domain.Cargo.Cargo>().First(x => x.CargoId == user.Cargo).Nombre;
                }
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
            }

            return oReturn;
        }
        public void DeactiveUser(string id)
        {


            try
            {
                using (var ctx = _dbContextScopeFactory.Create())
                {
                    var model = _applicationUserRepo.First(x => x.Id == id);
                    if (model.Enable)
                    {
                        model.Enable = false;
                        _applicationUserRepo.Update(model);

                    }
                    else
                    {
                        model.Enable = true;
                        _applicationUserRepo.Update(model);
                    }

                    ctx.SaveChanges();
                }
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
            }
        }

        #endregion

        #region Usuario Asignado

        public IEnumerable<UsuarioAsignadoForGridView> GetAllUsuarioAsignado()
        {
            var result = new List<UsuarioAsignadoForGridView>();

            try
            {
                using (var ctx = _dbContextScopeFactory.CreateReadOnly())
                {
                    result = _usuarioAsignadoRepo.GetAll()
                        .Select(x => new UsuarioAsignadoForGridView()
                        {
                            IdUsuarioAsignado = x.IdUsuarioAsignado,
                            IdEvaluacionAsignada = x.IdEvaluacionAsignada,
                            IdEvaluado = x.IdEvaluado,
                            IsEvaluado = x.IsEvaluado,
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
        public IEnumerable<UsuarioAsignadoForGridView> GetUsuarioAsignadosByEvaluacion(int idEvaluacionAsignada, bool? evaluado)
        {
            var result = new List<UsuarioAsignadoForGridView>();

            try
            {
                using (var ctx = _dbContextScopeFactory.CreateReadOnly())
                {
                    if (evaluado==null)
                    {
                        result = (from ua in _usuarioAsignadoRepo.Find(x => x.IdEvaluacionAsignada == idEvaluacionAsignada)                            
                            select new UsuarioAsignadoForGridView()
                            {
                                IdEvaluado = ua.IdEvaluado,
                                IdEvaluacionAsignada = ua.IdEvaluacionAsignada,
                                IdUsuarioAsignado = ua.IdUsuarioAsignado,
                                IsEvaluado = ua.IsEvaluado
                            }).ToList();
                    }
                    else
                    {
                        result = (from ua in _usuarioAsignadoRepo.Find(x => x.IdEvaluacionAsignada == idEvaluacionAsignada)
                            where ua.IsEvaluado == evaluado
                            select new UsuarioAsignadoForGridView()
                            {
                                IdEvaluado = ua.IdEvaluado,
                                IdEvaluacionAsignada = ua.IdEvaluacionAsignada,
                                IdUsuarioAsignado = ua.IdUsuarioAsignado,
                                IsEvaluado = ua.IsEvaluado
                            }).ToList();
                    }
                    
                }
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
            }

            return result;
        }
        public UsuariosAsignado GetUsuarioAsignado(int id)
        {
            var result = new UsuariosAsignado();

            try
            {
                using (var ctx = _dbContextScopeFactory.Create())
                {
                    result = _usuarioAsignadoRepo.SingleOrDefault(x => x.IdUsuarioAsignado == id);
                }
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
            }

            return result;
        }
        public ResponseHelper InsertOrUpdateUsuarioAsignado(UsuariosAsignado model)
        {
            var rh = new ResponseHelper();

            try
            {
                using (var ctx = _dbContextScopeFactory.Create())
                {
                    if (model.IdUsuarioAsignado == 0)
                    {
                        _usuarioAsignadoRepo.Insert(model);
                    }
                    else
                    {
                        _usuarioAsignadoRepo.Update(model);
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
        public ResponseHelper DeleteUsuarioAsignado(int id)
        {
            var rh = new ResponseHelper();

            try
            {
                using (var ctx = _dbContextScopeFactory.Create())
                {
                    var model = _usuarioAsignadoRepo.SingleOrDefault(x => x.IdUsuarioAsignado == id);
                    _usuarioAsignadoRepo.Delete(model);

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
