// ---------------------------------------------------------
// Copyright (c) North East London ICB. All rights reserved.
// ---------------------------------------------------------

using System;
using System.Collections.Generic;
using ISL.Security.Client.Models.Clients;
using ISL.Security.Client.Models.Foundations.Audits.Exceptions;

namespace ISL.Security.Client.Services.Foundations.Audits
{
    internal partial class AuditService
    {
        virtual internal void ValidateOnApplyAddAuditValues<T>(
            T entity,
            string userId,
            SecurityConfigurations securityConfigurations)
        {
            Validate(
                (Rule: IsInvalid(entity), Parameter: nameof(entity)),
                (Rule: IsInvalid(userId), Parameter: nameof(userId)),
                (Rule: IsInvalid(securityConfigurations), Parameter: nameof(SecurityConfigurations)));

            Validate(
                (Rule: IsInvalid(securityConfigurations.CreatedByPropertyName),
                    Parameter: nameof(SecurityConfigurations.CreatedByPropertyName)),

                (Rule: IsInvalidDataType(securityConfigurations.CreatedByPropertyType),
                    Parameter: nameof(SecurityConfigurations.CreatedByPropertyType)),

                (Rule: IsInvalid(securityConfigurations.CreatedWhenPropertyName),
                    Parameter: nameof(SecurityConfigurations.CreatedWhenPropertyName)),

                (Rule: IsInvalidDateType(securityConfigurations.CreatedWhenPropertyType),
                    Parameter: nameof(SecurityConfigurations.CreatedWhenPropertyType)),

                (Rule: IsInvalid(securityConfigurations.UpdatedByPropertyName),
                    Parameter: nameof(SecurityConfigurations.UpdatedByPropertyName)),

                (Rule: IsInvalidDataType(securityConfigurations.UpdatedByPropertyType),
                    Parameter: nameof(SecurityConfigurations.UpdatedByPropertyType)),

                (Rule: IsInvalid(securityConfigurations.UpdatedWhenPropertyName),
                    Parameter: nameof(SecurityConfigurations.UpdatedWhenPropertyName)),

                (Rule: IsInvalidDateType(securityConfigurations.UpdatedWhenPropertyType),
                    Parameter: nameof(SecurityConfigurations.UpdatedWhenPropertyType)));

            Validate(
                (Rule: IsInvalidProperty(
                    securityConfigurations.CreatedByPropertyName,
                    entity,
                    securityConfigurations.CreatedByPropertyType),
                Parameter: nameof(SecurityConfigurations.CreatedByPropertyName)),

                (Rule: IsInvalidProperty(
                    securityConfigurations.CreatedWhenPropertyName,
                    entity,
                    securityConfigurations.CreatedWhenPropertyType),
                Parameter: nameof(SecurityConfigurations.CreatedWhenPropertyName)),

                (Rule: IsInvalidProperty(
                    securityConfigurations.UpdatedByPropertyName,
                    entity,
                    securityConfigurations.UpdatedByPropertyType),
                Parameter: nameof(SecurityConfigurations.UpdatedByPropertyName)),

                (Rule: IsInvalidProperty(
                    securityConfigurations.UpdatedWhenPropertyName,
                    entity,
                    securityConfigurations.UpdatedWhenPropertyType),
                Parameter: nameof(SecurityConfigurations.UpdatedWhenPropertyName)));
        }

