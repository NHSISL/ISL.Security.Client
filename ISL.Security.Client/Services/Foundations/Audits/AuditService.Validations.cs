// ---------------------------------------------------------
// Copyright (c) North East London ICB. All rights reserved.
// ---------------------------------------------------------

using System;
using ISL.Security.Client.Models.Clients;
using ISL.Security.Client.Models.Foundations.Audits.Exceptions;

namespace ISL.Security.Client.Services.Foundations.Audits
{
    internal partial class AuditService
    {
        private static void ValidateInputs<T>(T entity, string userId)
        {
            Validate(
                (Rule: IsInvalid(entity), Parameter: nameof(entity)),
                (Rule: IsInvalid(userId), Parameter: nameof(userId)));
        }

        private static void ValidateInputs<T>(T entity, T storageEntity)
        {
            Validate(
                (Rule: IsInvalid(entity), Parameter: nameof(entity)),
                (Rule: IsInvalid(storageEntity), Parameter: nameof(storageEntity)));
        }

        private static void ValidateProperties<T>(T entity, SecurityConfigurations securityConfigurations)
        {
            var createdByName = securityConfigurations.CreatedByPropertyName;
            var createdDateName = securityConfigurations.CreatedDatePropertyName;
            var updatedByName = securityConfigurations.UpdatedByPropertyName;
            var updatedDateName = securityConfigurations.UpdatedDatePropertyName;

            Validate(
                (Rule: IsInvalidProperty(createdByName, entity, typeof(string)),
                    Parameter: nameof(createdByName)),

                (Rule: IsInvalidProperty(createdDateName, entity, typeof(DateTimeOffset)),
                    Parameter: nameof(createdDateName)),

                (Rule: IsInvalidProperty(updatedByName, entity, typeof(string)),
                    Parameter: nameof(updatedByName)),

                (Rule: IsInvalidProperty(updatedDateName, entity, typeof(DateTimeOffset)),
                    Parameter: nameof(updatedDateName)));
        }

        private static dynamic IsInvalidProperty<T>(string propertyName, T entity, Type expectedType) => new
        {
            Condition = IsInvalidPropertyOperation(propertyName, entity, expectedType),

            Message =
                $"Property '{propertyName}' not found, not settable, or not assignable from " +
                    $"'{expectedType.Name}' on type '{typeof(T).Name}'."
        };

        private static bool IsInvalidPropertyOperation<T>(string propertyName, T entity, Type expectedType)
        {
            var property = typeof(T).GetProperty(propertyName);

            if (property == null || !property.CanWrite)
            {
                return true;
            }

            var propertyType = property.PropertyType;
            var underlyingType = Nullable.GetUnderlyingType(propertyType) ?? propertyType;

            if (!underlyingType.IsAssignableFrom(expectedType))
            {
                return true;
            }

            return false;
        }

        private static dynamic IsInvalid(string text) => new
        {
            Condition = String.IsNullOrWhiteSpace(text),
            Message = "Text is required"
        };

        private static dynamic IsInvalid<T>(T entity) => new
        {
            Condition = entity == null,
            Message = "Entity is required"
        };

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidArgumentAuditException =
                new InvalidArgumentAuditException(
                    message: "Invalid audit input arguments. Please correct the errors and try again.");

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidArgumentAuditException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidArgumentAuditException.ThrowIfContainsErrors();
        }
    }
}
