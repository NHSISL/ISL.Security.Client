// ---------------------------------------------------------
// Copyright (c) North East London ICB. All rights reserved.
// ---------------------------------------------------------

using System;
using Xeptions;

namespace ISL.Security.Client.Models.Orchestrations.Audits.Exceptions
{
    public class AuditOrchestrationServiceException : Xeption
    {
        public AuditOrchestrationServiceException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}