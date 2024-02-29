using Foodico.Services.AuthAPI.Data;
using Foodico.Services.AuthAPI.Models.Dto;
using Foodico.Services.AuthAPI.Service.IService;
using Microsoft.AspNetCore.Identity;
using System.Reflection.Metadata.Ecma335;

namespace Foodico.Services.AuthAPI.Service
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthService(AppDbContext db, IJwtTokenGenerator jwtTokenGenerator, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _jwtTokenGenerator = jwtTokenGenerator;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            var user = await _userManager.FindByEmailAsync(loginRequestDto.Email);
            bool isvalid = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);

            if(isvalid==false || user==null)
            {//if user was not found
                return new LoginResponseDto() { User = null, Token = "" };
            }
            var roles = await _userManager.GetRolesAsync(user);
            var token = _jwtTokenGenerator.GenerateToken(user,roles);
            UserDto userdto = new()
            {
                Email = user.Email,
                ID = user.Id,
                Name = user.UserName,
                PhoneNumber = user.PhoneNumber
            };

            LoginResponseDto loginResponseDto = new LoginResponseDto()
            {
                User = userdto,
                Token = token
            };
            return loginResponseDto;
        }

        public async Task<string> Register(RegistrationRequestDto registrationRequestDto)
        {
            //Registering a new user to the system
            IdentityUser user = new()
            {
                UserName = registrationRequestDto.Name,
                Email = registrationRequestDto.Email,
                NormalizedEmail = registrationRequestDto.Email.ToUpper(),
                PhoneNumber = registrationRequestDto.PhoneNumber
              };

            try
            {
                var result = await _userManager.CreateAsync(user, registrationRequestDto.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "CUSTOMER");
                    var registeredUser = await _userManager.FindByEmailAsync(user.Email);

                    UserDto userDto = new()
                    {
                        Email = registeredUser.Email,
                        ID = registeredUser.Id,
                        Name = registeredUser.UserName,
                        PhoneNumber = registeredUser.PhoneNumber

                    };

                    return "";
                }
            }
            catch (Exception x)
            {
                return x.Message;
            }
            return "Error Encountered";
        }

         async Task<bool> IAuthService.AssignRole(string email, string roleName)
        {
            var user = _userManager.FindByEmailAsync(email).GetAwaiter().GetResult();
            if (user != null)
            {
                if (_roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult())
                {
                    _roleManager.CreateAsync(new IdentityRole(roleName)).GetAwaiter().GetResult();
                }
                await _userManager.AddToRoleAsync(user,roleName);
                return true;
            }
            return false;
        }
    }
}
