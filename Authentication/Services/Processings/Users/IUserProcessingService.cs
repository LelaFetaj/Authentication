using Authentication.Models.DTOs.Users;
using Authentication.Models.Entities.Users;

namespace Authentication.Services.Processings.Users {
    public interface IUserProcessingService 
    {
        ValueTask<User> CreateUserWithRoleAsync(CreateUserDto createUserDto, string roleName);
        ValueTask<List<User>> RetrieveAllUsersAsync();
        ValueTask<User> RetrieveUserByIdAsync(Guid userId);
        ValueTask<User> RetrieveUserByUsernameAsync(string username);
        ValueTask<string> RetrieveUserRoleAsync(User user);
        ValueTask<User> ModifyUserAsync(ModifyUserDto modifyUserDto);
        ValueTask<User> RemoveUserByIdAsync(Guid userId);
    }
}
