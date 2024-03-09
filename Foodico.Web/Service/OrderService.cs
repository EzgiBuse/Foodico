using Foodico.Web.Models;
using Foodico.Web.Service.IService;
using static Foodico.Web.Utility.Standard;

namespace Foodico.Web.Service
{
    public class OrderService :IOrderService
    {private readonly IBaseService _baseService;

        public OrderService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto> ValidateStripeSession(int orderId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {

                ApiType = ApiType.POST,
                Data = orderId,
                Url = OrderAPIBase + "/api/OrderAPI/ValidateStripeSession"
            });
        }

        public async Task<ResponseDto> CreateOrderAsync(OrderDto orderDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {

                ApiType = ApiType.POST,
                Data = orderDto,
                Url = OrderAPIBase + "/api/OrderAPI/CreateOrder"
            });
        }

        public async Task<ResponseDto> CreateStripreSessionAsync(StripeRequestDto stripeRequestDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {

                ApiType = ApiType.POST,
                Data = stripeRequestDto,
                Url = OrderAPIBase + "/api/OrderAPI/CreateStripeSession"
            });
        }
    }
}
