using EndProject.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace EndProject.MVC.Controllers;

public class UserInformationController : Controller
{
    Uri baseAddress = new Uri("https://localhost:7032/api");
    private readonly HttpClient _httpClient;
    public UserInformationController(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<IActionResult> Index()
    {
        var token = HttpContext.Session.GetString("JWToken");
        var userId = HttpContext.Request.Cookies["UserId"];
        if (string.IsNullOrEmpty(token))
        {
            return RedirectToAction("Login", "User");
        }


        var response = await _httpClient.GetAsync(baseAddress + "/UserProfile/UserAboutGet/" + userId);
        var json1 = await response.Content.ReadAsStringAsync();
        var userInfo = JsonConvert.DeserializeObject<UserProfileViewModel>(json1);

        UserProfileViewModel viewModel = new UserProfileViewModel()
        {
            Token = token,
            UserAboutId = userInfo.UserAboutId,
            UserId = userInfo.UserId,
            UserName = userInfo.UserName,
            FirstName = userInfo.FirstName,
            LastName = userInfo.LastName,
            Gender = userInfo.Gender,
            Bio = userInfo.Bio,
            Country = userInfo.Country,
            City = userInfo.City,
            ProfileImageUrl = userInfo.ProfileImageUrl,
            BackgroundImageUrl = userInfo.BackgroundImageUrl
        };
        return View(viewModel);
    }
}
