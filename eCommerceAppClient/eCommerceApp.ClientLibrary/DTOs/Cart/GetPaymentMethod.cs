using System.ComponentModel.DataAnnotations;

namespace eCommerceApp.ClientLibrary.DTOs.Cart
{
    public class GetPaymentMethod
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string? Name { get; set; }
    }
}