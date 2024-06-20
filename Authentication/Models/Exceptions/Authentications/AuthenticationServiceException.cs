using Authentication.Services.Foundations;
using System.Runtime.Serialization;

namespace Authentication.Models.Exceptions.Authentications
{
    [Serializable]
    public class AuthenticationServiceException : ConnException, IFailedDependencyException
    {
        protected AuthenticationServiceException(
           SerializationInfo serializationInfo,
           StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }

        public AuthenticationServiceException(Exception innerException)
             : base(message: "An error with sign in occurred, contact support.", innerException) { }
    }
}
