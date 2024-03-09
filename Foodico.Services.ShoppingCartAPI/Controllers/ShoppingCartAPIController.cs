using AutoMapper;
using Foodico.Services.ShoppingCartAPI.Data;
using Foodico.Services.ShoppingCartAPI.Models;
using Foodico.Services.ShoppingCartAPI.Models.Dto;
using Foodico.Services.ShoppingCartAPI.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;

namespace Foodico.Services.ShoppingCartAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartAPIController : ControllerBase
    {
        private ResponseDto _responseDto;
        private IMapper _mapper;
        private readonly AppDbContext _db;
        private IProductService _productService;
        private ICouponService _couponService;

        public ShoppingCartAPIController(AppDbContext db, IMapper mapper, IProductService productService, ICouponService couponService)
        {
            _responseDto = new ResponseDto();
            _mapper = mapper;
            _db = db;
            _productService = productService;
            _couponService = couponService;
        }

        [HttpGet("GetCart/{userId}")]
        [Authorize]
        public async Task<ResponseDto> GetCart(string userId)
        {
            try
            {
                CartDto cart = new CartDto();
                CartHeaderDto cartHeader = _mapper.Map<CartHeaderDto>(_db.CartHeaders.First(x => x.UserId == userId));
               
                    cart.CartHeader = cartHeader;
                    cart.CartDetails = _mapper.Map<IEnumerable<CartDetailsDto>>(_db.CartDetails.Where(x => x.CartHeaderId == 
                    cart.CartHeader.CartHeaderId));
                IEnumerable<ProductDto> productDtos = await _productService.GetProducts();
               foreach (var item in cart.CartDetails)
                {
                    item.Product = productDtos.FirstOrDefault(x => x.ProductId == item.ProductId);
                    cart.CartHeader.CartTotal += (item.Product.Price * item.Count);
                }
               if(!string.IsNullOrEmpty(cartHeader.CouponCode))
                {
                    CouponDto coupon = await _couponService.GetCoupon(cartHeader.CouponCode);
                    if (coupon != null && cart.CartHeader.CartTotal >= coupon.MinAmount)
                    {
                        cart.CartHeader.CartTotal -= coupon.DiscountAmount;
                       cart.CartHeader.Discount = coupon.DiscountAmount;
                        
                    }
                }
                _responseDto.Result = cart;
            }
            catch (Exception ex)
            {
                _responseDto.Message = ex.Message;
                _responseDto.IsSuccess = false;
            }
            return _responseDto;
        }

        [HttpPost("ApplyCoupon")]
        [Authorize]
        public async Task<object> ApplyCoupon([FromBody] CartDto cartDto)
        {
            try
            {
                var cartHeader = await _db.CartHeaders.FirstAsync(x => x.UserId == cartDto.CartHeader.UserId);
                cartHeader.CouponCode = cartDto.CartHeader.CouponCode;
                cartHeader.Discount = cartDto.CartHeader.Discount;
                _db.CartHeaders.Update(cartHeader);
                await _db.SaveChangesAsync();
               _responseDto.IsSuccess = true;
               
            }
            catch (Exception ex)
            {
                _responseDto.Message = ex.Message.ToString();
                _responseDto.IsSuccess = false;
            }
            return _responseDto;
        }

        [HttpPost("RemoveCoupon")]
        [Authorize]
        public async Task<object> RemoveCoupon([FromBody] CartDto cartDto)
        {
            try
            {
                var cartHeader = await _db.CartHeaders.FirstAsync(x => x.UserId == cartDto.CartHeader.UserId);
                cartHeader.CouponCode = "";
                cartHeader.Discount = 0;
                _db.CartHeaders.Update(cartHeader);
                await _db.SaveChangesAsync();
                _responseDto.IsSuccess = true;

            }
            catch (Exception ex)
            {
                _responseDto.Message = ex.Message.ToString();
                _responseDto.IsSuccess = false;
            }
            return _responseDto;
        }

        [HttpPost("UpdateCart")]
        [Authorize]
        public async Task<ResponseDto> UpdateCart(CartDto cartDto)
        {
            try
            {
                var dbCartHeader = await _db.CartHeaders.AsNoTracking().FirstOrDefaultAsync(x => x.UserId == cartDto.CartHeader.UserId);
                if (dbCartHeader == null)
                {
                    CartHeader cartHeader = _mapper.Map<CartHeader>(cartDto.CartHeader);
                    _db.CartHeaders.Add(cartHeader);
                    await _db.SaveChangesAsync();
                    cartDto.CartDetails.First().CartHeaderId = cartHeader.CartHeaderId;
                    _db.CartDetails.Add(_mapper.Map<CartDetails>(cartDto.CartDetails.First()));
                    await _db.SaveChangesAsync();
                }
                else
                {
                    var dbCartDetails = await _db.CartDetails.AsNoTracking().FirstOrDefaultAsync(
                        x => x.ProductId == cartDto.CartDetails.First().ProductId &&
                        x.CartHeaderId == dbCartHeader.CartHeaderId);
                    if (dbCartDetails == null)
                    {
                        cartDto.CartDetails.First().CartHeaderId = dbCartHeader.CartHeaderId;
                        _db.CartDetails.Add(_mapper.Map<CartDetails>(cartDto.CartDetails.First()));
                        await _db.SaveChangesAsync();
                    }
                    else
                    {
                        cartDto.CartDetails.First().Count += dbCartDetails.Count;
                        cartDto.CartDetails.First().CartHeaderId = dbCartDetails.CartHeaderId;
                        cartDto.CartDetails.First().CartDetailsId = dbCartDetails.CartDetailsId;
                        _db.CartDetails.Update(_mapper.Map<CartDetails>(cartDto.CartDetails.First()));
                        await _db.SaveChangesAsync();
                    }
                }
                _responseDto.Result = cartDto;
            }
            catch (Exception ex)
            {

                _responseDto.Message = ex.Message;
                _responseDto.IsSuccess = false;
            }
            return _responseDto;
        }
        [HttpPost("RemoveCart")]
        [Authorize]
        public async Task<ResponseDto> RemoveCart([FromBody]int cartDetailsId)
        {
            try
            {

                CartDetails cartDetails = _db.CartDetails.First(x => x.CartDetailsId == cartDetailsId);
                int totalCountOfCartItems = _db.CartDetails.Where(x => x.CartHeaderId == cartDetails.CartHeaderId).Count();
                _db.CartDetails.Remove(cartDetails);
                if (totalCountOfCartItems == 1)
                {
                    var cartHeadertoRemove = await  _db.CartHeaders.FirstAsync(x => x.CartHeaderId == cartDetails.CartHeaderId);
                    _db.CartHeaders.Remove(cartHeadertoRemove);
                }
                await _db.SaveChangesAsync();
                _responseDto.IsSuccess = true;
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message.ToString();
            }
            return _responseDto;
        }
    } }
