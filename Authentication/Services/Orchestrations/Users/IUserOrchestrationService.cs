using Authentication.Models.DTOs.Authentications;
using Authentication.Models.DTOs.Users;
using Authentication.Models.Entities.Users;

namespace Authentication.Services.Orchestrations.Users {
    public interface IUserOrchestrationService 
    {
        ValueTask<AuthenticatedResponse> UserLoginAsync(LoginRequestDto loginRequest);
        ValueTask<AuthenticatedResponse> UserRegisterAsync(CreateUserDto registerRequest);
        ValueTask<List<User>> RetrieveAllUsersAsync();
        ValueTask<string> GetUserRole(string username);
    }
}
