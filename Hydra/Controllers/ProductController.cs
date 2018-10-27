using System;
using Hydra.BL;
using Hydra.Data;
using Hydra.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hydra.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductBl _productBl;

        public ProductController(HydraContext hydraContext)
        {
            _productBl = new ProductBl(hydraContext);
        }

        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
            ViewData["UserId"] = HttpContext.Session.GetString("ConnectedUserId");

            return View(_productBl.GetProductById(id));
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
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

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            // add checking for user kind
            var product = _productBl.GetProductById(id);
            if(product == null)
            {
                return RedirectToAction("Index", "Error", new { error = string.Format("Could not find product with id {0}", id) });
            }
            return View(product);
        }

        // POST: Product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("ID", "Name", "Price", "ImageUrl", "Category", "Description")] Product product)
        {
            try
            {
                // TODO: Add user logic here
                _productBl.UpdateProduct(product);
                return View(product);
            }
            catch
            {
                // add error page?
                return RedirectToAction("Index", "Error", new { error = string.Format("Could not find product with id {0}", id) });
            }
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            // add checking for user kind
            var product = _productBl.GetProductById(id);
            if (product == null)
            {
                return RedirectToAction("Index", "Error", new { error = string.Format("Could not find product with id {0}", id) });
            }
            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                var product = _productBl.GetProductById(id);
                try
                {
                    _productBl.DeleteProduct(product);
                }
                catch
                {
                    return RedirectToAction("Index", "Error", new { error = string.Format("Could not find product with id {0}", id) });
                }

                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return RedirectToAction("Index", "Error", new { error = string.Format("Oops! failed to delete product with id {0}", id) });
            }
        }
    }
}