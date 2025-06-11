using System.Collections.Generic;
using Common;
using Model.Auth;
using Model.Custom;
using Model.Custom.UsuarioGridView;
using Model.Domain.Usuario;

namespace Service.Usuarios
{
    public interface IUserService
    {
        #region User
        IEnumerable<UserForGridView> GetAll(bool? enable);
        ApplicationUser GetUserByEmail(string email);
        UserForGridView GetUserById(string id);
        void DeactiveUser(string id);

        #endregion

        #region Usuario Asignado

        IEnumerable<UsuarioAsignadoForGridView> GetUsuarioAsignadosByEvaluacion(int idEvaluacionAsignada, bool? evaluado);
        IEnumerable<UsuarioAsignadoForGridView> GetAllUsuarioAsignado();
        UsuariosAsignado GetUsuarioAsignado(int id);
        ResponseHelper InsertOrUpdateUsuarioAsignado(UsuariosAsignado model);
        ResponseHelper DeleteUsuarioAsignado(int id);

        #endregion

        
    }
}