        virtual internal void ValidateOnApplyModifyAuditValues<T>(
            T entity,
            string userId,
            SecurityConfigurations securityConfigurations)
        {
            Validate(
                (Rule: IsInvalid(entity), Parameter: nameof(entity)),
                (Rule: IsInvalid(userId), Parameter: nameof(userId)),
                (Rule: IsInvalid(securityConfigurations), Parameter: nameof(SecurityConfigurations)));

            Validate(
                (Rule: IsInvalid(securityConfigurations.CreatedByPropertyName),
                    Parameter: nameof(SecurityConfigurations.CreatedByPropertyName)),

                (Rule: IsInvalidDataType(securityConfigurations.CreatedByPropertyType),
                    Parameter: nameof(SecurityConfigurations.CreatedByPropertyType)),

                (Rule: IsInvalid(securityConfigurations.CreatedWhenPropertyName),
                    Parameter: nameof(SecurityConfigurations.CreatedWhenPropertyName)),

                (Rule: IsInvalidDateType(securityConfigurations.CreatedWhenPropertyType),
                    Parameter: nameof(SecurityConfigurations.CreatedWhenPropertyType)),

                (Rule: IsInvalid(securityConfigurations.UpdatedByPropertyName),
                    Parameter: nameof(SecurityConfigurations.UpdatedByPropertyName)),

                (Rule: IsInvalidDataType(securityConfigurations.UpdatedByPropertyType),
                    Parameter: nameof(SecurityConfigurations.UpdatedByPropertyType)),

                (Rule: IsInvalid(securityConfigurations.UpdatedWhenPropertyName),
                    Parameter: nameof(SecurityConfigurations.UpdatedWhenPropertyName)),

                (Rule: IsInvalidDateType(securityConfigurations.UpdatedWhenPropertyType),
                    Parameter: nameof(SecurityConfigurations.UpdatedWhenPropertyType)));

            Validate(
                (Rule: IsInvalidProperty(
                    securityConfigurations.CreatedByPropertyName,
                    entity,
                    securityConfigurations.CreatedByPropertyType),
                Parameter: nameof(SecurityConfigurations.CreatedByPropertyName)),

                (Rule: IsInvalidProperty(
                    securityConfigurations.CreatedWhenPropertyName,
                    entity,
                    securityConfigurations.CreatedWhenPropertyType),
                Parameter: nameof(SecurityConfigurations.CreatedWhenPropertyName)),

                (Rule: IsInvalidProperty(
                    securityConfigurations.UpdatedByPropertyName,
                    entity,
                    securityConfigurations.UpdatedByPropertyType),
                Parameter: nameof(SecurityConfigurations.UpdatedByPropertyName)),

                (Rule: IsInvalidProperty(
                    securityConfigurations.UpdatedWhenPropertyName,
                    entity,
                    securityConfigurations.UpdatedWhenPropertyType),
                Parameter: nameof(SecurityConfigurations.UpdatedWhenPropertyName)));
        }

