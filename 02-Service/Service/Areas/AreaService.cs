using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using Model.Custom.AreaGridView;
using Model.Domain.Area;
using NLog;
using Persistence.DbContextScope.Interfaces;
using Persistence.Repository;

namespace Service.Areas
{
    public class AreaService : IAreaService
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();
        private readonly IDbContextScopeFactory _dbContextScopeFactory;
        private readonly IRepository<Area> _areaRepository;

        public AreaService(IRepository<Area> areaRepository, IDbContextScopeFactory dbContextScopeFactory)
        {
            _areaRepository = areaRepository;
            _dbContextScopeFactory = dbContextScopeFactory;
        }

        #region Area

        public IEnumerable<AreaForGridView> GetAll()
        {
            var result = new List<AreaForGridView>();

            try
            {
                using (var ctx = _dbContextScopeFactory.CreateReadOnly())
                {                    
                    result = _areaRepository.GetAll()
                        .Select(x => new AreaForGridView()
                        {
                            IdArea = x.IdArea,
                            Name = x.Name,
                        }).ToList();
                }
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
            }

            return result;
        }
        public Area Get(Int64 id)
        {
            var result = new Area();

            try
            {
                using (var ctx = _dbContextScopeFactory.Create())
                {
                    result = _areaRepository.SingleOrDefault(x => x.IdArea == id);
                }
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
            }

            return result;
        }
        public ResponseHelper InsertOrUpdateArea(Area model)
        {
            var rh = new ResponseHelper();

            try
            {
                using (var ctx = _dbContextScopeFactory.Create())
                {
                    if (model.IdArea == 0)
                    {
                        _areaRepository.Insert(model);
                    }
                    else
                    {
                        _areaRepository.Update(model);
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
        public ResponseHelper Delete(Int64 id)
        {
            var rh = new ResponseHelper();

            try
            {
                using (var ctx = _dbContextScopeFactory.Create())
                {
                    var model = _areaRepository.SingleOrDefault(x => x.IdArea == id);
                    _areaRepository.Delete(model);

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
