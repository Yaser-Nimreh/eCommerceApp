using eCommerceApp.Application.DTOs.Catalog.Category;
using System.ComponentModel.DataAnnotations;

namespace eCommerceApp.Application.DTOs.Catalog.Product
{
    public class GetProduct : ProductBase
    {
        [Required]
        public Guid Id { get; set; }
        public GetCategory? Category { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}