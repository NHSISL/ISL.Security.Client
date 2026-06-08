// ---------------------------------------------------------
// Copyright (c) North East London ICB. All rights reserved.
// ---------------------------------------------------------

using System;
using System.Threading.Tasks;
using FluentAssertions;
using Force.DeepCloner;
using ISL.Security.Client.Models.Clients;
using ISL.Security.Client.Tests.Acceptance.Models;

namespace ISL.Security.Client.Tests.Clients.Audits
{
    public partial class AuditClientTests
    {
        [Fact]
        public async Task ShouldEnsureOtherAuditValuesRemainsUnchangedOnRemoveAsync()
        {
            // Given
            DateTimeOffset currentDateTime = DateTime.UtcNow;

            Person inputPerson = new Person
            {
                Name = GetRandomString(),
                CreatedBy = GetRandomString(),
                CreatedWhen = currentDateTime,
                UpdatedBy = GetRandomString(),
                UpdatedWhen = currentDateTime
            };

            Person storagePerson = new Person
            {
                Name = GetRandomString(),
                CreatedBy = GetRandomString(),
                CreatedWhen = currentDateTime.AddDays(-1),
                UpdatedBy = GetRandomString(),
                UpdatedWhen = currentDateTime.AddDays(-1)
            };

            Person updatedPerson = new Person
            {
                Name = inputPerson.Name,
                CreatedBy = storagePerson.CreatedBy,
                CreatedWhen = storagePerson.CreatedWhen,
                UpdatedBy = storagePerson.UpdatedBy,
                UpdatedWhen = storagePerson.UpdatedWhen
            };

            Person expectedResult = updatedPerson.DeepClone();

            var inputSecurityConfigurations = new SecurityConfigurations
            {
                CreatedByPropertyName = "CreatedBy",
                CreatedByPropertyType = typeof(string),
                CreatedWhenPropertyName = "CreatedWhen",
                CreatedWhenPropertyType = typeof(DateTimeOffset),
                UpdatedByPropertyName = "UpdatedBy",
                UpdatedByPropertyType = typeof(string),
                UpdatedWhenPropertyName = "UpdatedWhen",
                UpdatedWhenPropertyType = typeof(DateTimeOffset)
            };

            // When
            Person actualResult = await this.securityClient.Audits
                .EnsureOtherAuditValuesRemainsUnchangedOnRemoveAsync(
                    inputPerson,
                    storagePerson,
                    inputSecurityConfigurations);

            // Then
            actualResult.Should().BeEquivalentTo(expectedResult);
        }
    }
}
