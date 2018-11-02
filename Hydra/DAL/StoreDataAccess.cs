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

        public void UpdateStore(Store storeToUpdate)
        {
            _hydraContext.Store.Update(storeToUpdate);
            _hydraContext.SaveChanges();
        }

        public Store GetStoreById(int id)
        {
            return _hydraContext.Store
                .Include(x => x.Stock)
                .ThenInclude(y => y.Product)
                .SingleOrDefault(x => x.ID == id);
        }

        public List<Store> GetAllStores()
        {
            return _hydraContext.Store
                .Include(x => x.Stock)
                .ThenInclude(y => y.Product)
                .ToList();
        }

        public IEnumerable<Store> GetStoreByName(string name)
        {
            return _hydraContext.Store
                                .Where(s => s.Name.Contains(name))
                                .ToList();
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
