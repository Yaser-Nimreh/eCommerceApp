using eCommerceApp.ClientLibrary.DTOs.Catalog.Category;
using eCommerceApp.ClientLibrary.DTOs.Catalog.Product;
using eCommerceApp.ClientLibrary.DTOs.Responses;

namespace eCommerceApp.ClientLibrary.Services.Interfaces.Catalog
{
    public interface ICategoryService
    {
        Task<IEnumerable<GetCategory>> GetAllAsync();
        Task<GetCategory> GetByIdAsync(Guid id);
        Task<ServiceResponse> CreateAsync(CreateCategory category);
        Task<ServiceResponse> UpdateAsync(UpdateCategory category);
        Task<ServiceResponse> DeleteAsync(Guid id);
        Task<IEnumerable<GetProduct>> GetProductsByCategoryAsync(Guid categoryId);
    }
}