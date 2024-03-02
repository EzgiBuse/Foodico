using Foodico.Web.Models;
using Foodico.Web.Service.IService;
using IdentityModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;

namespace Foodico.Web.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartService _shoppingCartService;
        private readonly ICouponService _couponService;

        public ShoppingCartController(IShoppingCartService shoppingCartService, ICouponService couponService)
        {
            _shoppingCartService = shoppingCartService;
            _couponService = couponService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await LoadCart());
        }

        public async Task<IActionResult> CheckoutIndex()
        {   OrderDto orderDto = new OrderDto();
            orderDto.Cart = await LoadCart();
            return View("Checkout", orderDto);
        }

        public async Task<IActionResult> PaymentIndex(OrderDto orderDto,string cart)
        {
            CartDto cartDto = Newtonsoft.Json.JsonConvert.DeserializeObject<CartDto>(cart);
            orderDto.Cart = cartDto;
            return View("Checkout", orderDto);
        }

        private async Task<CartDto> LoadCart()
        {
            var userId = User.Claims.Where(x=>x.Type==JwtRegisteredClaimNames.Sub).FirstOrDefault()?.Value;
            ResponseDto response = await _shoppingCartService.GetShoppingCartByUserIdAsync(userId);
            if (response != null && response.IsSuccess)
            {
                CartDto cartDto = JsonConvert.DeserializeObject<CartDto>(Convert.ToString(response.Result));
                var couponDto = await _couponService.GetCouponAsync(cartDto.CartHeader.CouponCode);
                if (couponDto != null && couponDto.IsSuccess)
                {
                    var couponCode = JsonConvert.DeserializeObject<CouponDto>(Convert.ToString(couponDto.Result));
                    cartDto.CartHeader.Discount = couponCode.DiscountAmount;
                }
                
                return cartDto;
            }
            return new CartDto();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UpdateCart(ProductDto productDto, int Quantity)
        {
            CartDto cartDto = new CartDto()
            {
                CartHeader = new CartHeaderDto
                {
                    UserId = User.Claims.Where(x => x.Type == JwtClaimTypes.Subject).FirstOrDefault()?.Value
                }
            };
            CartDetailsDto cartDetails = new CartDetailsDto
            {
                Count = Quantity,
                ProductId = productDto.ProductId,
            };

            List<CartDetailsDto> cartDetailsList = new() { cartDetails };
            cartDto.CartDetails = cartDetailsList;

            // If the product is not in the cache, retrieve it from the service
            ResponseDto? response = await _shoppingCartService.UpdateCartAysnc(cartDto);

            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Item added to cart successfully";
                return RedirectToAction("ProductDetails", "Shop", new { id = productDto.ProductId });
            }
            else
            {
                TempData["error"] = "Error occured while adding item to cart";

            }
            return RedirectToAction("ProductDetails", "Shop", new { id = productDto.ProductId });



        }

        
        [Authorize]
        public async Task<IActionResult> RemoveItem(int cartDetailsId)
        {
            var userId = User.Claims.Where(x => x.Type == JwtRegisteredClaimNames.Sub).FirstOrDefault()?.Value;
            ResponseDto response = await _shoppingCartService.RemoveFromCartAsync(cartDetailsId);
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Item removed from cart successfully";
                return RedirectToAction(nameof(Index));

            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> ApplyCoupon(CartDto cartDto)
        {
            var userId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value;
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            // Check if the coupon code is valid
            var couponResponse = await _couponService.GetCouponAsync(cartDto.CartHeader.CouponCode);
            if (couponResponse == null)
            {
                // Handle invalid coupon code
                TempData["error"] = "Invalid coupon code";
                return RedirectToAction(nameof(Index));
            }

            // Deserialize the coupon object
            var coupon = JsonConvert.DeserializeObject<CouponDto>(Convert.ToString(couponResponse.Result));

           
            // Apply the coupon
            var response = await _shoppingCartService.ApplyCouponAsync(cartDto);

            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RemoveCoupon(CartDto cartDto)
        {
            var userId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value;
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await _shoppingCartService.RemoveCouponAsync(cartDto);

            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
    }
}
