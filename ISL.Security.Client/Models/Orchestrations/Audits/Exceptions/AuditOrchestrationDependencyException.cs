// ---------------------------------------------------------
// Copyright (c) North East London ICB. All rights reserved.
// ---------------------------------------------------------

using Xeptions;

namespace ISL.Security.Client.Models.Orchestrations.Audits.Exceptions
{
    public class AuditOrchestrationDependencyException : Xeption
    {
        public AuditOrchestrationDependencyException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}
