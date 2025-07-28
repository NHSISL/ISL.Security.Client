// ---------------------------------------------------------
// Copyright (c) North East London ICB. All rights reserved.
// ---------------------------------------------------------

using System.Security.Claims;
using System.Threading.Tasks;
using FluentAssertions;
using ISL.Security.Client.Models.Clients;
using ISL.Security.Client.Models.Foundations.Audits.Exceptions;
using ISL.Security.Client.Tests.Unit.Models;

namespace ISL.Security.Client.Tests.Unit.Services.Foundations.Audits
{
    public partial class AuditOrchestrationServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnApplyAddAuditIfNullObjectsFoundAsync()
        {
            // given
            Person nullPerson = null;
            ClaimsPrincipal nullClaimsPrincipal = null;
            SecurityConfigurations nullSecurityConfigurations = null;

            InvalidArgumentAuditException invalidArgumentAuditException = new InvalidArgumentAuditException(
                message: "Invalid audit argument(s), correct the errors and try again.");

            invalidArgumentAuditException.AddData(
                key: "entity",
                values: "Entity is required");

            invalidArgumentAuditException.AddData(
                key: "claimsPrincipal",
                values: "Claims principal is required");

            invalidArgumentAuditException.AddData(
                key: nameof(SecurityConfigurations),
                values: "Entity is required");

            var expectedAuditValidationException =
                new AuditValidationException(
                    message: "Audit validation errors occurred, please try again.",
                    innerException: invalidArgumentAuditException);

            // when
            ValueTask<Person> taskask =
                auditOrchestrationService.ApplyAddAuditAsync(
                    nullPerson,
                    nullClaimsPrincipal,
                    nullSecurityConfigurations);

            AuditValidationException actualAuditValidationException =
                await Assert.ThrowsAsync<AuditValidationException>(taskask.AsTask);

            // then
            actualAuditValidationException.Should()
                .BeEquivalentTo(expectedAuditValidationException);
        }
    }
}