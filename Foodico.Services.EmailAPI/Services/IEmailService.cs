using Foodico.Services.EmailAPI.Models;
using Foodico.Services.EmailAPI.Models.Dto;

namespace Foodico.Services.EmailAPI.Services
{
    public interface IEmailService
    {
      
        Task SendOrderEmailAsync (string email);
    }
}
