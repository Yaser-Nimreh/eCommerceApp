using AutoMapper;
using eCommerceApp.Application.DTOs.Catalog.Product;
using eCommerceApp.Application.DTOs.Responses;
using eCommerceApp.Application.Services.Interfaces.Catalog;
using eCommerceApp.Domain.Entities.Catalog;
using eCommerceApp.Domain.Repositories.Catalog;

namespace eCommerceApp.Application.Services.Implementations.Catalog
{
    public class ProductService(IProductRepository productRepository, IMapper mapper) : IProductService
    {
        public async Task<ServiceResponse> CreateAsync(CreateProduct product)
        {
            var mappedProduct = mapper.Map<Product>(product);
            int result = await productRepository.CreateAsync(mappedProduct);
            return result > 0 ? new ServiceResponse(true, "Product added!") :
                new ServiceResponse(false, "Product failed to be added");
        }

        public async Task<ServiceResponse> DeleteAsync(Guid id)
        {
            int result = await productRepository.DeleteAsync(id);
            return result > 0 ? new ServiceResponse(true, "Product deleted!") :
                new ServiceResponse(false, "Product not found or failed to be deleted");
        }

        public async Task<IEnumerable<GetProduct>> GetAllAsync()
        {
            var products = await productRepository.GetAllAsync();
            if (!products.Any()) return [];

            return mapper.Map<IEnumerable<GetProduct>>(products);
        }

        public async Task<GetProduct> GetByIdAsync(Guid id)
        {
            var products = await productRepository.GetByIdAsync(id);
            if (products == null) return new GetProduct();

            return mapper.Map<GetProduct>(products);
        }

        public async Task<ServiceResponse> UpdateAsync(UpdateProduct product)
        {
            var mappedProduct = mapper.Map<Product>(product);
            int result = await productRepository.UpdateAsync(mappedProduct);
            return result > 0 ? new ServiceResponse(true, "Product updated!") :
                new ServiceResponse(false, "Product failed to be updated");
        }
    }
}