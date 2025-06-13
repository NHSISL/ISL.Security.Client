// ---------------------------------------------------------
// Copyright (c) North East London ICB. All rights reserved.
// ---------------------------------------------------------

using ISL.Security.Client.Clients.Audits;
using ISL.Security.Client.Clients.Users;

namespace ISL.Security.Client.Clients
{
    public interface ISecurityClient
    {
        IUserClient Users { get; }
        IAuditClient Audits { get; }
    }
}
