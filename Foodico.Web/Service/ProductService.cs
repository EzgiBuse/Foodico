using Foodico.Web.Models;
using Foodico.Web.Service.IService;
using static Foodico.Web.Utility.Standard;

namespace Foodico.Web.Service
{
    public class ProductService :IProductService
    {
        private readonly IBaseService _baseService;

        public ProductService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        //public async Task<ResponseDto?> GetProductsByPage(ShopIndexRequestDto shopIndexRequestDto)
        //{

        //    return await _baseService.SendAsync(new RequestDto(){
        //        ApiType = ApiType.GET,
        //        Data = shopIndexRequestDto,
        //        Url = ProductAPIBase + "/api/ProductAPI/GetProductsForPage/"
        //    });
        //}

        public async Task<ResponseDto?> CreateProductAsync(ProductDto productDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {

                ApiType = ApiType.POST,
                Data = productDto,
                Url = ProductAPIBase + "/api/ProductAPI/"
            });
        }

        public async Task<ResponseDto?> DeleteProductAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {

                ApiType = ApiType.DELETE,
                Url = ProductAPIBase + "/api/ProductAPI/" + id
            });
        }

        public async Task<ResponseDto?> GetAllProductsAsync()
        {
            return await _baseService.SendAsync(new RequestDto()
            {

                ApiType = ApiType.GET,
                Url = ProductAPIBase + "/api/ProductAPI/"
            });
        }

        public async Task<ResponseDto?> GetProductByIdAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {

                ApiType = ApiType.GET,
                Url = ProductAPIBase + "/api/ProductAPI/" + id
            });
        }

        public async Task<ResponseDto?> UpdateProductAsync(ProductDto productDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {

                ApiType = ApiType.PUT,
                Data = productDto,
                Url = ProductAPIBase + "/api/ProductAPI/"
            });
        }
    }
}
