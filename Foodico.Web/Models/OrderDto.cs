namespace Foodico.Web.Models
{
    public class OrderDto

    {  
        public int OrderId { get; set; }
        public CartDto Cart { get; set; }
        public AddressDto Address { get; set; }

        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
        public string OrderNotes { get; set; }
        public DateTime OrderTime { get; set; }
        public string PaymentIntentId { get; set; }
        public string StripeSessionId { get; set; }
    }
}
