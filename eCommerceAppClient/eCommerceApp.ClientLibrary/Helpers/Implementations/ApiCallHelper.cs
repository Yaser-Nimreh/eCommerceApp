using eCommerceApp.ClientLibrary.DTOs.Api;
using eCommerceApp.ClientLibrary.DTOs.Responses;
using eCommerceApp.ClientLibrary.Helpers.Interfaces;
using System.Net.Http.Json;

namespace eCommerceApp.ClientLibrary.Helpers.Implementations
{
    public class ApiCallHelper : IApiCallHelper
    {
        public async Task<HttpResponseMessage> ApiCallTypeCallAsync<TModel>(ApiCall apiCall)
        {
            try
            {
                switch (apiCall.Type)
                {
                    case "post":
                        return await apiCall.Client!.PostAsJsonAsync(apiCall.Route, (TModel)apiCall.Model!);
                    case "put":
                        return await apiCall.Client!.PutAsJsonAsync(apiCall.Route, (TModel)apiCall.Model!);
                    case "delete":
                        return await apiCall.Client!.DeleteAsync($"{apiCall.Route}/{apiCall.Id}");
                    case "get":
                        string idRoute = apiCall.Id != null ? $"/{apiCall.Id}" : null!;
                        return await apiCall.Client!.GetAsync($"{apiCall.Route}{idRoute}");
                    default:
                        throw new Exception("API call type not specified");
                }
            }
            catch
            {
                return null!;
            }
        }

        public ServiceResponse ConnectionError()
        {
            return new ServiceResponse(Message: "Error occurred in communicating to the server");
        }

        public async Task<TResponse> GetServiceResponseAsync<TResponse>(HttpResponseMessage message)
        {
            var response = await message.Content.ReadFromJsonAsync<TResponse>()!;
            return response!;
        }
    }
}