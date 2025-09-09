// ---------------------------------------------------------
// Copyright (c) North East London ICB. All rights reserved.
// ---------------------------------------------------------

using Xeptions;

namespace ISL.Security.Client.Models.Orchestrations.Audits.Exceptions
{
    internal class AuditOrchestrationDependencyException : Xeption
    {
        public AuditOrchestrationDependencyException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}
