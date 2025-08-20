using eCommerceApp.Domain.Entities.Identity;
using eCommerceApp.Domain.Repositories.Authentication;
using Microsoft.AspNetCore.Identity;

namespace eCommerceApp.Infrastructure.Repositories.Authentication
{
    public class RoleManagement(UserManager<ApplicationUser> userManager) : IRoleManagement
    {
        public async Task<bool> AddUserToRoleAsync(ApplicationUser user, string roleName) =>
            (await userManager.AddToRoleAsync(user, roleName)).Succeeded;

        public async Task<string?> GetUserRoleAsync(string userEmail)
        {
            var user = await userManager.FindByEmailAsync(userEmail);
            return (await userManager.GetRolesAsync(user!)).FirstOrDefault();
        }
    }
}