        virtual internal void ValidateOnApplyRemoveAuditValues<T>(
            T entity,
            string userId,
            SecurityConfigurations securityConfigurations)
        {
            Validate(
                (Rule: IsInvalid(entity), Parameter: nameof(entity)),
                (Rule: IsInvalid(userId), Parameter: nameof(userId)),
                (Rule: IsInvalid(securityConfigurations), Parameter: nameof(SecurityConfigurations)));

            Validate(
                (Rule: IsInvalid(securityConfigurations.CreatedByPropertyName),
                    Parameter: nameof(SecurityConfigurations.CreatedByPropertyName)),

                (Rule: IsInvalidDataType(securityConfigurations.CreatedByPropertyType),
                    Parameter: nameof(SecurityConfigurations.CreatedByPropertyType)),

                (Rule: IsInvalid(securityConfigurations.CreatedWhenPropertyName),
                    Parameter: nameof(SecurityConfigurations.CreatedWhenPropertyName)),

                (Rule: IsInvalidDateType(securityConfigurations.CreatedWhenPropertyType),
                    Parameter: nameof(SecurityConfigurations.CreatedWhenPropertyType)),

                (Rule: IsInvalid(securityConfigurations.UpdatedByPropertyName),
                    Parameter: nameof(SecurityConfigurations.UpdatedByPropertyName)),

                (Rule: IsInvalidDataType(securityConfigurations.UpdatedByPropertyType),
                    Parameter: nameof(SecurityConfigurations.UpdatedByPropertyType)),

                (Rule: IsInvalid(securityConfigurations.UpdatedWhenPropertyName),
                    Parameter: nameof(SecurityConfigurations.UpdatedWhenPropertyName)),

                (Rule: IsInvalidDateType(securityConfigurations.UpdatedWhenPropertyType),
                    Parameter: nameof(SecurityConfigurations.UpdatedWhenPropertyType)),

                (Rule: IsInvalid(securityConfigurations.DeletedByPropertyName),
                    Parameter: nameof(SecurityConfigurations.DeletedByPropertyName)),

                (Rule: IsInvalidDataType(securityConfigurations.DeletedByPropertyType),
                    Parameter: nameof(SecurityConfigurations.DeletedByPropertyType)),

                (Rule: IsInvalid(securityConfigurations.DeletedWhenPropertyName),
                    Parameter: nameof(SecurityConfigurations.DeletedWhenPropertyName)),

                (Rule: IsInvalidDateType(securityConfigurations.DeletedWhenPropertyType),
                    Parameter: nameof(SecurityConfigurations.DeletedWhenPropertyType)),

                (Rule: IsInvalid(securityConfigurations.IsDeletedPropertyName),
                    Parameter: nameof(SecurityConfigurations.IsDeletedPropertyName)),

                (Rule: IsInvalidBoolType(securityConfigurations.IsDeletedPropertyType),
                    Parameter: nameof(SecurityConfigurations.IsDeletedPropertyType)),

                (Rule: IsInvalid(securityConfigurations.DeletionReasonPropertyName),
                    Parameter: nameof(SecurityConfigurations.DeletionReasonPropertyName)),

                (Rule: IsInvalidDataType(securityConfigurations.DeletionReasonPropertyType),
                    Parameter: nameof(SecurityConfigurations.DeletionReasonPropertyType)));

            Validate(
                (Rule: IsInvalidProperty(
                    securityConfigurations.CreatedByPropertyName,
                    entity,
                    securityConfigurations.CreatedByPropertyType),
                Parameter: nameof(SecurityConfigurations.CreatedByPropertyName)),

                (Rule: IsInvalidProperty(
                    securityConfigurations.CreatedWhenPropertyName,
                    entity,
                    securityConfigurations.CreatedWhenPropertyType),
                Parameter: nameof(SecurityConfigurations.CreatedWhenPropertyName)),

                (Rule: IsInvalidProperty(
                    securityConfigurations.UpdatedByPropertyName,
                    entity,
                    securityConfigurations.UpdatedByPropertyType),
                Parameter: nameof(SecurityConfigurations.UpdatedByPropertyName)),

                (Rule: IsInvalidProperty(
                    securityConfigurations.UpdatedWhenPropertyName,
                    entity,
                    securityConfigurations.UpdatedWhenPropertyType),
                Parameter: nameof(SecurityConfigurations.UpdatedWhenPropertyName)),

                (Rule: IsInvalidProperty(
                    securityConfigurations.DeletedByPropertyName,
                    entity,
                    securityConfigurations.DeletedByPropertyType),
                Parameter: nameof(SecurityConfigurations.DeletedByPropertyName)),

                (Rule: IsInvalidProperty(
                    securityConfigurations.DeletedWhenPropertyName,
                    entity,
                    securityConfigurations.DeletedWhenPropertyType),
                Parameter: nameof(SecurityConfigurations.DeletedWhenPropertyName)),

                (Rule: IsInvalidProperty(
                    securityConfigurations.IsDeletedPropertyName,
                    entity,
                    securityConfigurations.IsDeletedPropertyType),
                Parameter: nameof(SecurityConfigurations.IsDeletedPropertyName)),

                (Rule: IsInvalidProperty(
                    securityConfigurations.DeletionReasonPropertyName,
                    entity,
                    securityConfigurations.DeletionReasonPropertyType),
                Parameter: nameof(SecurityConfigurations.DeletionReasonPropertyName)));
        }

