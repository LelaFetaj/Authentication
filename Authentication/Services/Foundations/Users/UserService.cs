using Authentication.Models.Entities.Users;
using Authentication.Repositories.Users;
using Microsoft.AspNetCore.Identity;

namespace Authentication.Services.Foundations.Users
{
    sealed partial class UserService : IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public ValueTask<User> RegisterUserAsync(User user, string password) =>
            TryCatch(async () =>
            {
                ValidateUserOnRegister(user, password);

                return await this.userRepository.InsertUserAsync(user, password);
            });

        public ValueTask<List<User>> RetrieveAllUsersAsync() =>
            TryCatch(this.userRepository.SelectAllUsersAsync);

        public ValueTask<User> RetrieveUserByIdAsync(Guid userId) =>
            TryCatch(async () =>
            {
                ValidateUserId(userId);
                User user = await this.userRepository.SelectUserByIdAsync(userId);
                ValidateStorageUser(user, userId);

                return user;
            });

        public ValueTask<User> RetrieveUserByUsernameAsync(string username) =>
            TryCatch(async () =>
            {
                ValidateUsername(username);

                return await this.userRepository.SelectUserByUsernameAsync(username);
            });

        public ValueTask<User> ModifyUserAsync(User user) =>
            TryCatch(async () =>
            {
                ValidateUserOnModify(user);

                return await this.userRepository.UpdateUserAsync(user);
            });

        public ValueTask<User> RemoveUserByIdAsync(Guid userId) =>
            TryCatch(async () =>
            {
                ValidateUserId(userId);
                User user = await this.userRepository.SelectUserByIdAsync(userId);
                ValidateStorageUser(user, userId);

                return user;
            });

        public ValueTask<bool> AssignUserRole(User user, string roleName) =>
            TryCatch(async () =>
                {
                ValidateUserIsNull(user);
                ValidateRoleName(roleName);

                IdentityResult result =
                    await this.userRepository.AddToRoleAsync(user, roleName);

                return result.Succeeded;
            });

        public ValueTask<string> RetreiveUserRoleAsync(User user) =>
            TryCatch(async () =>
            {
                ValidateUserIsNull(user);

                var roles = await this.userRepository.GetUserRoles(user);

                return roles.FirstOrDefault();
            });

        public ValueTask<bool> RemoveFromRoleAsync(User user, string roleName) =>
            TryCatch(async () =>
            {
                ValidateRoleName(roleName);

                IdentityResult result =
                await this.userRepository.RemoveFromRoleAsync(user, roleName);

                return result.Succeeded;
            });
    }
}
