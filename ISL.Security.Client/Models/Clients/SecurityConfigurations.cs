// ---------------------------------------------------------
// Copyright (c) North East London ICB. All rights reserved.
// ---------------------------------------------------------

using System;

namespace ISL.Security.Client.Models.Clients
{
    public class SecurityConfigurations
    {
        public string CreatedByPropertyName { get; set; } = "CreatedBy";
        public Type CreatedByPropertyType { get; set; } = typeof(string);
        public string CreatedWhenPropertyName { get; set; } = "CreatedWhen";
        public Type CreatedWhenPropertyType { get; set; } = typeof(DateTimeOffset);
        public string UpdatedByPropertyName { get; set; } = "UpdatedBy";
        public Type UpdatedByPropertyType { get; set; } = typeof(string);
        public string UpdatedWhenPropertyName { get; set; } = "UpdatedWhen";
        public Type UpdatedWhenPropertyType { get; set; } = typeof(DateTimeOffset);
        public string DeletedByPropertyName { get; set; } = "DeletedBy";
        public Type DeletedByPropertyType { get; set; } = typeof(string);
        public string DeletedWhenPropertyName { get; set; } = "DeletedWhen";
        public Type DeletedWhenPropertyType { get; set; } = typeof(DateTimeOffset);
        public string IsDeletedPropertyName { get; set; } = "IsDeleted";
        public Type IsDeletedPropertyType { get; set; } = typeof(bool);
        public string DeletionReasonPropertyName { get; set; } = "DeletionReason";
        public Type DeletionReasonPropertyType { get; set; } = typeof(string);
    }
}
