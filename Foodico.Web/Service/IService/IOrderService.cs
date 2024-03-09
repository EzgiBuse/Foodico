using Foodico.Web.Models;

namespace Foodico.Web.Service.IService
{
    public interface IOrderService
    {
       
        Task<ResponseDto?> CreateOrderAsync(OrderDto orderDto);
        Task<ResponseDto?>  CreateStripreSessionAsync(StripeRequestDto stripeRequestDto);
        Task<ResponseDto?>  ValidateStripeSession(int orderId);
    }
}
