using Authentication.Models.Exceptions.Authentications;
using Microsoft.AspNetCore.Identity;

namespace Authentication.Services.Foundations.Authentications
{
    sealed partial class AuthenticationService
    {
        private static void ThrowExceptionIfFailed(SignInResult result)
        {
            if (!result.Succeeded) {
                throw new InvalidPasswordException();
            }
        }
    }
}
