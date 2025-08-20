using eCommerceApp.Application.DTOs.Catalog.Product;
using System.ComponentModel.DataAnnotations;

namespace eCommerceApp.Application.DTOs.Catalog.Category
{
    public class GetCategory : CategoryBase
    {
        [Required]
        public Guid Id { get; set; }
        public ICollection<GetProduct>? Products { get; set; }
    }
}