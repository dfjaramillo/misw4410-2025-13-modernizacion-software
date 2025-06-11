using System;
using System.Collections.Generic;
using Common;
using Model.Custom.CargoGridView;

namespace Service.Cargo
{
    public interface ICargoService
    {
        IEnumerable<CargoForGridView> GetAll();
        Model.Domain.Cargo.Cargo Get(Int64 id);
        ResponseHelper InsertOrUpdateCargo(Model.Domain.Cargo.Cargo model);
        ResponseHelper Delete(Int64 id);
    }
}
