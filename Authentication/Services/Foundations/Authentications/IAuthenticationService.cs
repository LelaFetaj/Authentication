using Authentication.Models.Entities.Users;

namespace Authentication.Services.Foundations.Authentications
{
    public interface IAuthenticationService
    {
         ValueTask<bool> IsPasswordCorrect(
            User user,
            string password,
            bool lockoutOnFailure = false);
    }
}