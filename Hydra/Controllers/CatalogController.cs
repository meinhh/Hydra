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
        private readonly ProductBl _productBl;

        public CatalogController(HydraContext hydraContext)
        {
            _productBl = new ProductBl(hydraContext);
        }

        public ActionResult ByCategory(Category category)
        {
            return View("Views/Catalog/index.cshtml", _productBl.GetProductsByCategory(category));
        }

        public ActionResult Search(Category category, double from, double to)
        {
            try
            {
                if (from <= 0 || to <= 0)
                {
                    return RedirectToAction("Index", "Error", new { error = "from or to cant be negative or zero" });
                }

                if (from > to)
                {
                    return RedirectToAction("Index", "Error", new { error = "from range cant be higer than to" });
                }

                var products = _productBl.GetProductsByCategory(category)
                    .Where(p => p.Price <= to && p.Price >= from);
                return View("Views/Catalog/index.cshtml", products);
            }
            catch
            {
                return RedirectToAction("Index", "Error");
            }
        }

        // GET: Catalog
        public ActionResult Index()
        {
            return View(_productBl.GetAllProducts());
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