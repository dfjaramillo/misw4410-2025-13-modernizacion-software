using System.Collections.Generic;
using Common;
using Model.Custom.PreguntaGridView;
using Model.Domain.Pegunta;

namespace Service.Preguntas
{
    public interface IPreguntaService
    {
        IEnumerable<PreguntaForGridView> GetAll();
        IEnumerable<PreguntaForGridView> GetbyFormId(int id);
        Pregunta Get(int id);
        ResponseHelper InsertOrUpdate(Pregunta model);
        ResponseHelper Delete(int id);

        ResponseHelper InsertOrUpdatePreguntaDetalle(PreguntaDetalle model);
        PreguntaDetalle GetPreguntaDetalle(int id);
        List<PreguntaDetalle> GetAllPreguntaDetalle(int id);
        ResponseHelper DeletePreguntaDetalle(int id);

    }
}