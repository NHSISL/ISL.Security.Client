// ---------------------------------------------------------
// Copyright (c) North East London ICB. All rights reserved.
// ---------------------------------------------------------

using System;
using System.Security.Claims;
using System.Threading.Tasks;
using FluentAssertions;
using ISL.Security.Client.Models.Clients;
using ISL.Security.Client.Models.Foundations.Users;
using ISL.Security.Client.Tests.Unit.Models;
using Moq;

namespace ISL.Security.Client.Tests.Unit.Services.Foundations.Audits
{
    public partial class AuditOrchestrationServiceTests
    {
        [Fact]
        public async Task ShouldApplyRemoveAuditForDynamicObjectAsync()
        {
            // Given
            ClaimsPrincipal randomClaimsPrincipal = CreateRandomClaimsPrincipal();
            ClaimsPrincipal inputClaimsPrincipal = randomClaimsPrincipal;
            User randomUser = GetUser(randomClaimsPrincipal);
            User currentUser = randomUser;
            var inputPerson = new Person { Name = GetRandomString() };
            var updatedPerson = new Person { Name = GetRandomString() };
            var expectedResult = updatedPerson;

            var securityConfigurations = new SecurityConfigurations
            {
                CreatedByPropertyName = "CreatedBy",
                CreatedByPropertyType = typeof(string),
                CreatedDatePropertyName = "CreatedWhen",
                CreatedDatePropertyType = typeof(DateTimeOffset),
                UpdatedByPropertyName = "UpdatedBy",
                UpdatedByPropertyType = typeof(string),
                UpdatedDatePropertyName = "UpdatedWhen",
                UpdatedDatePropertyType = typeof(DateTimeOffset)
            };

            this.userServiceMock.Setup(service =>
                service.GetUserAsync(inputClaimsPrincipal))
                    .ReturnsAsync(currentUser);

            this.auditServiceMock.Setup(service =>
                service.ApplyRemoveAuditAsync(inputPerson, currentUser.UserId, securityConfigurations))
                    .ReturnsAsync(updatedPerson);

            // When
            var actualResult = await this.auditOrchestrationService
                .ApplyRemoveAuditAsync(inputPerson, inputClaimsPrincipal, securityConfigurations);

            // Then
            ((object)actualResult).Should().BeEquivalentTo(expectedResult);
        }
    }
}