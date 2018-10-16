using Hydra.DAL;
using Hydra.Data;
using Hydra.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hydra.BL
{
    public class ProductBl
    {
        private readonly ProductDataAccess _productDataAccess;

        public ProductBl(HydraContext hydraContext)
        {
            _productDataAccess = new ProductDataAccess(hydraContext);
        }

        public List<Product> GetAllProducts()
        {
            return _productDataAccess.GetAllProducts();
        }

        public Product GetProductById(int productId)
        {
            return _productDataAccess.GetProductById(productId);
        }

        public IEnumerable<Product> GetProductsByCategory(Category category)
        {
            return _productDataAccess.GetProductsByCategory(category);
        }

        public void SaveProducts(List<Product> products)
        {
            _productDataAccess.SaveProducts(products);
        }
    }
}
