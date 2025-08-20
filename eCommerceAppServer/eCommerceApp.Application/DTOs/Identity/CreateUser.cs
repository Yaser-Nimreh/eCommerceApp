using System.ComponentModel.DataAnnotations;

namespace eCommerceApp.Application.DTOs.Identity
{
    public class CreateUser : IdentityBase
    {
        public required string Fullname { get; set; }
        public required string ConfirmPassword { get; set; }
    }
}