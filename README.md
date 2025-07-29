# ISL.Security.Client

A Standard compliant client to abstract security operations away from the security broker.  

The client offers `User` capabilities to extract information from a `ClaimsPrincipal` like the current user, roles, and claims.
It also provides `Audit` capabilities that will allow you to configure the audit properties on objects and then applying those values server side like `CreatedBy`, `CreatedWhen`, `UpdatedBy`, `UpdatedWhen`.