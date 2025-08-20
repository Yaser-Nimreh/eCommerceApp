using AutoMapper;
using eCommerceApp.Application.DTOs.Cart;
using eCommerceApp.Application.DTOs.Responses;
using eCommerceApp.Application.Services.Interfaces.Cart;
using eCommerceApp.Domain.Entities.Cart;
using eCommerceApp.Domain.Entities.Catalog;
using eCommerceApp.Domain.Repositories.Authentication;
using eCommerceApp.Domain.Repositories.Cart;
using eCommerceApp.Domain.Repositories.Catalog;

namespace eCommerceApp.Application.Services.Implementations.Cart
{
    public class CartService(ICartRepository cartRepository, IMapper mapper, IProductRepository productRepository,
        IPaymentMethodService paymentMethodService, IPaymentService paymentService, IUserManagement userManagement) : ICartService
    {
        public async Task<ServiceResponse> CheckoutAsync(Checkout checkout)
        {
            var (products, totalAmount) = await GetCartTotalAmountAsync(checkout.Carts);
            var paymentMethods = await paymentMethodService.GetPaymentMethodsAsync();
            if (checkout.PaymentMethodId == paymentMethods.FirstOrDefault()!.Id)
                return await paymentService.Pay(totalAmount, products, checkout.Carts);
            else
                return new ServiceResponse(false, "Invalid payment method");
        }

        public async Task<IEnumerable<GetAchieve>> GetAchievesAsync()
        {
            var history = await cartRepository.GetAllCheckoutHistoryAsync();
            if (history == null) return [];
            var groupByCustomerId = history.GroupBy(_ => _.UserId).ToList();
            var products = await productRepository.GetAllAsync();
            var achieves = new List<GetAchieve>();
            foreach (var customerId in groupByCustomerId)
            {
                var customerDetails = await userManagement.GetUserByIdAsync(customerId.Key!);
                foreach (var item in customerId)
                {
                    var product = products.FirstOrDefault(_ => _.Id == item.ProductId);
                    achieves.Add(new GetAchieve 
                    {
                        CustomerName = customerDetails.Fullname,
                        CustomerEmail = customerDetails.Email,
                        ProductName = product!.Name,
                        AmountPayed = item.Quantity * product.Price,
                        QuantityOrdered = item.Quantity,
                        DatePurchased = item.CreatedDate
                    });
                }
            }
            return achieves;
        }

        public async Task<ServiceResponse> SaveCheckoutHistoryAsync(IEnumerable<CreateAchieve> achieves)
        {
            var mappedAchieves = mapper.Map<IEnumerable<Achieve>>(achieves);
            var result = await cartRepository.SaveCheckoutHistoryAsync(mappedAchieves);
            return result > 0 ? new ServiceResponse(true, "CheckoutAsync achieved") : new ServiceResponse(false, "Error occurred in saving");
        }

        private async Task<(IEnumerable<Product>, decimal)> GetCartTotalAmountAsync(IEnumerable<ProcessCart> carts)
        {
            if (!carts.Any()) return ([], 0);
            var products = await productRepository.GetAllAsync();
            if (!products.Any()) return ([], 0);

            var cartProducts = carts
                .Select(cartItem => products.FirstOrDefault(p => p.Id == cartItem.ProductId))
                .Where(product => product != null)
                .ToList();

            var totalAmount = carts
                .Where(cartItem => cartProducts.Any(p => p!.Id == cartItem.ProductId))
                .Sum(cartItem => cartItem.Quantity * cartProducts.First(p => p!.Id == cartItem.ProductId)!.Price);

            return (cartProducts!, totalAmount);
        }
    }
}