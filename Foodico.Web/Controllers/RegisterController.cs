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
            //registrationRequestDto.Role = "CUSTOMER";
            //cont here
            ResponseDto result = await _authService.RegisterAsync(registrationRequestDto);
            if (result.IsSuccess)
            {
               
               return RedirectToAction("Login", "Login");
              
            }
            return View("Register","Register");

        }
    }
}
