// ---------------------------------------------------------
// Copyright (c) North East London ICB. All rights reserved.
// ---------------------------------------------------------

using System;
using System.Threading.Tasks;
using ISL.Security.Client.Models.Foundations.Audits.Exceptions;
using ISL.Security.Client.Models.Foundations.Users.Exceptions;
using ISL.Security.Client.Models.Orchestrations.Audits.Exceptions;
using Xeptions;

namespace ISL.Security.Client.Services.Foundations.Audits
{
    internal partial class AuditOrchestrationService
    {
        private delegate ValueTask<T> ReturningObjectFunction<T>();

        private async ValueTask<T> TryCatch<T>(ReturningObjectFunction<T> returningObjectFunction)
        {
            try
            {
                return await returningObjectFunction();
            }
            catch (InvalidArgumentAuditOrchestrationException invalidArgumentAuditOrchestrationException)
            {
                throw await CreateAndLogValidationExceptionAsync(invalidArgumentAuditOrchestrationException);
            }
            catch (UserValidationException userValidationException)
            {
                throw await CreateAndLogDependencyValidationExceptionAsync(userValidationException);
            }
            catch (UserDependencyValidationException userDependencyValidationException)
            {
                throw await CreateAndLogDependencyValidationExceptionAsync(userDependencyValidationException);
            }
            catch (AuditValidationException auditValidationException)
            {
                throw await CreateAndLogDependencyValidationExceptionAsync(auditValidationException);
            }
            catch (AuditDependencyValidationException auditDependencyValidationException)
            {
                throw await CreateAndLogDependencyValidationExceptionAsync(auditDependencyValidationException);
            }
            catch (UserServiceException userServiceException)
            {
                throw await CreateAndLogDependencyExceptionAsync(userServiceException);
            }
            catch (AuditServiceException auditServiceException)
            {
                throw await CreateAndLogDependencyExceptionAsync(auditServiceException);
            }
            catch (Exception exception)
            {
                var failedAuditOrchestrationServiceException =
                    new FailedAuditOrchestrationServiceException(
                        message: "Failed audit orchestration service error occurred, please contact support.",
                        innerException: exception);

                throw await CreateAndLogServiceExceptionAsync(failedAuditOrchestrationServiceException);
            }
        }

        private async ValueTask<AuditOrchestrationValidationException>
            CreateAndLogValidationExceptionAsync(Xeption exception)
        {
            var auditOrchestrationValidationException =
                new AuditOrchestrationValidationException(
                    message: "Audit orchestration validation error occurred, please try again.",
                    innerException: exception);

            return auditOrchestrationValidationException;
        }

        private async ValueTask<AuditOrchestrationDependencyValidationException>
            CreateAndLogDependencyValidationExceptionAsync(Xeption exception)
        {
            var addressOrchestrationDependencyValidationException =
                new AuditOrchestrationDependencyValidationException(
                    message: "Audit orchestration dependency validation error occurred, " +
                    "fix the errors and try again.",
                    innerException: exception.InnerException as Xeption);

            return addressOrchestrationDependencyValidationException;
        }

        private async ValueTask<AuditOrchestrationDependencyException>
            CreateAndLogDependencyExceptionAsync(Xeption exception)
        {
            var addressOrchestrationDependencyException =
                new AuditOrchestrationDependencyException(
                    message: "Audit orchestration dependency error occurred, " +
                    "fix the errors and try again.",
                    innerException: exception.InnerException as Xeption);

            return addressOrchestrationDependencyException;
        }

        private async ValueTask<AuditOrchestrationServiceException> CreateAndLogServiceExceptionAsync(Xeption exception)
        {
            var auditServiceException =
                new AuditOrchestrationServiceException(
                    message: "Audit orchestration service error occurred, please contact support.",
                    innerException: exception);

            return auditServiceException;
        }
    }
}
