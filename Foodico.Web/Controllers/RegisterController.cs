using Foodico.Web.Models;
using Foodico.Web.Service.IService;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;

namespace Foodico.Web.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IAuthService _authService;

        public RegisterController(IAuthService authService)
        {
            _authService = authService;
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser(RegistrationRequestDto registrationRequestDto)
        {
            try
            {
                ResponseDto result = await _authService.RegisterAsync(registrationRequestDto);

                if (result.IsSuccess)
                {
                    TempData["NotificationType"] = "success";
                    TempData["NotificationMessage"] = "Registration successful";
                    return RedirectToAction("Login", "Login");
                }
                else
                {
                    TempData["NotificationType"] = "error";
                    TempData["NotificationMessage"] = "Registration failed";
                    return RedirectToAction("Register");
                }
            }
            catch (Exception ex)
            {
                
                TempData["NotificationType"] = "error";
                TempData["NotificationMessage"] = "An error occurred during registration. Please try again later.";
                return RedirectToAction("Register");
            }
        }
    }
}
