// ---------------------------------------------------------
// Copyright (c) North East London ICB. All rights reserved.
// ---------------------------------------------------------

using System;
using System.Threading.Tasks;
using ISL.Security.Client.Models.Foundations.Audits.Exceptions;
using Xeptions;

namespace ISL.Security.Client.Services.Foundations.Audits
{
    internal partial class AuditService
    {
        private delegate ValueTask<T> ReturningObjectFunction<T>();

        private async ValueTask<T> TryCatch<T>(ReturningObjectFunction<T> returningObjectFunction)
        {
            try
            {
                return await returningObjectFunction();
            }
            catch (Exception exception)
            {
                var failedAuditServiceException =
                    new FailedAuditServiceException(
                        message: "Failed audit service error occurred, please contact support.",
                        innerException: exception);

                throw await CreateAndLogServiceExceptionAsync(failedAuditServiceException);
            }
        }

        private async ValueTask<AuditServiceException> CreateAndLogServiceExceptionAsync(Xeption exception)
        {
            var auditServiceException =
                new AuditServiceException(
                    message: "Audit service error occurred, please contact support.",
                    innerException: exception);

            return auditServiceException;
        }
    }
}
