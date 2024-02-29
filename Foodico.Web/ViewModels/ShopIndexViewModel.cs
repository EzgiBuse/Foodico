using Foodico.Web.Models;

namespace Foodico.Web.ViewModels
{
    public class ShopIndexViewModel
    {
        public List<ProductDto> Products { get; set; } // List of products for the current page
        public int TotalProducts { get; set; } // Total number of products
    }
}
