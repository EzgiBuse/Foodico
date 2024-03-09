using System.ComponentModel.DataAnnotations;

namespace Foodico.Services.EmailAPI.Models.Dto
{
    public class AddressDto
    {
        [Key]
        public int AddressId { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PostCode { get; set; }
    }
}
