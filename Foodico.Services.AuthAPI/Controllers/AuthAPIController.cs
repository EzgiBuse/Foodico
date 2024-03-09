using Foodico.Services.AuthAPI.Models.Dto;
using Foodico.Services.AuthAPI.RabbitMQSender;
using Foodico.Services.AuthAPI.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Foodico.Services.AuthAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthAPIController : ControllerBase
    {
        private readonly IAuthService _authService;
        protected ResponseDto _responseDto;
        private readonly IRabbitMQAuthMessageSender _messageBus;
        private readonly IConfiguration _configuration;
        public AuthAPIController(IAuthService authService,IRabbitMQAuthMessageSender messageBus,IConfiguration configuration )
        {
            _authService = authService;
            _responseDto = new();
            _configuration = configuration;
            _messageBus = messageBus;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDto registrationRequestDto)
        {
            var errorMessage = await _authService.Register(registrationRequestDto);
            if (!string.IsNullOrEmpty(errorMessage))
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = errorMessage;
                return BadRequest(_responseDto);
            }
          
                return Ok(_responseDto);
            
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var loginresponse = await _authService.Login(loginRequestDto);
            if(loginresponse.User == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Username or Password is incorrect";
                return BadRequest(_responseDto);
            }
            _responseDto.Result= loginresponse;
          
            return Ok(_responseDto);
        }

        [HttpPost("assignRole")]
        public async Task<IActionResult> AssignRole([FromBody] RegistrationRequestDto registrationRequestDto)
        {

            var assignrolesuccess = await _authService.AssignRole(registrationRequestDto.Email, registrationRequestDto.Role.ToUpper());
            if (!assignrolesuccess)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Error";
                return BadRequest(_responseDto);
            }
           
            return Ok(_responseDto);
        }
    }
}
