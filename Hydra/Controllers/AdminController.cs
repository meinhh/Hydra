using Hydra.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Hydra.Controllers
{
    public class AdminController : Controller
    {
        private readonly AdminCredentials _settings;

        public AdminController(IOptions<AppSettings> settings)
        {
            _settings = settings.Value.AdminCredentials;
        }

        public ActionResult Index()
        {
            return HttpContext.Session.GetInt32("IsAdminConnected") == 1
                ? View() 
                : View("Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(IFormCollection form)
        {
            if (ModelState.IsValid)
            {
                var username = form["Username"].ToString() ?? string.Empty;
                var password = form["Password"].ToString() ?? string.Empty;

                if (username == _settings.UserName && password == _settings.Password)
                    return UpdateAdminState(true);
            }

            ModelState.AddModelError("", "Invalid username or password!");
            return View();
        }

        [HttpPost]
        public ActionResult Logout()
        {
            return UpdateAdminState(false);
        }

        private ActionResult UpdateAdminState(bool isConnected)
        {
            HttpContext.Session.SetInt32("IsAdminConnected", isConnected ? 1 : 0);
            return RedirectToAction("Index", "Admin");
        }
    }
}
