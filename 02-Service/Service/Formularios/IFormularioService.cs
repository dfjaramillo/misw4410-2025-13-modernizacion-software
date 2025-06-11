using System.Collections.Generic;
using Common;
using Model.Custom.FormularioGridView;
using Model.Domain.Formulario;

namespace Service.Formularios
{
    public interface IFormularioService
    {
        IEnumerable<FormularioForGridView> GetAll();
        Formulario Get(int id);
        ResponseHelper InsertOrUpdate(Formulario model);
        ResponseHelper InsertOrUpdateFormDetalle(FormularioDetalle model);
        FormularioDetalle GetFormDetalle(int id);
        List<FormDetalleForGridView> GetAllFormDetalle(int id);
        ResponseHelper Delete(int id);
        ResponseHelper DeleteFormDetalle(int id);
    }
}