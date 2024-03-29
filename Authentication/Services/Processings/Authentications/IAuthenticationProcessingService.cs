using Authentication.Models.Entities.Users;

namespace Authentication.Services.Processings.Authentications
{
    public interface IAuthenticationProcessingService
    {
        ValueTask<bool> IsPasswordCorrect(User user, string password);
        string GenerateJwtToken(User user, string role);
    }
}