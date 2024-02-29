using AutoMapper;
using Foodico.Services.ProductAPI.Models;
using Foodico.Services.ProductAPI.Models.Dto;

namespace Foodico.Services.ProductAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingconfig = new MapperConfiguration(config =>
            {
                config.CreateMap<Product, ProductDto>().ReverseMap();
            });
            return mappingconfig;
        }
    }
}
