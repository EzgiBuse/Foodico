using AutoMapper;
using Foodico.Services.CouponAPI.Data;
using Foodico.Services.CouponAPI.Models;
using Foodico.Services.CouponAPI.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace Foodico.Services.CouponAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponAPIController : ControllerBase
    {
        private readonly AppDbContext _db;
        private ResponseDto _responseDto;
        private IMapper _mapper;

        public CouponAPIController(AppDbContext db,IMapper mapper)
        {
            _db = db;
            _responseDto = new ResponseDto();
            _mapper = mapper;
        }
        [HttpGet]
        public ResponseDto Get()
        {
            try
            {
                IEnumerable<Coupon> objList = _db.Coupons.ToList();
                _responseDto.Result = _mapper.Map<IEnumerable<CouponDto>>(objList);
                return _responseDto;

            }catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;

            }
            return _responseDto;
        }

        [HttpGet]
        [Route("{id:int}")]
        
        public ResponseDto Get(int id)
        {
            try
            {
                Coupon obj = _db.Coupons.First(u=>u.CouponId==id);
                _mapper.Map<CouponDto>(obj);
                _responseDto.Result = obj;
                return _responseDto;
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;

            }
            return _responseDto;
        }

        [HttpGet]
        [Route("GetByCode/{code}")]
        
        public ResponseDto Get(string code)
        {
            try
            {
                Coupon obj = _db.Coupons.FirstOrDefault(u => u.CouponCode.ToLower() == code.ToLower());
                if(obj == null)
                {
                    _responseDto.IsSuccess=false;
                }
                _responseDto.Result=  _mapper.Map<CouponDto>(obj);
               
                return _responseDto;
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;

            }
            return _responseDto;
        }

        [HttpPost]
       
        public ResponseDto Post([FromBody] CouponDto couponDto)
        {
            try
            {
                Coupon obj = _mapper.Map<Coupon>(couponDto);
                _db.Coupons.Add(obj);
                _db.SaveChanges();

               
                var options = new Stripe.CouponCreateOptions
                {
                   
                    Name=couponDto.CouponCode,
                    Currency= "usd",
                    Id=couponDto.CouponCode,
                    AmountOff = (long)(couponDto.DiscountAmount*100)
                };
                var service = new Stripe.CouponService();
                service.Create(options);

                _responseDto.Result=_mapper.Map<CouponDto>(obj);
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;

            }
            return _responseDto;
        }

        [HttpPut]
       
        public ResponseDto Put([FromBody] CouponDto couponDto)
        {
            try
            {
                Coupon obj = _mapper.Map<Coupon>(couponDto);
                _db.Coupons.Update(obj);
                _db.SaveChanges();

                _responseDto.Result = _mapper.Map<CouponDto>(obj);
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;

            }
            return _responseDto;
        }

        [HttpDelete]
      
        public ResponseDto Delete(int id)
        {
            try
            {
                Coupon obj = _db.Coupons.First(x=>x.CouponId== id);
                _db.Coupons.Remove(obj);
                _db.SaveChanges();

                var service = new Stripe.CouponService();
                service.Delete(obj.CouponCode);


            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;

            }
            return _responseDto;
        }
    }
}
