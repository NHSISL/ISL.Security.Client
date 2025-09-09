// ---------------------------------------------------------
// Copyright (c) North East London ICB. All rights reserved.
// ---------------------------------------------------------

using Xeptions;

namespace ISL.Security.Client.Models.Orchestrations.Audits.Exceptions
{
    internal class InvalidArgumentAuditOrchestrationException : Xeption
    {
        public InvalidArgumentAuditOrchestrationException(string message)
            : base(message)
        { }
    }
}