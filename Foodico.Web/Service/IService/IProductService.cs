using Foodico.Web.Models;

namespace Foodico.Web.Service.IService
{
    public interface IProductService
    {
        //Task<ResponseDto?> GetProductsByPage(ShopIndexRequestDto shopIndexRequestDto);
        Task<ResponseDto?> GetAllProductsAsync();
        Task<ResponseDto?> GetProductByIdAsync(int id);
        Task<ResponseDto?> CreateProductAsync(ProductDto ProductDto);
        Task<ResponseDto?> UpdateProductAsync(ProductDto ProductDto);
        Task<ResponseDto?> DeleteProductAsync(int id);
    }
}
