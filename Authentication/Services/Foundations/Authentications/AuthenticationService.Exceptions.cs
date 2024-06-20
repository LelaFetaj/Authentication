using Authentication.Models.Exceptions.Authentications;
using System;

namespace Authentication.Services.Foundations.Authentications
{
    sealed partial class AuthenticationService
    {
        private delegate ValueTask<bool> ReturningBooleanFunction();

        private async ValueTask<bool> TryCatch(ReturningBooleanFunction returningBooleanFunction)
        {
            try 
            {
                return await returningBooleanFunction();
            }
            catch (ServerSqlException npgsqlException)
            {
                throw CreateAndLogServiceException(npgsqlException);
            }
            catch (Exception exception)
                when (exception is not ConnException)
            {
                throw CreateAndLogServiceException(exception);
            }
        }

        private AuthenticationServiceException CreateAndLogServiceException(Exception exception)
        {
            //this.loggingBroker.LogError(exception);

            return new AuthenticationServiceException(exception);
        }
    }
}
