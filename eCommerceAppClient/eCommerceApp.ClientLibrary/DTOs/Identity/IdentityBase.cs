using System.ComponentModel.DataAnnotations;

namespace eCommerceApp.ClientLibrary.DTOs.Identity
{
    public class IdentityBase
    {
        [EmailAddress, Required]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}