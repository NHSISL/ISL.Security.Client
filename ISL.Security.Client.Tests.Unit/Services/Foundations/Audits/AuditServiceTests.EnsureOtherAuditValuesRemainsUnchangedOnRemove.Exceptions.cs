// ---------------------------------------------------------
// Copyright (c) North East London ICB. All rights reserved.
// ---------------------------------------------------------

using System;
using System.Dynamic;
using System.Threading.Tasks;
using FluentAssertions;
using ISL.Security.Client.Models.Clients;
using ISL.Security.Client.Models.Foundations.Audits.Exceptions;
using ISL.Security.Client.Services.Foundations.Audits;
using Moq;

namespace ISL.Security.Client.Tests.Unit.Services.Foundations.Audits
{
    public partial class AuditServiceTests
    {
        [Fact]
        public async Task ShouldThrowServiceExceptionOnEnsureOtherAuditValuesRemainsUnchangedOnRemoveIfServiceErrorOccursAsync()
        {
            // given
            dynamic someObject = new ExpandoObject();
            someObject.Name = "John Doe";
            someObject.CreatedBy = string.Empty;
            someObject.CreatedDate = DateTimeOffset.MinValue;
            someObject.UpdatedBy = string.Empty;
            someObject.UpdatedDate = DateTimeOffset.MinValue;

            dynamic someStorageObject = new ExpandoObject();
            someStorageObject.Name = "John Doe";
            someStorageObject.CreatedBy = string.Empty;
            someStorageObject.CreatedDate = DateTimeOffset.MinValue;
            someStorageObject.UpdatedBy = string.Empty;
            someStorageObject.UpdatedDate = DateTimeOffset.MinValue;

            var serviceException = new Exception();

            var someSecurityConfigurations = new SecurityConfigurations
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

            var failedAuditServiceException =
                new FailedAuditServiceException(
                    message: "Failed audit service error occurred, please contact support.",
                    innerException: serviceException);

            var expectedAuditServiceException =
                new AuditServiceException(
                    message: "Audit service error occurred, please contact support.",
                    innerException: failedAuditServiceException);

            Mock<AuditService> auditServiceMock = new Mock<AuditService>(this.dateTimeBrokerMock.Object)
            {
                CallBase = true
            };

            auditServiceMock.Setup(broker =>
                broker.ValidateInputs(
                    It.IsAny<object>(),
                    It.IsAny<object>(),
                    It.IsAny<SecurityConfigurations>()))
                        .Throws(serviceException);

            // when
            ValueTask<ExpandoObject> task =
                auditServiceMock.Object.EnsureOtherAuditValuesRemainsUnchangedOnRemoveAsync(
                    someObject,
                    someStorageObject,
                    someSecurityConfigurations);

            AuditServiceException actualAuditServiceException =
                await Assert.ThrowsAsync<AuditServiceException>(task.AsTask);

            // then
            actualAuditServiceException.Should()
                .BeEquivalentTo(expectedAuditServiceException);

            auditServiceMock.Verify(broker =>
                broker.ValidateInputs(
                    It.IsAny<object>(),
                    It.IsAny<object>(),
                    It.IsAny<SecurityConfigurations>()),
                    Times.Once);

            auditServiceMock.VerifyNoOtherCalls();
        }
    }
}
