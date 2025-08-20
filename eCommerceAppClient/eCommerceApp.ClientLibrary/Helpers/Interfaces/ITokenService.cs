namespace eCommerceApp.ClientLibrary.Helpers.Interfaces
{
    public interface ITokenService
    {
        Task<string> GetJwtTokenAsync(string key);
        Task<string> GetRefreshTokenAsync(string key);
        string FormToken(string jwtToken, string refreshToken);
        Task SetCookieAsync(string key, string value, int days, string path);
        Task RemoveCookieAsync(string key);
    }
}