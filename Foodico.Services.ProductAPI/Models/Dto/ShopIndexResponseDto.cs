using Foodico.Services.ProductAPI.Models.Dto;

namespace Foodico.Services.ProductAPI
{
    public class ShopIndexResponseDto
    {
        public List<ProductDto>? Products { get; set; }
        public int TotalProducts { get; set; }
    }
}
