// ---------------------------------------------------------
// Copyright (c) North East London ICB. All rights reserved.
// ---------------------------------------------------------

using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ISL.Security.Client.Models.Foundations.Users;

namespace ISL.Security.Client.Services.Foundations.Users
{
    internal partial class UserService : IUserService
    {
        public async ValueTask<User> GetUserAsync(ClaimsPrincipal user)
        {
            var userIdString = user.FindFirst("oid")?.Value
                ?? user.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value
                ?? user.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value
                ?? user.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")?.Value;

            var userId = userIdString;
            var givenName = user.FindFirst(ClaimTypes.GivenName)?.Value;
            var surname = user.FindFirst(ClaimTypes.Surname)?.Value;
            var displayName = user.FindFirst("displayName")?.Value;
            var email = user.FindFirst(ClaimTypes.Email)?.Value;
            var jobTitle = user.FindFirst("jobTitle")?.Value;
            var roles = user.FindAll(ClaimTypes.Role).Select(role => role.Value).ToList();
            var claimsList = user.Claims;

            return new User(
                userId: userId,
                givenName: givenName,
                surname: surname,
                displayName: displayName,
                email: email,
                jobTitle: jobTitle,
                roles: roles,
                claims: claimsList);
        }

        public ValueTask<bool> UserHasClaimTypeAsync(ClaimsPrincipal user, string claimType, string claimValue) =>
        TryCatch(async () =>
        {
            return user.HasClaim(claimType, claimValue);
        });

        public ValueTask<bool> UserHasClaimTypeAsync(ClaimsPrincipal user, string claimType) =>
        TryCatch(async () =>
        {
            return user.FindFirst(claimType) != null;
        });

        public ValueTask<bool> IsUserAuthenticatedAsync(ClaimsPrincipal user) =>
        TryCatch(async () =>
        {
            return user.Identity?.IsAuthenticated ?? false;
        });

        public ValueTask<bool> IsUserInRoleAsync(ClaimsPrincipal user, string roleName) =>
        TryCatch(async () =>
        {
            var roles = user.FindAll(ClaimTypes.Role).Select(role => role.Value);

            return roles.Contains(roleName);
        });
    }
}
