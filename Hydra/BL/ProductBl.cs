using Hydra.DAL;
using Hydra.Data;
using Hydra.Models;
using System.Collections.Generic;
using System;
using System.Linq;

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

        public Category? NaiveBayesFetchCategoryByName(string Name)
        {
            List<Product> products = _productDataAccess.GetAllProducts();
            double[] wordFoundArray = new double[Category.GetNames(typeof(Category)).Length];
            double[] categoryCountArray = new double[Category.GetNames(typeof(Category)).Length];

            // save in a counter array the number of times the name appeared in each categoty
            foreach (Product product in products)
            {
                categoryCountArray[(int)product.Category]++;

                if (product.Name.ToUpper().Contains(Name.ToUpper()))
                {
                    wordFoundArray[(int)product.Category]++;
                }
            }

            // getting the index of the most fitting category
            double max = 0;
            int maxIndex = 0;
            for (int i = 0; i < wordFoundArray.Length; i++)
            {
                if ((double)(wordFoundArray[i] / categoryCountArray[i]) > max)
                {
                    max = (double)(wordFoundArray[i] / categoryCountArray[i]);
                    maxIndex = i;
                }
            }

            if (max == 0)
            {
                return null;
            }

            // getting from maxIndex relevent category
            Category resultCategory = (Category)Category.Parse(typeof(Category), maxIndex.ToString());

            return resultCategory;
        }
    }
}
