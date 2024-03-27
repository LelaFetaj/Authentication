using Authentication.Models.Entities.Users;
using Microsoft.AspNetCore.Identity;

namespace Authentication.Repositories.Users
{
    public interface IUserRepository
    {
        ValueTask<User> InsertUserAsync(User user, string password);
        ValueTask<List<User>> SelectAllUsersAsync();
        ValueTask<User> SelectUserByIdAsync(Guid userId);
        ValueTask<User> SelectUserByUsernameAsync(string username);
        ValueTask<User> UpdateUserAsync(User user);
        ValueTask<User> DeleteUserAsync(User user);
        ValueTask<IdentityResult> AddToRoleAsync(User user, string roleName);
        ValueTask<IdentityResult> RemoveFromRoleAsync(User user, string role);
        ValueTask<bool> IsInRoleAsync(User user, string role);
        ValueTask<IList<User>> GetUsersInRoleAsync(string roleName);
        ValueTask<IEnumerable<string>> GetUserRoles(User user);
    }
}
