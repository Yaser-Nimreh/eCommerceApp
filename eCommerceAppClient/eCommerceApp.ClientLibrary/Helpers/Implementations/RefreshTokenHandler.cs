using eCommerceApp.ClientLibrary.DTOs.Responses;
using eCommerceApp.ClientLibrary.Helpers.Constant;
using eCommerceApp.ClientLibrary.Helpers.Interfaces;
using eCommerceApp.ClientLibrary.Services.Interfaces.Authentication;

namespace eCommerceApp.ClientLibrary.Helpers.Implementations
{
    public class RefreshTokenHandler(ITokenService tokenService, IAuthenticationService authenticationService, IHttpClientHelper httpClientHelper) : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            bool IsPost = request.Method.Equals("POST");
            bool IsPut = request.Method.Equals("PUT");
            bool IsDelete = request.Method.Equals("DELETE");

            var result = await base.SendAsync(request, cancellationToken);
            if (IsPost || IsPut || IsDelete) 
            {
                if (result.StatusCode != System.Net.HttpStatusCode.Unauthorized) return result;
                var refreshToken = await tokenService.GetRefreshTokenAsync(Constants.Cookie.Name);

                if (string.IsNullOrEmpty(refreshToken)) return result;

                var loginResponse = await MakeApiCall(refreshToken);
                if (loginResponse == null)
                    return result;
                await httpClientHelper.GetPrivateClientAsync();
                return await base.SendAsync(request, cancellationToken);
            }
            return result;
        }

        private async Task<LoginResponse> MakeApiCall(string refreshToken)
        {
            var result = await authenticationService.RefreshTokenAsync(refreshToken);
            if (result.Success)
            {
                string cookieValue = tokenService.FormToken(result.Token, result.RefreshToken);
                await tokenService.RemoveCookieAsync(Constants.Cookie.Name);
                await tokenService.SetCookieAsync(Constants.Cookie.Name, cookieValue, Constants.Cookie.Days, Constants.Cookie.Path);
                return result;
            }
            return null!;
        }
    }
}