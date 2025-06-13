// ---------------------------------------------------------
// Copyright (c) North East London ICB. All rights reserved.
// ---------------------------------------------------------

using Xeptions;

namespace ISL.Security.Client.Models.Foundations.Audits.Exceptions
{
    public class InvalidArgumentAuditException : Xeption
    {
        public InvalidArgumentAuditException(string message)
            : base(message)
        { }
    }
}