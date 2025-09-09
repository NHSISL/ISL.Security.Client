// ---------------------------------------------------------
// Copyright (c) North East London ICB. All rights reserved.
// ---------------------------------------------------------

using System;
using System.Security.Claims;
using System.Threading.Tasks;
using FluentAssertions;
using ISL.Security.Client.Models.Foundations.Users.Exceptions;
using ISL.Security.Client.Services.Foundations.Users;
using Moq;

namespace ISL.Security.Client.Tests.Unit.Services.Foundations.Users
{
    public partial class UserServiceTests
    {
        [Fact]
        public async Task ShouldThrowServiceExceptionOnGetUserIdIfServiceErrorOccursAndLogItAsync()
        {
            // given
            ClaimsPrincipal someClaimsPrincipal = new ClaimsPrincipal();
            var serviceException = new Exception();

            var failedUserServiceException =
                new FailedUserServiceException(
                    message: "Failed user service error occurred, please contact support.",
                    innerException: serviceException);

            var expectedUserServiceException =
                new UserServiceException(
                    message: "User service error occurred, please contact support.",
                    innerException: failedUserServiceException);

            var userServiceMock = new Mock<UserService> { CallBase = true };

            userServiceMock.Setup(service =>
                service.ValidateOnGetUserId(It.IsAny<ClaimsPrincipal>()))
                    .Throws(serviceException);

            // when
            ValueTask<string> getUserIdTask =
                userServiceMock.Object.GetUserIdAsync(someClaimsPrincipal);

            UserServiceException actualUserServiceException =
                await Assert.ThrowsAsync<UserServiceException>(
                    getUserIdTask.AsTask);

            // then
            actualUserServiceException.Should()
                .BeEquivalentTo(expectedUserServiceException);

            userServiceMock.Verify(service =>
                service.ValidateOnGetUserId(It.IsAny<ClaimsPrincipal>()),
                    Times.Once);

            userServiceMock.VerifyNoOtherCalls();
        }
    }
}
