﻿// ---------------------------------------------------------
// Copyright (c) North East London ICB. All rights reserved.
// ---------------------------------------------------------

using System;
using System.Security.Claims;
using ISL.Security.Client.Models.Foundations.Users.Exceptions;

namespace ISL.Security.Client.Services.Foundations.Users
{
    internal partial class UserService
    {
        virtual internal void ValidateOnGetUser(ClaimsPrincipal claimsPrincipal)
        {
            Validate((Rule: IsInvalid(claimsPrincipal), Parameter: nameof(ClaimsPrincipal)));
        }

        virtual internal void ValidateOnIsUserInRole(ClaimsPrincipal claimsPrincipal, string roleName)
        {
            Validate(
                (Rule: IsInvalid(roleName), Parameter: "RoleName"),
                (Rule: IsInvalid(claimsPrincipal), Parameter: nameof(ClaimsPrincipal)));
        }

        virtual internal void ValidateOnUserHasClaimType(ClaimsPrincipal claimsPrincipal, string claimType)
        {
            Validate(
                (Rule: IsInvalid(claimType), Parameter: "ClaimType"),
                (Rule: IsInvalid(claimsPrincipal), Parameter: nameof(ClaimsPrincipal)));
        }

        virtual internal void ValidateOnGetUserClaimValue(ClaimsPrincipal claimsPrincipal, string claimType)
        {
            Validate(
                (Rule: IsInvalid(claimType), Parameter: "ClaimType"),
                (Rule: IsInvalid(claimsPrincipal), Parameter: nameof(ClaimsPrincipal)));
        }

        virtual internal void ValidateOnUserHasClaimType(
            ClaimsPrincipal claimsPrincipal,
            string claimType,
            string claimValue)
        {
            Validate(
                (Rule: IsInvalid(claimType), Parameter: "ClaimType"),
                (Rule: IsInvalid(claimValue), Parameter: "ClaimValue"),
                (Rule: IsInvalid(claimsPrincipal), Parameter: nameof(ClaimsPrincipal)));
        }

        virtual internal void ValidateOnIsUserAuthenticated(ClaimsPrincipal claimsPrincipal)
        {
            Validate((Rule: IsInvalid(claimsPrincipal), Parameter: nameof(ClaimsPrincipal)));
        }

        private static dynamic IsInvalid(string? text) => new
        {
            Condition = String.IsNullOrWhiteSpace(text),
            Message = "Text is required"
        };

        private static dynamic IsInvalid(ClaimsPrincipal claimsPrincipal) => new
        {
            Condition = claimsPrincipal == null,
            Message = "ClaimsPrincipal is required"
        };

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidArgumentUserException =
                new InvalidArgumentUserException(
                    message: "Invalid user argument(s), correct the errors and try again.");

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidArgumentUserException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidArgumentUserException.ThrowIfContainsErrors();
        }
    }
}
