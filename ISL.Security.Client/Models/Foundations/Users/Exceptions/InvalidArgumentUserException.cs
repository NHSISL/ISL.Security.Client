// ---------------------------------------------------------
// Copyright (c) North East London ICB. All rights reserved.
// ---------------------------------------------------------

using Xeptions;

namespace ISL.Security.Client.Models.Foundations.Users.Exceptions
{
    public class InvalidArgumentUserException : Xeption
    {
        public InvalidArgumentUserException(string message)
            : base(message)
        { }
    }
}