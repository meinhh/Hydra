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
    public class CatalogController : Controller
    {
        private readonly HydraContext _hydraContext;
        private readonly ProductBl _productBl;
        private readonly StoreBl _storeBl;

        public CatalogController(HydraContext hydraContext)
        {
            _hydraContext = hydraContext;
            _productBl = new ProductBl(hydraContext);
            _storeBl = new StoreBl(hydraContext);
        }

        // GET: Catalog
        public ActionResult Index()
        {
            // put the stores in the view bag, so we can use them later
            ViewBag.stores = _storeBl.GetAllStores();
            return View(_productBl.GetAllProducts());
        }

        [HttpPost]
        public ActionResult GetProductsByStoreName(string name)
        {
            List<Store> stores = _storeBl.GetAllStores();
            List<Product> filteredProducts = new List<Product>();
            Store choosenStore = stores.Find(s => s.Name == name);

            foreach (Stock currStoke in choosenStore.Stock)
            {
                filteredProducts.Add(currStoke.Product);
            }

            return View(filteredProducts);
        }


        // GET: Catalog
        public ActionResult GetAllStores()
        {
            return View(_storeBl.GetAllStores());
        }


        // GET: Catalog/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Catalog/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Catalog/Create
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

        // GET: Catalog/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Catalog/Edit/5
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

        // GET: Catalog/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Catalog/Delete/5
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
    }
}