using System.ComponentModel.DataAnnotations;

namespace eCommerceApp.ClientLibrary.DTOs.Cart
{
    public class CreateAchieve : ProcessCart
    {
        [Required]
        public string UserId { get; set; } = string.Empty;
    }
}