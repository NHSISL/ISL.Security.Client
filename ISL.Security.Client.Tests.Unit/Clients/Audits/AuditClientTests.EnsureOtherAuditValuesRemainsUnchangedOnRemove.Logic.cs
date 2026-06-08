// ---------------------------------------------------------
// Copyright (c) North East London ICB. All rights reserved.
// ---------------------------------------------------------

using System;
using System.Threading.Tasks;
using FluentAssertions;
using ISL.Security.Client.Models.Clients;
using ISL.Security.Client.Tests.Unit.Models;
using Moq;

namespace ISL.Security.Client.Tests.Clients.Audits
{
    public partial class AuditClientTests
    {
        [Fact]
        public async Task ShouldEnsureOtherAuditValuesRemainsUnchangedOnRemoveAsync()
        {
            // Given
            DateTimeOffset currentDateTime = DateTime.UtcNow;
            Person inputPerson = new Person { Name = GetRandomString() };
            Person storagePerson = new Person { Name = GetRandomString() };
            Person updatedPerson = new Person { Name = GetRandomString() };
            Person expectedResult = updatedPerson;
            var securityConfigurations = new SecurityConfigurations();

            this.auditOrchestrationServiceMock.Setup(service =>
                service.EnsureOtherAuditValuesRemainsUnchangedOnRemoveAsync(
                    inputPerson,
                    storagePerson,
                    securityConfigurations))
                .ReturnsAsync(updatedPerson);

            // When
            dynamic actualResult = await this.auditClient
                .EnsureOtherAuditValuesRemainsUnchangedOnRemoveAsync(
                    inputPerson, storagePerson, securityConfigurations);

            // Then
            ((object)actualResult).Should().BeEquivalentTo(expectedResult);

            this.auditOrchestrationServiceMock.Verify(service =>
                service.EnsureOtherAuditValuesRemainsUnchangedOnRemoveAsync(
                    inputPerson,
                    storagePerson,
                    securityConfigurations),
                        Times.Once);

            this.auditOrchestrationServiceMock.VerifyNoOtherCalls();
        }
    }
}
