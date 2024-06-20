using Authentication.Services.Foundations;
using System.Runtime.Serialization;

namespace Authentication.Models.Exceptions.Users
{
    [Serializable]
    public class UserServiceException : ConnException, IFailedDependencyException
    {
        protected UserServiceException(
            SerializationInfo serializationInfo,
            StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }

        public UserServiceException(Exception innerException)
            : base(message: "Service error occurred, contact support.", innerException) { }
    }
}
