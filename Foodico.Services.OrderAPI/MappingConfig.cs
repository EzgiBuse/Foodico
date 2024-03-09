using AutoMapper;
using Foodico.Services.OrderAPI.Models;
using Foodico.Services.OrderAPI.Models.Dto;


namespace Foodico.Services.OrderAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingconfig = new MapperConfiguration(config =>
            {
                //config.CreateMap<OrderDto, CartHeaderDto>().
                //ForMember(dest => dest.CartTotal, x => x.MapFrom(src => src.)).ReverseMap();

                //config.CreateMap<CartDetailsDto,OrderDto>().
                // ForMember(dest => dest.ProductName, x => x.MapFrom(src => src.Product.Name)).ReverseMap().
                // ForMember(dest => dest.Price, x => x.MapFrom(src => src.Product.Price)).ReverseMap();

                config.CreateMap<OrderDto, CartDto>().ReverseMap();
                config.CreateMap<Order, OrderDto>().ReverseMap();
              

            });
            return mappingconfig;
        }
    }
}
