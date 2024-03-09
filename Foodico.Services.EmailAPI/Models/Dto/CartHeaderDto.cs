using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Foodico.Services.EmailAPI.Models.Dto
{
    public class CartHeader
    {
        public int CartHeaderId { get; set; }
       
        public string? UserId { get; set; }

        public string? CouponCode { get; set; }
        
        public double Discount { get; set; }
      
        public double CartTotal { get; set; }
    }
}
