using Hydra.Data;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;

namespace Hydra.Controllers
{


    public class StatisticsController : Controller
    {
        private HydraContext _hydraContext;

        public StatisticsController(HydraContext hydraContext)
        {
            _hydraContext = hydraContext;
        }

        public PartialViewResult GetStatistics()
        {
            var productsByStore = _hydraContext.Store
                .Select(s => new { store = s.Name, count = s.Stock.Count() })
                .ToList();

            var prodcutsByCategory = _hydraContext.Product
                .GroupBy(p => p.Category)
                .Select(x => new { category = x.Key.ToString(), count = x.Count() })
                .ToList();

            var commentsByGender = _hydraContext.Comment
                .GroupBy(c => c.Publisher.Gender)
                .Select(x => new { gender = x.Key.ToString(), count = x.Count() });

            ViewBag.prodcutsByCategory = JsonConvert.SerializeObject(prodcutsByCategory);
            ViewBag.productsByStore = JsonConvert.SerializeObject(productsByStore);
            ViewBag.commentsByGender = JsonConvert.SerializeObject(commentsByGender);

            return PartialView("Views/Statistics/Statistics.cshtml");
        }
    }
}