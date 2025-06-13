// ---------------------------------------------------------
// Copyright (c) North East London ICB. All rights reserved.
// ---------------------------------------------------------

using System.Threading.Tasks;
using ISL.Security.Client.Models.Clients;

namespace ISL.Security.Client.Services.Foundations.Audits
{
    internal interface IAuditService
    {
        ValueTask<T> ApplyAddAuditAsync<T>(T entity, string userId, SecurityConfigurations securityConfigurations);
        ValueTask<T> ApplyModifyAuditAsync<T>(T entity, string userId, SecurityConfigurations securityConfigurations);
        ValueTask<T> ApplyRemoveAuditAsync<T>(T entity, string userId, SecurityConfigurations securityConfigurations);

        ValueTask<T> EnsureAddAuditValuesRemainsUnchangedOnModifyAsync<T>(
            T entity,
            T storageEntity,
            SecurityConfigurations securityConfigurations);
    }
}
