using System.Collections.Generic;
using Common;
using Model.Custom.RespuestaGridView;
using Model.Domain.Respuesta;

namespace Service.Respuestas
{
    public interface IRespuestaService
    {
        #region Respuesta

        IEnumerable<RespuestaForGridView> GetAll();        
        Respuesta Get(int id);
        ResponseHelper<Respuesta> InsertOrUpdate(Respuesta model);
        ResponseHelper Delete(int id);

        #endregion

        #region Respuesta Detalle

        IEnumerable<RespuestaDetalleForGridView> GetAllRespuestaDetalle();
        RespuestaViewModel GetRespuestaDetalleByRespuestaId(int idRespuesta);
        RespuestaDetalle GetRespuestaDetalle(int id);
        ResponseHelper InsertOrUpdateRespuestaDetalle(RespuestaDetalle model);
        ResponseHelper DeleteRespuestaDealle(int id);

        #endregion


    }
}