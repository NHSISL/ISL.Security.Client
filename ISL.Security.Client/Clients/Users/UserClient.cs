// ---------------------------------------------------------
// Copyright (c) North East London ICB. All rights reserved.
// ---------------------------------------------------------

using System.Security.Claims;
using System.Threading.Tasks;
using ISL.Security.Client.Models.Clients.Users.Exceptions;
using ISL.Security.Client.Models.Foundations.Users;
using ISL.Security.Client.Models.Foundations.Users.Exceptions;
using ISL.Security.Client.Services.Foundations.Users;
using Xeptions;

namespace ISL.Security.Client.Clients.Users
{
    internal class UserClient : IUserClient
    {
        private readonly IUserService userPrincipalService;

        public UserClient(IUserService userPrincipalService)
        {
            this.userPrincipalService = userPrincipalService;
        }

        public async ValueTask<User> GetUserAsync(ClaimsPrincipal claimsPrincipal)
        {
            try
            {
                return await userPrincipalService.GetUserAsync(claimsPrincipal);
            }
            catch (UserValidationException userValidationException)
            {
                throw CreateUserClientValidationException(
                    userValidationException.InnerException as Xeption);
            }
            catch (UserDependencyValidationException userDependencyValidationException)
            {
                throw CreateUserClientValidationException(
                    userDependencyValidationException.InnerException as Xeption);
            }
            catch (UserDependencyException userDependencyException)
            {
                throw CreateUserClientDependencyException(
                    userDependencyException.InnerException as Xeption);
            }
            catch (UserServiceException userServiceException)
            {
                throw CreateUserClientServiceException(
                    userServiceException.InnerException as Xeption);
            }
        }

        public async ValueTask<bool> IsUserAuthenticatedAsync(ClaimsPrincipal claimsPrincipal)
        {
            try
            {
                return await userPrincipalService.IsUserAuthenticatedAsync(claimsPrincipal);
            }
            catch (UserValidationException userValidationException)
            {
                throw CreateUserClientValidationException(
                    userValidationException.InnerException as Xeption);
            }
            catch (UserDependencyValidationException userDependencyValidationException)
            {
                throw CreateUserClientValidationException(
                    userDependencyValidationException.InnerException as Xeption);
            }
            catch (UserDependencyException userDependencyException)
            {
                throw CreateUserClientDependencyException(
                    userDependencyException.InnerException as Xeption);
            }
            catch (UserServiceException userServiceException)
            {
                throw CreateUserClientServiceException(
                    userServiceException.InnerException as Xeption);
            }
        }

        public async ValueTask<bool> IsUserInRoleAsync(ClaimsPrincipal claimsPrincipal, string roleName)
        {
            try
            {
                return await userPrincipalService.IsUserInRoleAsync(claimsPrincipal, roleName);
            }
            catch (UserValidationException userValidationException)
            {
                throw CreateUserClientValidationException(
                    userValidationException.InnerException as Xeption);
            }
            catch (UserDependencyValidationException userDependencyValidationException)
            {
                throw CreateUserClientValidationException(
                    userDependencyValidationException.InnerException as Xeption);
            }
            catch (UserDependencyException userDependencyException)
            {
                throw CreateUserClientDependencyException(
                    userDependencyException.InnerException as Xeption);
            }
            catch (UserServiceException userServiceException)
            {
                throw CreateUserClientServiceException(
                    userServiceException.InnerException as Xeption);
            }
        }

        public async ValueTask<bool> UserHasClaimTypeAsync(
            ClaimsPrincipal claimsPrincipal,
            string claimType,
            string claimValue)
        {
            try
            {
                return await userPrincipalService.UserHasClaimTypeAsync(claimsPrincipal, claimType, claimValue);
            }
            catch (UserValidationException userValidationException)
            {
                throw CreateUserClientValidationException(
                    userValidationException.InnerException as Xeption);
            }
            catch (UserDependencyValidationException userDependencyValidationException)
            {
                throw CreateUserClientValidationException(
                    userDependencyValidationException.InnerException as Xeption);
            }
            catch (UserDependencyException userDependencyException)
            {
                throw CreateUserClientDependencyException(
                    userDependencyException.InnerException as Xeption);
            }
            catch (UserServiceException userServiceException)
            {
                throw CreateUserClientServiceException(
                    userServiceException.InnerException as Xeption);
            }
        }

        public async ValueTask<bool> UserHasClaimTypeAsync(ClaimsPrincipal claimsPrincipal, string claimType)
        {
            try
            {
                return await userPrincipalService.UserHasClaimTypeAsync(claimsPrincipal, claimType);
            }
            catch (UserValidationException userValidationException)
            {
                throw CreateUserClientValidationException(
                    userValidationException.InnerException as Xeption);
            }
            catch (UserDependencyValidationException userDependencyValidationException)
            {
                throw CreateUserClientValidationException(
                    userDependencyValidationException.InnerException as Xeption);
            }
            catch (UserDependencyException userDependencyException)
            {
                throw CreateUserClientDependencyException(
                    userDependencyException.InnerException as Xeption);
            }
            catch (UserServiceException userServiceException)
            {
                throw CreateUserClientServiceException(
                    userServiceException.InnerException as Xeption);
            }
        }

        private static UserClientValidationException CreateUserClientValidationException(Xeption innerException)
        {
            return new UserClientValidationException(
                message: "User client validation error occurred, fix errors and try again.",
                innerException,
                data: innerException.Data);
        }

        private static UserClientDependencyException CreateUserClientDependencyException(Xeption innerException)
        {
            return new UserClientDependencyException(
                message: "User client dependency error occurred, please contact support.",
                innerException,
                data: innerException.Data);
        }

        private static UserClientServiceException CreateUserClientServiceException(Xeption innerException)
        {
            return new UserClientServiceException(
                message: "User client service error occurred, please contact support.",
                innerException,
                data: innerException.Data);
        }
    }
}
