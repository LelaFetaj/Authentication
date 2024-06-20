using Authentication.Services.Foundations;
using System.Runtime.Serialization;

namespace Authentication.Models.Exceptions.Roles
{
    [Serializable]
    public class InvalidRoleException : ConnException, IValidationException
    {
        protected InvalidRoleException(
            SerializationInfo serializationInfo,
            StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }

        public InvalidRoleException(string parameterName, object parameterValue)
           : base(message: $"Invalid role, " +
                 $"parameter name: {parameterName}, " +
                 $"parameter value: {parameterValue}.")
        { }

        public InvalidRoleException()
            : base(message: "Invalid role. Please fix the errors and try again.") { }
    }
}
