// ---------------------------------------------------------
// Copyright (c) North East London ICB. All rights reserved.
// ---------------------------------------------------------

using System.Security.Claims;
using System.Threading.Tasks;
using FluentAssertions;
using ISL.Security.Client.Models.Foundations.Users;
using Moq;

namespace ISL.Security.Client.Tests.Unit.Services.Orchestrations.Audits
{
    public partial class AuditOrchestrationServiceTests
    {
        [Theory]
        [InlineData("username", true)]
        [InlineData("username", false)]
        [InlineData("", false)]
        public async Task ShouldGetCurrentUserIdAsync(string userId, bool isAuthenticated)
        {
            // Given
            ClaimsPrincipal randomClaimsPrincipal = CreateRandomClaimsPrincipal(userId, isAuthenticated);
            ClaimsPrincipal inputClaimsPrincipal = randomClaimsPrincipal;

            string securityUserId = isAuthenticated
                ? userId
                : string.IsNullOrEmpty(userId)
                    ? "anonymous" : userId;

            User randomUser = GetUser(randomClaimsPrincipal);
            User currentUser = randomUser;
            var expectedResult = securityUserId;

            this.userServiceMock.Setup(service =>
                service.GetUserAsync(inputClaimsPrincipal))
                    .ReturnsAsync(currentUser);

            this.userServiceMock.Setup(service =>
                service.IsUserAuthenticatedAsync(inputClaimsPrincipal))
                    .ReturnsAsync(isAuthenticated);

            // When
            var actualResult = await this.auditOrchestrationService
                .GetCurrentUserIdAsync(inputClaimsPrincipal);

            // Then
            ((object)actualResult).Should().BeEquivalentTo(expectedResult);

            this.userServiceMock.Verify(service =>
                service.GetUserAsync(inputClaimsPrincipal),
                    Times.Once);

            this.userServiceMock.Verify(service =>
                service.IsUserAuthenticatedAsync(inputClaimsPrincipal),
                    Times.Once);

            this.userServiceMock.VerifyNoOtherCalls();
            this.auditServiceMock.VerifyNoOtherCalls();
        }
    }
}