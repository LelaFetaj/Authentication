using Authentication.Services.Foundations;
using System.Runtime.Serialization;

namespace Authentication.Models.Exceptions.Roles
{
    [Serializable]
    public class NullRoleException : ConnException, IValidationException
    {
        protected NullRoleException(
            SerializationInfo serializationInfo,
            StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }

        public NullRoleException()
            : base(message: "The role is empty.") { }

        public NullRoleException(string message)
            : base(message) { }
    }
}
