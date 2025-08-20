using Microsoft.AspNetCore.Identity;

namespace eCommerceApp.Domain.Entities.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string Fullname { get; set; } = string.Empty;
    }
}