using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Hydra.Models;
using Hydra.BL;
using Hydra.Data;
using System;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

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
            //InitializeProducts();

            ViewBag.Categories = Enum.GetValues(typeof(Category));
            return View(_productBl.GetAllProducts());
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


        private void InitializeProducts()
        {
            var fileEntries = Directory.GetFiles("./products");
            foreach (string fileName in fileEntries)
            {
                var json = System.IO.File.ReadAllText(fileName);
                var figures = JsonConvert.DeserializeObject<IEnumerable<Product>>(json);
                figures.First().Comments = new List<Comment>{new Comment{
                            Publisher = new User {
                                Name = "Alison Hendrix",
                                Gender = Gender.Female,
                                email = "clone@orphan.black",
                                IsManager = false,
                                BirthDate = new DateTime(1970,1,1)
                            },
                            Date = DateTime.Now.AddDays(-1),
                            Text = "This is the worst product i've ever seen"
                    }};
                var figuresNoId = figures.Select(x => new Product
                {
                    Name = x.Name,
                    Price = x.Price,
                    ImageUrl = x.ImageUrl,
                    Category = x.Category,
                    Description = x.Description,
                    Comments = x.Comments
                });
                _productBl.SaveProducts(figuresNoId);
            }
        }
    }
}
