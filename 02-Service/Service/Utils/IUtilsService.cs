using System.Collections.Generic;
using Model.Custom.Utils;

namespace Service.Utils
{
    public interface IUtilsService
    {
        IEnumerable<GenericItemModel> GetAllbyCatalogId(int id);
        GenericItemModel Get(int id);        
        IEnumerable<CatalogModel> GetAllEvaluacionDetalle(int id);
        CatalogModel GetEvaluacionDetalle(int id);        
        IEnumerable<GenericItemInfoModel> GetAllEvaluacionConfig(int id);
        GenericItemInfoModel GetEvaluacionConfig(int id);                
    }
}