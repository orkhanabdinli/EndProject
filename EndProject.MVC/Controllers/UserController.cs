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
            var dataStr = JsonConvert.SerializeObject(loginViewModel);
            var stringContent = new StringContent(dataStr, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(baseAdress + "/User/Login", stringContent);

            if (response.IsSuccessStatusCode)
            {
                var tokenResponse = await response.Content.ReadFromJsonAsync<TokenViewModel>();
                if (tokenResponse != null)
                {
                    HttpContext.Session.SetString("JWToken", tokenResponse.AccesToken);
                    HttpContext.Response.Cookies.Append("UserId", tokenResponse.UserId);
                    TempData["Successed"] = "Login successful!";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid response from server.");
                }
            }
            else
            {
                var errorResponse = await response.Content.ReadAsStringAsync();
                var apiError = JsonConvert.DeserializeObject<ErrorViewModel>(errorResponse);
                ModelState.AddModelError(string.Empty, apiError.Message);
            }
            return View(loginViewModel);
        }
    }
}
