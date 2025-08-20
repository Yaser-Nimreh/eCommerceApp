using eCommerceApp.ClientLibrary.Helpers.Interfaces;
using NetcodeHub.Packages.WebAssembly.Storage.Cookie;

namespace eCommerceApp.ClientLibrary.Helpers.Implementations
{
    public class TokenService(IBrowserCookieStorageService cookieService) : ITokenService
    {
        public string FormToken(string jwtToken, string refreshToken)
        {
            return $"{jwtToken}--{refreshToken}";
        }

        public async Task<string> GetJwtTokenAsync(string key)
        {
            return await GetTokenAsync(key, 0);
        }

        private async Task<string> GetTokenAsync(string key, int position)
        {
            try
            {
                string token = await cookieService.GetAsync(key);
                return token != null ? token.Split("--")[position] : null!;
            }
            catch
            {
                return null!;
            }
        }

        public async Task<string> GetRefreshTokenAsync(string key)
        {
            return await GetTokenAsync(key, 1);
        }

        public async Task RemoveCookieAsync(string key)
        {
            await cookieService.RemoveAsync(key);
        }

        public async Task SetCookieAsync(string key, string value, int days, string path)
        {
            await cookieService.SetAsync(key, value, days, path);
        }
    }
}