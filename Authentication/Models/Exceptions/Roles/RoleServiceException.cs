using Authentication.Services.Foundations;
using System.Runtime.Serialization;

namespace Authentication.Models.Exceptions.Roles
{
    [Serializable]
    public class RoleServiceException : ConnException, IFailedDependencyException
    {
        protected RoleServiceException(
            SerializationInfo serializationInfo,
            StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }

        public RoleServiceException(Exception innerException)
            : base(message: "Service error occurred, contact support.", innerException) { }
    }
}
