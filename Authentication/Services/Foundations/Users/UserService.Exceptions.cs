using Authentication.Models.Entities.Users;
using Authentication.Models.Exceptions.Users;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System;

namespace Authentication.Services.Foundations.Users
{
    sealed partial class UserService
    {
        private delegate ValueTask<User> ReturningUserFunction();
        private delegate ValueTask<bool> ReturningBoolFunction();
        private delegate ValueTask<string> ReturningStringFunction();
        private delegate ValueTask<List<User>> ReturningUserListFunction();

        private async ValueTask<User> TryCatch(ReturningUserFunction returningUserFunction)
        {
            try {
                return await returningUserFunction();
            }
            catch (PostgresException postgresException)
                when (postgresException.ErrorCode == 547) {
                var invalidUserRoleReferenceException =
                    new InvalidUserRoleReferenceException(postgresException);

                throw CreateAndLogServiceException(invalidUserRoleReferenceException);
            }
            catch (PostgresException postgresException)
                when (postgresException.ErrorCode == 2627) {
                var invalidUserRoleReferenceException =
                    new AlreadyExistsUserException(postgresException);

                throw CreateAndLogServiceException(invalidUserRoleReferenceException);
            }
            catch (DbUpdateException dbUpdateException)
                when (dbUpdateException.InnerException is PostgresException postgresException
                    && postgresException.SqlState == "23505") {
                var invalidUserRoleReferenceException =
                    new InvalidUserRoleReferenceException(postgresException);

                throw CreateAndLogServiceException(invalidUserRoleReferenceException);
            }
            catch (DbUpdateConcurrencyException dbUpdateConcurrencyException) {
                throw new LockedUserException(dbUpdateConcurrencyException);
            }
            catch (ServerSqlException npgsqlException) {
                throw CreateAndLogServiceException(npgsqlException);
            }
            catch (DbUpdateException dbUpdateException) {
                throw CreateAndLogServiceException(dbUpdateException);
            }
            catch (Exception exception)
                when (exception is not ConnException) {
                throw CreateAndLogServiceException(exception);
            }
        }

        private async ValueTask<bool> TryCatch(ReturningBoolFunction returningBoolFunction)
        {
            try {
                return await returningBoolFunction();
            }
            catch (PostgresException postgresException)
                when (postgresException.ErrorCode == 547) {
                var invalidUserRoleReferenceException =
                    new InvalidUserRoleReferenceException(postgresException);

                throw CreateAndLogServiceException(invalidUserRoleReferenceException);
            }
            catch (PostgresException postgresException)
                when (postgresException.ErrorCode == 2627) {
                var invalidUserRoleReferenceException =
                    new AlreadyExistsUserException(postgresException);

                throw CreateAndLogServiceException(invalidUserRoleReferenceException);
            }
            catch (DbUpdateException dbUpdateException)
                when (dbUpdateException.InnerException is PostgresException postgresException
                    && postgresException.SqlState == "23505") {
                var invalidUserRoleReferenceException =
                    new InvalidUserRoleReferenceException(postgresException);

                throw CreateAndLogServiceException(invalidUserRoleReferenceException);
            }
            catch (DbUpdateConcurrencyException dbUpdateConcurrencyException) {
                throw new LockedUserException(dbUpdateConcurrencyException);
            }
            catch (ServerSqlException npgsqlException) {
                throw CreateAndLogServiceException(npgsqlException);
            }
            catch (DbUpdateException dbUpdateException) {
                throw CreateAndLogServiceException(dbUpdateException);
            }
            catch (Exception exception)
                when (exception is not ConnException) {
                throw CreateAndLogServiceException(exception);
            }
        }

        private async ValueTask<List<User>> TryCatch(ReturningUserListFunction returningUserListFunction)
        {
            try {
                return await returningUserListFunction();
            }
            catch (ServerSqlException npgsqlException) {
                throw CreateAndLogServiceException(npgsqlException);
            }
            catch (DbUpdateException dbUpdateException) {
                throw CreateAndLogServiceException(dbUpdateException);
            }
            catch (Exception exception)
                when (exception is not ConnException) {
                throw CreateAndLogServiceException(exception);
            }
        }

        private async ValueTask<string> TryCatch(ReturningStringFunction returningStringFunction)
        {
            try {
                return await returningStringFunction();
            }
            catch (ServerSqlException npgsqlException) {
                throw CreateAndLogServiceException(npgsqlException);
            }
            catch (DbUpdateException dbUpdateException) {
                throw CreateAndLogServiceException(dbUpdateException);
            }
            catch (Exception exception)
                when (exception is not ConnException) {
                throw CreateAndLogServiceException(exception);
            }
        }

        private UserServiceException CreateAndLogServiceException(Exception exception)
        {
            //this.loggingBroker.LogError(exception);

            return new UserServiceException(exception);
        }
    }
}
