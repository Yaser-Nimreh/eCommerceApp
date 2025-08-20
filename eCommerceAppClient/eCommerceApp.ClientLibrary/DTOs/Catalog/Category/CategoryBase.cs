using System.ComponentModel.DataAnnotations;

namespace eCommerceApp.ClientLibrary.DTOs.Catalog.Category
{
    public class CategoryBase
    {
        [Required]
        public string? Name { get; set; }
    }
}