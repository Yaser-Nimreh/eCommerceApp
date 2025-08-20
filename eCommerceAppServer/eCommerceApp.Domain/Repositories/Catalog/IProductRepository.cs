using eCommerceApp.Domain.Entities.Catalog;
using eCommerceApp.Domain.Repositories.Generic;

namespace eCommerceApp.Domain.Repositories.Catalog
{
    public interface IProductRepository : IGenericRepository<Product>
    {
    }
}