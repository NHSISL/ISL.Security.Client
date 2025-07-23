﻿// ---------------------------------------------------------
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
        private static void ValidateInputs<T>(T entity, string userId, SecurityConfigurations securityConfigurations)
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

                (Rule: IsInvalid(securityConfigurations.CreatedDatePropertyName),
                    Parameter: nameof(SecurityConfigurations.CreatedDatePropertyName)),

                (Rule: IsInvalidDateType(securityConfigurations.CreatedDatePropertyType),
                    Parameter: nameof(SecurityConfigurations.CreatedDatePropertyType)),

                (Rule: IsInvalid(securityConfigurations.UpdatedByPropertyName),
                    Parameter: nameof(SecurityConfigurations.UpdatedByPropertyName)),

                (Rule: IsInvalidDataType(securityConfigurations.UpdatedByPropertyType),
                    Parameter: nameof(SecurityConfigurations.UpdatedByPropertyType)),

                (Rule: IsInvalid(securityConfigurations.UpdatedDatePropertyName),
                    Parameter: nameof(SecurityConfigurations.UpdatedDatePropertyName)),

                (Rule: IsInvalidDateType(securityConfigurations.UpdatedDatePropertyType),
                    Parameter: nameof(SecurityConfigurations.UpdatedDatePropertyType)));

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

        private static void ValidateInputs<T>(T entity, T storageEntity, SecurityConfigurations securityConfigurations)
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

                (Rule: IsInvalid(securityConfigurations.CreatedDatePropertyName),
                    Parameter: nameof(SecurityConfigurations.CreatedDatePropertyName)),

                (Rule: IsInvalidDateType(securityConfigurations.CreatedDatePropertyType),
                    Parameter: nameof(SecurityConfigurations.CreatedDatePropertyType)),

                (Rule: IsInvalid(securityConfigurations.UpdatedByPropertyName),
                    Parameter: nameof(SecurityConfigurations.UpdatedByPropertyName)),

                (Rule: IsInvalidDataType(securityConfigurations.UpdatedByPropertyType),
                    Parameter: nameof(SecurityConfigurations.UpdatedByPropertyType)),

                (Rule: IsInvalid(securityConfigurations.UpdatedDatePropertyName),
                    Parameter: nameof(SecurityConfigurations.UpdatedDatePropertyName)),

                (Rule: IsInvalidDateType(securityConfigurations.UpdatedDatePropertyType),
                    Parameter: nameof(SecurityConfigurations.UpdatedDatePropertyType)));

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

        private static dynamic IsInvalidDateType(Type type) => new
        {
            Condition = type != typeof(DateTime) || type != typeof(DateTimeOffset),
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
