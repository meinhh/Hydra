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
            //InitializeProducts(hydraContext);
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


        private void InitializeProducts(HydraContext hydraContext)
        {
            var mickeyMouse = new Product
            {
                Name = "Mickey Mouse",
                Category = Category.Disney,
                Description = "Mickey mouse figure",
                Price = 95,
                ImageUrl = "https://cdn.shopify.com/s/files/1/0552/1401/products/Mickey_POP_GLAM.jpg?v=1510191643"
            };

            var batGirl = new Product
            {
                Name = "Bat Girl",
                Category = Category.DC,
                Description = "Bat girl figure",
                Price = 90,
                ImageUrl = "https://cdn.shopify.com/s/files/1/0552/1401/products/13632_BatmanTV_Batgirl_POP_GLAM_HiRes.jpg?v=1490738932"
            };

            var ironMan = new Product
            {
                Name = "Ironman",
                Category = Category.Marvel,
                Description = "Iron man figure",
                Price = 75,
                ImageUrl = "https://cdn.shopify.com/s/files/1/0552/1401/products/26463_AvengersInfinityWar_IronMan_POP_GLAM.png?v=1519854776"
            };

            var allisonHendrix = new Product
            {
                Name = "Allison Hendrix",
                Category = Category.TV,
                Description = "Allison Hendrix figure",
                Price = 66.5,
                ImageUrl = "https://cdn.shopify.com/s/files/1/0552/1401/products/5033_Orphan_Black_Allison_hires.jpg?v=1510191796"
            };

            var freddyKrueger = new Product
            {
                Name = "Freddy Krueger",
                Category = Category.Movies,
                Description = "Freddy Krueger figure",
                Price = 70.5,
                ImageUrl = "https://cdn.shopify.com/s/files/1/0552/1401/products/FREDDY_POP_GLAM.jpg?v=1510191943"
            };

            var telAviv = new Store
            {
                Name = "Tel Aviv Pop",
                ClosingHour = "22:00",
                OpeningHour = "12:00",
                Latitude = 32.074031,
                Lontitude = 34.792868,
                Stock = new List<Stock>
                {
                    new Stock
                    {
                        Product = mickeyMouse,
                        Quantity = 5
                    },
                    new Stock
                    {
                        Product = allisonHendrix,
                        Quantity = 24
                    }
                }
            };

            var jerusalem = new Store
            {
                Name = "Jerusalem Pop",
                ClosingHour = "22:00",
                OpeningHour = "12:00",
                Latitude = 31.777820,
                Lontitude = 35.209204,
                Stock = new List<Stock>
                {
                    new Stock
                    {
                        Product = freddyKrueger,
                        Quantity = 5
                    },
                    new Stock
                    {
                        Product = mickeyMouse,
                        Quantity = 5
                    },
                    new Stock
                    {
                        Product = ironMan,
                        Quantity = 12
                    }
                }
            };

            var eilat = new Store
            {
                Name = "Eilat Pop",
                ClosingHour = "22:00",
                OpeningHour = "12:00",
                Latitude = 29.556008,
                Lontitude = 34.961806,
                Stock = new List<Stock>
                {
                    new Stock
                    {
                        Product = mickeyMouse,
                        Quantity = 125
                    },
                    new Stock
                    {
                        Product = batGirl,
                        Quantity = 32
                    }
                }
            };

            var meirav = new User
            {
                Gender = Gender.Female,
                Name = "Meirav Shenhar"
            };

            var gal = new User
            {
                Gender = Gender.Male,
                Name = "Gal Hen"
            };

            hydraContext.User.AddRange(meirav, gal);
            hydraContext.Store.AddRange(telAviv, jerusalem, eilat);
            hydraContext.SaveChanges();

            var fileEntries = Directory.GetFiles("./products");
            foreach (string fileName in fileEntries)
            {
                var json = System.IO.File.ReadAllText(fileName);
                var figures = JsonConvert.DeserializeObject<IEnumerable<Product>>(json);                
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
                figuresNoId.First().Comments = new List<Comment>{new Comment{
                            Publisher = new User {
                                Name = "Alison Hendrix",
                                Gender = Gender.Female
                            },
                            Date = DateTime.Now.AddDays(-1),
                            Text = "This is the worst product i've ever seen"
                    }};
                _productBl.UpdateProduct(figuresNoId.First());
            }
        }
    }
}
