using System.ComponentModel.DataAnnotations;

namespace eCommerceApp.ClientLibrary.DTOs.Cart
{
    public class Checkout
    {
        [Required]
        public Guid PaymentMethodId { get; set; }
        [Required]
        public IEnumerable<ProcessCart>? Carts { get; set; }
    }
}