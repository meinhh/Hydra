using Hydra.DAL;
using Hydra.Data;
using Hydra.Models;
using System.Collections.Generic;

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

        public void SaveProducts(IEnumerable<Product> products)
        {
            _productDataAccess.SaveProducts(products);
        }

        public void SaveProdcut(Product product)
        {
            _productDataAccess.SaveProducts(new List<Product> { product });
        }

        public void UpdateProduct(Product product)
        {
            _productDataAccess.UpdateProduct(product);
        }

        public void DeleteProduct(Product product)
        {
            _productDataAccess.DeleteProduct(product);
        }
    }
}
