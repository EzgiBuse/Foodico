using Foodico.Web.Models;
using Foodico.Web.Service.IService;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Foodico.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly IAuthService _authService;
        private readonly ITokenProvider _tokenProvider;

        public LoginController(IAuthService authService, ITokenProvider tokenProvider)
        {
            _authService = authService;
            _tokenProvider = tokenProvider;
        }

        [HttpGet]
        public IActionResult Login()
        {
            LoginRequestDto loginRequest = new();
            return View(loginRequest);

        }


        [HttpPost]
        public async Task<IActionResult> LoginUser(LoginRequestDto loginRequestDto)
        {

            ResponseDto responseDto = await _authService.LoginAsync(loginRequestDto);
            if (responseDto.IsSuccess)
            {
                LoginResponseDto loginResponse = JsonConvert.DeserializeObject<LoginResponseDto>(Convert.ToString(responseDto.Result));
                await SignInUser(loginResponse);
                _tokenProvider.SetToken(loginResponse.Token);
                TempData["NotificationType"] = "success";
                TempData["NotificationMessage"] = "Welcome!";
                
                return RedirectToAction("Index","Home");
            }
            else
            {
                TempData["NotificationType"] = "error";
                TempData["NotificationMessage"] = "Email or password is incorrect"; 
                
                return RedirectToAction("Login");
            }
            //return View();

        }

        private async Task SignInUser(LoginResponseDto loginResponseDto)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(loginResponseDto.Token);
            if(jwt!=null)
            {
                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                identity.AddClaim(new Claim(JwtRegisteredClaimNames.Email,
                    jwt.Claims.FirstOrDefault(x=>x.Type==JwtRegisteredClaimNames.Email).Value));
                identity.AddClaim(new Claim(JwtRegisteredClaimNames.Sub,
                    jwt.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub).Value));
                identity.AddClaim(new Claim(JwtRegisteredClaimNames.Name,
                    jwt.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Name).Value));

                identity.AddClaim(new Claim(ClaimTypes.Name,
                   jwt.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Email).Value));
                //identity.AddClaim(new Claim(ClaimTypes.Role,
                //   jwt.Claims.FirstOrDefault(x => x.Type == "role").Value));

                var principal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
               
            }

        }

       
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            _tokenProvider.ClearToken();
            return RedirectToAction("Index","Home");

        }

    }
}
