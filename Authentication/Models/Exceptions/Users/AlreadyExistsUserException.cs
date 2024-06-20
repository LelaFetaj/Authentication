using Authentication.Services.Foundations;
using System.Runtime.Serialization;

namespace Authentication.Models.Exceptions.Users
{
    [Serializable]
    public class AlreadyExistsUserException : ConnException, IUnprocessableException
    {
        protected AlreadyExistsUserException(
            SerializationInfo serializationInfo,
            StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }

        public AlreadyExistsUserException(string email)
            : base($"Account with '{email}' already exists.")
        { }

        public AlreadyExistsUserException(Exception innerException)
            : base(message: "User with the same details already exists.", innerException) { }
    }
}
