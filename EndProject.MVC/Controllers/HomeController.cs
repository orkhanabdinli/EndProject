using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EndProject.MVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var token = HttpContext.Session.GetString("JWToken");
            var userId = HttpContext.Request.Cookies["UserId"];
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "User");
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        
    }
}
