using eCommerceApp.Domain.Entities.Catalog;
using eCommerceApp.Domain.Repositories.Catalog;
using eCommerceApp.Infrastructure.Data;
using eCommerceApp.Infrastructure.Repositories.Generic;
using Microsoft.EntityFrameworkCore;

namespace eCommerceApp.Infrastructure.Repositories.Catalog
{
    public class CategoryRepository(ApplicationDbContext context) : GenericRepository<Category>(context), ICategoryRepository
    {
        private readonly ApplicationDbContext _context = context;
        public async Task<IEnumerable<Product>> GetProductsByCategory(Guid categoryId)
        {
            var products = await _context
                .Products
                .Include(_ => _.Category)
                .Where(_ => _.CategoryId == categoryId)
                .AsNoTracking()
                .ToListAsync();
            return products.Count > 0 ? products : [];
        }
    }
}