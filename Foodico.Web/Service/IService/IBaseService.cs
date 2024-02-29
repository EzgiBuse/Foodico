using Foodico.Web.Models;

namespace Foodico.Web.Service.IService
{
    public interface IBaseService
    {
        Task<ResponseDto?> SendAsync(RequestDto requestDto, bool withBearer= true);
    }
}
