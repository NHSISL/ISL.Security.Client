// ---------------------------------------------------------
// Copyright (c) North East London ICB. All rights reserved.
// ---------------------------------------------------------

using System;
using System.Security.Claims;
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
        public async Task ShouldApplyRemoveAuditWithDeletionReasonForDynamicObjectAsync()
        {
            // Given
            ClaimsPrincipal randomClaimsPrincipal = CreateRandomClaimsPrincipal();
            ClaimsPrincipal inputClaimsPrincipal = randomClaimsPrincipal;
            string randomUserId = GetRandomString();
            string inputDeletionReason = GetRandomString();
            var inputPerson = new Person { Name = GetRandomString() };
            var updatedPerson = new Person { Name = GetRandomString() };
            var expectedResult = updatedPerson;

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

            this.userServiceMock.Setup(service =>
                service.GetUserIdAsync(inputClaimsPrincipal))
                    .ReturnsAsync(randomUserId);

            this.auditServiceMock.Setup(service =>
                service.ApplyRemoveAuditValuesAsync(
                    inputPerson, randomUserId, securityConfigurations, inputDeletionReason))
                .ReturnsAsync(updatedPerson);

            // When
            var actualResult = await this.auditOrchestrationService
                .ApplyRemoveAuditValuesAsync(
                    inputPerson, inputClaimsPrincipal, securityConfigurations, inputDeletionReason);

            // Then
            ((object)actualResult).Should().BeEquivalentTo(expectedResult);

            this.userServiceMock.Verify(service =>
                service.GetUserIdAsync(inputClaimsPrincipal),
                    Times.Once);

            this.auditServiceMock.Verify(service =>
                service.ApplyRemoveAuditValuesAsync(
                    inputPerson, randomUserId, securityConfigurations, inputDeletionReason),
                Times.Once);

            this.userServiceMock.VerifyNoOtherCalls();
            this.auditServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldApplyRemoveAuditForDynamicObjectAsync()
        {
            // Given
            ClaimsPrincipal randomClaimsPrincipal = CreateRandomClaimsPrincipal();
            ClaimsPrincipal inputClaimsPrincipal = randomClaimsPrincipal;
            string randomUserId = GetRandomString();
            var inputPerson = new Person { Name = GetRandomString() };
            var updatedPerson = new Person { Name = GetRandomString() };
            var expectedResult = updatedPerson;

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

            this.userServiceMock.Setup(service =>
                service.GetUserIdAsync(inputClaimsPrincipal))
                    .ReturnsAsync(randomUserId);

            this.auditServiceMock.Setup(service =>
                service.ApplyRemoveAuditValuesAsync(inputPerson, randomUserId, securityConfigurations, null))
                    .ReturnsAsync(updatedPerson);

            // When
            var actualResult = await this.auditOrchestrationService
                .ApplyRemoveAuditValuesAsync(inputPerson, inputClaimsPrincipal, securityConfigurations);

            // Then
            ((object)actualResult).Should().BeEquivalentTo(expectedResult);

            this.userServiceMock.Verify(service =>
                service.GetUserIdAsync(inputClaimsPrincipal),
                    Times.Once);

            this.auditServiceMock.Verify(service =>
                service.ApplyRemoveAuditValuesAsync(inputPerson, randomUserId, securityConfigurations, null),
                    Times.Once);

            this.userServiceMock.VerifyNoOtherCalls();
            this.auditServiceMock.VerifyNoOtherCalls();
        }
    }
}