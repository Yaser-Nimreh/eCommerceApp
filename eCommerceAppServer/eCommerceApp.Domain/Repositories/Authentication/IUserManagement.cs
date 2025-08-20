using eCommerceApp.Domain.Entities.Identity;
using System.Security.Claims;

namespace eCommerceApp.Domain.Repositories.Authentication
{
    public interface IUserManagement
    {
        Task<bool> CreateUserAsync(ApplicationUser user);
        Task<bool> LoginUserAsync(ApplicationUser user);
        Task<ApplicationUser?> GetUserByEmailAsync(string email);
        Task<ApplicationUser> GetUserByIdAsync(string id);
        Task<IEnumerable<ApplicationUser>?> GetAllUsersAsync();
        Task<int> RemoveUserByEmailAsync(string email);
        Task<List<Claim>> GetUserClaimsAsync(string email);
    }
}