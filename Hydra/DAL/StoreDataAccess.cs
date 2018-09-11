using Hydra.Data;
using Hydra.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Hydra.DAL
{
    public class StoreDataAccess
    {
        private readonly HydraContext _hydraContext;

        public StoreDataAccess(HydraContext hydraContext)
        {
            _hydraContext = hydraContext;
        }

        public List<Store> GetAllStores()
        {
            return _hydraContext.Store
                .Include(x => x.Stock)
                .Include(x => x.Orders)
                .ToList();
        }

        public Store GetStroeById(int storeId)
        {
            return _hydraContext.Store
                .Include(x => x.Stock)
                .Include(x => x.Orders)
                .SingleOrDefault(store => store.ID == storeId);
        }

        public void AddStore(Store store)
        {
            _hydraContext.Store.Add(store);
            _hydraContext.SaveChanges();
        }

        public void DeleteStore(Store store)
        {
            _hydraContext.Store.Remove(store);
            _hydraContext.SaveChanges();
        }
    }
}
