using Authentication.Services.Foundations;
using System.Runtime.Serialization;

namespace Authentication.Models.Exceptions.Roles
{
    [Serializable]
    public class AlreadyExistsRoleException : ConnException, IUnprocessableException
    {
        protected AlreadyExistsRoleException(
            SerializationInfo serializationInfo,
            StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }

        public AlreadyExistsRoleException(string name)
            : base($"Role with name: '{name}' already exists.")
        { }

        public AlreadyExistsRoleException(Exception innerException)
            : base(message: "Role with the same details already exists.", innerException) { }
    }
}
