using eCommerceApp.ClientLibrary.DTOs.Identity;
using eCommerceApp.ClientLibrary.DTOs.Responses;

namespace eCommerceApp.ClientLibrary.Services.Interfaces.Authentication
{
    public interface IAuthenticationService
    {
        Task<ServiceResponse> CreateUserAsync(CreateUser user);
        Task<LoginResponse> LoginUserAsync(LoginUser user);
        Task<LoginResponse> RefreshTokenAsync(string refreshToken);
    }
}