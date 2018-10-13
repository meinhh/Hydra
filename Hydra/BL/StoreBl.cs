using Hydra.DAL;
using Hydra.Data;
using Hydra.Models;
using System.Collections.Generic;

namespace Hydra.BL
{
    public class StoreBl
    {
        private readonly StoreDataAccess _storeDataAccess;

        public StoreBl(HydraContext hydraContext)
        {
            _storeDataAccess = new StoreDataAccess(hydraContext);
        }

        public List<Store> GetAllStores()
        {
            return _storeDataAccess.GetAllStores();
        }

        public IEnumerable<Store> GetStoreByName(string name)
        {
            return _storeDataAccess.GetStoreByName(name);
        }

        public void AddStore(Store store)
        {
            _storeDataAccess.AddStore(store);
        }

    }
}
