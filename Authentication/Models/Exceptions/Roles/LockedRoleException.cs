using Authentication.Services.Foundations;
using System.Runtime.Serialization;

namespace Authentication.Models.Exceptions.Roles
{
    [Serializable]
    public class LockedRoleException : ConnException, ILockedException
    {
        protected LockedRoleException(
            SerializationInfo serializationInfo,
            StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }

        public LockedRoleException(Exception innerException)
            : base(message: "Locked role record exception, please try again later.", innerException)
        { }
    }
}
