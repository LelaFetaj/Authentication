using Authentication.Services.Foundations;
using System.Runtime.Serialization;

namespace Authentication.Models.Exceptions.Users
{
    [Serializable]
    public class NotFoundUserException : ConnException, INotFoundException
    {
        protected NotFoundUserException(
            SerializationInfo serializationInfo,
            StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }

        public NotFoundUserException(Guid userId)
            : base(message: $"User with ID: {userId} does not exist")
        { }

        public NotFoundUserException(string username)
            : base(message: $"User with username/email {username} does not exist")
        { }
    }
}
