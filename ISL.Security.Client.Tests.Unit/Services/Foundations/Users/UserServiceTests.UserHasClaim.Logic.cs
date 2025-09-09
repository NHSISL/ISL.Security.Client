﻿// ---------------------------------------------------------
// Copyright (c) North East London ICB. All rights reserved.
// ---------------------------------------------------------

using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FluentAssertions;
using ISL.Security.Client.Models.Foundations.Users;

namespace ISL.Security.Client.Tests.Unit.Services.Foundations.Users
{
    public partial class UserServiceTests
    {
        [Theory]
        [InlineData("displayName", true)]
        [InlineData("nickName", false)]
        public async Task ShouldPerformUserHasClaimTypeAsync(string claimType, bool hasClaimType)
        {
            // Given
            string userId = GetRandomString();
            ClaimsPrincipal claimsPrincipal = CreateRandomClaimsPrincipal(userId);
            bool expectedResult = hasClaimType;

            User expectedUser = new User(
                userId: claimsPrincipal.FindFirst("oid")?.Value,
                givenName: claimsPrincipal.FindFirst(ClaimTypes.GivenName)?.Value,
                surname: claimsPrincipal.FindFirst(ClaimTypes.Surname)?.Value,
                displayName: claimsPrincipal.FindFirst("displayName")?.Value,
                email: claimsPrincipal.FindFirst(ClaimTypes.Email)?.Value,
                jobTitle: claimsPrincipal.FindFirst("jobTitle")?.Value,
                roles: claimsPrincipal.FindAll(ClaimTypes.Role).Select(role => role.Value).ToList(),
                claims: claimsPrincipal.Claims.ToList());

            // When
            bool actualResult = await this.userService.UserHasClaimAsync(claimsPrincipal, claimType);

            // Then
            actualResult.Should().Be(expectedResult);
        }

        [Fact]
        public async Task ShouldPerformUserHasClaimTypeWithValueAsync()
        {
            // Given
            string userId = GetRandomString();
            ClaimsPrincipal claimsPrincipal = CreateRandomClaimsPrincipal(userId);
            string claimType = "displayName";
            string claimValue = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == claimType)?.Value;
            bool expectedResult = true;

            User expectedUser = new User(
                userId: claimsPrincipal.FindFirst("oid")?.Value,
                givenName: claimsPrincipal.FindFirst(ClaimTypes.GivenName)?.Value,
                surname: claimsPrincipal.FindFirst(ClaimTypes.Surname)?.Value,
                displayName: claimsPrincipal.FindFirst("displayName")?.Value,
                email: claimsPrincipal.FindFirst(ClaimTypes.Email)?.Value,
                jobTitle: claimsPrincipal.FindFirst("jobTitle")?.Value,
                roles: claimsPrincipal.FindAll(ClaimTypes.Role).Select(role => role.Value).ToList(),
                claims: claimsPrincipal.Claims.ToList());

            // When
            bool actualResult = await this.userService.UserHasClaimAsync(claimsPrincipal, claimType, claimValue);

            // Then
            actualResult.Should().Be(expectedResult);
        }
    }
}
