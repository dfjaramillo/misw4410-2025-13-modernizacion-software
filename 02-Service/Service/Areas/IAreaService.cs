using System;
using System.Collections.Generic;
using Common;
using Model.Custom.AreaGridView;
using Model.Domain.Area;

namespace Service.Areas
{
    public interface IAreaService
    {
        IEnumerable<AreaForGridView> GetAll();
        Area Get(Int64 id);
        ResponseHelper InsertOrUpdateArea(Area model);
        ResponseHelper Delete(Int64 id);    
    }
}
