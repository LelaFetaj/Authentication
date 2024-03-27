using Authentication.Models.Entities.Users;
using Microsoft.AspNetCore.Identity;

namespace Authentication.Repositories.Authentications
{
    public interface IAuthenticationRepository
    {
        ValueTask<SignInResult> CheckPasswordSignInAsync(
            User user,
            string password,
            bool lockoutOnFailure = false);
    }
}
