using Authentication.Models.Entities.Users;

namespace Authentication.Services.Foundations.Users
{
    public interface IUserService
    {
        ValueTask<User> RegisterUserAsync(User user, string password);
        ValueTask<List<User>> RetrieveAllUsersAsync();
        ValueTask<User> RetrieveUserByIdAsync(Guid userId);
        ValueTask<User> RetrieveUserByUsernameAsync(string username);
        ValueTask<User> ModifyUserAsync(User user);
        ValueTask<User> RemoveUserByIdAsync(Guid userId);
        ValueTask<bool> AssignUserRole(User user, string roleName);
        ValueTask<string> RetreiveUserRoleAsync(User user);
        ValueTask<bool> RemoveFromRoleAsync(User user, string roleName);
    }
}
