using System.ComponentModel.DataAnnotations;

namespace Foodico.Web.Models
{
    public class AddressDto
    {
        [Required]
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string PostCode { get; set; }
    }
}
