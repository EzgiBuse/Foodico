using static Azure.Core.HttpHeader;

namespace Foodico.Services.AuthAPI.Models.Dto
{
    public class ResponseDto
    {//Common response dto for all API responses
        public object? Result { get; set; }
        public bool IsSuccess { get; set; } = true;
        public string Message { get; set; } = "";
    }
}
