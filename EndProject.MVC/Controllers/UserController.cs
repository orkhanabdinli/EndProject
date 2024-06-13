using EndProject.MVC.Models;
using EndProject.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EndProject.MVC.Controllers
{
    public class UserController : Controller
    {
        Uri baseAdress = new Uri("https://localhost:7032/api");
        private readonly IHttpClientFactory _httpClientFactory;

        public UserController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            return;
        }
    }
}
