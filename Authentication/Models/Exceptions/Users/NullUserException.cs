using Authentication.Services.Foundations;
using System.Runtime.Serialization;

namespace Authentication.Models.Exceptions.Users
{
    [Serializable]
    public class NullUserException : ConnException, IValidationException
    {
        protected NullUserException(
            SerializationInfo serializationInfo,
            StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }

        public NullUserException()
            : base(message: "The user is empty.") { }

        public NullUserException(string message)
            : base(message) { }
    }
}
