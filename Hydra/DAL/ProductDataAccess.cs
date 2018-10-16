﻿using Hydra.Data;
using Hydra.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hydra.DAL
{
    public class ProductDataAccess
    {
        private readonly HydraContext _hydraContext;

        public ProductDataAccess(HydraContext hydraContext)
        {
            _hydraContext = hydraContext;
        }

        public List<Product> GetAllProducts()
        {
            return _hydraContext.Product.ToList();
        }

        public Product GetProductById(int productId)
        {
            return _hydraContext.Find<Product>(productId);
        }

        public IEnumerable<Product> GetProductsByCategory(Category category)
        {
            return _hydraContext.Product
                    .Where(p => p.Category == category)
                    .AsEnumerable();
        }

        public void SaveProducts(List<Product> products)
        {
            _hydraContext.Product.AddRange(products);
            _hydraContext.SaveChanges();
        }
    }
}
