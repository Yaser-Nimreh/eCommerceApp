using eCommerceApp.ClientLibrary.DTOs.Cart;

namespace eCommerceApp.ClientLibrary.Services.Interfaces.Cart
{
    public interface IPaymentMethodService
    {
        Task<IEnumerable<GetPaymentMethod>> GetPaymentMethodsAsync();
    }
}