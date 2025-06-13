// ---------------------------------------------------------
// Copyright (c) North East London ICB. All rights reserved.
// ---------------------------------------------------------

using System.Security.Claims;
using ISL.Security.Client.Models.Orchestrations.Audits.Exceptions;

namespace ISL.Security.Client.Services.Foundations.Audits
{
    internal partial class AuditOrchestrationService
    {
        private static void ValidateInputs<T>(T entity, ClaimsPrincipal user)
        {
            Validate(
                (Rule: IsInvalid(entity), Parameter: nameof(entity)),
                (Rule: IsInvalid(user), Parameter: nameof(user)));
        }

        private static void ValidateInputs<T>(T entity, T storageEntity)
        {
            Validate(
                (Rule: IsInvalid(entity), Parameter: nameof(entity)),
                (Rule: IsInvalid(storageEntity), Parameter: nameof(storageEntity)));
        }

        private static dynamic IsInvalid(ClaimsPrincipal user) => new
        {
            Condition = user == null,
            Message = "Claims principal is required"
        };

        private static dynamic IsInvalid<T>(T entity) => new
        {
            Condition = entity == null,
            Message = "Entity is required"
        };

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidArgumentAuditOrchestrationException =
                new InvalidArgumentAuditOrchestrationException(
                    message: "Invalid audit orchestration input arguments. Please correct the errors and try again.");

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidArgumentAuditOrchestrationException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidArgumentAuditOrchestrationException.ThrowIfContainsErrors();
        }
    }
}
