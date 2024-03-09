using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Foodico.Web.Models
{
    public class RegistrationRequestDto
    {

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z\s]*$")]
        public string Name { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]*$")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Phone number must be 10 digits")]
        public string PhoneNumber { get; set; }

        [Required]
        public string Password { get; set; }

        public string? Role { get; set; }

       
    }
}
