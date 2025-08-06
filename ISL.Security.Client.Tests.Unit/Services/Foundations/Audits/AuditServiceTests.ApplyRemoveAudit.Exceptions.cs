﻿// ---------------------------------------------------------
// Copyright (c) North East London ICB. All rights reserved.
// ---------------------------------------------------------

using System;
using System.Dynamic;
using System.Threading.Tasks;
using FluentAssertions;
using ISL.Security.Client.Models.Clients;
using ISL.Security.Client.Models.Foundations.Audits.Exceptions;
using Moq;

namespace ISL.Security.Client.Tests.Unit.Services.Foundations.Audits
{
    public partial class AuditServiceTests
    {
        [Fact]
        public async Task ShouldThrowServiceExceptionOnApplyRemoveAuditIfServiceErrorOccursAndLogItAsync()
        {
            // given
            dynamic someObject = new ExpandoObject();
            someObject.Name = "John Doe";
            someObject.CreatedBy = string.Empty;
            someObject.CreatedDate = DateTimeOffset.MinValue;
            someObject.UpdatedBy = string.Empty;
            someObject.UpdatedDate = DateTimeOffset.MinValue;
            string someUserId = GetRandomString();
            var serviceException = new Exception();

            var someSecurityConfigurations = new SecurityConfigurations
            {
                CreatedByPropertyName = "CreatedBy",
                CreatedByPropertyType = typeof(string),
                CreatedDatePropertyName = "CreatedDate",
                CreatedDatePropertyType = typeof(DateTimeOffset),
                UpdatedByPropertyName = "UpdatedBy",
                UpdatedByPropertyType = typeof(string),
                UpdatedDatePropertyName = "UpdatedDate",
                UpdatedDatePropertyType = typeof(DateTimeOffset)
            };

            var failedAuditServiceException =
                new FailedAuditServiceException(
                    message: "Failed audit service error occurred, please contact support.",
                    innerException: serviceException);

            var expectedAuditServiceException =
                new AuditServiceException(
                    message: "Audit service error occurred, please contact support.",
                    innerException: failedAuditServiceException);

            dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTimeOffsetAsync())
                    .Throws(serviceException);

            // when
            ValueTask<ExpandoObject> applyModifyAuditTask =
                auditService.ApplyRemoveAuditValuesAsync(someObject, someUserId, someSecurityConfigurations);

            AuditServiceException actualAuditServiceException =
                await Assert.ThrowsAsync<AuditServiceException>(
                    applyModifyAuditTask.AsTask);

            // then
            actualAuditServiceException.Should()
                .BeEquivalentTo(expectedAuditServiceException);

            dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTimeOffsetAsync(),
                    Times.Once);

            dateTimeBrokerMock.VerifyNoOtherCalls();
        }
    }
}