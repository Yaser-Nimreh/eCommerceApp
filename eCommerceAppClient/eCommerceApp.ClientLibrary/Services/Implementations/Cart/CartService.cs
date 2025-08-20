using eCommerceApp.ClientLibrary.DTOs.Api;
using eCommerceApp.ClientLibrary.DTOs.Cart;
using eCommerceApp.ClientLibrary.DTOs.Catalog.Category;
using eCommerceApp.ClientLibrary.DTOs.Responses;
using eCommerceApp.ClientLibrary.Helpers.Constant;
using eCommerceApp.ClientLibrary.Helpers.Interfaces;
using eCommerceApp.ClientLibrary.Services.Interfaces.Cart;

namespace eCommerceApp.ClientLibrary.Services.Implementations.Cart
{
    public class CartService(IHttpClientHelper httpClientHelper, IApiCallHelper apiCallHelper) : ICartService
    {
        public async Task<ServiceResponse> CheckoutAsync(Checkout checkout)
        {
            var client = await httpClientHelper.GetPrivateClientAsync();
            var apiCallModel = new ApiCall
            {
                Route = Constants.Cart.Checkout,
                Type = Constants.ApiCallType.Post,
                Client = client,
                Id = null!,
                Model = checkout,
            };
            var result = await apiCallHelper.ApiCallTypeCallAsync<Checkout>(apiCallModel);
            if (result == null)
                return apiCallHelper.ConnectionError();
            else
                return await apiCallHelper.GetServiceResponseAsync<ServiceResponse>(result);
        }

        public async Task<IEnumerable<GetAchieve>> GetAchievesAsync()
        {
            var client = await httpClientHelper.GetPrivateClientAsync();
            var apiCall = new ApiCall
            {
                Route = Constants.Cart.GetAchieves,
                Type = Constants.ApiCallType.Get,
                Client = client,
                Model = null!,
                Id = null!,
            };
            var result = await apiCallHelper.ApiCallTypeCallAsync<object>(apiCall);
            if (result.IsSuccessStatusCode)
                return await apiCallHelper.GetServiceResponseAsync<IEnumerable<GetAchieve>>(result);
            else
                return [];
        }

        public async Task<ServiceResponse> SaveCheckoutHistoryAsync(IEnumerable<CreateAchieve> achieves)
        {
            var client = await httpClientHelper.GetPrivateClientAsync();
            var apiCallModel = new ApiCall
            {
                Route = Constants.Cart.SaveCheckout,
                Type = Constants.ApiCallType.Post,
                Client = client,
                Id = null!,
                Model = achieves,
            };
            var result = await apiCallHelper.ApiCallTypeCallAsync<IEnumerable<CreateAchieve>>(apiCallModel);
            if (result == null)
                return apiCallHelper.ConnectionError();
            else
                return await apiCallHelper.GetServiceResponseAsync<ServiceResponse>(result);
        }
    }
}