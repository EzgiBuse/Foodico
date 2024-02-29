using Foodico.Web.Models;
using Foodico.Web.Service.IService;
using static Foodico.Web.Utility.Standard;

namespace Foodico.Web.Service
{
    public class CouponService :ICouponService
    {private readonly IBaseService _baseService;

        public CouponService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto?> CreateCouponAsync(CouponDto couponDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {

                ApiType = ApiType.POST,
                Data = couponDto,
                Url = CouponAPIBase + "/api/CouponAPI/" 
            });
        }

        public async Task<ResponseDto?> DeleteCouponAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {

                ApiType = ApiType.DELETE,
                Url = CouponAPIBase + "/api/CouponAPI/" + id
            });
        }

        public async Task<ResponseDto?> GetAllCouponsAsync()
        {
            return await _baseService.SendAsync(new RequestDto()
            {

                ApiType = ApiType.GET,
                Url = CouponAPIBase + "/api/CouponAPI/"
            });
        }

        public async Task<ResponseDto?> GetCouponAsync(string couponCode)
        {
            return await _baseService.SendAsync(new RequestDto()
            {

                ApiType = ApiType.GET,
                Url=CouponAPIBase+ "/api/CouponAPI/GetByCode/" + couponCode
            });
        }

        public async Task<ResponseDto?> GetCouponByIdAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {

                ApiType = ApiType.GET,
                Url = CouponAPIBase + "/api/CouponAPI/" + id
            });
        }

        public async Task<ResponseDto?> UpdateCouponAsync(CouponDto couponDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {

                ApiType = ApiType.PUT,
                Data = couponDto,
                Url = CouponAPIBase + "/api/CouponAPI/"
            });
        }
    }
}
