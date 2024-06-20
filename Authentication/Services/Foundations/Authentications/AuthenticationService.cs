using Authentication.Models.Entities.Users;
using Authentication.Repositories.Authentications;

namespace Authentication.Services.Foundations.Authentications
{
    sealed partial class AuthenticationService : IAuthenticationService
    {
        private readonly IAuthenticationRepository authenticationRepository;

        public AuthenticationService(IAuthenticationRepository authenticationRepository)
        {
            this.authenticationRepository = authenticationRepository;
        }

        public ValueTask<bool> IsPasswordCorrect(
            User user,
            string password,
            bool lockoutOnFailure = false) =>
            TryCatch(async () =>
            {
                var result = await this.authenticationRepository.CheckPasswordSignInAsync(
                    user,
                    password,
                    lockoutOnFailure);

                ThrowExceptionIfFailed(result);

                return result.Succeeded;
            });
    }
}