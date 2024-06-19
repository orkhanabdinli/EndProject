using Microsoft.AspNetCore.Mvc;

namespace EndProject.MVC.Controllers
{
    public class UserSettings : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
