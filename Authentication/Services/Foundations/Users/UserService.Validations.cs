using Authentication.Models.Entities.Users;
using Authentication.Models.Exceptions.Users;

namespace Authentication.Services.Foundations.Users
{
    sealed partial class UserService
    {
        private static void ValidateUserOnRegister(User user, string password)
        {
            ValidateUserIsNull(user);

            var invalidUserException = new InvalidUserException();

            invalidUserException.Validate(
                (Rule: ModelValidator.IsInvalid(user.UserName, nameof(User.UserName)), Parameter: nameof(User.UserName)),
                (Rule: ModelValidator.IsInvalidEmail(user.Email), Parameter: nameof(User.Email)),
                (Rule: ModelValidator.IsInvalidPassword(password), Parameter: nameof(password)),
                (Rule: ModelValidator.IsInvalid(user.FirstName, nameof(User.FirstName)), Parameter: nameof(User.FirstName)),
                (Rule: ModelValidator.IsInvalid(user.LastName, nameof(User.LastName)), Parameter: nameof(User.LastName))
                );
        }

        private static void ValidateUserOnModify(User user)
        {
            ValidateUserIsNull(user);
            ValidateUserId(user.Id);

            var invalidUserException = new InvalidUserException();

            invalidUserException.Validate(
                (Rule: ModelValidator.IsInvalid(user.Id, nameof(User.Id)), Parameter: nameof(User.Id)),
                (Rule: ModelValidator.IsInvalid(user.UserName, nameof(User.UserName)), Parameter: nameof(User.UserName)),
                (Rule: ModelValidator.IsInvalidEmail(user.Email), Parameter: nameof(User.Email)),
                (Rule: ModelValidator.IsInvalid(user.FirstName, nameof(User.FirstName)), Parameter: nameof(User.FirstName)),
                (Rule: ModelValidator.IsInvalid(user.LastName, nameof(User.LastName)), Parameter: nameof(User.LastName))
                );
        }

        private static void ValidateStorageUser(User storageUser, Guid userId)
        {
            if (storageUser is null)
                throw new NotFoundUserException(userId);
        }

        private static void ValidateUserIsNull(User user)
        {
            if (user is null) {
                throw new NullUserException();
            }
        }

        private static void ValidateUserId(Guid userId)
        {
            if (userId == Guid.Empty) {
                throw new InvalidUserException(
                    parameterName: nameof(User.Id),
                    parameterValue: userId);
            }
        }

        private static void ValidateUserEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) {
                throw new InvalidUserException(
                    parameterName: nameof(User.Email),
                    parameterValue: email);
            }
        }

        private static void ValidateRoleName(string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName)) {
                throw new InvalidUserException(
                    parameterName: "Role name",
                    parameterValue: roleName);
            }
        }

        private static void ValidateUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username)) {
                throw new InvalidUserException(
                    parameterName: nameof(User.UserName),
                    parameterValue: username);
            }
        }
    }
}
