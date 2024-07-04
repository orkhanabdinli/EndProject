﻿using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Net.Http;
using Newtonsoft.Json;
using EndProject.MVC.ViewModels;
using System.Text;

namespace EndProject.MVC.Controllers;

public class UserProfileController : Controller
{
    Uri baseAddress = new Uri("https://localhost:7032/api");
    private readonly HttpClient _httpClient;
    public UserProfileController(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var token = HttpContext.Session.GetString("JWToken");
        var userId = HttpContext.Request.Cookies["UserId"];
        if (string.IsNullOrEmpty(token))
        {
            return RedirectToAction("Login", "User");
        }


        var response = await _httpClient.GetAsync(baseAddress + "/UserProfile/UserAboutGet/" + userId);
        var json = await response.Content.ReadAsStringAsync();
        var userInfo = JsonConvert.DeserializeObject<UserProfileViewModel>(json);

        var response1 = await _httpClient.GetAsync(baseAddress + "/Post/GetMyPosts/" + userId);
        var json1 = await response1.Content.ReadAsStringAsync();
        var posts = JsonConvert.DeserializeObject<List<PostViewModel>>(json1);

        UserProfileViewModel viewModel = new UserProfileViewModel()
        {
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
            BackgroundImageUrl = userInfo.BackgroundImageUrl,
            Posts = posts.OrderByDescending(x => x.PostId).ToList()
    };
        return View(viewModel);
    }
}
