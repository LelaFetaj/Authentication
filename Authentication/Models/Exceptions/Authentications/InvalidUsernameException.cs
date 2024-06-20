using Authentication.Services.Foundations;
using System.Runtime.Serialization;

namespace Authentication.Models.Exceptions.Authentications
{
    [Serializable]
    public class InvalidUsernameException : ConnException, IUnauthorizedException
    {
        protected InvalidUsernameException(
            SerializationInfo serializationInfo,
            StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }

        public InvalidUsernameException()
            : base("The username does not exist.")
        {
        }

        public InvalidUsernameException(string message)
            : base(message)
        { }
    }
}
