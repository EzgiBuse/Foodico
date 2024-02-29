using AutoMapper;
using Foodico.Services.CouponAPI.Models;
using Foodico.Services.CouponAPI.Models.Dto;

namespace Foodico.Services.CouponAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingconfig = new MapperConfiguration(config =>
            {
                config.CreateMap<Coupon, CouponDto>().ReverseMap();
            });
            return mappingconfig;
        }
    }
}
