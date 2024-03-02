namespace Foodico.Web.Models
{
    public class OrderDto
    {   public CartDto Cart { get; set; }
        public AddressDto Address { get; set; }
        public string OrderNotes { get; set; }
    }
}
