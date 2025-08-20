using eCommerceApp.Application.DTOs.Identity;
using eCommerceApp.Application.DTOs.Responses;

namespace eCommerceApp.Application.Services.Interfaces.Authentication
{
    public interface IAuthenticationService
    {
        Task<ServiceResponse> CreateUserAsync(CreateUser user);
        Task<LoginResponse> LoginUserAsync(LoginUser user);
        Task<LoginResponse> RefreshTokenAsync(string refreshToken);
    }
}