        virtual internal void ValidateInputs<T>(
            T entity,
            T storageEntity,
            SecurityConfigurations securityConfigurations)
        {
            Validate(
                (Rule: IsInvalid(entity), Parameter: nameof(entity)),
                (Rule: IsInvalid(storageEntity), Parameter: nameof(storageEntity)),
                (Rule: IsInvalid(securityConfigurations), Parameter: nameof(SecurityConfigurations)));

            Validate(
                (Rule: IsInvalid(securityConfigurations.CreatedByPropertyName),
                    Parameter: nameof(SecurityConfigurations.CreatedByPropertyName)),

                (Rule: IsInvalidDataType(securityConfigurations.CreatedByPropertyType),
                    Parameter: nameof(SecurityConfigurations.CreatedByPropertyType)),

                (Rule: IsInvalid(securityConfigurations.CreatedWhenPropertyName),
                    Parameter: nameof(SecurityConfigurations.CreatedWhenPropertyName)),

                (Rule: IsInvalidDateType(securityConfigurations.CreatedWhenPropertyType),
                    Parameter: nameof(SecurityConfigurations.CreatedWhenPropertyType)),

                (Rule: IsInvalid(securityConfigurations.UpdatedByPropertyName),
                    Parameter: nameof(SecurityConfigurations.UpdatedByPropertyName)),

                (Rule: IsInvalidDataType(securityConfigurations.UpdatedByPropertyType),
                    Parameter: nameof(SecurityConfigurations.UpdatedByPropertyType)),

                (Rule: IsInvalid(securityConfigurations.UpdatedWhenPropertyName),
                    Parameter: nameof(SecurityConfigurations.UpdatedWhenPropertyName)),

                (Rule: IsInvalidDateType(securityConfigurations.UpdatedWhenPropertyType),
                    Parameter: nameof(SecurityConfigurations.UpdatedWhenPropertyType)),

                (Rule: IsInvalid(securityConfigurations.DeletedByPropertyName),
                    Parameter: nameof(SecurityConfigurations.DeletedByPropertyName)),

                (Rule: IsInvalidDataType(securityConfigurations.DeletedByPropertyType),
                    Parameter: nameof(SecurityConfigurations.DeletedByPropertyType)),

                (Rule: IsInvalid(securityConfigurations.DeletedWhenPropertyName),
                    Parameter: nameof(SecurityConfigurations.DeletedWhenPropertyName)),

                (Rule: IsInvalidDateType(securityConfigurations.DeletedWhenPropertyType),
                    Parameter: nameof(SecurityConfigurations.DeletedWhenPropertyType)),

                (Rule: IsInvalid(securityConfigurations.IsDeletedPropertyName),
                    Parameter: nameof(SecurityConfigurations.IsDeletedPropertyName)),

                (Rule: IsInvalidBoolType(securityConfigurations.IsDeletedPropertyType),
                    Parameter: nameof(SecurityConfigurations.IsDeletedPropertyType)),

                (Rule: IsInvalid(securityConfigurations.DeletionReasonPropertyName),
                    Parameter: nameof(SecurityConfigurations.DeletionReasonPropertyName)),

                (Rule: IsInvalidDataType(securityConfigurations.DeletionReasonPropertyType),
                    Parameter: nameof(SecurityConfigurations.DeletionReasonPropertyType)));

            Validate(
                (Rule: IsInvalidProperty(
                    securityConfigurations.CreatedByPropertyName,
                    entity,
                    securityConfigurations.CreatedByPropertyType),
                Parameter: nameof(SecurityConfigurations.CreatedByPropertyName)),

                (Rule: IsInvalidProperty(
                    securityConfigurations.CreatedWhenPropertyName,
                    entity,
                    securityConfigurations.CreatedWhenPropertyType),
                Parameter: nameof(SecurityConfigurations.CreatedWhenPropertyName)),

                (Rule: IsInvalidProperty(
                    securityConfigurations.UpdatedByPropertyName,
                    entity,
                    securityConfigurations.UpdatedByPropertyType),
                Parameter: nameof(SecurityConfigurations.UpdatedByPropertyName)),

                (Rule: IsInvalidProperty(
                    securityConfigurations.UpdatedWhenPropertyName,
                    entity,
                    securityConfigurations.UpdatedWhenPropertyType),
                Parameter: nameof(SecurityConfigurations.UpdatedWhenPropertyName)),

                (Rule: IsInvalidProperty(
                    securityConfigurations.CreatedByPropertyName,
                    storageEntity,
                    securityConfigurations.CreatedByPropertyType),
                Parameter: nameof(SecurityConfigurations.CreatedByPropertyName)),

                (Rule: IsInvalidProperty(
                    securityConfigurations.CreatedWhenPropertyName,
                    storageEntity,
                    securityConfigurations.CreatedWhenPropertyType),
                Parameter: nameof(SecurityConfigurations.CreatedWhenPropertyName)),

                (Rule: IsInvalidProperty(
                    securityConfigurations.UpdatedByPropertyName,
                    storageEntity,
                    securityConfigurations.UpdatedByPropertyType),
                Parameter: nameof(SecurityConfigurations.UpdatedByPropertyName)),

                (Rule: IsInvalidProperty(
                    securityConfigurations.UpdatedWhenPropertyName,
                    storageEntity,
                    securityConfigurations.UpdatedWhenPropertyType),
                Parameter: nameof(SecurityConfigurations.UpdatedWhenPropertyName)),

                (Rule: IsInvalidProperty(
                    securityConfigurations.DeletedByPropertyName,
                    entity,
                    securityConfigurations.DeletedByPropertyType),
                Parameter: nameof(SecurityConfigurations.DeletedByPropertyName)),

                (Rule: IsInvalidProperty(
                    securityConfigurations.DeletedWhenPropertyName,
                    entity,
                    securityConfigurations.DeletedWhenPropertyType),
                Parameter: nameof(SecurityConfigurations.DeletedWhenPropertyName)),

                (Rule: IsInvalidProperty(
                    securityConfigurations.IsDeletedPropertyName,
                    entity,
                    securityConfigurations.IsDeletedPropertyType),
                Parameter: nameof(SecurityConfigurations.IsDeletedPropertyName)),

                (Rule: IsInvalidProperty(
                    securityConfigurations.DeletionReasonPropertyName,
                    entity,
                    securityConfigurations.DeletionReasonPropertyType),
                Parameter: nameof(SecurityConfigurations.DeletionReasonPropertyName)),

                (Rule: IsInvalidProperty(
                    securityConfigurations.DeletedByPropertyName,
                    storageEntity,
                    securityConfigurations.DeletedByPropertyType),
                Parameter: nameof(SecurityConfigurations.DeletedByPropertyName)),

                (Rule: IsInvalidProperty(
                    securityConfigurations.DeletedWhenPropertyName,
                    storageEntity,
                    securityConfigurations.DeletedWhenPropertyType),
                Parameter: nameof(SecurityConfigurations.DeletedWhenPropertyName)),

                (Rule: IsInvalidProperty(
                    securityConfigurations.IsDeletedPropertyName,
                    storageEntity,
                    securityConfigurations.IsDeletedPropertyType),
                Parameter: nameof(SecurityConfigurations.IsDeletedPropertyName)),

                (Rule: IsInvalidProperty(
                    securityConfigurations.DeletionReasonPropertyName,
                    storageEntity,
                    securityConfigurations.DeletionReasonPropertyType),
                Parameter: nameof(SecurityConfigurations.DeletionReasonPropertyName)));
        }

