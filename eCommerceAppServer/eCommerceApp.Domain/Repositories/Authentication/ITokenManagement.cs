using System.Security.Claims;

namespace eCommerceApp.Domain.Repositories.Authentication
{
    public interface ITokenManagement
    {
        string GetRefreshToken();
        List<Claim> GetUserClaimsFromToken(string token);
        Task<bool> ValidateRefreshTokenAsync(string refreshToken);
        Task<string> GetUserIdByRefreshTokenAsync(string refreshToken);
        Task<int> AddRefreshTokenAsync(string userId, string refreshToken);
        Task<int> UpdateRefreshTokenAsync(string userId, string refreshToken);
        string GenerateToken(List<Claim> userClaims);
    }
}