using eCommerceApp.ClientLibrary.Helpers.Constant;
using eCommerceApp.ClientLibrary.Helpers.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace eCommerceApp.ClientLibrary.Helpers.Implementations
{
    public class CustomAuthenticationStateProvider(ITokenService tokenService) : AuthenticationStateProvider
    {
        private readonly ClaimsPrincipal _anonymous = new(new ClaimsIdentity());
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                string jwt = await tokenService.GetJwtTokenAsync(Constants.Cookie.Name);
                if (string.IsNullOrEmpty(jwt))
                    return await Task.FromResult(new AuthenticationState(_anonymous));

                var claims = GetClaims(jwt);
                if (claims.Count == 0) return await Task.FromResult(new AuthenticationState(_anonymous));

                var claimPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwtAuth"));
                return await Task.FromResult(new AuthenticationState(claimPrincipal));
            }
            catch 
            {
                return await Task.FromResult(new AuthenticationState(_anonymous));
            }
        }

        public void NotifyAuthenticationState()
        {
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        private static List<Claim> GetClaims(string jwt) 
        {
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(jwt);
            return token.Claims.ToList();
        }
    }
}