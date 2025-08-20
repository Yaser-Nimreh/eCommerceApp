using eCommerceApp.Application.DTOs.Catalog.Category;
using eCommerceApp.Application.DTOs.Catalog.Product;
using eCommerceApp.Application.DTOs.Responses;

namespace eCommerceApp.Application.Services.Interfaces.Catalog
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