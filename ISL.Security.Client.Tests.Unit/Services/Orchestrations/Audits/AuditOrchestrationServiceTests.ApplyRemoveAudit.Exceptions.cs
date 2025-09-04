// ---------------------------------------------------------
// Copyright (c) North East London ICB. All rights reserved.
// ---------------------------------------------------------

using System;
using System.Security.Claims;
using System.Threading.Tasks;
using FluentAssertions;
using ISL.Security.Client.Models.Orchestrations.Audits.Exceptions;
using ISL.Security.Client.Tests.Unit.Models;
using Moq;
using Xeptions;

namespace ISL.Security.Client.Tests.Unit.Services.Foundations.Audits
{
    public partial class AuditOrchestrationServiceTests
    {
        [Theory]
        [MemberData(nameof(DependencyValidationExceptions))]
        public async Task ShouldThrowDependencyValidationOnApplyRemoveAuditIfDependencyValidationOccursAndLogItAsync(
            Xeption dependancyValidationException)
        {
            // given
            string userId = GetRandomString();
            ClaimsPrincipal someClaimsPrincipal = CreateRandomClaimsPrincipal(userId);
            var somePerson = new Person { Name = GetRandomString() };
            var someSecurityConfiguration = GetSecurityConfigurations();

            var expectedDependencyException =
                new AuditOrchestrationDependencyValidationException(
                    message: "Audit orchestration dependency validation error occurred, fix the errors and try again.",
                    innerException: dependancyValidationException.InnerException as Xeption);

            this.userServiceMock.Setup(service =>
               service.GetUserAsync(It.IsAny<ClaimsPrincipal>()))
                   .ThrowsAsync(dependancyValidationException);

            // when
            ValueTask<Person> task = this.auditOrchestrationService.ApplyRemoveAuditValuesAsync(
                somePerson,
                someClaimsPrincipal,
                someSecurityConfiguration);

            AuditOrchestrationDependencyValidationException actualException =
                await Assert.ThrowsAsync<AuditOrchestrationDependencyValidationException>(task.AsTask);

            // then
            actualException.Should()
                .BeEquivalentTo(expectedDependencyException);

            this.userServiceMock.Verify(service =>
                service.GetUserAsync(It.IsAny<ClaimsPrincipal>()),
                    Times.Once);

            this.userServiceMock.VerifyNoOtherCalls();
            this.auditServiceMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(DependencyExceptions))]
        public async Task ShouldThrowDependencyExceptionOnApplyRemoveAuditIfDependencyExceptionOccursAndLogItAsync(
            Xeption dependancyException)
        {
            // given
            string userId = GetRandomString();
            ClaimsPrincipal someClaimsPrincipal = CreateRandomClaimsPrincipal(userId);
            var somePerson = new Person { Name = GetRandomString() };
            var someSecurityConfiguration = GetSecurityConfigurations();

            var expectedDependencyException =
                new AuditOrchestrationDependencyException(
                    message: "Audit orchestration dependency error occurred, fix the errors and try again.",
                    innerException: dependancyException.InnerException as Xeption);

            this.userServiceMock.Setup(service =>
               service.GetUserAsync(It.IsAny<ClaimsPrincipal>()))
                  .ThrowsAsync(dependancyException);

            // when
            ValueTask<Person> task = this.auditOrchestrationService.ApplyRemoveAuditValuesAsync(
                somePerson,
                someClaimsPrincipal,
                someSecurityConfiguration);

            AuditOrchestrationDependencyException actualException =
                await Assert.ThrowsAsync<AuditOrchestrationDependencyException>(task.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedDependencyException);

            this.userServiceMock.Verify(service =>
                service.GetUserAsync(It.IsAny<ClaimsPrincipal>()),
                    Times.Once);

            this.userServiceMock.VerifyNoOtherCalls();
            this.auditServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnApplyRemoveAuditIfServiceErrorOccursAndLogItAsync()
        {
            //Given
            string userId = GetRandomString();
            ClaimsPrincipal someClaimsPrincipal = CreateRandomClaimsPrincipal(userId);
            var somePerson = new Person { Name = GetRandomString() };
            var someSecurityConfiguration = GetSecurityConfigurations();
            var serviceException = new Exception();

            var failedAuditOrchestrationServiceException =
                new FailedAuditOrchestrationServiceException(
                    message: "Failed audit orchestration service error occurred, please contact support.",
                    innerException: serviceException);

            var expectedAuditOrchestrationServiceException =
                new AuditOrchestrationServiceException(
                    message: "Audit orchestration service error occurred, please contact support.",
                    innerException: failedAuditOrchestrationServiceException);

            this.userServiceMock.Setup(service =>
               service.GetUserAsync(It.IsAny<ClaimsPrincipal>()))
                    .ThrowsAsync(serviceException);

            // when
            ValueTask<Person> task = this.auditOrchestrationService.ApplyRemoveAuditValuesAsync(
                somePerson,
                someClaimsPrincipal,
                someSecurityConfiguration);

            AuditOrchestrationServiceException actualException =
                await Assert.ThrowsAsync<AuditOrchestrationServiceException>(task.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedAuditOrchestrationServiceException);

            this.userServiceMock.Verify(service =>
                service.GetUserAsync(It.IsAny<ClaimsPrincipal>()),
                    Times.Once);

            this.userServiceMock.VerifyNoOtherCalls();
            this.auditServiceMock.VerifyNoOtherCalls();
        }
    }
}