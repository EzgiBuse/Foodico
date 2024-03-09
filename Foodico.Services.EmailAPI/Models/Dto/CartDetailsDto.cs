using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Foodico.Services.EmailAPI.Models.Dto
{
    public class CartDetails
    {

        public int CartDetailsId { get; set; }
        public int CartHeaderId { get; set; }

        public CartHeader? CartHeader { get; set; }
        public int ProductId { get; set; }

        public ProductDto Product { get; set; }
        public double Price { get; set; }
        public int Count { get; set; }
    }
}
