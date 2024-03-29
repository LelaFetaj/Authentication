using Authentication.Models.Entities.Users;
using Authentication.Repositories.Authentications;
using Authentication.Repositories.Roles;
using Authentication.Repositories.Users;
using Microsoft.AspNetCore.Identity;

namespace Authentication.Services.Foundations.Users {
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IRoleRepository roleRepository;
        private readonly IAuthenticationRepository authenticationRepository;
        private readonly string privateKey;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async ValueTask<User> RegisterUserAsync(User user, string password) =>
            await this.userRepository.InsertUserAsync(user, password);

        public async ValueTask<List<User>> RetrieveAllUsersAsync() =>
            await this.userRepository.SelectAllUsersAsync();

        public async ValueTask<User> RetrieveUserByIdAsync(Guid userId) =>
            await this.userRepository.SelectUserByIdAsync(userId);

        public async ValueTask<User> RetrieveUserByUsernameAsync(string username) =>
            await this.userRepository.SelectUserByUsernameAsync(username);

        public async ValueTask<User> ModifyUserAsync(User user) =>
            await this.userRepository.UpdateUserAsync(user);

        public async ValueTask<User> RemoveUserByIdAsync(Guid userId) 
        {
            User maybeUser = await this.userRepository.SelectUserByIdAsync(userId);

            return await this.userRepository.DeleteUserAsync(maybeUser);
        }

        public async ValueTask<bool> AssignUserRole(User user, string roleName) 
        { 
            IdentityResult result =
                await this.userRepository.AddToRoleAsync(user, roleName);

            return result.Succeeded;
        }

        public async ValueTask<string> RetreiveUserRoleAsync(User user) 
        { 
            var roles = await this.userRepository.GetUserRoles(user);

            return roles.FirstOrDefault();
        }

        public async ValueTask<string> GetRoleOfUserAsync(string username) 
        {
            var user =
                await this.userRepository.SelectUserByUsernameAsync(username);

            IEnumerable<string> userRoles = await this.userRepository.GetUserRoles(user);
            string userRole = userRoles.FirstOrDefault();

            return userRole;
        }
    }
}
