using Foodico.Services.ShoppingCartAPI.Models.Dto;
using Foodico.Services.ShoppingCartAPI.Service.IService;
using Newtonsoft.Json;

namespace Foodico.Services.ShoppingCartAPI.Service
{
    public class ProductService : IProductService
    {
        private readonly IHttpClientFactory _httpClientFactory;
       
      

        public ProductService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            
        }

        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            var client = _httpClientFactory.CreateClient("Product");
            var response = await client.GetAsync("/api/ProductAPI"); //check here
            var content = await response.Content.ReadAsStringAsync();
            var resp = JsonConvert.DeserializeObject<ResponseDto>(content);
            if (resp.IsSuccess)
            {
                return JsonConvert.DeserializeObject<IEnumerable<ProductDto>>(Convert.ToString(resp.Result));
            }
            return new List<ProductDto>();
        }

        //public async Task<ProductDto> GetProductById(int id)
        //{
        //    var response = await _client.GetAsync($"/api/products/{id}");
        //    var content = await response.Content.ReadAsStringAsync();
        //    var product = JsonConvert.DeserializeObject<ProductDto>(content);
        //    return product;
        //}   
    }
}
