// ---------------------------------------------------------
// Copyright (c) North East London ICB. All rights reserved.
// ---------------------------------------------------------

using System.Security.Claims;
using System.Threading.Tasks;
using ISL.Security.Client.Models.Clients;
using ISL.Security.Client.Models.Foundations.Users;
using ISL.Security.Client.Services.Foundations.Users;
using ISL.Security.Client.Services.Orchestrations.Audits;

namespace ISL.Security.Client.Services.Foundations.Audits
{
    internal partial class AuditOrchestrationService : IAuditOrchestrationService
    {
        private readonly IUserService userService;
        private readonly IAuditService auditService;

        public AuditOrchestrationService(IUserService userService, IAuditService auditService)
        {
            this.userService = userService;
            this.auditService = auditService;
        }

        public ValueTask<T> ApplyAddAuditAsync<T>(
            T entity,
            ClaimsPrincipal claimsPrincipal,
            SecurityConfigurations securityConfigurations) =>
        TryCatch<T>(async () =>
        {
            ValidateInputs(entity, claimsPrincipal);
            User user = await this.userService.GetUserAsync(claimsPrincipal);

            T updatedEntity = await this.auditService
                .ApplyAddAuditAsync(entity, user.UserId, securityConfigurations);

            return updatedEntity;
        });

        public ValueTask<T> ApplyModifyAuditAsync<T>(
            T entity,
            ClaimsPrincipal claimsPrincipal,
            SecurityConfigurations securityConfigurations) =>
        TryCatch<T>(async () =>
        {
            ValidateInputs(entity, claimsPrincipal);
            User user = await this.userService.GetUserAsync(claimsPrincipal);

            T updatedEntity = await this.auditService
                .ApplyModifyAuditAsync(entity, user.UserId, securityConfigurations);

            return updatedEntity;
        });

        public ValueTask<T> ApplyRemoveAuditAsync<T>(
            T entity,
            ClaimsPrincipal claimsPrincipal,
            SecurityConfigurations securityConfigurations) =>
        TryCatch<T>(async () =>
        {
            ValidateInputs(entity, claimsPrincipal);
            User user = await this.userService.GetUserAsync(claimsPrincipal);

            T updatedEntity = await this.auditService
                .ApplyRemoveAuditAsync(entity, user.UserId, securityConfigurations);

            return updatedEntity;
        });

        public ValueTask<T> EnsureAddAuditValuesRemainsUnchangedOnModifyAsync<T>(
            T entity,
            T storageEntity,
            SecurityConfigurations securityConfigurations) =>
        TryCatch<T>(async () =>
        {
            ValidateInputs(entity, storageEntity);

            T updatedEntity = await this.auditService
                .EnsureAddAuditValuesRemainsUnchangedOnModifyAsync(entity, storageEntity, securityConfigurations);

            return updatedEntity;
        });
    }
}
