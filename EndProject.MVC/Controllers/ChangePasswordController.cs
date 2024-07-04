using EndProject.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EndProject.MVC.Controllers
{
    public class ChangePasswordController : Controller
    {
        public IActionResult Index()
        {
            var token = HttpContext.Session.GetString("JWToken");
            var userId = HttpContext.Request.Cookies["UserId"];
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "User");
            }
            ChangePasswordViewModel changePasswordViewModel = new ChangePasswordViewModel();
            changePasswordViewModel.UserId = userId;
            changePasswordViewModel.Token = token;
            return View(changePasswordViewModel);
        }
    }
}
