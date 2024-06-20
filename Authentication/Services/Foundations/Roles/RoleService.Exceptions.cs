using Authentication.Models.Entities.Roles;
using Authentication.Models.Exceptions.Roles;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System;
using System.Runtime.Serialization;

namespace Authentication.Services.Foundations.Roles
{
    sealed partial class RoleService 
    {
        private delegate ValueTask<Role> ReturningRoleFunction();
        private delegate ValueTask<List<Role>> ReturningRoleListFunction();

        private async ValueTask<Role> TryCatch(ReturningRoleFunction returningRoleFunction)
        {
            try {
                return await returningRoleFunction();
            }
            catch (PostgresException postgresException)
                when (postgresException.ErrorCode == 2627) 
            {
                var alreadyExistsRoleException =
                    new AlreadyExistsRoleException(postgresException);

                throw CreateAndLogServiceException(alreadyExistsRoleException);
            }
            catch (DbUpdateConcurrencyException dbUpdateConcurrencyException)
            {
                throw new LockedRoleException(dbUpdateConcurrencyException);
            }
            catch (ServerSqlException npgsqlException)
            {
                throw CreateAndLogServiceException(npgsqlException);
            }
            catch (DbUpdateException dbUpdateException) 
            {
                throw CreateAndLogServiceException(dbUpdateException);
            }
            catch (Exception exception)
                when (exception is not ConnException)
            {
                throw CreateAndLogServiceException(exception);
            }
        }

        private async ValueTask<List<Role>> TryCatch(ReturningRoleListFunction returningRoleListFunction)
        {
            try
            {
                return await returningRoleListFunction();
            }
            catch (ServerSqlException npgsqlException)
            {
                throw CreateAndLogServiceException(npgsqlException);
            }
            catch (DbUpdateException dbUpdateException)
            {
                throw CreateAndLogServiceException(dbUpdateException);
            }
            catch (Exception exception)
                when (exception is not ConnException)
            {
                throw CreateAndLogServiceException(exception);
            }
        }

        private RoleServiceException CreateAndLogServiceException(Exception exception)
        {
            //this.loggingBroker.LogError(exception);

            return new RoleServiceException(exception);
        }
    }
}
