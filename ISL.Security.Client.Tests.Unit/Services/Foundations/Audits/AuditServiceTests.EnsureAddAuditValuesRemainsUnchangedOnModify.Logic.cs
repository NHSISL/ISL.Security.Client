// ---------------------------------------------------------
// Copyright (c) North East London ICB. All rights reserved.
// ---------------------------------------------------------

using System;
using System.Dynamic;
using System.Threading.Tasks;
using FluentAssertions;
using ISL.Security.Client.Models.Clients;
using ISL.Security.Client.Tests.Unit.Models;
using Moq;

namespace ISL.Security.Client.Tests.Unit.Services.Foundations.Audits
{
    public partial class AuditServiceTests
    {
        [Fact]
        public async Task ShouldEnsureAddAuditValuesRemainsUnchangedOnModifyForExpandoObjectAsync()
        {
            // Given
            DateTimeOffset currentDateTime = DateTime.UtcNow;
            string createdUserId = "CreatedUser";
            string modifiedUserId = "ModifiedUser";
            string storageDeletedByUserId = "StorageDeletedUser";

            dynamic inputPerson = new ExpandoObject();
            inputPerson.Name = "John Doe";
            inputPerson.CreatedBy = modifiedUserId;
            inputPerson.CreatedDate = currentDateTime;
            inputPerson.UpdatedBy = modifiedUserId;
            inputPerson.UpdatedDate = currentDateTime;
            inputPerson.DeletedBy = modifiedUserId;
            inputPerson.DeletedDate = currentDateTime;
            inputPerson.IsDeleted = false;
            inputPerson.DeletionReason = (string)null;

            dynamic storagePerson = new ExpandoObject();
            storagePerson.Name = "John Doe";
            storagePerson.CreatedBy = createdUserId;
            storagePerson.CreatedDate = DateTimeOffset.MinValue;
            storagePerson.UpdatedBy = createdUserId;
            storagePerson.UpdatedDate = DateTimeOffset.MinValue;
            storagePerson.DeletedBy = storageDeletedByUserId;
            storagePerson.DeletedDate = DateTimeOffset.MinValue;
            storagePerson.IsDeleted = false;
            storagePerson.DeletionReason = (string)null;

            dynamic expectedResult = new ExpandoObject();
            expectedResult.Name = "John Doe";
            expectedResult.CreatedBy = createdUserId;
            expectedResult.CreatedDate = DateTimeOffset.MinValue;
            expectedResult.UpdatedBy = modifiedUserId;
            expectedResult.UpdatedDate = currentDateTime;
            expectedResult.DeletedBy = storageDeletedByUserId;
            expectedResult.DeletedDate = DateTimeOffset.MinValue;
            expectedResult.IsDeleted = false;     // restored from storage
            expectedResult.DeletionReason = (string)null; // restored from storage

            var securityConfigurations = new SecurityConfigurations
            {
                CreatedByPropertyName = "CreatedBy",
                CreatedByPropertyType = typeof(string),
                CreatedWhenPropertyName = "CreatedDate",
                CreatedWhenPropertyType = typeof(DateTimeOffset),
                UpdatedByPropertyName = "UpdatedBy",
                UpdatedByPropertyType = typeof(string),
                UpdatedWhenPropertyName = "UpdatedDate",
                UpdatedWhenPropertyType = typeof(DateTimeOffset),
                DeletedByPropertyName = "DeletedBy",
                DeletedByPropertyType = typeof(string),
                DeletedWhenPropertyName = "DeletedDate",
                DeletedWhenPropertyType = typeof(DateTimeOffset),
                IsDeletedPropertyName = "IsDeleted",
                IsDeletedPropertyType = typeof(bool),
                DeletionReasonPropertyName = "DeletionReason",
                DeletionReasonPropertyType = typeof(string)
            };

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTimeOffsetAsync())
                    .ReturnsAsync(currentDateTime);

            // When
            dynamic actualResult = await this.auditService
                .EnsureOtherAuditValuesRemainsUnchangedOnModifyAsync(inputPerson, storagePerson, securityConfigurations);

            // Then
            ((object)actualResult).Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async Task ShouldEnsureAddAuditValuesRemainsUnchangedOnModifyForObjectAsync()
        {
            // Given
            DateTimeOffset currentDateTime = DateTime.UtcNow;
            string createdUserId = GetRandomString();
            string modifiedUserId = GetRandomString();
            string storageDeletedByUserId = GetRandomString();

            var inputPerson = new Person
            {
                Name = "John Doe",
                CreatedBy = modifiedUserId,
                CreatedWhen = currentDateTime,
                UpdatedBy = modifiedUserId,
                UpdatedWhen = currentDateTime,
                DeletedBy = modifiedUserId,
                DeletedWhen = currentDateTime,
                IsDeleted = true,
                DeletionReason = "tampered",
            };

            var storagePerson = new Person
            {
                Name = "John Doe",
                CreatedBy = createdUserId,
                CreatedWhen = DateTimeOffset.MinValue,
                UpdatedBy = createdUserId,
                UpdatedWhen = DateTimeOffset.MinValue,
                DeletedBy = storageDeletedByUserId,
                DeletedWhen = DateTimeOffset.MinValue,
                IsDeleted = false,
                DeletionReason = null,
            };

            var expectedResult = new Person
            {
                Name = "John Doe",
                CreatedBy = createdUserId,
                CreatedWhen = DateTimeOffset.MinValue,
                UpdatedBy = modifiedUserId,
                UpdatedWhen = currentDateTime,
                DeletedBy = storageDeletedByUserId,
                DeletedWhen = DateTimeOffset.MinValue,
                IsDeleted = false,      // restored from storage
                DeletionReason = null,  // restored from storage
            };

            var securityConfigurations = new SecurityConfigurations
            {
                CreatedByPropertyName = "CreatedBy",
                CreatedByPropertyType = typeof(string),
                CreatedWhenPropertyName = "CreatedWhen",
                CreatedWhenPropertyType = typeof(DateTimeOffset),
                UpdatedByPropertyName = "UpdatedBy",
                UpdatedByPropertyType = typeof(string),
                UpdatedWhenPropertyName = "UpdatedWhen",
                UpdatedWhenPropertyType = typeof(DateTimeOffset),
                DeletedByPropertyName = "DeletedBy",
                DeletedByPropertyType = typeof(string),
                DeletedWhenPropertyName = "DeletedWhen",
                DeletedWhenPropertyType = typeof(DateTimeOffset),
                IsDeletedPropertyName = "IsDeleted",
                IsDeletedPropertyType = typeof(bool),
                DeletionReasonPropertyName = "DeletionReason",
                DeletionReasonPropertyType = typeof(string)
            };

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTimeOffsetAsync())
                    .ReturnsAsync(currentDateTime);

            // When
            var actualResult = await this.auditService
                .EnsureOtherAuditValuesRemainsUnchangedOnModifyAsync(inputPerson, storagePerson, securityConfigurations);

            // Then
            ((object)actualResult).Should().BeEquivalentTo(expectedResult);
        }
    }
}
