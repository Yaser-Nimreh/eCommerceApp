using System.ComponentModel.DataAnnotations;

namespace eCommerceApp.Application.DTOs.Catalog.Product
{
    public class UpdateProduct : ProductBase
    {
        [Required]
        public Guid Id { get; set; }
    }
}