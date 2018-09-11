﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Hydra.Models;
using Hydra.Data;
using Microsoft.EntityFrameworkCore;
using Hydra.DAL;

namespace Hydra.Controllers
{
    public class HomeController : Controller
    {
        private readonly HydraContext _hydraContext;
        private readonly StoreDataAccess _storeDataAccess; // TODO: create a BL class to use the _storeDataAccess

        public HomeController(HydraContext hydraContext)
        {
            _hydraContext = hydraContext;
            _storeDataAccess = new StoreDataAccess(hydraContext);
        }

        public IActionResult Index()
        {
            HowToUseHydraContext(); // TODO: remove once we have the needed logic
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private void HowToUseHydraContext() // TODO: remove once we have the needed logic
        {
            var product1 = new Product
            {
                Name = "Milk",
                Price = 5.0
            };

            var product2 = new Product
            {
                Name = "Bread",
                Price = 3.5
            };

            var customer = new User
            {
                Address = "somewhere over the rainbow",
                Name = "Toto",
                Phone = "0541112312"
            };

            var order = new Order
            {
                Buyer = customer,
                ProductsInStore = new List<ProductInStore> { new ProductInStore { Product = product1, Quantity = 2 } },
                Date = DateTime.Now,
                PaymentType = PaymentType.Bitcoin,
            };

            var store = new Store
            {
                Address = "israel",
                Orders = new List<Order> { order },
                Stock = new List<Stock> { new Stock { Product = product1, Quantity = 666 } }
            };

            _hydraContext.Add(product1);
            _hydraContext.Add(product2);
            _storeDataAccess.AddStore(store);
            _hydraContext.SaveChanges();
        }
    }
}
