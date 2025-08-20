using eCommerceApp.Domain.Entities.Identity;

namespace eCommerceApp.Domain.Repositories.Authentication
{
    public interface IRoleManagement
    {
        Task<string?> GetUserRoleAsync(string userEmail);
        Task<bool> AddUserToRoleAsync(ApplicationUser user, string roleName);
    }
}