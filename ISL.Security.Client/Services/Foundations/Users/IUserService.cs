// ---------------------------------------------------------
// Copyright (c) North East London ICB. All rights reserved.
// ---------------------------------------------------------

using System.Security.Claims;
using System.Threading.Tasks;
using ISL.Security.Client.Models.Foundations.Users;

namespace ISL.Security.Client.Services.Foundations.Users
{
    internal interface IUserService
    {
        ValueTask<User> GetUserAsync(ClaimsPrincipal user);
        ValueTask<bool> IsUserAuthenticatedAsync(ClaimsPrincipal user);
        ValueTask<bool> IsUserInRoleAsync(ClaimsPrincipal user, string roleName);
        ValueTask<bool> UserHasClaimTypeAsync(ClaimsPrincipal user, string claimType, string claimValue);
        ValueTask<bool> UserHasClaimTypeAsync(ClaimsPrincipal user, string claimType);
    }
}