        private static dynamic IsInvalidProperty<T>(string propertyName, T entity, Type expectedType) => new
        {
            Condition = IsInvalidPropertyOperation(propertyName, entity, expectedType),

            Message =
                $"Property '{propertyName}' not found, not settable, or not assignable from " +
                    $"'{expectedType.Name}' on entity '{typeof(T).Name}'."
        };

        private static bool IsInvalidPropertyOperation<T>(string propertyName, T entity, Type expectedType)
        {
            if (entity == null || string.IsNullOrWhiteSpace(propertyName))
            {
                return true;
            }

            if (entity is IDictionary<string, object> expando)
            {
                if (!expando.TryGetValue(propertyName, out var value))
                {
                    return true;
                }

                var actualType = value?.GetType() ?? expectedType;
                var actualUnderlyingType = Nullable.GetUnderlyingType(actualType) ?? actualType;
                var expectedUnderlyingType = Nullable.GetUnderlyingType(expectedType) ?? expectedType;
                var result = !expectedUnderlyingType.IsAssignableFrom(actualUnderlyingType);

                return result;
            }
            else
            {
                var property = entity.GetType().GetProperty(propertyName);

                if (property == null || !property.CanWrite)
                {
                    return true;
                }

                var propertyType = property.PropertyType;
                var underlyingType = Nullable.GetUnderlyingType(propertyType) ?? propertyType;
                bool result = !underlyingType.IsAssignableFrom(expectedType);

                return result;
            }
        }

        private static dynamic IsInvalid(string text) => new
        {
            Condition = String.IsNullOrWhiteSpace(text),
            Message = "Text is required"
        };

        private static dynamic IsInvalidDataType(Type type) => new
        {
            Condition =
                type == typeof(DateTime) ||
                type == typeof(DateTimeOffset) ||
                type == typeof(byte[]) ||
                type == typeof(bool),
            Message = "A type of String / Guid / Long is required"
        };

        private static dynamic IsInvalidBoolType(Type type) => new
        {
            Condition = type != typeof(bool),
            Message = "A type of Boolean is required"
        };

        private static dynamic IsInvalidDateType(Type type) => new
        {
            Condition = !(type == typeof(DateTime) || type == typeof(DateTimeOffset)),
            Message = "A type of DateTime / DateTimeOffset is required"
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
                    message: "Invalid audit argument(s), correct the errors and try again.");

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
