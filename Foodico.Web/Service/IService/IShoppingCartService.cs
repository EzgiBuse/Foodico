using Foodico.Web.Models;

namespace Foodico.Web.Service.IService
{
    public interface IShoppingCartService
    {
        Task<ResponseDto?> GetShoppingCartByUserIdAsync(string userId);
        Task<ResponseDto?> UpdateCartAysnc(CartDto cartDto);
        Task<ResponseDto?> RemoveFromCartAsync(int cartDetailsId);
        Task<ResponseDto?> ApplyCouponAsync(CartDto cartDto);
        Task<ResponseDto?> RemoveCouponAsync(CartDto cartDto);




    }
}
