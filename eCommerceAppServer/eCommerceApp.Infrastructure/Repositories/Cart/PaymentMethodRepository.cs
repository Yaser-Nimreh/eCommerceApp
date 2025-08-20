using eCommerceApp.Domain.Entities.Cart;
using eCommerceApp.Domain.Repositories.Cart;
using eCommerceApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace eCommerceApp.Infrastructure.Repositories.Cart
{
    public class PaymentMethodRepository(ApplicationDbContext context) : IPaymentMethodRepository
    {
        public async Task<IEnumerable<PaymentMethod>> GetPaymentMethodsAsync()
        {
            return await context.PaymentMethods.AsNoTracking().ToListAsync();
        }
    }
}