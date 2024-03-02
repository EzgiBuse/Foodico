using AutoMapper;
using Foodico.Services.ShoppingCartAPI.Models;
using Foodico.Services.ShoppingCartAPI.Models.Dto;

namespace Foodico.Services.ShoppingCartAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingconfig = new MapperConfiguration(config =>
            {
                config.CreateMap<CartHeader, CartHeaderDto>().ReverseMap();
                config.CreateMap<CartDetails, CartDetailsDto>().ReverseMap();
            });
            return mappingconfig;
        }
    }
}
