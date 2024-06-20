using Authentication.Services.Foundations;
using System.Runtime.Serialization;

namespace Authentication.Models.Exceptions.Users
{
    [Serializable]
    public class LockedUserException : ConnException, ILockedException
    {
        protected LockedUserException(
            SerializationInfo serializationInfo,
            StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }

        public LockedUserException(Exception innerException)
            : base(message: "Locked user record exception, please try again later.", innerException)
        { }
    }
}
