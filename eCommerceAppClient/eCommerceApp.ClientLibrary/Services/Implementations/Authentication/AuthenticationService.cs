using eCommerceApp.ClientLibrary.DTOs;
using eCommerceApp.ClientLibrary.DTOs.Api;
using eCommerceApp.ClientLibrary.DTOs.Identity;
using eCommerceApp.ClientLibrary.DTOs.Responses;
using eCommerceApp.ClientLibrary.Helpers.Constant;
using eCommerceApp.ClientLibrary.Helpers.Interfaces;
using eCommerceApp.ClientLibrary.Services.Interfaces.Authentication;
using System.Web;

namespace eCommerceApp.ClientLibrary.Services.Implementations.Authentication
{
    public class AuthenticationService(IHttpClientHelper httpClientHelper, IApiCallHelper apiCallHelper) : IAuthenticationService
    {
        public async Task<ServiceResponse> CreateUserAsync(CreateUser user)
        {
            var client = await httpClientHelper.GetPrivateClientAsync();
            var apiCall = new ApiCall
            {
                Route = Constants.Authentication.CreateUser,
                Type = Constants.ApiCallType.Post,
                Client = client,
                Id = null!,
                Model = user
            };
            var result = await apiCallHelper.ApiCallTypeCallAsync<CreateUser>(apiCall);
            return result == null ? apiCallHelper.ConnectionError() :
                await apiCallHelper.GetServiceResponseAsync<ServiceResponse>(result);
        }

        public async Task<LoginResponse> LoginUserAsync(LoginUser user)
        {
            var client = await httpClientHelper.GetPrivateClientAsync();
            var apiCall = new ApiCall
            {
                Route = Constants.Authentication.LoginUser,
                Type = Constants.ApiCallType.Post,
                Client = client,
                Id = null!,
                Model = user
            };
            var result = await apiCallHelper.ApiCallTypeCallAsync<LoginUser>(apiCall);
            return result == null ? new LoginResponse(Message: apiCallHelper.ConnectionError().Message) :
                await apiCallHelper.GetServiceResponseAsync<LoginResponse>(result);
        }

        public async Task<LoginResponse> RefreshTokenAsync(string refreshToken)
        {
            var client = httpClientHelper.GetPublicClient();
            var apiCall = new ApiCall
            {
                Route = Constants.Authentication.RefreshToken,
                Type = Constants.ApiCallType.Post,
                Client = client,
                Id = HttpUtility.UrlEncode(refreshToken),
                Model = null!
            };
            var result = await apiCallHelper.ApiCallTypeCallAsync<object>(apiCall);
            return result == null ? new LoginResponse(Message: apiCallHelper.ConnectionError().Message) :
                await apiCallHelper.GetServiceResponseAsync<LoginResponse>(result);
        }
    }
}