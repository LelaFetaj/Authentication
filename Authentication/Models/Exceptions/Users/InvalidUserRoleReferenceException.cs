using Authentication.Services.Foundations;
using System.Runtime.Serialization;

namespace Authentication.Models.Exceptions.Users
{
    [Serializable]
    public class InvalidUserRoleReferenceException : ConnException, IValidationException
    {
        protected InvalidUserRoleReferenceException(
            SerializationInfo serializationInfo,
            StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }

        public InvalidUserRoleReferenceException(Exception innerException)
            : base(message: "The user role reference is invalid.", innerException) { }
    }
}
