using Authentication.Services.Foundations;
using System.Runtime.Serialization;

namespace Authentication.Models.Exceptions.Roles
{
    [Serializable]
    public class NotFoundRoleException : ConnException, INotFoundException
    {
        protected NotFoundRoleException(
            SerializationInfo serializationInfo,
            StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }

        public NotFoundRoleException(Guid roleId)
            : base(message: $"Role with ID: {roleId} does not exist")
        { }

        public NotFoundRoleException(string roleName)
            : base(message: $"Role with name: {roleName} does not exist")
        { }
    }
}
