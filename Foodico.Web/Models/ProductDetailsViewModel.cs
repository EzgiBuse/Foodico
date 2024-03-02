namespace Foodico.Web.Models
{
    public class ProductDetailsViewModel
    {
        public ProductDto Product { get; set; }
        public List<ProductDto> RelatedProducts { get; set; }
    }
}
