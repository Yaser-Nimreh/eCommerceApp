using eCommerceApp.ClientLibrary.DTOs.Catalog.Product;
using System.ComponentModel.DataAnnotations;

namespace eCommerceApp.ClientLibrary.DTOs.Catalog.Category
{
    public class GetCategory : CategoryBase
    {
        [Required]
        public Guid Id { get; set; }
        public ICollection<GetProduct>? Products { get; set; }
    }
}