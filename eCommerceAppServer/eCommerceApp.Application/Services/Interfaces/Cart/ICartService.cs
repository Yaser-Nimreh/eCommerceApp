using eCommerceApp.Application.DTOs.Cart;
using eCommerceApp.Application.DTOs.Responses;

namespace eCommerceApp.Application.Services.Interfaces.Cart
{
    public interface ICartService
    {
        Task<ServiceResponse> SaveCheckoutHistoryAsync(IEnumerable<CreateAchieve> achieves);
        Task<ServiceResponse> CheckoutAsync(Checkout checkout);
        Task<IEnumerable<GetAchieve>> GetAchievesAsync();
    }
}