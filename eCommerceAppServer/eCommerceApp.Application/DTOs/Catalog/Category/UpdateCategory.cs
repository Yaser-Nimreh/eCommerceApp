using System.ComponentModel.DataAnnotations;

namespace eCommerceApp.Application.DTOs.Catalog.Category
{
    public class UpdateCategory : CategoryBase
    {
        [Required]
        public Guid Id { get; set; }
    }
}