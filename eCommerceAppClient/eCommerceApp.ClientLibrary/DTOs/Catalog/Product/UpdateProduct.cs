using System.ComponentModel.DataAnnotations;

namespace eCommerceApp.ClientLibrary.DTOs.Catalog.Product
{
    public class UpdateProduct : ProductBase
    {
        [Required]
        public Guid Id { get; set; }
    }
}