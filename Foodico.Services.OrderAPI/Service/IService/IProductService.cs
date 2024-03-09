using Foodico.Services.OrderAPI.Models.Dto;

namespace Foodico.Services.OrderAPI.Service.IService
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProducts();
    }
}
