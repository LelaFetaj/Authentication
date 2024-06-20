using Npgsql;
using System.Data.Common;
using System.Net.Sockets;
using System.Runtime.Serialization;

namespace Authentication.Services.Foundations
{
    [Serializable]
    public class ServerSqlException : DbException
    {
        // Summary:
        //     Specifies whether the exception is considered transient, that is, whether retrying
        //     the operation could succeed (e.g. a network error or a timeout).
        public override bool IsTransient
        {
            get
            {
                Exception innerException = base.InnerException;
                if (innerException is IOException || innerException is SocketException || innerException is TimeoutException || (innerException is Npgsql.NpgsqlException ex && ex.IsTransient)) {
                    return true;
                }

                return false;
            }
        }

        public new NpgsqlBatchCommand? BatchCommand { get; set; }

        protected override DbBatchCommand? DbBatchCommand => BatchCommand;



        public ServerSqlException() { }

        public ServerSqlException(string? message) : base(message) { }

        public ServerSqlException(string? message, Exception? innerException) : base(message, innerException) { }

        protected ServerSqlException(SerializationInfo info, StreamingContext context) : base(info, context) { }

    }
}
