using Authentication.Models.DTOs.Users;
using Authentication.Models.Entities.Users;
using Authentication.Services.Foundations.Users;

namespace Authentication.Services.Processings.Users {
    public class UserProcessingService : IUserProcessingService 
    {
        private readonly IUserService userService;

        public UserProcessingService(IUserService userService) 
        {
            this.userService = userService;
        }

        public async ValueTask<User> CreateUserWithRoleAsync(CreateUserDto createUserDto, string roleName) 
        {

            var user = new User {
                UserName = createUserDto.UserName,
                FirstName = createUserDto.Firstname,
                LastName = createUserDto.Lastname,
                Gender = createUserDto.Gender,
                BirthDate = (DateTimeOffset)createUserDto.BirthDate,
                CreatedDate = DateTimeOffset.UtcNow,
                UpdatedDate = DateTimeOffset.Now
            };

            _ = await this.userService.RegisterUserAsync(user, createUserDto.Password);

            if (!string.IsNullOrWhiteSpace(roleName)) 
            {
                await this.userService.AssignUserRole(user, roleName);
            }

            return user;
        }

        public async ValueTask<List<User>> RetrieveAllUsersAsync() =>
            await this.userService.RetrieveAllUsersAsync();

        public async ValueTask<User> RetrieveUserByIdAsync(Guid userId) =>
            await this.userService.RetrieveUserByIdAsync(userId);

        public async ValueTask<User> RetrieveUserByUsernameAsync(string username) =>
            await this.userService.RetrieveUserByUsernameAsync(username);

        public async ValueTask<string> RetrieveUserRoleAsync(User user) =>
            await this.userService.RetreiveUserRoleAsync(user);

        public async ValueTask<User> ModifyUserAsync(ModifyUserDto modifyUserDto) 
        {
            User storageUser = await this.userService.RetrieveUserByIdAsync(modifyUserDto.Id);

            storageUser.UserName = string.IsNullOrWhiteSpace(modifyUserDto.Username)
                ? storageUser.UserName
                : modifyUserDto.Username;

            storageUser.FirstName = string.IsNullOrWhiteSpace(modifyUserDto.Firstname)
                ? storageUser.FirstName
                : modifyUserDto.Firstname;

            storageUser.LastName = string.IsNullOrWhiteSpace(modifyUserDto.Lastname)
                ? storageUser.LastName
                : modifyUserDto.Lastname;

            storageUser.Gender = modifyUserDto.Gender == Gender.Other
                ? storageUser.Gender
                : modifyUserDto.Gender;

            storageUser.BirthDate = modifyUserDto.BirthDate == default
                ? storageUser.BirthDate
                : modifyUserDto.BirthDate;

            storageUser.UpdatedDate = DateTimeOffset.UtcNow;

            return await this.userService.ModifyUserAsync(storageUser);
        }

        public async ValueTask<User> RemoveUserByIdAsync(Guid userId) =>
            await this.userService.RemoveUserByIdAsync(userId);

        public async ValueTask<bool> AssignUserRoleAsync(User user, string roleName) =>
             await this.userService.AssignUserRole(user, roleName);

        public async ValueTask<bool> RemoveFromRoleAsync (User user, string roleName) =>
            await this.userService.RemoveFromRoleAsync(user, roleName);
    }
}
