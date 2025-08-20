using eCommerceApp.ClientLibrary.DTOs.Cart;
using eCommerceApp.ClientLibrary.DTOs.Responses;

namespace eCommerceApp.ClientLibrary.Services.Interfaces.Cart
{
    public interface ICartService
    {
        Task<ServiceResponse> SaveCheckoutHistoryAsync(IEnumerable<CreateAchieve> achieves);
        Task<ServiceResponse> CheckoutAsync(Checkout checkout);
        Task<IEnumerable<GetAchieve>> GetAchievesAsync();
    }
}