using AutoMapper;
using eCommerceApp.Application.DTOs.Catalog.Category;
using eCommerceApp.Application.DTOs.Catalog.Product;
using eCommerceApp.Application.DTOs.Responses;
using eCommerceApp.Application.Services.Interfaces.Catalog;
using eCommerceApp.Domain.Entities.Catalog;
using eCommerceApp.Domain.Repositories.Catalog;

namespace eCommerceApp.Application.Services.Implementations.Catalog
{
    public class CategoryService(ICategoryRepository categoryRepository, IMapper mapper) : ICategoryService
    {
        public async Task<ServiceResponse> CreateAsync(CreateCategory category)
        {
            var mappedCategory = mapper.Map<Category>(category);
            int result = await categoryRepository.CreateAsync(mappedCategory);
            return result > 0 ? new ServiceResponse(true, "Category added!") :
                new ServiceResponse(false, "Category failed to be added");
        }

        public async Task<ServiceResponse> DeleteAsync(Guid id)
        {
            int result = await categoryRepository.DeleteAsync(id);
            return result > 0 ? new ServiceResponse(true, "Category deleted!") :
                new ServiceResponse(false, "Category not found or failed to be deleted");
        }

        public async Task<IEnumerable<GetCategory>> GetAllAsync()
        {
            var categories = await categoryRepository.GetAllAsync();
            if (!categories.Any()) return [];

            return mapper.Map<IEnumerable<GetCategory>>(categories);
        }

        public async Task<GetCategory> GetByIdAsync(Guid id)
        {
            var category = await categoryRepository.GetByIdAsync(id);
            if (category == null) return new GetCategory();

            return mapper.Map<GetCategory>(category);
        }

        public async Task<IEnumerable<GetProduct>> GetProductsByCategoryAsync(Guid categoryId)
        {
            var products = await categoryRepository.GetProductsByCategory(categoryId);
            if (!products.Any()) return [];
            return mapper.Map<IEnumerable<GetProduct>>(products);
        }

        public async Task<ServiceResponse> UpdateAsync(UpdateCategory category)
        {
            var mappedCategory = mapper.Map<Category>(category);
            int result = await categoryRepository.UpdateAsync(mappedCategory);
            return result > 0 ? new ServiceResponse(true, "Category updated!") :
                new ServiceResponse(false, "Category failed to be updated");
        }
    }
}