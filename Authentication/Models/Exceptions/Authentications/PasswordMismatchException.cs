using Authentication.Services.Foundations;
using System.Runtime.Serialization;

namespace Authentication.Models.Exceptions.Authentications
{
    [Serializable]
    public class PasswordMismatchException : ConnException, IUnauthorizedException
    {
        protected PasswordMismatchException(
            SerializationInfo serializationInfo,
            StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }

        public PasswordMismatchException()
            : base("Passowrds do not match.")
        {
        }

        public PasswordMismatchException(string message)
            : base(message)
        { }
    }
}
