using Authentication.Models.DTOs.Authentications;
using Authentication.Models.DTOs.Users;
using Authentication.Models.Entities.Roles;
using Authentication.Models.Entities.Users;
using Authentication.Services.Processings.Authentications;
using Authentication.Services.Processings.Roles;
using Authentication.Services.Processings.Users;
using Microsoft.AspNetCore.Identity.Data;

namespace Authentication.Services.Orchestrations.Users {
    public class UserOrchestrationService : IUserOrchestrationService
    {
        private readonly IUserProcessingService userProcessingService;
        private readonly IRoleProcessingService roleProcessingService;
        private readonly IAuthenticationProcessingService authenticationProcessingService;

        public UserOrchestrationService(
            IUserProcessingService userProcessingService,
            IRoleProcessingService roleProcessingService,
            IAuthenticationProcessingService authenticationProcessingService
        )
        {
            this.userProcessingService = userProcessingService;
            this.roleProcessingService = roleProcessingService;
            this.authenticationProcessingService = authenticationProcessingService;
        }

         public async ValueTask<AuthenticatedResponse> UserLoginAsync(LoginRequestDto loginRequest)
        {
            var user =
                await this.userProcessingService.RetrieveUserByUsernameAsync(loginRequest?.Username);

            // if (user is null)
            // {
            //     throw "User is null";
            // }

            await this.authenticationProcessingService.IsPasswordCorrect(user, loginRequest.Password);

            string userRole = await this.userProcessingService.RetrieveUserRoleAsync(user);

            return new AuthenticatedResponse
            {
                AuthenticationToken =
                    this.authenticationProcessingService.GenerateJwtToken(user, userRole)
            };
        }

        public async ValueTask<AuthenticatedResponse> UserRegisterAsync(CreateUserDto registerRequest)
        {
            var storageUser =
                await this.userProcessingService.RetrieveUserByUsernameAsync(registerRequest.UserName);

            // if (storageUser is not null)
            // {
            //     throw ("A user with this name already exists! Please choose another name!");
            // }
            
            Role role =
                await this.roleProcessingService.RetrieveRoleByNameAsync(registerRequest.RoleName);

            // if (role is null)
            // {
            //     throw ("A role with this name doesn't exists! Please choose another rolename!");
            // }

            var user =
                await this.userProcessingService.CreateUserWithRoleAsync(registerRequest, registerRequest.RoleName);

            return new AuthenticatedResponse
            {
                AuthenticationToken = this.authenticationProcessingService.GenerateJwtToken(user, role.Name)
            };
        }

        public async ValueTask<List<User>> RetrieveAllUsersAsync() =>
            await this.userProcessingService.RetrieveAllUsersAsync();

        public async ValueTask<string> GetUserRole(string username) 
        {
            var user = await this.userProcessingService.RetrieveUserByUsernameAsync(username);

            return await this.userProcessingService.RetrieveUserRoleAsync(user);
        }

        public async ValueTask<User> ModifyUserRole(Guid userId, string roleName)
        {

            var storageUser = 
                await this.userProcessingService.RetrieveUserByIdAsync(userId);

            if (storageUser == null) 
            { 
                throw new Exception("User not found.");
            }

            Role role =
                await this.roleProcessingService.RetrieveRoleByNameAsync(roleName);

            if (role == null) 
            {
                throw new Exception("Role not found.");
            }

            var currentUserRole = await this.userProcessingService.RetrieveUserRoleAsync(storageUser);

            if (currentUserRole.Any()) 
            {
                var removeRolesResult = await this.userProcessingService.RemoveFromRoleAsync(storageUser, currentUserRole);

                if (!removeRolesResult) 
                {
                    throw new Exception("Failed to remove user from current roles.");
                }
            }

            var addRoleResult = await this.userProcessingService.AssignUserRoleAsync(storageUser, roleName);

            if (!addRoleResult) 
            {
                throw new Exception("Failed to add user to the new role.");
            }

            return storageUser;
        }
    }
}
