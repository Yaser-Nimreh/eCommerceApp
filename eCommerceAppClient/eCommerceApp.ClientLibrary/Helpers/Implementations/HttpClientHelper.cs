using eCommerceApp.ClientLibrary.Helpers.Constant;
using eCommerceApp.ClientLibrary.Helpers.Interfaces;
using System.Net.Http.Headers;

namespace eCommerceApp.ClientLibrary.Helpers.Implementations
{
    public class HttpClientHelper(IHttpClientFactory clientFactory, ITokenService tokenService) : IHttpClientHelper
    {
        public async Task<HttpClient> GetPrivateClientAsync()
        {
            var client = clientFactory.CreateClient(Constants.ApiClient.Name);
            string token = await tokenService.GetJwtTokenAsync(Constants.Cookie.Name);
            if (string.IsNullOrEmpty(token))
                return client;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.Authentication.Type, token);
            return client;
        }

        public HttpClient GetPublicClient()
        {
            return clientFactory.CreateClient(Constants.ApiClient.Name);
        }
    }
}