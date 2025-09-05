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

        public ValueTask<T> ApplyAddAuditValuesAsync<T>(
            T entity,
            ClaimsPrincipal claimsPrincipal,
            SecurityConfigurations securityConfigurations) =>
        TryCatch<T>(async () =>
        {
            ValidateInputs(entity, claimsPrincipal, securityConfigurations);
            string userId = await GetUserIdAsync(claimsPrincipal);

            T updatedEntity = await this.auditService
                .ApplyAddAuditValuesAsync(entity, userId, securityConfigurations);

            return updatedEntity;
        });

        public ValueTask<T> ApplyModifyAuditValuesAsync<T>(
            T entity,
            ClaimsPrincipal claimsPrincipal,
            SecurityConfigurations securityConfigurations) =>
        TryCatch<T>(async () =>
        {
            ValidateInputs(entity, claimsPrincipal, securityConfigurations);
            string userId = await GetUserIdAsync(claimsPrincipal);

            T updatedEntity = await this.auditService
                .ApplyModifyAuditValuesAsync(entity, userId, securityConfigurations);

            return updatedEntity;
        });

        public ValueTask<T> ApplyRemoveAuditValuesAsync<T>(
            T entity,
            ClaimsPrincipal claimsPrincipal,
            SecurityConfigurations securityConfigurations) =>
        TryCatch<T>(async () =>
        {
            ValidateInputs(entity, claimsPrincipal, securityConfigurations);
            string userId = await GetUserIdAsync(claimsPrincipal);

            T updatedEntity = await this.auditService
                .ApplyRemoveAuditValuesAsync(entity, userId, securityConfigurations);

            return updatedEntity;
        });

        public ValueTask<T> EnsureAddAuditValuesRemainsUnchangedOnModifyAsync<T>(
            T entity,
            T storageEntity,
            SecurityConfigurations securityConfigurations) =>
        TryCatch<T>(async () =>
        {
            ValidateInputs(entity, storageEntity, securityConfigurations);

            var updatedEntity = await this.auditService
                .EnsureAddAuditValuesRemainsUnchangedOnModifyAsync<T>(entity, storageEntity, securityConfigurations);

            return updatedEntity;
        });

        public ValueTask<string> GetCurrentUserIdAsync(ClaimsPrincipal claimsPrincipal) =>
        TryCatch(async () =>
        {
            ValidateOnGetCurrentUserId(claimsPrincipal);
            string userId = await GetUserIdAsync(claimsPrincipal);

            return userId;
        });

        private async ValueTask<string> GetUserIdAsync(ClaimsPrincipal claimsPrincipal)
        {
            User user = await this.userService.GetUserAsync(claimsPrincipal);
            bool isAuthenticated = await this.userService.IsUserAuthenticatedAsync(claimsPrincipal);

            string userId = isAuthenticated
                ? user.UserId
                : string.IsNullOrEmpty(user.UserId)
                    ? "anonymous" : user.UserId;

            return userId;
        }
    }
}
