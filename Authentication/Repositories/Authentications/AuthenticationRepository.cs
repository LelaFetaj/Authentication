using Authentication.Models.Entities.Users;
using Microsoft.AspNetCore.Identity;

namespace Authentication.Repositories.Authentications
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly SignInManager<User> signInManager;

        public AuthenticationRepository(SignInManager<User> signInManager)
        {
            this.signInManager = signInManager;
        }

        public async ValueTask<SignInResult> CheckPasswordSignInAsync(
            User user,
            string password,
            bool lockoutOnFailure = false)
        {
            return await this.signInManager.CheckPasswordSignInAsync(
                user,
                password,
                lockoutOnFailure);
        }
    }
}
