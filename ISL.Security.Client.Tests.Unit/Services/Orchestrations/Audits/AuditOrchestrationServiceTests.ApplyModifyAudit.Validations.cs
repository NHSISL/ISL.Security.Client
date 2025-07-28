﻿// ---------------------------------------------------------
// Copyright (c) North East London ICB. All rights reserved.
// ---------------------------------------------------------

using System.Security.Claims;
using System.Threading.Tasks;
using FluentAssertions;
using ISL.Security.Client.Models.Clients;
using ISL.Security.Client.Models.Orchestrations.Audits.Exceptions;
using ISL.Security.Client.Tests.Unit.Models;

namespace ISL.Security.Client.Tests.Unit.Services.Foundations.Audits
{
    public partial class AuditOrchestrationServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnApplyModifyAuditIfNullObjectsFoundAsync()
        {
            // given
            Person nullPerson = null;
            ClaimsPrincipal nullClaimsPrincipal = null;
            SecurityConfigurations nullSecurityConfigurations = null;

            InvalidArgumentAuditOrchestrationException invalidArgumentAuditException =
                new InvalidArgumentAuditOrchestrationException(
                    message: "Invalid audit orchestration argument(s), correct the errors and try again.");

            invalidArgumentAuditException.AddData(
                key: "entity",
                values: "Entity is required");

            invalidArgumentAuditException.AddData(
                key: "claimsPrincipal",
                values: "Claims principal is required");

            invalidArgumentAuditException.AddData(
                key: "securityConfigurations",
                values: "Entity is required");

            var expectedAuditValidationException =
                new AuditOrchestrationValidationException(
                    message: "Audit orchestration validation error occurred, please try again.",
                    innerException: invalidArgumentAuditException);

            // when
            ValueTask<Person> task =
                auditOrchestrationService.ApplyModifyAuditAsync(
                    nullPerson,
                    nullClaimsPrincipal,
                    nullSecurityConfigurations);

            AuditOrchestrationValidationException actualAuditOrchestrationValidationException =
                await Assert.ThrowsAsync<AuditOrchestrationValidationException>(task.AsTask);

            // then
            actualAuditOrchestrationValidationException.Should()
                .BeEquivalentTo(expectedAuditValidationException);
        }
    }
}