﻿// ---------------------------------------------------------
// Copyright (c) North East London ICB. All rights reserved.
// ---------------------------------------------------------

using System.Security.Claims;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;

namespace ISL.Security.Client.Tests.Unit.Clients.Users
{
    public partial class UserClientTests
    {
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task ShouldPerformUserHasClaimTypeAsync(bool hasClaimType)
        {
            // Given
            string claimType = GetRandomString();
            ClaimsPrincipal claimsPrincipal = CreateRandomClaimsPrincipal();
            bool expectedResult = hasClaimType;

            this.userServiceMock.Setup(service =>
                service.UserHasClaimTypeAsync(claimsPrincipal, claimType))
                    .ReturnsAsync(expectedResult);

            // When
            bool actualResult = await this.userClient.UserHasClaimTypeAsync(claimsPrincipal, claimType);

            // Then
            actualResult.Should().Be(expectedResult);

            this.userServiceMock.Verify(service =>
                service.UserHasClaimTypeAsync(claimsPrincipal, claimType),
                    Times.Once);

            this.userServiceMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task ShouldPerformUserHasClaimTypeWithValueAsync(bool hasClaimType)
        {
            // Given
            ClaimsPrincipal claimsPrincipal = CreateRandomClaimsPrincipal();
            string claimType = GetRandomString();
            string claimValue = GetRandomString();
            bool expectedResult = hasClaimType;

            this.userServiceMock.Setup(service =>
                service.UserHasClaimTypeAsync(claimsPrincipal, claimType, claimValue))
                    .ReturnsAsync(expectedResult);

            // When
            bool actualResult = await this.userClient.UserHasClaimTypeAsync(claimsPrincipal, claimType, claimValue);

            // Then
            actualResult.Should().Be(expectedResult);

            this.userServiceMock.Verify(service =>
                service.UserHasClaimTypeAsync(claimsPrincipal, claimType, claimValue),
                    Times.Once);

            this.userServiceMock.VerifyNoOtherCalls();
        }
    }
}
