using eCommerceApp.Domain.Entities.Identity;
using eCommerceApp.Domain.Repositories.Authentication;
using eCommerceApp.Infrastructure.Data;
using eCommerceApp.Infrastructure.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace eCommerceApp.Infrastructure.Repositories.Authentication
{
    public class TokenManagement(ApplicationDbContext context, IOptions<JwtSettings> options) : ITokenManagement
    {
        private readonly JwtSettings jwtSettings = options.Value;
        public async Task<int> AddRefreshTokenAsync(string userId, string refreshToken)
        {
            context.RefreshTokens.Add(new RefreshToken
            {
                UserId = userId,
                Token = refreshToken
            });
            return await context.SaveChangesAsync();
        }

        public string GenerateToken(List<Claim> userClaims)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddHours(2);
            var token = new JwtSecurityToken(
                issuer: jwtSettings.Issuer,
                audience: jwtSettings.Audience,
                claims: userClaims,
                expires: expiration,
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GetRefreshToken()
        {
            const int byteSize = 64;
            byte[] randomBytes = new byte[byteSize];
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }
            string refreshToken = Convert.ToBase64String(randomBytes);
            return WebUtility.UrlEncode(refreshToken);
        }

        public List<Claim> GetUserClaimsFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);
            if (jwtToken != null) return [.. jwtToken.Claims];
            else return [];
        }

        public async Task<string> GetUserIdByRefreshTokenAsync(string refreshToken) =>
            (await context.RefreshTokens.FirstOrDefaultAsync(_ => _.Token == refreshToken))!.UserId;

        public async Task<int> UpdateRefreshTokenAsync(string userId, string refreshToken)
        {
            var user = await context.RefreshTokens.FirstOrDefaultAsync(_ => _.Token == refreshToken);
            if (user == null) return -1;
            user.Token = refreshToken;
            return await context.SaveChangesAsync();
        }

        public async Task<bool> ValidateRefreshTokenAsync(string refreshToken)
        {
            var user = await context.RefreshTokens.FirstOrDefaultAsync(_ => _.Token == refreshToken);
            return user != null;
        }
    }
}