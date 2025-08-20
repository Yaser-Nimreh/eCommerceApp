using eCommerceApp.Application.DTOs.Cart;
using eCommerceApp.Application.DTOs.Responses;
using eCommerceApp.Application.Services.Interfaces.Cart;
using eCommerceApp.Domain.Entities.Catalog;
using Stripe.Checkout;

namespace eCommerceApp.Infrastructure.Services.Implementations.Cart
{
    public class StripePaymentService : IPaymentService
    {
        public async Task<ServiceResponse> Pay(decimal totalAmount, IEnumerable<Product> cartProducts, IEnumerable<ProcessCart> carts)
        {
            try
            {
                var lineItems = new List<SessionLineItemOptions>();
                foreach (var item in cartProducts)
                {
                    var productQuantity = carts.FirstOrDefault(_ => _.ProductId == item.Id);
                    lineItems.Add(new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            Currency = "usd",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = item.Name,
                                Description = item.Description
                            },
                            UnitAmount = (long)(item.Price * 100),
                        },
                        Quantity = productQuantity!.Quantity,
                    });
                }

                var options = new SessionCreateOptions
                {
                    PaymentMethodTypes = ["card"],
                    LineItems = lineItems,
                    Mode = "payment",
                    SuccessUrl = "https://localhost:7002/payment-success",
                    CancelUrl = "https://localhost:7002/payment-cancel"
                };

                var service = new SessionService();
                Session session = await service.CreateAsync(options);

                return new ServiceResponse(true, session.Url);
            }
            catch (Exception ex)
            {
                return new ServiceResponse(false, ex.Message);
            }
        }
    }
}