using Foodico.Services.ShoppingCartAPI.Models.Dto;

namespace Foodico.Services.ShoppingCartAPI.Service.IService
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProducts();
    }
}
