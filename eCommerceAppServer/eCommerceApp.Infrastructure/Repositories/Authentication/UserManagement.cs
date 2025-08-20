using eCommerceApp.Domain.Entities.Identity;
using eCommerceApp.Domain.Repositories.Authentication;
using eCommerceApp.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace eCommerceApp.Infrastructure.Repositories.Authentication
{
    public class UserManagement(IRoleManagement roleManagement, UserManager<ApplicationUser> userManager, ApplicationDbContext context) : IUserManagement
    {
        public async Task<bool> CreateUserAsync(ApplicationUser user)
        {
            var _user = await GetUserByEmailAsync(user.Email!);
            if (_user != null) return false;
            return (await userManager.CreateAsync(user!, user!.PasswordHash!)).Succeeded;
        }

        public async Task<IEnumerable<ApplicationUser>?> GetAllUsersAsync() => await context.Users.ToListAsync();

        public async Task<ApplicationUser?> GetUserByEmailAsync(string email) => await userManager.FindByEmailAsync(email);

        public async Task<ApplicationUser> GetUserByIdAsync(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            return user!;
        }

        public async Task<List<Claim>> GetUserClaimsAsync(string email)
        {
            var user = await GetUserByEmailAsync(email);
            string? roleName = await roleManagement.GetUserRoleAsync(user!.Email!);

            List<Claim> claims = [
                new Claim("Fullname", user!.Fullname),
                new Claim(ClaimTypes.NameIdentifier, user!.Id),
                new Claim(ClaimTypes.Email, user!.Email!),
                new Claim(ClaimTypes.Role, roleName!)
            ];
            return claims;
        }

        public async Task<bool> LoginUserAsync(ApplicationUser user)
        {
            var _user = await GetUserByEmailAsync(user.Email!);
            if (_user is null) return false;

            string? roleName = await roleManagement.GetUserRoleAsync(_user!.Email!);
            if (string.IsNullOrEmpty(roleName)) return false;

            return await userManager.CheckPasswordAsync(_user, user.PasswordHash!);
        }

        public async Task<int> RemoveUserByEmailAsync(string email)
        {
            var user = await context.Users.FirstOrDefaultAsync(_ => _.Email == email);
            context.Users.Remove(user!);
            return await context.SaveChangesAsync();
        }
    }
}