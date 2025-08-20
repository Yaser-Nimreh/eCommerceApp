using eCommerceApp.ClientLibrary.DTOs.Api;
using eCommerceApp.ClientLibrary.DTOs.Cart;
using eCommerceApp.ClientLibrary.Helpers.Constant;
using eCommerceApp.ClientLibrary.Helpers.Interfaces;
using eCommerceApp.ClientLibrary.Services.Interfaces.Cart;

namespace eCommerceApp.ClientLibrary.Services.Implementations.Cart
{
    public class PaymentMethodService(IHttpClientHelper httpClientHelper, IApiCallHelper apiCallHelper) : IPaymentMethodService
    {
        public async Task<IEnumerable<GetPaymentMethod>> GetPaymentMethodsAsync()
        {
            var client = httpClientHelper.GetPublicClient();
            var apiCall = new ApiCall
            {
                Route = Constants.Payment.GetAll,
                Type = Constants.ApiCallType.Get,
                Client = client,
                Id = null!,
                Model = null!
            };
            var result = await apiCallHelper.ApiCallTypeCallAsync<object>(apiCall);
            if (result.IsSuccessStatusCode)
                return await apiCallHelper.GetServiceResponseAsync<IEnumerable<GetPaymentMethod>>(result);
            else
                return [];
        }
    }
}