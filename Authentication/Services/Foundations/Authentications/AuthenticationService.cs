using Authentication.Models.Entities.Users;
using Authentication.Repositories.Authentications;

namespace Authentication.Services.Foundations.Authentications
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAuthenticationRepository authenticationRepository;

        public AuthenticationService(IAuthenticationRepository authenticationRepository)
        {
            this.authenticationRepository = authenticationRepository;
        }

        public async ValueTask<bool> IsPasswordCorrect(
            User user,
            string password,
            bool lockoutOnFailure = false) 
            {
                var result = await this.authenticationRepository.CheckPasswordSignInAsync(
                    user,
                    password,
                    lockoutOnFailure);

                return result.Succeeded;
            }
    }
}