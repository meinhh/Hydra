using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hydra.BL;
using Hydra.DAL;
using Hydra.Data;
using Hydra.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hydra.Controllers
{
    public class StoreController : Controller
    {
        private readonly HydraContext _hydraContext;
        private readonly StoreBl _storeBl;

        public StoreController(HydraContext hydraContext)
        {
            _hydraContext = hydraContext;
            _storeBl = new StoreBl(hydraContext);
        }


        // GET: Store
        public ActionResult Index()
        {
            // HowToUseHydraContext();
            return View(_storeBl.GetAllStores());
        }

        // GET: Store/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Store/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Store/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Store/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Store/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Store/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Store/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
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
                
                Orders = new List<Order> { order },
                Stock = new List<Stock> { new Stock { Product = product1, Quantity = 666 } }
            };

            _hydraContext.Add(product1);
            _hydraContext.Add(product2);
            _storeBl.AddStore(store);
            _hydraContext.SaveChanges();
        }
    }
}