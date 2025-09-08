﻿// ---------------------------------------------------------
// Copyright (c) North East London ICB. All rights reserved.
// ---------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ISL.Security.Client.Models.Foundations.Users;
using ISL.Security.Client.Models.Foundations.Users.Exceptions;

namespace ISL.Security.Client.Services.Foundations.Users
{
    internal partial class UserService : IUserService
    {
        public ValueTask<User> GetUserAsync(ClaimsPrincipal claimsPrincipal) =>
        TryCatch(async () =>
        {
            ValidateOnGetUser(claimsPrincipal);

            var userIdString = claimsPrincipal.FindFirst("oid")?.Value
                ?? claimsPrincipal
                    .FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value

                ?? claimsPrincipal
                    .FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value

                ?? claimsPrincipal
                    .FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")?.Value;

            var userId = userIdString;
            var givenName = claimsPrincipal.FindFirst(ClaimTypes.GivenName)?.Value;
            var surname = claimsPrincipal.FindFirst(ClaimTypes.Surname)?.Value;
            var displayName = claimsPrincipal.FindFirst("displayName")?.Value;
            var email = claimsPrincipal.FindFirst(ClaimTypes.Email)?.Value;
            var jobTitle = claimsPrincipal.FindFirst("jobTitle")?.Value;
            var roles = claimsPrincipal.FindAll(ClaimTypes.Role).Select(role => role.Value).ToList();
            var claimsList = claimsPrincipal.Claims;

            return new User(
                userId: userId,
                givenName: givenName,
                surname: surname,
                displayName: displayName,
                email: email,
                jobTitle: jobTitle,
                roles: roles,
                claims: claimsList);
        });

        public ValueTask<bool> UserHasClaimAsync(
            ClaimsPrincipal claimsPrincipal,
            string claimType,
            string claimValue) =>
        TryCatch(async () =>
        {
            ValidateOnUserHasClaimType(claimsPrincipal, claimType, claimValue);

            return claimsPrincipal.HasClaim(claimType, claimValue);
        });

        public ValueTask<bool> UserHasClaimAsync(ClaimsPrincipal claimsPrincipal, string claimType) =>
        TryCatch(async () =>
        {
            ValidateOnUserHasClaimType(claimsPrincipal, claimType);

            return claimsPrincipal.FindFirst(claimType) != null;
        });

        public ValueTask<bool> IsUserAuthenticatedAsync(ClaimsPrincipal claimsPrincipal) =>
        TryCatch(async () =>
        {
            ValidateOnIsUserAuthenticated(claimsPrincipal);

            return claimsPrincipal.Identity?.IsAuthenticated ?? false;
        });

        public ValueTask<bool> IsUserInRoleAsync(ClaimsPrincipal claimsPrincipal, string roleName) =>
        TryCatch(async () =>
        {
            ValidateOnIsUserInRole(claimsPrincipal, roleName);
            var roles = claimsPrincipal.FindAll(ClaimTypes.Role).Select(role => role.Value);

            return roles.Contains(roleName);
        });

        public ValueTask<string> GetUserClaimValueAsync(ClaimsPrincipal claimsPrincipal, string type) =>
        TryCatch(async () =>
        {
            ValidateOnGetUserClaimValue(claimsPrincipal, type);

            var claim = claimsPrincipal.FindFirst(type);

            if (claim is null)
            {
                throw new ClaimNotFoundUserException($"Claim with type '{type}' not found.");
            }

            return claim.Value;
        });

        public ValueTask<IReadOnlyList<string>> GetUserClaimValuesAsync(ClaimsPrincipal claimsPrincipal, string type) =>
        TryCatch<IReadOnlyList<string>>(async () =>
        {
            ValidateOnGetUserClaimValue(claimsPrincipal, type);

            var values = claimsPrincipal.FindAll(type)
                .Select(c => c.Value)
                .ToArray();

            if (values.Count == 0)
            {
                throw new ClaimNotFoundUserException($"Claim with type '{type}' not found.");
            }

            return values;
        });
    }
}
