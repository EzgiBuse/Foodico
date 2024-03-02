using Foodico.Services.ShoppingCartAPI.Models.Dto;

namespace Foodico.Services.ShoppingCartAPI.Service.IService
{
    public interface ICouponService
    {
         Task<CouponDto> GetCoupon(string couponCode);
    }
}
