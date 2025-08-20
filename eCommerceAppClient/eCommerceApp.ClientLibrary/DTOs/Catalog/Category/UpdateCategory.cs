using System.ComponentModel.DataAnnotations;

namespace eCommerceApp.ClientLibrary.DTOs.Catalog.Category
{
    public class UpdateCategory : CategoryBase
    {
        [Required]
        public Guid Id { get; set; }
    }
}