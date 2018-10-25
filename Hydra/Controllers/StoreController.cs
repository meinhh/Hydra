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
                Latitude = 31.776555,
                Lontitude = 35.234390,
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
                Name = "Meirav Shenhar",
                Gender = Gender.Female
            };

            var gal = new User
            {
                Name = "Gal Hen",
                Gender = Gender.Male
            };
             
            var comments = new List<Comment>
            {
                new Comment
                {
                    Date = new DateTime(2018, 9, 27),
                    Publisher = meirav,
                    Text = "Hydra pop is awsome!!"
                },
                new Comment
                {
                    Date = new DateTime(2018, 10, 16),
                    Publisher = gal,
                    Text = "We all love hydra pop ♥"
                },
                new Comment
                {
                    Date = new DateTime(2018, 10, 16),
                    Publisher = meirav,
                    Text = "Disney pop figures are the best"
                }
            };

            _hydraContext.User.AddRange(meirav, gal);
            _hydraContext.Store.AddRange(telAviv, jerusalem, eilat);
            _hydraContext.Comment.AddRange(comments);

            _hydraContext.SaveChanges();
        }
    }
}