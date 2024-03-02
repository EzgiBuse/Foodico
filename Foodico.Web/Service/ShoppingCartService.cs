using Foodico.Web.Models;
using Foodico.Web.Service.IService;
using static Foodico.Web.Utility.Standard;

namespace Foodico.Web.Service
{
    public class ShoppingCartService :IShoppingCartService
    {private readonly IBaseService _baseService;

        public ShoppingCartService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto?> ApplyCouponAsync(CartDto cartDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {

                ApiType = ApiType.POST,
                Data = cartDto,
                Url = ShoppingCartAPIBase + "/api/ShoppingCartAPI/ApplyCoupon"
            });
        }
        public async Task<ResponseDto?> RemoveCouponAsync(CartDto cartDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {

                ApiType = ApiType.POST,
                Data = cartDto,
                Url = ShoppingCartAPIBase + "/api/ShoppingCartAPI/RemoveCoupon"
            });
        }


        public async Task<ResponseDto?> GetShoppingCartByUserIdAsync(string userId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {

                ApiType = ApiType.GET,
                Url = ShoppingCartAPIBase + "/api/ShoppingCartAPI/GetCart/" + userId
            });
        }

        public async Task<ResponseDto?> RemoveFromCartAsync(int cartDetailsId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {

                ApiType = ApiType.POST,
                Data = cartDetailsId,
                Url = ShoppingCartAPIBase + "/api/ShoppingCartAPI/RemoveCart"
            });
        }

        public async Task<ResponseDto?> UpdateCartAysnc(CartDto cartDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {

                ApiType = ApiType.POST,
                Data = cartDto,
                Url = ShoppingCartAPIBase + "/api/ShoppingCartAPI/UpdateCart"
            });
        }

       
    }
}
