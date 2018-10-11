using Hydra.DAL;
using Hydra.Data;
using Hydra.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public void AddStore(Store store)
        {
            _storeDataAccess.AddStore(store);
        }

    }
}
