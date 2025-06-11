using System;
using System.Collections.Generic;
using System.Linq;
using Model.Auth;
using Model.Custom.Utils;
using Model.Domain.Utils;
using NLog;
using Persistence.DbContextScope.Interfaces;
using Persistence.Repository;

namespace Service.Utils
{
    public class UtilsService : IUtilsService
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();
        private readonly IDbContextScopeFactory _dbContextScopeFactory;
        private readonly IRepository<Item> _itemRepository;
        private readonly IRepository<Catalogo> _catalogoRepository;        
        private readonly IRepository<ApplicationUser> _applicationUser;
        private readonly IRepository<ApplicationRole> _applicationRole;

        public UtilsService(
            IDbContextScopeFactory dbContextScopeFactory,
            IRepository<Item> itemRepository,
            IRepository<Catalogo> catalogoRepository,            
            IRepository<ApplicationUser> applicationUser,
            IRepository<ApplicationRole> applicationRole
        )
        {
            _dbContextScopeFactory = dbContextScopeFactory;
            _itemRepository = itemRepository;
            _catalogoRepository = catalogoRepository;            
            _applicationUser = applicationUser;
            _applicationRole = applicationRole;
        }

        public IEnumerable<GenericItemModel> GetAllbyCatalogId(int id)
        {
            var result = new List<GenericItemModel>();

            try
            {
                using (var ctx = _dbContextScopeFactory.CreateReadOnly())
                {
                    result = _itemRepository.Find(x=>x.IdCatalogo==id)
                        .Select(x => new GenericItemModel()
                        {
                            ItemId = x.IdItem,
                            ItemName = x.ItemName,
                            ItemType = new CatalogModel()
                            {
                                ItemId = x.IdItem,
                                ItemName = x.ItemName,
                                CatalogId = x.IdCatalogo,                                
                            }
                            
                        }).ToList();
                }
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
            }

            return result;
        }

        public GenericItemModel Get(int id)
        {
            var result = new GenericItemModel();

            try
            {
                using (var ctx = _dbContextScopeFactory.Create())
                {
                    var oItem = _itemRepository.FirstOrDefault(x => x.IdItem == id);

                    result.ItemId = oItem.IdItem;
                    result.ItemName = oItem.ItemName;                                       
                }
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
            }

            return result;
        }

        public IEnumerable<CatalogModel> GetAllEvaluacionDetalle(int id)
        {
            throw new NotImplementedException();
        }

        public CatalogModel GetEvaluacionDetalle(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GenericItemInfoModel> GetAllEvaluacionConfig(int id)
        {
            throw new NotImplementedException();
        }

        public GenericItemInfoModel GetEvaluacionConfig(int id)
        {
            throw new NotImplementedException();
        }

    }
}
