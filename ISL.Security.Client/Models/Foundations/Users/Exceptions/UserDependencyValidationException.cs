// ---------------------------------------------------------
// Copyright (c) North East London ICB. All rights reserved.
// ---------------------------------------------------------

using Xeptions;

namespace ISL.Security.Client.Models.Foundations.Users.Exceptions
{
    internal class UserDependencyValidationException : Xeption
    {
        public UserDependencyValidationException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}