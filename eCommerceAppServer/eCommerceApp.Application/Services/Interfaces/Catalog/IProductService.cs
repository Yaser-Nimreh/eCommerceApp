using eCommerceApp.Application.DTOs.Catalog.Product;
using eCommerceApp.Application.DTOs.Responses;

namespace eCommerceApp.Application.Services.Interfaces.Catalog
{
    public interface IProductService
    {
        Task<IEnumerable<GetProduct>> GetAllAsync();
        Task<GetProduct> GetByIdAsync(Guid id);
        Task<ServiceResponse> CreateAsync(CreateProduct product);
        Task<ServiceResponse> UpdateAsync(UpdateProduct product);
        Task<ServiceResponse> DeleteAsync(Guid id);
    }
}