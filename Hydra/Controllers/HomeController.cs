using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Hydra.Models;
using Hydra.BL;
using Hydra.Data;
using System;

namespace Hydra.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProductBl _productBl;

        public HomeController(HydraContext hydraContext)
        {            
            _productBl = new ProductBl(hydraContext);
        }

        public IActionResult Index()
        {
            ViewBag.Categories = Enum.GetValues(typeof(Category));
            return View(_productBl.GetAllProducts());
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Hydra pop!";
            ViewData["ShareUrl"] = "https://www.quertime.com/article/how-facebook-steals-sells-your-private-information/";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Please Contact Us";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }        
    }
}
