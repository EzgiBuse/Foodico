using Foodico.Web.Models;
using Foodico.Web.Service.IService;
using IdentityModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using static System.Net.WebRequestMethods;

namespace Foodico.Web.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartService _shoppingCartService;
        private readonly ICouponService _couponService;
        private readonly IOrderService _orderService;
        private readonly IAuthService _authService;

        public ShoppingCartController(IShoppingCartService shoppingCartService, ICouponService couponService, IOrderService orderService, IAuthService authService)
        {
            _shoppingCartService = shoppingCartService;
            _couponService = couponService;
            _orderService = orderService;
            _authService = authService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await LoadCart());
        }

        public async Task<IActionResult> CheckoutIndex()
        {
            try
            {
                // Load the cart asynchronously
                var cart = await LoadCart();

                // Ensure the cart is not null before creating the OrderDto
                if (cart == null)
                {
                    // Handle the case where the cart could not be loaded
                    TempData["NotificationType"] = "error";
                    TempData["NotificationMessage"] = "Failed to load the cart. Please try again later.";
                    return RedirectToAction("Index", "Home");
                }

                // Create the OrderDto and populate the Cart property
                var orderDto = new OrderDto
                {
                    Cart = cart
                };

                // Return the Checkout view with the populated OrderDto
                return View("Checkout", orderDto);
            }
            catch (Exception ex)
            {
                
                return RedirectToAction("Error", "Home");
            }
        }

       

        public async Task<IActionResult> PaymentIndex(OrderDto orderDto,string cart)
        {
            try
            {

                CartDto cartDto = Newtonsoft.Json.JsonConvert.DeserializeObject<CartDto>(cart);
                orderDto.Cart = cartDto;
                orderDto.OrderTime = DateTime.Now;
                orderDto.Status = "Pending";
                orderDto.Email = User.Claims.Where(x => x.Type == JwtRegisteredClaimNames.Email).FirstOrDefault()?.Value;
                var domain = Request.Scheme + "://" + Request.Host.Value + "/";

                StripeRequestDto stripeRequestDto = new StripeRequestDto
                {
                    // ApprovedUrl= domain+"ShoppingCart/Confirmation?orderId="+orderDto.OrderId,
                    ApprovedUrl = "https://localhost:7079",
                    CancelUrl = "https://localhost:7079",
                    Order = orderDto,

                };

                var stripeResponse = await _orderService.CreateStripreSessionAsync(stripeRequestDto);
                StripeRequestDto stripeResponseResult = JsonConvert.DeserializeObject<StripeRequestDto>(Convert.ToString(stripeResponse.Result));
                Response.Headers.Add("Location", stripeResponseResult.StripeSessionUrl);
                return new StatusCodeResult(303);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                
            }
        }

       

        private async Task<CartDto> LoadCart()
        {
            try
            {

                var userId = User.Claims.Where(x => x.Type == JwtRegisteredClaimNames.Sub).FirstOrDefault()?.Value;
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
            catch (Exception ex)
            {

                return new CartDto();
            }
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
                TempData["NotificationType"] = "success";
                TempData["NotificationMessage"] = "Item added to cart successfully";
               
                return RedirectToAction("ProductDetails", "Shop", new { id = productDto.ProductId });
            }
            else
            {
                TempData["NotificationType"] = "error";
                TempData["NotificationMessage"] = "Error occured while adding item to cart";
               

            }
            return RedirectToAction("ProductDetails", "Shop", new { id = productDto.ProductId });



        }

        
        [Authorize]
        public async Task<IActionResult> RemoveItem(int cartDetailsId)
        {
            try
            {
                var userId = User.Claims.Where(x => x.Type == JwtRegisteredClaimNames.Sub).FirstOrDefault()?.Value;
                ResponseDto response = await _shoppingCartService.RemoveFromCartAsync(cartDetailsId);
                if (response != null && response.IsSuccess)
                {
                    TempData["NotificationType"] = "success";
                    TempData["NotificationMessage"] = "Item removed from cart successfully";
                   
                    return RedirectToAction(nameof(Index));

                }
                TempData["NotificationType"] = "error";
                TempData["NotificationMessage"] = "Item removal failed";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["NotificationType"] = "error";
                TempData["NotificationMessage"] = "Item removal failed";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public async Task<IActionResult> ApplyCoupon(CartDto cartDto)
        {
            try
            {
                var userId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value;
                var accessToken = await HttpContext.GetTokenAsync("access_token");

                // Check if the coupon code is valid
                var couponResponse = await _couponService.GetCouponAsync(cartDto.CartHeader.CouponCode);
                if (couponResponse == null)
                {
                    // Handle invalid coupon code
                    
                    TempData["NotificationType"] = "error";
                    TempData["NotificationMessage"] = "Invalid coupon code";
                    return RedirectToAction(nameof(Index));
                }

                // Deserialize the coupon object
                var coupon = JsonConvert.DeserializeObject<CouponDto>(Convert.ToString(couponResponse.Result));


                // Apply the coupon
                var response = await _shoppingCartService.ApplyCouponAsync(cartDto);

                if (response != null && response.IsSuccess)
                {
                    TempData["NotificationType"] = "success";
                    TempData["NotificationMessage"] = "Coupon succesfully added";
                    return RedirectToAction(nameof(Index));
                }
                return View();
            }
            catch (Exception ex)
            {
                return View();
                TempData["NotificationType"] = "error";
                TempData["NotificationMessage"] = "an error has occured while applying coupon";
            }
        }

        [HttpPost]
        public async Task<IActionResult> RemoveCoupon(CartDto cartDto)
        {
            try
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
            catch (Exception)
            {

                return View();
            }
        }
    }
}
