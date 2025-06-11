using System.Collections.Generic;
using Common;
using Model.Custom.EvaluacionGridView;
using Model.Domain.Evaluacion;

namespace Service.Evaluaciones
{
    public interface IEvaluacionService
    {
        #region Evaluacion
        IEnumerable<EvaluacionForGridView> GetAll();
        IEnumerable<EvaluacionForGridView> GetEvaluacionesbyUserId(string userId);

        Evaluacion Get(int id);
        ResponseHelper InsertOrUpdate(Evaluacion model);
        ResponseHelper Delete(int id);
        #endregion

        #region EvaluacionDetalle
        IEnumerable<EvaluacionDetalleForGridView> GetAllEvaluacionDetalle(int id);
        EvaluacionDetalle GetEvaluacionDetalle(int id);
        ResponseHelper InsertOrUpdateEvaluacionDetalle(EvaluacionDetalle model);
        ResponseHelper DeleteEvaluacionDetalle(int id);
        #endregion

        #region Evaluacion Config

        IEnumerable<EvaluacionConfigForGridView> GetAllEvaluacionConfig(int id);
        EvaluacionConfig GetEvaluacionConfig(int id);
        ResponseHelper InsertOrUpdateEvaluacionConfig(EvaluacionConfig model);
        ResponseHelper DeleteEvaluacionConfig(int id);


        #endregion
  
        #region Evaluacion Usuario

        EvaluacionUsuarioForGridView GetAllEvaluacionUsuariobyId(string id);
        EvaluacionUsuario GetEvaluacionUsuario(int id);

        #endregion

        #region Evaluacion Asignada

        IEnumerable<EvaluacionAsignadaForGridView> GetAllEvaluacionAsignada();
        EvaluacionAsignada GetEvaluacionAsignada(int id);
        ResponseHelper InsertOrUpdateEvaluacionAsignada(EvaluacionAsignada model);
        ResponseHelper DeleteEvaluacionAsignada(int id);

        #endregion

    }
}