using EndProject.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace EndProject.MVC.Controllers
{
    public class UserController : Controller
    {
        Uri baseAdress = new Uri("https://localhost:7032/api");
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClient _httpClient;

        public UserController(IHttpClientFactory httpClientFactory, HttpClient httpClient)
        {
            _httpClientFactory = httpClientFactory;
            _httpClient = httpClient;
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
            if (ModelState.IsValid)
            {
                var client = _httpClientFactory.CreateClient();
                var response = await client.PostAsJsonAsync(baseAdress + "/User/Login", loginViewModel);
                if (response.IsSuccessStatusCode)
                {
                    var tokenResponse = await response.Content.ReadFromJsonAsync<TokenViewModel>();
                    HttpContext.Session.SetString("JWToken", tokenResponse.AccesToken);
                    HttpContext.Response.Cookies.Append("UserId", tokenResponse.UserId);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    var errorResponse = await response.Content.ReadAsStringAsync();
                    var apiError = JsonConvert.DeserializeObject<ErrorViewModel>(errorResponse);
                    ModelState.AddModelError(string.Empty, apiError.Message);
                }
            }
            return View(loginViewModel);
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                var dataStr = JsonConvert.SerializeObject(registerViewModel);
                var stringContent = new StringContent(dataStr, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(baseAdress + "/User/Register", stringContent);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Login", "User");
                }
                else
                {
                    var errorResponse = await response.Content.ReadAsStringAsync();
                    var apiError = JsonConvert.DeserializeObject<ErrorViewModel>(errorResponse);
                    ModelState.AddModelError(string.Empty, apiError.Message);
                }
            }
            return View(registerViewModel);
        }

    }
}
