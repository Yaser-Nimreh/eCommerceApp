using eCommerceApp.Domain.Entities.Cart;
using eCommerceApp.Domain.Repositories.Cart;
using eCommerceApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace eCommerceApp.Infrastructure.Repositories.Cart
{
    public class CartRepository(ApplicationDbContext context) : ICartRepository
    {
        public async Task<IEnumerable<Achieve>> GetAllCheckoutHistoryAsync()
        {
            return await context.CheckoutAchieves.AsNoTracking().ToListAsync();
        }

        public async Task<int> SaveCheckoutHistoryAsync(IEnumerable<Achieve> checkouts)
        {
            context.CheckoutAchieves.AddRange(checkouts);
            return await context.SaveChangesAsync();
        }
    }
}