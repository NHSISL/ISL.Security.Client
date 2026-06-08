// ---------------------------------------------------------
// Copyright (c) North East London ICB. All rights reserved.
// ---------------------------------------------------------

using System;
using System.Threading.Tasks;
using FluentAssertions;
using ISL.Security.Client.Models.Clients;
using ISL.Security.Client.Tests.Unit.Models;
using Moq;

namespace ISL.Security.Client.Tests.Unit.Services.Orchestrations.Audits
{
    public partial class AuditOrchestrationServiceTests
    {
        [Fact]
        public async Task ShouldEnsureOtherAuditValuesRemainsUnchangedOnRemoveForExpandoObjectAsync()
        {
            // Given
            DateTimeOffset currentDateTime = DateTime.UtcNow;
            Person inputPerson = new Person { Name = GetRandomString() };
            Person storagePerson = new Person { Name = GetRandomString() };
            Person updatedPerson = new Person { Name = GetRandomString() };
            Person expectedResult = updatedPerson;

            var securityConfigurations = new SecurityConfigurations
            {
                CreatedByPropertyName = "CreatedBy",
                CreatedByPropertyType = typeof(string),
                CreatedWhenPropertyName = "CreatedDate",
                CreatedWhenPropertyType = typeof(DateTimeOffset),
                UpdatedByPropertyName = "UpdatedBy",
                UpdatedByPropertyType = typeof(string),
                UpdatedWhenPropertyName = "UpdatedDate",
                UpdatedWhenPropertyType = typeof(DateTimeOffset)
            };

            this.auditServiceMock.Setup(service =>
                service.EnsureOtherAuditValuesRemainsUnchangedOnRemoveAsync(
                    inputPerson,
                    storagePerson,
                    securityConfigurations))
                .ReturnsAsync(updatedPerson);

            // When
            dynamic actualResult = await this.auditOrchestrationService
                .EnsureOtherAuditValuesRemainsUnchangedOnRemoveAsync(inputPerson, storagePerson, securityConfigurations);

            // Then
            ((object)actualResult).Should().BeEquivalentTo(expectedResult);

            this.auditServiceMock.Verify(service =>
                service.EnsureOtherAuditValuesRemainsUnchangedOnRemoveAsync(
                    inputPerson,
                    storagePerson,
                    securityConfigurations),
                        Times.Once);

            this.userServiceMock.VerifyNoOtherCalls();
            this.auditServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldEnsureOtherAuditValuesRemainsUnchangedOnRemoveForObjectAsync()
        {
            // Given
            DateTimeOffset currentDateTime = DateTime.UtcNow;
            string createdUserId = GetRandomString();
            string modifiedUserId = GetRandomString();

            var inputPerson = new Person
            {
                Name = "John Doe",
                CreatedBy = modifiedUserId,
                CreatedWhen = currentDateTime,
                UpdatedBy = modifiedUserId,
                UpdatedWhen = currentDateTime,
            };

            var storagePerson = new Person
            {
                Name = "John Doe",
                CreatedBy = createdUserId,
                CreatedWhen = DateTimeOffset.MinValue,
                UpdatedBy = createdUserId,
                UpdatedWhen = DateTimeOffset.MinValue,
            };

            var updatedPerson = new Person
            {
                Name = "John Doe",
                CreatedBy = createdUserId,
                CreatedWhen = DateTimeOffset.MinValue,
                UpdatedBy = createdUserId,
                UpdatedWhen = DateTimeOffset.MinValue,
            };

            Person expectedResult = updatedPerson;

            var securityConfigurations = new SecurityConfigurations
            {
                CreatedByPropertyName = "CreatedBy",
                CreatedByPropertyType = typeof(string),
                CreatedWhenPropertyName = "CreatedWhen",
                CreatedWhenPropertyType = typeof(DateTimeOffset),
                UpdatedByPropertyName = "UpdatedBy",
                UpdatedByPropertyType = typeof(string),
                UpdatedWhenPropertyName = "UpdatedWhen",
                UpdatedWhenPropertyType = typeof(DateTimeOffset)
            };

            this.auditServiceMock.Setup(broker =>
                broker.EnsureOtherAuditValuesRemainsUnchangedOnRemoveAsync(
                    inputPerson,
                    storagePerson,
                    securityConfigurations))
                        .ReturnsAsync(updatedPerson);

            // When
            var actualResult = await this.auditOrchestrationService
                .EnsureOtherAuditValuesRemainsUnchangedOnRemoveAsync(inputPerson, storagePerson, securityConfigurations);

            // Then
            ((object)actualResult).Should().BeEquivalentTo(expectedResult);

            this.auditServiceMock.Verify(broker =>
                broker.EnsureOtherAuditValuesRemainsUnchangedOnRemoveAsync(
                    inputPerson,
                    storagePerson,
                    securityConfigurations),
                        Times.Once);

            this.userServiceMock.VerifyNoOtherCalls();
            this.auditServiceMock.VerifyNoOtherCalls();
        }
    }
}
