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
        ValueTask<User> GetUserAsync(ClaimsPrincipal claimsPrincipal);
        ValueTask<bool> IsUserAuthenticatedAsync(ClaimsPrincipal claimsPrincipal);
        ValueTask<bool> IsUserInRoleAsync(ClaimsPrincipal claimsPrincipal, string roleName);
        ValueTask<bool> UserHasClaimTypeAsync(ClaimsPrincipal claimsPrincipal, string claimType, string claimValue);
        ValueTask<bool> UserHasClaimTypeAsync(ClaimsPrincipal claimsPrincipal, string claimType);
    }
}
