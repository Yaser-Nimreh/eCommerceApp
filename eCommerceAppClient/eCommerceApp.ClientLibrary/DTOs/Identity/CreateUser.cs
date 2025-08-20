using System.ComponentModel.DataAnnotations;

namespace eCommerceApp.ClientLibrary.DTOs.Identity
{
    public class CreateUser : IdentityBase
    {
        [Required]
        public string? Fullname { get; set; }
        [Required, Compare(nameof(Password))]
        public string? ConfirmPassword { get; set; }
    }
}