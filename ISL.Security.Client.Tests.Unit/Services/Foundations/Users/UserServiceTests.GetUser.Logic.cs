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
        [Fact]
        public async Task ShouldGetUserAsync()
        {
            // Given
            string userId = GetRandomString();
            ClaimsPrincipal claimsPrincipal = CreateRandomClaimsPrincipal(userId);

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
            User actualUser = await this.userService.GetUserAsync(claimsPrincipal);

            // Then
            actualUser.Should().BeEquivalentTo(expectedUser);
        }
    }
}
