using Foodico.Services.ShoppingCartAPI.Models.Dto;
using Foodico.Services.ShoppingCartAPI.Service.IService;
using Newtonsoft.Json;

namespace Foodico.Services.ShoppingCartAPI.Service
{
    public class CouponService : ICouponService 
    {
        IHttpClientFactory _httpClientFactory;
        public CouponService(IHttpClientFactory clientFactory)
        {
            _httpClientFactory = clientFactory;
        }

      
        public async Task<CouponDto> GetCoupon(string couponCode)
        {
            var client = _httpClientFactory.CreateClient("Coupon");
            var response = await client.GetAsync("/api/CouponAPI/GetByCode/{couponCode}"); //check here
            var content = await response.Content.ReadAsStringAsync();
            var resp = JsonConvert.DeserializeObject<ResponseDto>(content);
            if (resp.IsSuccess)
            {
                return JsonConvert.DeserializeObject<CouponDto>(Convert.ToString(resp.Result));
            }
            return new CouponDto();
        }

       
    }
}
