using AutoMapper;
using eCommerceApp.Application.DTOs.Cart;
using eCommerceApp.Application.Services.Interfaces.Cart;
using eCommerceApp.Domain.Repositories.Cart;

namespace eCommerceApp.Application.Services.Implementations.Cart
{
    public class PaymentMethodService(IPaymentMethodRepository paymentMethodRepository, IMapper mapper) : IPaymentMethodService
    {
        public async Task<IEnumerable<GetPaymentMethod>> GetPaymentMethodsAsync()
        {
            var paymentMethods = await paymentMethodRepository.GetPaymentMethodsAsync();
            if (!paymentMethods.Any()) return [];

            return mapper.Map<IEnumerable<GetPaymentMethod>>(paymentMethods);
        }
    }
}