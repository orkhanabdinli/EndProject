﻿using EndProject.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EndProject.MVC.Controllers
{
    public class UserSettings : Controller
    {
        private readonly HttpClient _httpClient;
        public UserSettings(HttpClient httpClient)
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
            return View();
        }
    }
}
