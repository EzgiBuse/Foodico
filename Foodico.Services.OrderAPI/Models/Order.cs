using Foodico.Services.OrderAPI.Models.Dto;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Foodico.Services.OrderAPI.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        [NotMapped]
        public CartDto Cart { get; set; }
        public AddressDto Address { get; set; }

        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
        public string OrderNotes { get; set; }
        public string PaymentIntentId { get; set; }
        public string StripeSessionId { get; set; }
    }
}
