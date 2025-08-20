using System.ComponentModel.DataAnnotations;

namespace eCommerceApp.Application.DTOs.Catalog.Category
{
    public class CategoryBase
    {
        [Required]
        public string? Name { get; set; }
    }
}