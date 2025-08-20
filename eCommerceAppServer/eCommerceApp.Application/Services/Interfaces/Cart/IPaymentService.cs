using eCommerceApp.Application.DTOs.Cart;
using eCommerceApp.Application.DTOs.Responses;
using eCommerceApp.Domain.Entities.Catalog;

namespace eCommerceApp.Application.Services.Interfaces.Cart
{
    public interface IPaymentService
    {
        Task<ServiceResponse> Pay(decimal totalAmount, IEnumerable<Product> cartProducts, IEnumerable<ProcessCart> carts);
    }
}