using eCommerceApp.Domain.Entities.Cart;

namespace eCommerceApp.Domain.Repositories.Cart
{
    public interface ICartRepository
    {
        Task<int> SaveCheckoutHistoryAsync(IEnumerable<Achieve> checkouts);
        Task<IEnumerable<Achieve>> GetAllCheckoutHistoryAsync();
    }
}