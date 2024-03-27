using Authentication.Models.DTOs.Authentications;
using Authentication.Models.DTOs.Users;
using Authentication.Models.Entities.Roles;
using Authentication.Models.Entities.Users;
using Authentication.Repositories.Authentications;
using Authentication.Repositories.Roles;
using Authentication.Repositories.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace Authentication.Services.Foundations.Users {
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IRoleRepository roleRepository;
        private readonly IAuthenticationRepository authenticationRepository;
        private readonly string privateKey;

        public UserService(IUserRepository userRepository, 
            IRoleRepository roleRepository, 
            IConfiguration configuration,
            IAuthenticationRepository authenticationRepository)
        {
            this.userRepository = userRepository;
            this.roleRepository = roleRepository;
            this.authenticationRepository = authenticationRepository;
            this.privateKey = configuration["AuthConfiguration:SigningKey"];
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

            return await this.userRepository.SelectUserByIdAsync(userId);
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





        public async ValueTask<AuthenticatedResponse> UserRegisterAsync(CreateUserDto createUserDto)
        {
            var storageUser =
               await this.userRepository.SelectUserByUsernameAsync(createUserDto.UserName);

            Role role =
                await this.roleRepository.SelectRoleByNameAsync(createUserDto.RoleName);

            var user =
                await CreateUserWithRoleAsync(createUserDto, createUserDto.RoleName);

            return new AuthenticatedResponse
            {
                AuthenticationToken = GenerateJwtToken(user, role.Name)
            };
        }

        public async ValueTask<AuthenticatedResponse> UserLoginAsync(LoginRequestDto loginRequest)
        {
            var user =
                await this.userRepository.SelectUserByUsernameAsync(loginRequest?.Username);

            await this.authenticationRepository.CheckPasswordSignInAsync(user, loginRequest.Password);

            //string userRole = await this.userRepository.GetUserRoles(user);

            IEnumerable<string> userRoles = await this.userRepository.GetUserRoles(user);
            string userRole = userRoles.FirstOrDefault();


            return new AuthenticatedResponse
            {
                AuthenticationToken = GenerateJwtToken(user, userRole)
            };
        }

        

        

        private string GenerateJwtToken(User user, string role)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(type: ClaimTypes.NameIdentifier,value: user.Id.ToString()),
                new Claim(ClaimTypes.Role,JsonSerializer.Serialize(new List<string> { role }))
            };

            var securityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(privateKey));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

    }
}
