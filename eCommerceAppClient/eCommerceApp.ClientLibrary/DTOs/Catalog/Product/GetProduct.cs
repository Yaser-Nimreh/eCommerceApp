using eCommerceApp.ClientLibrary.DTOs.Catalog.Category;
using System.ComponentModel.DataAnnotations;

namespace eCommerceApp.ClientLibrary.DTOs.Catalog.Product
{
    public class GetProduct : ProductBase
    {
        [Required]
        public Guid Id { get; set; }
        public GetCategory? Category { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsNew => DateTime.Now <= CreatedDate.AddDays(7);
    }
}