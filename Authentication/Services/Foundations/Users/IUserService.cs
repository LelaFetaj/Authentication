﻿using Authentication.Models.DTOs.Authentications;
using Authentication.Models.DTOs.Users;
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
        ValueTask<string> GetRoleOfUserAsync(string username);


        ValueTask<AuthenticatedResponse> UserRegisterAsync(CreateUserDto createUserDto);
        ValueTask<AuthenticatedResponse> UserLoginAsync(LoginRequestDto loginRequest);
        ValueTask<List<User>> SelectAllUsersAsync();
        
    }
}
