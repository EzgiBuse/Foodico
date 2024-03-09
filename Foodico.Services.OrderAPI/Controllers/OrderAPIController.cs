using AutoMapper;
using Foodico.Services.OrderAPI.Data;
using Foodico.Services.OrderAPI.Models;
using Foodico.Services.OrderAPI.Models.Dto;
using Foodico.Services.OrderAPI.RabbitMQSender;
using Foodico.Services.OrderAPI.Service.IService;
using Foodico.Services.OrderAPI.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Stripe;
using Stripe.Checkout;

namespace Foodico.Services.OrderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderAPIController : ControllerBase
    {
        public ResponseDto _response;
        private IMapper _mapper;
        private readonly AppDbContext _db;
        private readonly IRabbitMQAuthMessageSender _messageBus;
        private readonly IConfiguration _configuration;
        // private IProductService _productService;

        public OrderAPIController(IMapper mapper, AppDbContext db, IRabbitMQAuthMessageSender messageBus, IConfiguration configuration)
        {
            this._response = new ResponseDto();
            _mapper = mapper;
            _db = db;
            _configuration = configuration;
            _messageBus = messageBus;
           
        }


        [HttpPost("CreateStripeSession")]
        public async Task<ResponseDto> CreateStripeSession([FromBody]StripeRequestDto stripeRequestDto)
        {
            try
            {
                
                var options = new SessionCreateOptions
                {
                    SuccessUrl = stripeRequestDto.ApprovedUrl,
                    CancelUrl = stripeRequestDto.CancelUrl,
                    LineItems = new List<SessionLineItemOptions>(),
                    Mode="payment"
                    
                };

                var DiscountsObj = new List<SessionDiscountOptions>() {
                   new SessionDiscountOptions
                   {
                       Coupon=stripeRequestDto.Order.Cart.CartHeader.CouponCode
                   }
                };

                var productlist = stripeRequestDto.Order.Cart.CartDetails;
                foreach (var item in productlist)
                {
                    var p = item.Product.Price;
                    var sessionLineItem = new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmount = (long)(p * 100),
                            Currency = "usd",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = item.Product.Name,
                                
                            }

                        },
                        Quantity = item.Count,
                       
                    };
                    options.LineItems.Add(sessionLineItem);



                }

                if(stripeRequestDto.Order.Cart.CartHeader.Discount > 0)
                {
                    options.Discounts = DiscountsObj;
                }
                
                var service = new SessionService();
                Session session = service.Create(options);
                stripeRequestDto.StripeSessionUrl = session.Url;
                Order order = _mapper.Map<Order>(stripeRequestDto.Order);

                order.StripeSessionId = session.Id;
                stripeRequestDto.StripeSessionId = session.Id;
                _db.SaveChanges();
                _response.Result = stripeRequestDto;
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
                return _response;
            }
            string jsonMessage = JsonConvert.SerializeObject(stripeRequestDto.Order.Email);

            _messageBus.SendMessage(jsonMessage, "OrderConfirmationEmail");
            return _response;
        }

        [HttpPost("ValidateStripeSession")]
       
        public async Task<ResponseDto> ValidateStripeSession(int orderId)
        {
            try
            {

                Order order = await _db.Orders.FirstOrDefaultAsync(u => u.OrderId == orderId);
                var service = new SessionService();
                Session session = service.Get(order.StripeSessionId);

                PaymentIntentService paymentIntentService = new PaymentIntentService();
                PaymentIntent paymentIntent = paymentIntentService.Get(session.PaymentIntentId);

                if(paymentIntent.Status == "succeeded")
                {
                    order.Status = SD.Status_Approved;
                    order.PaymentIntentId = paymentIntent.Id;
                    await _db.SaveChangesAsync();
                    _response.Result = _mapper.Map<OrderDto>(order);
                }
                else
                {
                    _response.Result = false;
                }

            }catch(Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
                return _response;
            }
            return _response;
        }
    }
}
