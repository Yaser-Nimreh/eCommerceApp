using eCommerceApp.Domain.Entities.Catalog;
using eCommerceApp.Domain.Repositories.Catalog;
using eCommerceApp.Infrastructure.Data;
using eCommerceApp.Infrastructure.Repositories.Generic;

namespace eCommerceApp.Infrastructure.Repositories.Catalog
{
    public class ProductRepository(ApplicationDbContext context) : GenericRepository<Product>(context), IProductRepository
    {
    }
}