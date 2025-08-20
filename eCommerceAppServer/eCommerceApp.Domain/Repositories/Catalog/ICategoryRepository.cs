using eCommerceApp.Domain.Entities.Catalog;
using eCommerceApp.Domain.Repositories.Generic;

namespace eCommerceApp.Domain.Repositories.Catalog
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<IEnumerable<Product>> GetProductsByCategory(Guid categoryId);
    }
}