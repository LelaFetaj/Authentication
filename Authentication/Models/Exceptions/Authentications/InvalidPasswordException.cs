using Authentication.Services.Foundations;
using System.Runtime.Serialization;

namespace Authentication.Models.Exceptions.Authentications
{
    [Serializable]
    public class InvalidPasswordException : ConnException, IUnauthorizedException
    {
        protected InvalidPasswordException(
            SerializationInfo serializationInfo,
            StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }

        public InvalidPasswordException()
            : base("Passowrd is incorrect.")
        {
        }

        public InvalidPasswordException(string message)
            : base(message)
        { }
    }
}
