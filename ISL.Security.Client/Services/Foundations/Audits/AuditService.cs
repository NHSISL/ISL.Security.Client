// ---------------------------------------------------------
// Copyright (c) North East London ICB. All rights reserved.
// ---------------------------------------------------------

using System;
using System.Threading.Tasks;
using ISL.Security.Client.Brokers.DateTimes;
using ISL.Security.Client.Models.Clients;

namespace ISL.Security.Client.Services.Foundations.Audits
{
    internal partial class AuditService : IAuditService
    {
        private readonly IDateTimeBroker dateTimeBroker;

        public AuditService(IDateTimeBroker dateTimeBroker) =>
            this.dateTimeBroker = dateTimeBroker;

        public ValueTask<T> ApplyAddAuditAsync<T>(
            T entity,
            string userId,
            SecurityConfigurations securityConfigurations) =>
        TryCatch(async () =>
        {
            ValidateInputs(entity, userId);
            var auditDateTimeOffset = await this.dateTimeBroker.GetCurrentDateTimeOffsetAsync();
            var createdByName = securityConfigurations.CreatedByPropertyName;
            var createdDateName = securityConfigurations.CreatedDatePropertyName;
            var updatedByName = securityConfigurations.UpdatedByPropertyName;
            var updatedDateName = securityConfigurations.UpdatedDatePropertyName;
            ValidateProperties(entity, securityConfigurations);

            SetProperty(
                entity,
                propertyName: createdByName,
                value: userId);

            SetProperty(
                entity,
                propertyName: createdDateName,
                value: auditDateTimeOffset);

            SetProperty(
                entity,
                propertyName: updatedByName,
                value: userId);

            SetProperty(
                entity,
                propertyName: updatedDateName,
                value: auditDateTimeOffset);

            return entity;
        });

        public ValueTask<T> ApplyModifyAuditAsync<T>(
            T entity,
            string userId,
            SecurityConfigurations securityConfigurations) =>
        TryCatch(async () =>
        {
            ValidateInputs(entity, userId);
            var auditDateTimeOffset = await this.dateTimeBroker.GetCurrentDateTimeOffsetAsync();
            var updatedByName = securityConfigurations.UpdatedByPropertyName;
            var updatedDateName = securityConfigurations.UpdatedDatePropertyName;
            ValidateProperties(entity, securityConfigurations);

            SetProperty(
                entity,
                propertyName: updatedByName,
                value: userId);

            SetProperty(
                entity,
                propertyName: updatedDateName,
                value: auditDateTimeOffset);

            return entity;
        });

        public ValueTask<T> ApplyRemoveAuditAsync<T>(
            T entity,
            string userId,
            SecurityConfigurations securityConfigurations) =>
        TryCatch(async () =>
        {
            ValidateInputs(entity, userId);
            var auditDateTimeOffset = await this.dateTimeBroker.GetCurrentDateTimeOffsetAsync();
            var updatedByName = securityConfigurations.UpdatedByPropertyName;
            var updatedDateName = securityConfigurations.UpdatedDatePropertyName;
            ValidateProperties(entity, securityConfigurations);

            SetProperty(
                entity,
                propertyName: updatedByName,
                value: userId);

            SetProperty(
                entity,
                propertyName: updatedDateName,
                value: auditDateTimeOffset);

            return entity;
        });

        public ValueTask<T> EnsureAddAuditValuesRemainsUnchangedOnModifyAsync<T>(
            T entity,
            T storageEntity,
            SecurityConfigurations securityConfigurations) =>
        TryCatch<T>(async () =>
        {
            ValidateInputs(entity, storageEntity);
            ValidateProperties(entity, securityConfigurations);

            var createdByName = securityConfigurations.CreatedByPropertyName;
            var createdDateName = securityConfigurations.CreatedDatePropertyName;

            // Get current values from storageEntity for CreatedBy and CreatedDate
            var createdByProperty = typeof(T).GetProperty(createdByName);
            var createdDateProperty = typeof(T).GetProperty(createdDateName);

            if (createdByProperty == null || !createdByProperty.CanRead)
                throw new InvalidOperationException(
                    $"Property '{createdByName}' not found or not readable on type '{typeof(T).Name}'.");

            if (createdDateProperty == null || !createdDateProperty.CanRead)
                throw new InvalidOperationException(
                    $"Property '{createdDateName}' not found or not readable on type '{typeof(T).Name}'.");

            object createdByValue = createdByProperty.GetValue(storageEntity);
            object createdDateValue = createdDateProperty.GetValue(storageEntity);

            SetProperty(entity, createdByName, createdByValue);
            SetProperty(entity, createdDateName, createdDateValue);

            return entity;
        });

        private static void SetProperty<T>(T entity, string propertyName, object value)
        {
            var prop = typeof(T).GetProperty(propertyName);
            prop.SetValue(entity, value);
        }
    }
}
