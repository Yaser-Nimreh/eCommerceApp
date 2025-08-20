using System.ComponentModel.DataAnnotations;

namespace eCommerceApp.Application.DTOs.Cart
{
    public class CreateAchieve : ProcessCart
    {
        [Required]
        public string? UserId { get; set; }
    }
}