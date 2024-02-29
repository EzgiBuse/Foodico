using Foodico.Web.Models;
using Foodico.Web.Service.IService;
using static Foodico.Web.Utility.Standard;

namespace Foodico.Web.Service
{
    public class AuthService : IAuthService
    {

        private readonly IBaseService _baseService;

        public AuthService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto?> AssignRoleAsync(RegistrationRequestDto registrationRequestDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {

                ApiType = ApiType.POST,
                Data = registrationRequestDto,
                Url = AuthAPIBase + "/api/authAPI/AssignRole"
            });
        }

        public async Task<ResponseDto?> LoginAsync(LoginRequestDto loginRequestDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {

                ApiType = ApiType.POST,
                Data = loginRequestDto,
                Url = AuthAPIBase + "/api/authAPI/login"
            }, withBearer: false);
        }

        public async Task<ResponseDto?> RegisterAsync(RegistrationRequestDto registrationRequestDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {

                ApiType = ApiType.POST,
                Data = registrationRequestDto,
                Url = AuthAPIBase + "/api/authAPI/register"
            }, withBearer: false);
        }
    }
}
