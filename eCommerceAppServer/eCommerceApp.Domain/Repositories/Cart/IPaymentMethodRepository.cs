using eCommerceApp.Domain.Entities.Cart;

namespace eCommerceApp.Domain.Repositories.Cart
{
    public interface IPaymentMethodRepository
    {
        Task<IEnumerable<PaymentMethod>> GetPaymentMethodsAsync();
    }
}