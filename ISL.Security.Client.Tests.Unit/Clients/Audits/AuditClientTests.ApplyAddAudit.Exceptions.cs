﻿// ---------------------------------------------------------
// Copyright (c) North East London ICB. All rights reserved.
// ---------------------------------------------------------

using System;
using System.Security.Claims;
using System.Threading.Tasks;
using FluentAssertions;
using ISL.Security.Client.Models.Clients;
using ISL.Security.Client.Models.Clients.Audits.Exceptions;
using ISL.Security.Client.Tests.Unit.Models;
using Moq;
using Xeptions;

namespace ISL.Security.Client.Tests.Clients.Audits
{
    public partial class AuditClientTests
    {
        [Theory]
        [MemberData(nameof(ValidationExceptions))]
        public async Task ShouldThrowDependencyValidationOnApplyAddAuditIfDependencyValidationOccursAndLogItAsync(
            Xeption validationException)
        {
            // given
            ClaimsPrincipal someClaimsPrincipal = CreateRandomClaimsPrincipal();
            var somePerson = new Person { Name = GetRandomString() };
            var someSecurityConfiguration = GetSecurityConfigurations();

            var expectedAuditClientValidationException =
                new AuditClientValidationException(
                    message: "Audit client validation error occurred, fix the error and try again.",
                    innerException: validationException.InnerException as Xeption,
                    data: validationException.InnerException.Data);

            this.auditOrchestrationServiceMock.Setup(service =>
                service.ApplyAddAuditAsync(
                    It.IsAny<Person>(),
                    It.IsAny<ClaimsPrincipal>(),
                    It.IsAny<SecurityConfigurations>()))
                        .ThrowsAsync(validationException);

            // when
            ValueTask<Person> task = this.auditClient.ApplyAddAuditAsync(
                somePerson,
                someClaimsPrincipal,
                someSecurityConfiguration);

            AuditClientValidationException actualAuditClientValidationException =
                await Assert.ThrowsAsync<AuditClientValidationException>(task.AsTask);

            // then
            actualAuditClientValidationException.Should()
                .BeEquivalentTo(expectedAuditClientValidationException);

            this.auditOrchestrationServiceMock.Verify(service =>
                service.ApplyAddAuditAsync(
                    It.IsAny<Person>(),
                    It.IsAny<ClaimsPrincipal>(),
                    It.IsAny<SecurityConfigurations>()),
                        Times.Once);

            this.auditOrchestrationServiceMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(DependencyExceptions))]
        public async Task ShouldThrowDependencyExceptionOnApplyAddAuditIfDependencyExceptionOccursAndLogItAsync(
            Xeption dependancyException)
        {
            // given
            ClaimsPrincipal someClaimsPrincipal = CreateRandomClaimsPrincipal();
            var somePerson = new Person { Name = GetRandomString() };
            var someSecurityConfiguration = GetSecurityConfigurations();

            var expectedAuditClientDependencyException =
                new AuditClientDependencyException(
                    message: "Audit client dependency error occurred, please contact support.",
                    innerException: dependancyException.InnerException as Xeption,
                    data: dependancyException.InnerException.Data);

            this.auditOrchestrationServiceMock.Setup(service =>
                service.ApplyAddAuditAsync(
                    It.IsAny<Person>(),
                    It.IsAny<ClaimsPrincipal>(),
                    It.IsAny<SecurityConfigurations>()))
                        .ThrowsAsync(dependancyException);

            // when
            ValueTask<Person> task = this.auditClient.ApplyAddAuditAsync(
                somePerson,
                someClaimsPrincipal,
                someSecurityConfiguration);

            AuditClientDependencyException actualAuditClientDependencyException =
                await Assert.ThrowsAsync<AuditClientDependencyException>(task.AsTask);

            // then
            actualAuditClientDependencyException.Should().BeEquivalentTo(expectedAuditClientDependencyException);

            this.auditOrchestrationServiceMock.Verify(service =>
                service.ApplyAddAuditAsync(
                    It.IsAny<Person>(),
                    It.IsAny<ClaimsPrincipal>(),
                    It.IsAny<SecurityConfigurations>()),
                        Times.Once);

            this.auditOrchestrationServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnApplyAddAuditIfServiceErrorOccursAndLogItAsync()
        {
            //Given
            ClaimsPrincipal someClaimsPrincipal = CreateRandomClaimsPrincipal();
            var somePerson = new Person { Name = GetRandomString() };
            var someSecurityConfiguration = GetSecurityConfigurations();
            var serviceException = new Exception();

            var expectedAuditClientServiceException =
                new AuditClientServiceException(
                    message: "Audit client service error occurred, please contact support.",
                    innerException: serviceException,
                    data: serviceException.Data);

            this.auditOrchestrationServiceMock.Setup(service =>
               service.ApplyAddAuditAsync(
                   It.IsAny<Person>(),
                   It.IsAny<ClaimsPrincipal>(),
                   It.IsAny<SecurityConfigurations>()))
                    .ThrowsAsync(serviceException);

            // when
            ValueTask<Person> task = this.auditClient.ApplyAddAuditAsync(
                somePerson,
                someClaimsPrincipal,
                someSecurityConfiguration);

            AuditClientServiceException actualAuditClientServiceException =
                await Assert.ThrowsAsync<AuditClientServiceException>(task.AsTask);

            // then
            actualAuditClientServiceException.Should().BeEquivalentTo(expectedAuditClientServiceException);

            this.auditOrchestrationServiceMock.Verify(service =>
                service.ApplyAddAuditAsync(
                    It.IsAny<Person>(),
                    It.IsAny<ClaimsPrincipal>(),
                    It.IsAny<SecurityConfigurations>()),
                        Times.Once);

            this.auditOrchestrationServiceMock.VerifyNoOtherCalls();
        }
    }
}