using eCommerceApp.ClientLibrary.DTOs.Api;
using eCommerceApp.ClientLibrary.DTOs.Responses;

namespace eCommerceApp.ClientLibrary.Helpers.Interfaces
{
    public interface IApiCallHelper
    {
        Task<HttpResponseMessage> ApiCallTypeCallAsync<TModel>(ApiCall apiCall);
        Task<TResponse> GetServiceResponseAsync<TResponse>(HttpResponseMessage message);
        ServiceResponse ConnectionError();
    }
}