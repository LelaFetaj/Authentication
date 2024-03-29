using Authentication.Models.DTOs.Authentications;
using Authentication.Models.DTOs.Users;
using Authentication.Models.Entities.Roles;
using Authentication.Models.Entities.Users;
using Authentication.Services.Processings.Authentications;
using Authentication.Services.Processings.Roles;
using Authentication.Services.Processings.Users;

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
    }
}
