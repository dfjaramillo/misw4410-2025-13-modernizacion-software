using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using Model.Custom.CargoGridView;
using NLog;
using Persistence.DbContextScope.Interfaces;
using Persistence.Repository;

namespace Service.Cargo
{
    public class CargoService : ICargoService
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();
        private readonly IDbContextScopeFactory _dbContextScopeFactory;
        private readonly IRepository<Model.Domain.Cargo.Cargo> _cargoRepository;

        public CargoService(IDbContextScopeFactory dbContextScopeFactory, IRepository<Model.Domain.Cargo.Cargo> cargoRepository)
        {
            _dbContextScopeFactory = dbContextScopeFactory;
            _cargoRepository = cargoRepository;
        }

        public ResponseHelper Delete(long id)
        {
            var rh = new ResponseHelper();

            try
            {
                using (var ctx = _dbContextScopeFactory.Create())
                {
                    var model = _cargoRepository.SingleOrDefault(x => x.CargoId == id);
                    _cargoRepository.Delete(model);

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

        public Model.Domain.Cargo.Cargo Get(long id)
        {
            var result = new Model.Domain.Cargo.Cargo();

            try
            {
                using (var ctx = _dbContextScopeFactory.Create())
                {
                    result = _cargoRepository.SingleOrDefault(x => x.CargoId == id);
                }
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
            }

            return result;
        }

        public IEnumerable<CargoForGridView> GetAll()
        {
            var result = new List<CargoForGridView>();

            try
            {
                using (var ctx = _dbContextScopeFactory.CreateReadOnly())
                {
                    result = _cargoRepository.GetAll()
                        .Select(x => new CargoForGridView()
                        {
                            CargoId = x.CargoId,
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

        public ResponseHelper InsertOrUpdateCargo(Model.Domain.Cargo.Cargo model)
        {
            var rh = new ResponseHelper();

            try
            {
                using (var ctx = _dbContextScopeFactory.Create())
                {
                    if (model.CargoId == 0)
                    {
                        _cargoRepository.Insert(model);
                    }
                    else
                    {
                        _cargoRepository.Update(model);
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
    }
}
